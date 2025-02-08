using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TableSideMenuSlot : Mgr
{
    /** 사이드메뉴가 올려졌는지 여부 **/
    public SideMenu pickle { get; private set; }

    [SerializeField] private TableDrinkSlot drinkSlot;
    [SerializeField] private Image          pickleImg;
    [SerializeField] private GameObject     slotUI;
    [SerializeField] private TutoObj        tutoObj;
    [SerializeField] private ScrollObj      scrollObj;

    public void OnMouseEnter()
    {
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        kitchenMgr.mouseArea = DragArea.SideMenu_Slot;
        kitchenMgr.pickleSlot = this;

        if (pickle != SideMenu.None)
        {
            //사이드가 이미 놓여있음
            return;
        }

        if (kitchenMgr.dragState != DragState.None)
        {
            //사이드를 놓을수있는 상태이긴하다.
            SideMenu sideMenu = SubMenuMgr.GetDragStateSideMenu(kitchenMgr.dragState);
            SideMenuData sideMenuData = subMenuMgr.GetSideMenuData(sideMenu);
            if (sideMenuData == null)
                return;
            pickleImg.sprite = sideMenuData.img;
            pickleImg.color = new Color(1, 1, 1, 0.5f);
            slotUI.gameObject.SetActive(true);
        }
    }

    public void OnMouseExit()
    {
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        kitchenMgr.mouseArea = DragArea.None;
        kitchenMgr.pickleSlot = null;

        if (pickle != SideMenu.None)
        {
            //음료가 이미 놓여있음
            return;
        }
        pickleImg.color = new Color(0, 0, 0, 0);
        slotUI.gameObject.SetActive(true);
    }

    public void OnMouseDown()
    {
        if (gameMgr.playData != null && gameMgr.playData.tutoComplete1 == false)
        {
            //튜토리얼에서는 제거가 되지않음
            return;
        }

        //올려져있는 피클을 제거할때 사용
        if (pickle == SideMenu.None)
            return;

        pickleImg.color = new Color(0, 0, 0, 0);
        slotUI.gameObject.SetActive(true);

        pickle = SideMenu.None;

        scrollObj.isRun = true;
    }

    public bool Put_Pickle(SideMenu eSideMenu)
    {
        if (pickle != SideMenu.None)
        {
            //이미 사이드가 놓임
            return false;
        }

        soundMgr.PlaySE(Sound.Put_SE);

        if (gameMgr.playData != null && gameMgr.playData.tutoComplete1 == false && tutoMgr.nowTuto == Tutorial.Tuto_10 && drinkSlot.drink != Drink.None)
        {
            tutoObj.PlayTuto();

            KitchenMgr kitchenMgr = KitchenMgr.Instance;
            //kitchenMgr.ui.goCounter.OpenBtn();
        }

        pickleImg.color = new Color(1, 1, 1, 1);
        slotUI.gameObject.SetActive(false);

        pickle = eSideMenu;

        scrollObj.isRun = false;

        return true;
    }

    public void Init()
    {
        pickleImg.color = new Color(0, 0, 0, 0);
        slotUI.gameObject.SetActive(true);

        pickle = SideMenu.None;

        scrollObj.isRun = true;
    }
}
