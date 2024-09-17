using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrinkObj : Mgr
{
    private Drink eDrink;
    [SerializeField] private List<Image> drinkImg = new List<Image>();

    public void SetObj(Drink pDrink)
    {
        eDrink = pDrink;

        DrinkData drinkData = subMenuMgr.GetDrinkData(pDrink);
        foreach (Image img in drinkImg)
            img.sprite = drinkData.img;
    }

    public void OnMouseDrag()
    {
        if (gameMgr.playData.tutoComplete1 == false && tutoMgr.nowTuto != Tutorial.Tuto_10)
        {
            //튜토리얼이 아직 완료안된듯
            //혹시모르니 튜토리얼 타이밍때만 작동하도록 막아놓자
            return;
        }

        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if(kitchenMgr.cameraObj.lookArea != LookArea.Kitchen)
        {
            //주방을 보고있는 상태에서만 상호 작용 가능
            return;
        }

        switch(eDrink)
        {
            case Drink.Cola:
                kitchenMgr.dragState = DragState.Cola;
                break;
            case Drink.Beer:
                kitchenMgr.dragState = DragState.Beer;
                break;
        }

    }

    public void OnMouseUp()
    {
        if (gameMgr.playData.tutoComplete1 == false && tutoMgr.nowTuto != Tutorial.Tuto_10)
        {
            //튜토리얼이 아직 완료안된듯
            //혹시모르니 튜토리얼 타이밍때만 작동하도록 막아놓자
            return;
        }

        KitchenMgr kitchenMgr = KitchenMgr.Instance;

        //손을때면 음료가 떨어짐
        kitchenMgr.dragState = DragState.None;

        if (kitchenMgr.mouseArea == DragArea.SideMenu_Slot)
        {
            //음료를 올려놓는다.
            if (kitchenMgr.drinkSlot.Put_Drink())
            {
                return;
            }
        }
    }
}
