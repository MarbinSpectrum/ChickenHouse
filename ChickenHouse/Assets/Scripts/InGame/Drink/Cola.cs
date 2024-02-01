using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cola : Mgr
{
    private void OnMouseDrag()
    {
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        kitchenMgr.dragState = DragState.Cola;
    }

    private void OnMouseUp()
    {
        KitchenMgr kitchenMgr = KitchenMgr.Instance;

        //�������� ���ᰡ ������
        kitchenMgr.dragState = DragState.None;
        if (kitchenMgr.mouseArea == DragArea.Drink_Slot)
        {
            //���Ḧ �÷����´�.
            if (kitchenMgr.drinkSlot.Put_Drinkt())
            {
                return;
            }
        }
    }
}
