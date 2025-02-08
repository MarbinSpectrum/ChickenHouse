using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TableSideMenuSlot : Mgr
{
    /** ���̵�޴��� �÷������� ���� **/
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
            //���̵尡 �̹� ��������
            return;
        }

        if (kitchenMgr.dragState != DragState.None)
        {
            //���̵带 �������ִ� �����̱��ϴ�.
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
            //���ᰡ �̹� ��������
            return;
        }
        pickleImg.color = new Color(0, 0, 0, 0);
        slotUI.gameObject.SetActive(true);
    }

    public void OnMouseDown()
    {
        if (gameMgr.playData != null && gameMgr.playData.tutoComplete1 == false)
        {
            //Ʃ�丮�󿡼��� ���Ű� ��������
            return;
        }

        //�÷����ִ� ��Ŭ�� �����Ҷ� ���
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
            //�̹� ���̵尡 ����
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
