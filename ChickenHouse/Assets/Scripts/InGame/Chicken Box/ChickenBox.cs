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
            //빈손인 상태에서 드래그해야됨
            return;
        }
        kitchenMgr.dragState = DragState.Normal;
    }

    private void OnMouseUp()
    {
        //손을때면 치킨이 떨어짐
        KitchenMgr kitchenMgr = KitchenMgr.Instance;

        if (kitchenMgr.mouseArea == DragArea.Tray_Egg && 
            kitchenMgr.dragState == DragState.Normal)
        {
            //계란물쪽으로 치킨을 드래그함
            if(kitchenMgr.trayEgg.AddChicken())
            {
                kitchenMgr.dragState = DragState.None;
                return;
            }
        }

        //손을때면 치킨이 떨어짐
        kitchenMgr.dragState = DragState.None;
    }
}
