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
        //������ ��ư Ȱ��ȭ
        animator.SetBool("Open", true);
    }

    public void CloseBtn()
    {
        //������ ��ư ��Ȱ��ȭ
        animator.SetBool("Open", false);
    }

    public void ChickenStrainter_SetData(Oil_Zone pOilZone, ChickenStrainter pChickenStrainter)
    {
        //�������� �⸧�뿡�� �丮�� �����ؾߵǹǷ�
        //�⸧�� ���
        oilZone = pOilZone;

        //ġŲ������ ���� ��ġ�� ���������ϹǷ�
        //ġŲ���� ���
        chickenStrainter = pChickenStrainter;
    }

    public void ChickenStrainter_TakeOut()
    {
        //�ν����Ϳ� ��� ����ϴ� �Լ��Դϴ�.
        //������ ��ư Ȱ��ȭ�� ����˴ϴ�.

        if (oilZone == null || chickenStrainter == null)
            return;

        //�丮 ����
        oilZone.Cook_Stop();

        //ġŲ ������ ����� �������� �ʱ�ȭ
        chickenStrainter.Init();

        //��������� �ߺ�����ϸ� ����ϹǷ�
        //null������ �����
        oilZone             = null;
        chickenStrainter    = null;
    }
}
