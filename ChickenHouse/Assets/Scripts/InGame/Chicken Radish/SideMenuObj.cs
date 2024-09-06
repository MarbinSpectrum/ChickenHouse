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
            case SideMenu.Pickle:
                kitchenMgr.dragState = DragState.Chicken_Pickle;
                break;
        }

    }

    public void OnMouseUp()
    {
        if (tutoMgr.tutoComplete == false && tutoMgr.nowTuto != Tutorial.Tuto_6)
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
            if (kitchenMgr.pickleSlot.Put_Pickle())
            {
                return;
            }
        }
    } 
}
