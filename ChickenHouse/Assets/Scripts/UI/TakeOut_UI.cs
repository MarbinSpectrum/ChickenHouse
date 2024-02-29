using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TakeOut_UI : Mgr
{
    [SerializeField] private Animator   animator;
    private Oil_Zone                    oilZone;
    private ChickenStrainter            chickenStrainter;

    public void OnMouseEnter()
    {
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        kitchenMgr.mouseArea = DragArea.Trash_Btn;
    }

    public void OnMouseExit()
    {
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        kitchenMgr.mouseArea = DragArea.None;
    }

    public void OpenBtn()
    {
        //버리기 버튼 활성화
        animator.SetBool("Open", true);
    }

    public void CloseBtn()
    {
        //버리기 버튼 비활성화
        animator.SetBool("Open", false);
    }

    public void ChickenStrainter_SetData(Oil_Zone pOilZone, ChickenStrainter pChickenStrainter)
    {
        //버렸을때 기름통에서 요리를 종료해야되므로
        //기름통 등록
        oilZone = pOilZone;

        //치킨건지를 원래 위치로 돌려놔야하므로
        //치킨건지 등록
        chickenStrainter = pChickenStrainter;
    }

    public void ChickenStrainter_TakeOut()
    {
        //인스펙터에 끌어서 사용하는 함수입니다.
        //버리기 버튼 활성화시 실행됩니다.

        if (oilZone == null || chickenStrainter == null)
            return;

        //요리 종료
        oilZone.Cook_Stop();

        //치킨 건지를 사용을 끝냈으니 초기화
        chickenStrainter.Init();

        //사용했을때 중복사용하면 곤란하므로
        //null값으로 비워줌
        oilZone             = null;
        chickenStrainter    = null;
    }
}
