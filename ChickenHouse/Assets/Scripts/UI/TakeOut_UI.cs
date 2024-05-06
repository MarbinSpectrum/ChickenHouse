using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TakeOut_UI : Mgr
{
    [SerializeField] private Animator   animator;

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

    public void ChickenStrainter_TakeOut(Oil_Zone pOil_Zone)
    {
        //버리기 버튼 활성화시 실행됩니다.

        if (pOil_Zone == null)
            return;

        //요리 종료
        pOil_Zone.Cook_Stop();
    }
}
