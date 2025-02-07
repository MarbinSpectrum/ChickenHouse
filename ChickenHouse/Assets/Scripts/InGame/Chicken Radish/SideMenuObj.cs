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
        if (gameMgr.playData.tutoComplete1 == false && tutoMgr.nowTuto != Tutorial.Tuto_10)
        {
            //Ʃ�丮���� ���� �Ϸ�ȵȵ�
            //Ȥ�ø𸣴� Ʃ�丮�� Ÿ�ֶ̹��� �۵��ϵ��� ���Ƴ���
            return;
        }

        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if (kitchenMgr.cameraObj.lookArea != LookArea.Kitchen)
        {
            //�ֹ��� �����ִ� ���¿����� ��ȣ �ۿ� ����
            return;
        }

        switch(eSideMenu)
        {
            case SideMenu.ChickenRadish:
                kitchenMgr.dragState = DragState.Chicken_Radish;
                break;
        }

    }

    public void OnMouseUp()
    {
        if (gameMgr.playData.tutoComplete1 == false && tutoMgr.nowTuto != Tutorial.Tuto_10)
        {
            //Ʃ�丮���� ���� �Ϸ�ȵȵ�
            //Ȥ�ø𸣴� Ʃ�丮�� Ÿ�ֶ̹��� �۵��ϵ��� ���Ƴ���
            return;
        }

        KitchenMgr kitchenMgr = KitchenMgr.Instance;

        //�������� ġŲ ���� ������
        kitchenMgr.dragState = DragState.None;

        if (kitchenMgr.mouseArea == DragArea.SideMenu_Slot)
        {
            //ġŲ ���� �÷����´�.
            if (kitchenMgr.pickleSlot.Put_Pickle(eSideMenu))
            {
                return;
            }
        }
    } 
}
