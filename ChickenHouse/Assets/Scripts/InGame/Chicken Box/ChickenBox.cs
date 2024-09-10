using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenBox : Mgr
{

    public void OnMouseDrag()
    {
        if (tutoMgr.tutoComplete == false && (tutoMgr.nowTuto == Tutorial.Tuto_1 || tutoMgr.nowTuto == Tutorial.Tuto_5) == false)
        {
            //튜토리얼이 아직 완료안된듯
            //혹시모르니 튜토리얼 타이밍때만 작동하도록 막아놓자
            return;
        }

        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if (kitchenMgr.dragState != DragState.None)
        {
            //빈손인 상태에서 드래그해야됨
            return;
        }
        if (kitchenMgr.cameraObj.lookArea != LookArea.Kitchen)
        {
            //주방을 보고있는 상태에서만 상호 작용 가능
            return;
        }

        kitchenMgr.dragState = DragState.Normal;
    }

    public void OnMouseUp()
    {
        //손을때면 치킨이 떨어짐
        KitchenMgr kitchenMgr = KitchenMgr.Instance;

        if (kitchenMgr.mouseArea == DragArea.Bowl_Egg &&
           kitchenMgr.dragState == DragState.Normal)
        {
            //계란물쪽으로 치킨을 드래그함
            if (kitchenMgr.bowlEgg.AddChicken())
            {
                kitchenMgr.dragState = DragState.None;
                return;
            }
        }
        else if (kitchenMgr.mouseArea == DragArea.Tray_Egg && 
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

    public void OnMouseEnter()
    {
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        kitchenMgr.chickenBox = this;
        kitchenMgr.mouseArea = DragArea.Chicken_Box;
    }

    public void OnMouseExit()
    {
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        kitchenMgr.chickenBox = null;
        kitchenMgr.mouseArea = DragArea.None;
    }
}
