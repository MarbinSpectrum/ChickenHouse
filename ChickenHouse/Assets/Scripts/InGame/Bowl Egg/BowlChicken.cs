using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlChicken : Mgr
{
    [SerializeField] private ChickenLerp_Shader lerpShader;
    [SerializeField] private BowlEgg bowlEgg;

    [SerializeField] private float disW;
    [SerializeField] private float disH;
    private Vector3 basePos;
    private bool init;
    private float eggTime = 0;
    private const float EGG_DELAY = 1f;
    public bool isDrag { private set; get; }

    public void Init()
    {
        if (init == false)
        {
            init = true;
            basePos = lerpShader.transform.localPosition;
        }

        lerpShader.transform.localPosition = basePos;
        eggTime = 0;
        lerpShader.SetValue(0);
        lerpShader.gameObject.SetActive(true);
    }

    public void OnMouseDrag()
    {
        if (tutoMgr.tutoComplete == false && tutoMgr.nowTuto != Tutorial.Tuto_2)
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
            eggTime += Time.deltaTime;
            float v = eggTime / EGG_DELAY;
            v = Mathf.Min(v, 1);
            lerpShader.SetValue(v);

            MoveDragPos();
            lerpShader.gameObject.SetActive(true);
        }
        else
        {
            kitchenMgr.dragState = DragState.Egg;
            lerpShader.gameObject.SetActive(false);
        }
    }

    private void MoveDragPos()
    {
        //치킨을 볼안에서만 움직일 수 있도록한다.
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos = new Vector3(mousePos.x, mousePos.y, lerpShader.transform.position.z);

        double result = ((mousePos.x - transform.position.x) * (mousePos.x - transform.position.x)) /
            (disW * disW) + ((mousePos.y - transform.position.y) * (mousePos.y - transform.position.y)) / (disH * disH);

        if (result <= 1)
        {
            //타원 범위안이라면 마우스 좌표로
            lerpShader.transform.position = mousePos;
        }
        else
        {
            //타원밖이면 타원안에서 최대한 마우스 좌표랑 가까운 곳으로 이동하도록

            Vector3 zeroBaseMousePos = mousePos - transform.position;
            float x1 = ((disW * disH) / Mathf.Sqrt(disW * disW * zeroBaseMousePos.y * zeroBaseMousePos.y + disH * disH * zeroBaseMousePos.x * zeroBaseMousePos.x)) * zeroBaseMousePos.x;
            float x2 = -x1;
            float y1 = ((disW * disH) / Mathf.Sqrt(disW * disW * zeroBaseMousePos.y * zeroBaseMousePos.y + disH * disH * zeroBaseMousePos.x * zeroBaseMousePos.x)) * zeroBaseMousePos.y;
            float y2 = -y1;

            // 두 교점 중 mousePos와 가까운 점 선택
            float dist1 = Vector2.Distance(new Vector2(x1, y1), zeroBaseMousePos);
            float dist2 = Vector2.Distance(new Vector2(x2, y2), zeroBaseMousePos);

            lerpShader.transform.position = (dist1 < dist2 ? new Vector3(x1, y1) : new Vector3(x2, y2)) + transform.position;
        }
    }



    public void OnMouseUp()
    {
        if (tutoMgr.tutoComplete == false && tutoMgr.nowTuto != Tutorial.Tuto_2)
        {
            //튜토리얼이 아직 완료안된듯
            //혹시모르니 튜토리얼 타이밍때만 작동하도록 막아놓자
            return;
        }

        isDrag = false;

        //손을때면 치킨이 떨어짐
        KitchenMgr kitchenMgr = KitchenMgr.Instance;

        if (kitchenMgr.mouseArea == DragArea.Tray_Flour &&
            kitchenMgr.dragState == DragState.Egg)
        {
            //밀가루 쪽으로 치킨을 드래그함
            if (kitchenMgr.trayFlour.AddChicken())
            {
                kitchenMgr.dragState = DragState.None;
                bowlEgg.RemoveChicken();
                return;
            }
        }
        else if (kitchenMgr.mouseArea == DragArea.Tray_Flour2 &&
            kitchenMgr.dragState == DragState.Egg)
        {
            //밀가루 쪽으로 치킨을 드래그함
            if (kitchenMgr.trayFlour2.AddChicken())
            {
                kitchenMgr.dragState = DragState.None;
                bowlEgg.RemoveChicken();
                return;
            }
        }

        kitchenMgr.dragState = DragState.None;

        if (bowlEgg.IsMax())
            lerpShader.gameObject.SetActive(true);
        else
            lerpShader.gameObject.SetActive(false);
    }

    public bool CompleteEgg()
    {
        //계란물 묻히기 작업이 완료됬는지 여부를 반환
        return eggTime >= EGG_DELAY;
    }

    public void WorkerEggChickenPutAway()
    {
        if (isDrag)
            return;
        Init();
        eggTime = EGG_DELAY;
        lerpShader.SetValue(1);
        lerpShader.gameObject.SetActive(true);
    }

    public void WorkerDragChicken(float v)
    {
        eggTime += v;
        lerpShader.SetValue(eggTime/ EGG_DELAY);
    }
}
