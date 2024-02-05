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

    private void OnMouseDrag()
    {
        if (chickenCnt == 0)
        {
            //치킨이 내부에 존재해야지 드래그가 가능
            return;
        }

        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if (kitchenMgr.cameraObj.lookArea != LookArea.Kitchen)
        {
            //주방을 보고있는 상태에서만 상호 작용 가능
            return;
        }
        kitchenMgr.dragState = DragState.Chicken_Pack;

        boxImg.gameObject.SetActive(false);

        //버리기 버튼도 표시해준다.
        kitchenMgr.ui.takeOut.OpenBtn();
    }

    private void OnMouseUp()
    {
        KitchenMgr kitchenMgr = KitchenMgr.Instance;

        if (kitchenMgr.dragState != DragState.Chicken_Pack)
        {
            //해당 오브젝트를 드래그중이라고 판단되었을때만 적용
            return;
        }

        boxImg.gameObject.SetActive(true);

        //버리기 버튼 비활성
        kitchenMgr.ui.takeOut.CloseBtn();

        //손을때면 치킨 박스 떨어짐
        kitchenMgr.dragState = DragState.None;
        if (kitchenMgr.mouseArea == DragArea.Trash_Btn)
        {
            //버리기 버튼처리
            Init();
            kitchenMgr.ui.goCounter.CloseBtn();
            return;
        }
    }


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

    public void Init()
    {
        //초기화 함
        chickenCnt = 0;
        chickenState = ChickenState.NotCook;
        source0 = ChickenSpicy.None;
        source1 = ChickenSpicy.None;

        boxImg.gameObject.SetActive(true);
    }
}
