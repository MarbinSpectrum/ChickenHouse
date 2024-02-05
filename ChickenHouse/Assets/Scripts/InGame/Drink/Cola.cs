using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cola : Mgr
{
    private void OnMouseDrag()
    {
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if(kitchenMgr.cameraObj.lookArea != LookArea.Kitchen)
        {
            //주방을 보고있는 상태에서만 상호 작용 가능
            return;
        }

        kitchenMgr.dragState = DragState.Cola;
    }

    private void OnMouseUp()
    {
        KitchenMgr kitchenMgr = KitchenMgr.Instance;

        //손을때면 음료가 떨어짐
        kitchenMgr.dragState = DragState.None;
        if (kitchenMgr.mouseArea == DragArea.Drink_Slot)
        {
            //음료를 올려놓는다.
            if (kitchenMgr.drinkSlot.Put_Drinkt())
            {
                return;
            }
        }
    }
}
