using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenRadish : Mgr
{
    private void OnMouseDrag()
    {
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if (kitchenMgr.cameraObj.lookArea != LookArea.Kitchen)
        {
            //�ֹ��� �����ִ� ���¿����� ��ȣ �ۿ� ����
            return;
        }

        kitchenMgr.dragState = DragState.Chicken_Pickle;
    }

    private void OnMouseUp()
    {
        KitchenMgr kitchenMgr = KitchenMgr.Instance;

        //�������� ġŲ ���� ������
        kitchenMgr.dragState = DragState.None;
        if (kitchenMgr.mouseArea == DragArea.Pickle_Slot)
        {
            //ġŲ ���� �÷����´�.
            if (kitchenMgr.pickleSlot.Put_Pickle())
            {
                return;
            }
        }
    } 
}
