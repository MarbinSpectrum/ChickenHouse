using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenRadish : Mgr
{
    public void OnMouseDrag()
    {
        if (tutoMgr.tutoComplete == false && tutoMgr.nowTuto != Tutorial.Tuto_6)
        {
            //Ʃ�丮���� ���� �Ϸ�ȵȵ�
            //Ȥ�ø𸣴� Ʃ�丮�� Ÿ�ֶ̹��� �۵��ϵ��� ���Ƴ���
            return;
        }

        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if (kitchenMgr.cameraObj.lookArea != LookArea.Kitchen)
        {
            //�ֹ��� �����ִ� ���¿����� ��ȣ �ۿ� ����
            return;
        }

        kitchenMgr.dragState = DragState.Chicken_Pickle;

    }

    public void OnMouseUp()
    {
        if (tutoMgr.tutoComplete == false && tutoMgr.nowTuto != Tutorial.Tuto_6)
        {
            //Ʃ�丮���� ���� �Ϸ�ȵȵ�
            //Ȥ�ø𸣴� Ʃ�丮�� Ÿ�ֶ̹��� �۵��ϵ��� ���Ƴ���
            return;
        }

        KitchenMgr kitchenMgr = KitchenMgr.Instance;

        //�������� ġŲ ���� ������
        kitchenMgr.dragState = DragState.None;

        if (kitchenMgr.mouseArea == DragArea.SideMenu_Slot)
        {
            //ġŲ ���� �÷����´�.
            if (kitchenMgr.pickleSlot.Put_Pickle())
            {
                return;
            }
        }
    } 
}
