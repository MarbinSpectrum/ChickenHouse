using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SideMenuObj : Mgr
{
    [SerializeField] private SideMenu sideMenu;
    public SideMenu SideMenu => sideMenu;

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

        kitchenMgr.dragState = SubMenuMgr.GetSideMenuDragState(sideMenu);
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
            if (kitchenMgr.pickleSlot.Put_Pickle(sideMenu))
            {
                return;
            }
        }
    } 
}
