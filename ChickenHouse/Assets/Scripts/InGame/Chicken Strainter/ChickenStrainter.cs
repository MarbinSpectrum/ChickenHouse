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

    public bool isRun { get; private set; } = true;

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
        kitchenMgr.chickenStrainter = null;
        kitchenMgr.mouseArea = DragArea.None;
    }

    public void OnMouseDrag()
    {
        if (tutoMgr.tutoComplete == false && tutoMgr.nowTuto != Tutorial.Tuto_4)
        {
            //튜토리얼이 아직 완료안된듯
            //혹시모르니 튜토리얼 타이밍때만 작동하도록 막아놓자
            return;
        }

        if (isRun == false)
            return;

        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if (kitchenMgr.cameraObj.lookArea != LookArea.Kitchen)
        {
            //주방을 보고있는 상태에서만 상호 작용 가능
            return;
        }
        if (chickenCnt >= MAX_CHICKEN_SLOT)
        {
            obj.gameObject.SetActive(false);
            isRun = false;
            kitchenMgr.dragObj.DragStrainter(chickenCnt, DragState.Chicken_Strainter);
        }
    }

    public void OnMouseUp()
    {
        if (tutoMgr.tutoComplete == false && tutoMgr.nowTuto != Tutorial.Tuto_4)
        {
            //튜토리얼이 아직 완료안된듯
            //혹시모르니 튜토리얼 타이밍때만 작동하도록 막아놓자
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
        if (kitchenMgr.mouseArea == DragArea.Oil_Zone)
        {
            //치킨 튀기기 시작
            if (kitchenMgr.oilZone.Cook_Start(chickenCnt,this))
            {
                kitchenMgr.ui.takeOut.ChickenStrainter_SetData(kitchenMgr.oilZone, this);

                for (int i = 0; i < chickenCnt; i++)
                {
                    RemoveChicken();
                }
                return;
            }
        }

        isRun = true;
        obj.gameObject.SetActive(true);
    }

    public bool AddChicken()
    {
        if (isRun == false)
            return false;

        if (chickenCnt >= MAX_CHICKEN_SLOT)
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

        if (chickenCnt == MAX_CHICKEN_SLOT)
        {
            foreach (ScrollObj sObj in scrollObj)
            {
                sObj.isRun = false;
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

    public bool RemoveChicken()
    {
        //트레이에 올려져있는 닭 감소
        if (chickenCnt <= 0)
            return false;

        foreach(ScrollObj sObj in scrollObj)
        {
            sObj.isRun = true;
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
        isRun = true;
        obj.gameObject.SetActive(true);

        foreach (ScrollObj sObj in scrollObj)
        {
            sObj.isRun = true;
        }
    }
}
