using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenBox : Mgr
{

    private void OnMouseDrag()
    {
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if (kitchenMgr.dragState != DragState.None)
        {
            //����� ���¿��� �巡���ؾߵ�
            return;
        }
        kitchenMgr.dragState = DragState.Normal;
    }

    private void OnMouseUp()
    {
        //�������� ġŲ�� ������
        KitchenMgr kitchenMgr = KitchenMgr.Instance;

        if (kitchenMgr.mouseArea == DragArea.Tray_Egg && 
            kitchenMgr.dragState == DragState.Normal)
        {
            //����������� ġŲ�� �巡����
            if(kitchenMgr.trayEgg.AddChicken())
            {
                kitchenMgr.dragState = DragState.None;
                return;
            }
        }

        //�������� ġŲ�� ������
        kitchenMgr.dragState = DragState.None;
    }
}
