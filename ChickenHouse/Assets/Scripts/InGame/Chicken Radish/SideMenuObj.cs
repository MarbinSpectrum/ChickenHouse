using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SideMenuObj : Mgr
{
    private SideMenu eSideMenu;
    [SerializeField] private List<Image> sideMenuImg = new List<Image>();

    public void SetObj(SideMenu pSideMenu)
    {
        eSideMenu = pSideMenu;

        SideMenuData sideMenuData = subMenuMgr.GetSideMenuData(eSideMenu);
        foreach (Image img in sideMenuImg)
            img.sprite = sideMenuData.img;
    }

    public void OnMouseDrag()
    {
        if (tutoMgr.tutoComplete == false && tutoMgr.nowTuto != Tutorial.Tuto_6)
        {
            //튜토리얼이 아직 완료안된듯
            //혹시모르니 튜토리얼 타이밍때만 작동하도록 막아놓자
            return;
        }

        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if (kitchenMgr.cameraObj.lookArea != LookArea.Kitchen)
        {
            //주방을 보고있는 상태에서만 상호 작용 가능
            return;
        }

        switch(eSideMenu)
        {
            case SideMenu.Pickle:
                kitchenMgr.dragState = DragState.Chicken_Pickle;
                break;
        }

    }

    public void OnMouseUp()
    {
        if (tutoMgr.tutoComplete == false && tutoMgr.nowTuto != Tutorial.Tuto_6)
        {
            //튜토리얼이 아직 완료안된듯
            //혹시모르니 튜토리얼 타이밍때만 작동하도록 막아놓자
            return;
        }

        KitchenMgr kitchenMgr = KitchenMgr.Instance;

        //손을때면 치킨 무가 떨어짐
        kitchenMgr.dragState = DragState.None;

        if (kitchenMgr.mouseArea == DragArea.SideMenu_Slot)
        {
            //치킨 무를 올려놓는다.
            if (kitchenMgr.pickleSlot.Put_Pickle())
            {
                return;
            }
        }
    } 
}
