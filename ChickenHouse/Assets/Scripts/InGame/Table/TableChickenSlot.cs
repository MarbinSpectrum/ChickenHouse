using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableChickenSlot : Mgr
{
    /** 담겨있는 치킨 갯수 **/
    private int chickenCnt;
    /** 치킨 상태 **/
    private ChickenState chickenState;

    /** 소스0 **/
    private ChickenSpicy source0;
    /** 소스1 **/
    private ChickenSpicy source1;

    [SerializeField] private SpriteRenderer boxImg;
    [SerializeField] private GameObject     slotUI;

    private void Update()
    {
        UpdateChickenSlot();
    }

    private void UpdateChickenSlot()
    {
        if (chickenCnt > 0)
        {
            //치킨이 이미 놓여있음
            boxImg.color = new Color(1, 1, 1, 1);
            slotUI.gameObject.SetActive(false);
            return;
        }
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if (kitchenMgr.dragState == DragState.Chicken_Pack)
        {
            //치킨을 놓을수있는 상태이긴하다.
            boxImg.color = new Color(1, 1, 1, 0.5f);
            slotUI.gameObject.SetActive(true);
        }
        else
        {
            //마우스를 밖으로 내보내면 이펙트 비활성화
            boxImg.color = new Color(0, 0, 0, 0);
            slotUI.gameObject.SetActive(true);
        }
    }

    public bool Put_ChickenPack(int pChickenCnt, ChickenState pChickenState, ChickenSpicy pSource0, ChickenSpicy pSource1)
    {
        if (chickenCnt > 0)
        {
            //이미 치킨이 놓임
            return false;
        }

        chickenCnt = pChickenCnt;
        chickenState = pChickenState;
        source0 = pSource0;
        source1 = pSource1;

        return true;
    }
}
