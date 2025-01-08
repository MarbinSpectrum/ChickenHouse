using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BowlEgg : Mgr
{
    private const int MAX_CHICKEN_SLOT = 4;
    /**닭 갯수 **/
    private int chickenCnt;

    [System.Serializable]
    public struct SPITE_IMG
    {
        //오브젝트 스프라이트 이미지
        public Sprite normalSprite;
        public Sprite canUseSprite;
    }
    [SerializeField] private SPITE_IMG          sprite;
    [SerializeField] private Image              image;
    [SerializeField] private float              chickenDragPow;
    [SerializeField] private float              eggPow;

    [SerializeField] private float              ellipseW;
    [SerializeField] private float              ellipseH;
    [SerializeField] private Transform          ellipseCenterTrans;

    //Tuto_2
    [SerializeField] private TutoObj            tutoObj;
    [SerializeField] private BowlChicken[]      bowlChicken;
    private BowlChicken dragBowlChicken;
    List<KeyValuePair<float, int>> sortArray = new List<KeyValuePair<float, int>>();

    private Vector2 prevMousePos;

    public bool isDrag { private set; get; }

    public void OnMouseDrag()
    {
        if (gameMgr.playData != null && gameMgr.playData.tutoComplete1 == false && (tutoMgr.nowTuto == Tutorial.Tuto_2 || tutoMgr.nowTuto == Tutorial.Tuto_3 || tutoMgr.nowTuto == Tutorial.Tuto_5) == false)
        {
            //튜토리얼이 아직 완료안된듯
            //혹시모르니 튜토리얼 타이밍때만 작동하도록 막아놓자
            return;
        }

        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if (kitchenMgr.dragState != DragState.None)
        {
            //빈손인 상태에서 드래그해야됨
            return;
        }
        if (kitchenMgr.cameraObj.lookArea != LookArea.Kitchen)
        {
            //주방을 보고있는 상태에서만 상호 작용 가능
            return;
        }

        isDrag = true;

        if (CompleteEgg() == false)
        {
            for (int i = 0; i < bowlChicken.Length; i++)
                bowlChicken[i].RigidFreeze(RigidbodyConstraints2D.FreezeAll);

            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (prevMousePos != mousePos)
            {
                prevMousePos = mousePos;

                for (int i = 0; i < bowlChicken.Length; i++)
                    bowlChicken[i].RigidFreeze(RigidbodyConstraints2D.FreezeRotation);

                for (int i = 0; i < bowlChicken.Length; i++)
                {
                    if (bowlChicken[i].isUse)
                    {
                        bowlChicken[i].RigidFreeze(RigidbodyConstraints2D.FreezeAll);

                        MoveDragPos(bowlChicken[i].transform, mousePos);
                        UpdateSort();
                        break;
                    }
                }
            }
            for (int i = 0; i < bowlChicken.Length; i++)
                if (bowlChicken[i].isUse)
                    bowlChicken[i].WorkerDragChicken(Time.deltaTime * eggPow);

            dragBowlChicken = null;
        }
        else
        {
            if (gameMgr.playData != null && gameMgr.playData.tutoComplete1 == false && tutoMgr.nowTuto == Tutorial.Tuto_2)
            {
                //다 저어줌 튜토리얼 진행
                tutoObj.PlayTuto();
            }

            kitchenMgr.dragState = DragState.Egg;
            for(int i = 0; i < bowlChicken.Length; i++)
            {
                if(bowlChicken[i].CompleteEgg())
                {
                    bowlChicken[i].gameObject.SetActive(false);
                    dragBowlChicken = bowlChicken[i];
                    break;
                }
            }
        }
    }

    private void Update()
    {
        for (int i = 0; i < bowlChicken.Length; i++)
            if (bowlChicken[i].isUse)
                CheckEllipese(bowlChicken[i].transform);

        if(isDrag == false)
        {
            for (int i = 0; i < bowlChicken.Length; i++)
                bowlChicken[i].RigidFreeze(RigidbodyConstraints2D.FreezeRotation);
        }
    }

    private void UpdateSort()
    {
        sortArray.Clear();
        for (int i = 0; i < bowlChicken.Length; i++)
        {
            if (bowlChicken[i].isUse == false)
                continue;
            sortArray.Add(new KeyValuePair<float, int>(bowlChicken[i].transform.position.y, i));
        }
        sortArray.Sort((x, y) => y.Key.CompareTo(x.Key));
        for(int i = 0; i < sortArray.Count; i++)
            bowlChicken[sortArray[i].Value].transform.SetSiblingIndex(i);
    }

    private void MoveDragPos(Transform moveTrans,Vector3 pMovePos)
    {
        //치킨을 볼안에서만 움직일 수 있도록한다.

        Vector3 mousePos = new Vector3(pMovePos.x, pMovePos.y, moveTrans.position.z);

        double result = ((mousePos.x - ellipseCenterTrans.position.x) * (mousePos.x - ellipseCenterTrans.position.x)) /
            (ellipseW * ellipseW) + ((mousePos.y - ellipseCenterTrans.position.y) * (mousePos.y - ellipseCenterTrans.position.y)) / (ellipseH * ellipseH);

        if (result <= 1)
        {
            //타원 범위안이라면 마우스 좌표로
            moveTrans.position = mousePos;
        }
        else
        {
            //타원밖이면 타원안에서 최대한 마우스 좌표랑 가까운 곳으로 이동하도록

            Vector3 zeroBaseMousePos = mousePos - ellipseCenterTrans.position;
            float x1 = ((ellipseW * ellipseH) / Mathf.Sqrt(ellipseW * ellipseW * zeroBaseMousePos.y * zeroBaseMousePos.y + ellipseH * ellipseH * zeroBaseMousePos.x * zeroBaseMousePos.x)) * zeroBaseMousePos.x;
            float x2 = -x1;
            float y1 = ((ellipseW * ellipseH) / Mathf.Sqrt(ellipseW * ellipseW * zeroBaseMousePos.y * zeroBaseMousePos.y + ellipseH * ellipseH * zeroBaseMousePos.x * zeroBaseMousePos.x)) * zeroBaseMousePos.y;
            float y2 = -y1;

            // 두 교점 중 mousePos와 가까운 점 선택
            float dist1 = Vector2.Distance(new Vector2(x1, y1), zeroBaseMousePos);
            float dist2 = Vector2.Distance(new Vector2(x2, y2), zeroBaseMousePos);

            moveTrans.position = (dist1 < dist2 ? new Vector3(x1, y1) : new Vector3(x2, y2)) + ellipseCenterTrans.position;
        }
    }

    private void CheckEllipese(Transform moveTrans)
    {
        //타원박에 나가게된 치킨오브젝트 다시 안으로
        double result = ((moveTrans.transform.position.x - ellipseCenterTrans.position.x) * (moveTrans.transform.position.x - ellipseCenterTrans.position.x)) /
            (ellipseW * ellipseW) + ((moveTrans.transform.position.y - ellipseCenterTrans.position.y) * (moveTrans.transform.position.y - ellipseCenterTrans.position.y)) / (ellipseH * ellipseH);

        if (result > 1)
        {
            //타원밖이면 타원안에서 최대한 마우스 좌표랑 가까운 곳으로 이동하도록

            Vector3 zeroBaseMousePos = moveTrans.transform.position - ellipseCenterTrans.position;
            float x1 = ((ellipseW * ellipseH) / Mathf.Sqrt(ellipseW * ellipseW * zeroBaseMousePos.y * zeroBaseMousePos.y + ellipseH * ellipseH * zeroBaseMousePos.x * zeroBaseMousePos.x)) * zeroBaseMousePos.x;
            float x2 = -x1;
            float y1 = ((ellipseW * ellipseH) / Mathf.Sqrt(ellipseW * ellipseW * zeroBaseMousePos.y * zeroBaseMousePos.y + ellipseH * ellipseH * zeroBaseMousePos.x * zeroBaseMousePos.x)) * zeroBaseMousePos.y;
            float y2 = -y1;

            // 두 교점 중 mousePos와 가까운 점 선택
            float dist1 = Vector2.Distance(new Vector2(x1, y1), zeroBaseMousePos);
            float dist2 = Vector2.Distance(new Vector2(x2, y2), zeroBaseMousePos);

            Vector3 targetPos = (dist1 < dist2 ? new Vector3(x1, y1) : new Vector3(x2, y2)) + ellipseCenterTrans.position;
            moveTrans.position = targetPos;
        }
    }

    public void OnMouseUp()
    {
        if (gameMgr.playData != null && gameMgr.playData.tutoComplete1 == false && (tutoMgr.nowTuto == Tutorial.Tuto_2 || tutoMgr.nowTuto == Tutorial.Tuto_3 || tutoMgr.nowTuto == Tutorial.Tuto_5) == false)
        {
            //튜토리얼이 아직 완료안된듯
            //혹시모르니 튜토리얼 타이밍때만 작동하도록 막아놓자
            return;
        }

        isDrag = false;

        //손을때면 치킨이 떨어짐
        KitchenMgr kitchenMgr = KitchenMgr.Instance;

        for (int i = 0; i < bowlChicken.Length; i++)
            if (bowlChicken[i].isUse)
                bowlChicken[i].MoveChicken(Vector2.zero);

        if (kitchenMgr.mouseArea == DragArea.Tray_Flour &&
            kitchenMgr.dragState == DragState.Egg)
        {
            //밀가루 쪽으로 치킨을 드래그함
            if (dragBowlChicken != null && kitchenMgr.trayFlour.AddChicken())
            {
                kitchenMgr.dragState = DragState.None;
                dragBowlChicken.gameObject.SetActive(false);
                dragBowlChicken.Init();
                chickenCnt--;
                dragBowlChicken = null;
                return;
            }
        }
        else if (kitchenMgr.mouseArea == DragArea.Tray_Flour2 &&
            kitchenMgr.dragState == DragState.Egg)
        {
            //밀가루 쪽으로 치킨을 드래그함
            if (dragBowlChicken != null && kitchenMgr.trayFlour2.AddChicken())
            {
                kitchenMgr.dragState = DragState.None;
                dragBowlChicken.gameObject.SetActive(false);
                dragBowlChicken.Init();
                chickenCnt--;
                dragBowlChicken = null;
                return;
            }
        }

        if (dragBowlChicken != null)
            dragBowlChicken.gameObject.SetActive(true);
        dragBowlChicken = null;

        kitchenMgr.dragState = DragState.None;
    }

    public void OnMouseEnter()
    {
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if (kitchenMgr.dragState == DragState.Normal && chickenCnt < MAX_CHICKEN_SLOT)
        {
            //치킨을 드래그해서 가져왔으며
            //슬롯이 비어져있다.
            image.sprite = sprite.canUseSprite;
        }
        else
        {
            //나머지 상태면 이미지가 보이지 않는다.
            image.sprite = sprite.normalSprite;
        }
        kitchenMgr.bowlEgg = this;
        kitchenMgr.mouseArea = DragArea.Bowl_Egg;
    }

    public void OnMouseExit()
    {
        image.sprite = sprite.normalSprite;

        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        kitchenMgr.trayEgg = null;
        kitchenMgr.mouseArea = DragArea.None;
    }

    public bool AddChicken()
    {
        if (IsMax())
            return false;

        //트레이에 올려져있는 닭 증가
        for (int i = 0; i < bowlChicken.Length; i++)
        {
            if (bowlChicken[i].isUse)
                continue;

            bowlChicken[i].Init();
            bowlChicken[i].gameObject.SetActive(true);
            bowlChicken[i].UseChicken();
            UpdateSort();

            chickenCnt++;

            soundMgr.PlaySE(Sound.Put_SE);
            image.sprite = sprite.normalSprite;

            if (IsMax())
            {
                KitchenMgr kitchenMgr = KitchenMgr.Instance;
                if (gameMgr.playData != null && gameMgr.playData.tutoComplete1 == false && kitchenMgr.cameraObj.lookArea == LookArea.Kitchen)
                {
                    //튜토리얼을 진행안한듯?
                    //튜토리얼로 진입
                    tutoObj.PlayTuto();
                }
            }
            break;
        }

        return true;
    }

    public void WorkerEggChickenPutAway()
    {
        for (int i = 0; i < bowlChicken.Length; i++)
        {
            if (bowlChicken[i].isUse == false)
            {
                bowlChicken[i].WorkerEggChickenPutAway();
                return;
            }
        }
    }

    public bool IsMax() => (chickenCnt == MAX_CHICKEN_SLOT);

    public bool WorkerRemoveChicken()
    {
        //트레이에 올려져있는 닭 감소
        if (chickenCnt <= 0)
            return false;

        for (int i = 0; i < bowlChicken.Length; i++)
        {
            if (bowlChicken[i].CompleteEgg())
            {
                bowlChicken[i].gameObject.SetActive(false);
                bowlChicken[i].Init();
                chickenCnt--;

                return true;
            }
        }

        return false;
    }

    public bool CompleteEgg()
    {
        //계란물 묻히기 작업이 완료됬는지 여부를 반환
        for(int i = 0; i < bowlChicken.Length; i++)
        {
            bool complete = bowlChicken[i].CompleteEgg();
            if (complete)
                return true;
        }
        return false;
    }

    public void WorkerDragChicken(float v)
    {
        for (int i = 0; i < bowlChicken.Length; i++)
            bowlChicken[i].WorkerDragChicken(v);
    }
}
