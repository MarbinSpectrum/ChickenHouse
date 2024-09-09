using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChickenStrainter : Mgr
{
    private const int MAX_CHICKEN_SLOT = 4;

    /**닭 갯수 **/
    private int chickenCnt;

    //public bool isRun { get; private set; } = true;

    public bool isHold { get; private set; } = false;

    [System.Serializable]
    public struct SPITE_IMG
    {
        //오브젝트 스프라이트 이미지
        public Sprite           normalSprite;
        public Sprite           canUseSprite;
    }
    [SerializeField] private SPITE_IMG      sprite;
    [SerializeField] private Image          image;
    [SerializeField] private Animation[]    chickenAni;
    [SerializeField] private ScrollObj[]    scrollObj;
    [SerializeField] private GameObject     obj;
    [SerializeField] private TutoObj        tutoObj;

    public void OnMouseEnter()
    {
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if (kitchenMgr.dragState == DragState.Flour && chickenCnt < MAX_CHICKEN_SLOT)
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
        kitchenMgr.chickenStrainter = this;
        kitchenMgr.mouseArea = DragArea.Chicken_Strainter;
    }

    public void OnMouseExit()
    {
        image.sprite = sprite.normalSprite;

        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        kitchenMgr.mouseArea = DragArea.None;

        if(isHold == false)
            kitchenMgr.chickenStrainter = null;
    }

    public void HoldStrainter()
    {
        if (CheckMode.IsDropMode() == false)
            return;



        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if (kitchenMgr.dragObj.holdGameObj == null)
        {
            HoldStrainter(true);
        }
        else if (kitchenMgr.dragObj.holdGameObj == gameObject)
        {
            HoldStrainter(false);
        }
        else if (kitchenMgr.dragObj.holdGameObj != gameObject)
        {
            kitchenMgr.dragState = DragState.None;
            kitchenMgr.dragObj.HoldObj(null);
            kitchenMgr.dragObj.HoldOut();
        }
    }

    public void HoldStrainter(bool state)
    {
        //인스펙터에서 끌어서 사용하는 함수임
        if (CheckMode.IsDropMode() == false)
            return;

        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if (state)
        {
            if (tutoMgr.tutoComplete == false && tutoMgr.nowTuto != Tutorial.Tuto_4)
            {
                //튜토리얼이 아직 완료안된듯
                //혹시모르니 튜토리얼 타이밍때만 작동하도록 막아놓자
                return;
            }

            if (kitchenMgr.cameraObj.lookArea != LookArea.Kitchen)
            {
                //주방을 보고있는 상태에서만 상호 작용 가능
                return;
            }

            if (isHold)
            {
                //이미 들고있는데 들수없지않을까?
                return;
            }

            if (IsMax())
            {
                isHold = true;
                obj.gameObject.SetActive(false);
                kitchenMgr.chickenStrainter = this;
                kitchenMgr.dragObj.DragStrainter(chickenCnt, DragState.Chicken_Strainter);
                kitchenMgr.dragObj.HoldObj(gameObject);
            }
        }
        else
        {
            if (isHold == false)
            {
                return;
            }

            isHold = false;
            obj.gameObject.SetActive(true);
            kitchenMgr.chickenStrainter = null;
            kitchenMgr.dragState = DragState.None;
            kitchenMgr.dragObj.HoldObj(null);
        }
    }

    public void PutDown(Oil_Zone pOilZone)
    {
        if (tutoMgr.tutoComplete == false && tutoMgr.nowTuto != Tutorial.Tuto_4)
        {
            //튜토리얼이 아직 완료안된듯
            //혹시모르니 튜토리얼 타이밍때만 작동하도록 막아놓자
            return;
        }

        if (IsMax() == false)
        {
            //가득찬 상태로 내려놔야됨
            return;
        }

        KitchenMgr kitchenMgr = KitchenMgr.Instance;

        if (kitchenMgr.dragState != DragState.Chicken_Strainter)
        {
            //해당 오브젝트를 드래그중이라고 판단되었을때만 적용
            return;
        }

        //손을때면 치킨건지 떨어짐
        kitchenMgr.dragState = DragState.None;

        //치킨 튀기기 시작
        if (pOilZone.Cook_Start(chickenCnt))
        {
            int removeCnt = chickenCnt;
            for (int i = 0; i < removeCnt; i++)
            {
                RemoveChicken();
            }
        }

        isHold = false;
        obj.gameObject.SetActive(true);
        kitchenMgr.dragObj.HoldObj(null);
    }

    public void OnMouseDrag()
    {
        if (CheckMode.IsDropMode())
            return;

        if (tutoMgr.tutoComplete == false && tutoMgr.nowTuto != Tutorial.Tuto_4)
        {
            return;
        }

        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if (kitchenMgr.cameraObj.lookArea != LookArea.Kitchen)
        {
            return;
        }
        if (IsMax())
        {
            isHold = true;
            obj.gameObject.SetActive(false);
            kitchenMgr.dragObj.DragStrainter(chickenCnt, DragState.Chicken_Strainter);
        }
    }

    public void OnMouseUp()
    {
        if (CheckMode.IsDropMode())
            return;

        if (tutoMgr.tutoComplete == false && tutoMgr.nowTuto != Tutorial.Tuto_4)
        {
            return;
        }

        KitchenMgr kitchenMgr = KitchenMgr.Instance;

        if (kitchenMgr.dragState != DragState.Chicken_Strainter)
        {
            return;
        }

        kitchenMgr.dragState = DragState.None;
        if (kitchenMgr.mouseArea == DragArea.Oil_Zone)
        {
            if (kitchenMgr.oilZone.Cook_Start(chickenCnt))
            {
                int removeCnt = chickenCnt;
                for (int i = 0; i < removeCnt; i++)
                {
                    RemoveChicken();
                }
            }
        }

        isHold = false;
        obj.gameObject.SetActive(true);
    }

    public bool AddChicken()
    {
        if (IsMax())
            return false;

        //기름 건지에 올려져있는 닭 증가
        chickenCnt++;
        image.sprite = sprite.normalSprite;

        for (int i = 0; i < chickenAni.Length; i++)
        {
            if(i < chickenCnt)
                chickenAni[i].gameObject.SetActive(true);
            else
                chickenAni[i].gameObject.SetActive(false);
        }

        if (chickenAni.Length > chickenCnt - 1)
        {
            //닭올리는 애니메이션 실행
            chickenAni[chickenCnt - 1].Play("ToChickenStrainter");
        }

        if (IsMax())
        {
            if (CheckMode.IsDropMode() == false)
            {
                foreach (ScrollObj sObj in scrollObj)
                {
                    sObj.isRun = false;
                }
            }

            if (tutoMgr.tutoComplete == false)
            {
                //튜토리얼을 진행안한듯?
                //튜토리얼로 진입
                tutoObj.PlayTuto();
            }
        }

        return true;
    }

    public bool IsMax() => (chickenCnt == MAX_CHICKEN_SLOT);

    public bool RemoveChicken()
    {
        //트레이에 올려져있는 닭 감소
        if (chickenCnt <= 0)
            return false;
        if (CheckMode.IsDropMode() == false)
        {
            foreach (ScrollObj sObj in scrollObj)
            {
                sObj.isRun = true;
            }
        }

            chickenCnt--;
        chickenAni[chickenCnt].gameObject.SetActive(false);

        return true;
    }

    public void Init()
    {
        //초기화 함
        chickenCnt = 0;
        Array.ForEach(chickenAni, x => x.gameObject.SetActive(false));
        obj.gameObject.SetActive(true);

        if (CheckMode.IsDropMode() == false)
        {
            foreach (ScrollObj sObj in scrollObj)
            {
                sObj.isRun = true;
            }
        }
    }
}
