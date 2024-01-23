using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenRadish : MonoBehaviour
{
    private void OnMouseDrag()
    {
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        kitchenMgr.dragState = DragState.Chicken_Radish;
    }

    private void OnMouseUp()
    {
        KitchenMgr kitchenMgr = KitchenMgr.Instance;

        //손을때면 치킨 무가 떨어짐
        kitchenMgr.dragState = DragState.None;
        if (kitchenMgr.mouseArea == DragArea.Chicken_Pack)
        {
            //치킨 무 넣기
            if (kitchenMgr.chickenPack.AddChickenRadish())
            {

            }
        }
    } 
}
