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

    public void ChickenStrainter_TakeOut()
    {
        //버리기 버튼 활성화시 실행됩니다.

        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if (kitchenMgr.dragObj.holdGameObj != null)
        {
            Oil_Zone oilZone = kitchenMgr.dragObj.holdGameObj.GetComponent<Oil_Zone>();

            kitchenMgr.dragState = DragState.None;
            kitchenMgr.dragObj.HoldObj(null);
            kitchenMgr.dragObj.HoldOut();

            if (oilZone)
            {
                //요리 종료
                oilZone.Cook_Stop();
                return;
            }

        }
    }

    public void ChickenStrainter_TakeOut(Oil_Zone pOilZone)
    {
        if (CheckMode.IsDropMode())
            return;

        //버리기 버튼 활성화시 실행됩니다.
        if (pOilZone)
        {
            //요리 종료
            pOilZone.Cook_Stop();
            return;
        }
    }
}
