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

    public void ChickenStrainter_TakeOut(Oil_Zone pOil_Zone)
    {
        //������ ��ư Ȱ��ȭ�� ����˴ϴ�.

        if (pOil_Zone == null)
            return;

        //�丮 ����
        pOil_Zone.Cook_Stop();
    }
}
