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
        //������ ��ư Ȱ��ȭ
        animator.SetBool("Open", true);
    }

    public void CloseBtn()
    {
        //������ ��ư ��Ȱ��ȭ
        animator.SetBool("Open", false);
    }

    public void ChickenStrainter_TakeOut()
    {
        //������ ��ư Ȱ��ȭ�� ����˴ϴ�.

        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if (kitchenMgr.dragObj.holdGameObj != null)
        {
            Oil_Zone oilZone = kitchenMgr.dragObj.holdGameObj.GetComponent<Oil_Zone>();

            kitchenMgr.dragState = DragState.None;
            kitchenMgr.dragObj.HoldObj(null);
            kitchenMgr.dragObj.HoldOut();

            if (oilZone)
            {
                //�丮 ����
                oilZone.Cook_Stop();
                return;
            }

        }
    }

    public void ChickenStrainter_TakeOut(Oil_Zone pOilZone)
    {
        if (CheckMode.IsDropMode())
            return;

        //������ ��ư Ȱ��ȭ�� ����˴ϴ�.
        if (pOilZone)
        {
            //�丮 ����
            pOilZone.Cook_Stop();
            return;
        }
    }
}
