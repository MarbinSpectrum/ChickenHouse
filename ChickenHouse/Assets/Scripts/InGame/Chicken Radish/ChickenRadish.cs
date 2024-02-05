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
            //주방을 보고있는 상태에서만 상호 작용 가능
            return;
        }

        kitchenMgr.dragState = DragState.Chicken_Pickle;
    }

    private void OnMouseUp()
    {
        KitchenMgr kitchenMgr = KitchenMgr.Instance;

        //손을때면 치킨 무가 떨어짐
        kitchenMgr.dragState = DragState.None;
        if (kitchenMgr.mouseArea == DragArea.Pickle_Slot)
        {
            //치킨 무를 올려놓는다.
            if (kitchenMgr.pickleSlot.Put_Pickle())
            {
                return;
            }
        }
    } 
}
