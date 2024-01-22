using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenSource : MonoBehaviour
{
    private void OnMouseDrag()
    {
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if (kitchenMgr.dragState != DragState.None)
        {
            //빈손인 상태에서 드래그해야됨
            return;
        }
        kitchenMgr.dragState = DragState.Chicken_Source;
    }

    private void OnMouseUp()
    {
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
    }
}
