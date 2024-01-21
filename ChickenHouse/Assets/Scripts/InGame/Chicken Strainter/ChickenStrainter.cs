using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenStrainter : Mgr
{
    private const int MAX_CHICKEN_SLOT = 6;

    /**닭 갯수 **/
    private int chickenCnt;

    public bool isRun { get; private set; } = true;

    [System.Serializable]
    public struct SPITE_IMG
    {
        //오브젝트 스프라이트 이미지
        public SpriteRenderer   spriteImg;
        public Sprite           normalSprite;
        public Sprite           canUseSprite;
    }
    [SerializeField] private SPITE_IMG sprite;

    [SerializeField] private Animation[]    chickenAni;
    [SerializeField] private GameObject     obj;

    private void OnMouseDrag()
    {
        if (isRun == false)
            return;

        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if(chickenCnt >= 4)
        {
            obj.gameObject.SetActive(false);
            isRun = false;
            kitchenMgr.dragChickenStrainter.DragStart(chickenCnt, DragState.ChickenStrainter);
        }
    }

    private void OnMouseUp()
    {
        KitchenMgr kitchenMgr = KitchenMgr.Instance;

        if (kitchenMgr.dragState != DragState.ChickenStrainter)
        {
            //해당 오브젝트를 드래그중이라고 판단되었을때만 적용
            return;
        }

        //손을때면 치킨건지 떨어짐
        kitchenMgr.dragState = DragState.None;
        if (kitchenMgr.mouseArea == DragArea.Oil_Zone)
        {
            //치킨 튀기기 시작
            if (kitchenMgr.oilZone.Cook_Start(chickenCnt))
            {
                kitchenMgr.ui.takeOut.SetData(kitchenMgr.oilZone, this);

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

    private void Update()
    {
        UpdateChickenTray();
    }

    private void UpdateChickenTray()
    {
        if (isRun == false)
            return;

        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if (kitchenMgr.mouseArea == DragArea.Chicken_Strainter &&
            (kitchenMgr.chickenStrainter != null && kitchenMgr.chickenStrainter == this))
        {
            if (kitchenMgr.dragState == DragState.Flour &&
                    chickenCnt < MAX_CHICKEN_SLOT)
            {
                //치킨을 드래그해서 가져왔으며
                //슬롯이 비어져있다.
                sprite.spriteImg.sprite = sprite.canUseSprite;
            }
            else
            {
                //나머지 상태면 이미지가 보이지 않는다.
                sprite.spriteImg.sprite = sprite.normalSprite;
            }
        }
        else
        {
            //마우스를 밖으로 내보내면 이펙트 비활성화
            sprite.spriteImg.sprite = sprite.normalSprite;
        }
    }

    public bool AddChicken()
    {
        if (isRun == false)
            return false;

        if (chickenCnt >= MAX_CHICKEN_SLOT)
            return false;

        //기름 건지에 올려져있는 닭 증가
        chickenCnt++;

        for(int i = 0; i < chickenAni.Length; i++)
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

        return true;
    }

    public bool RemoveChicken()
    {
        //트레이에 올려져있는 닭 감소
        if (chickenCnt <= 0)
            return false;
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
    }
}
