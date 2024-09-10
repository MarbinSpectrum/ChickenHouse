using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenBox : Mgr
{

    public void OnMouseDrag()
    {
        if (tutoMgr.tutoComplete == false && (tutoMgr.nowTuto == Tutorial.Tuto_1 || tutoMgr.nowTuto == Tutorial.Tuto_5) == false)
        {
            //Ʃ�丮���� ���� �Ϸ�ȵȵ�
            //Ȥ�ø𸣴� Ʃ�丮�� Ÿ�ֶ̹��� �۵��ϵ��� ���Ƴ���
            return;
        }

        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if (kitchenMgr.dragState != DragState.None)
        {
            //����� ���¿��� �巡���ؾߵ�
            return;
        }
        if (kitchenMgr.cameraObj.lookArea != LookArea.Kitchen)
        {
            //�ֹ��� �����ִ� ���¿����� ��ȣ �ۿ� ����
            return;
        }

        kitchenMgr.dragState = DragState.Normal;
    }

    public void OnMouseUp()
    {
        //�������� ġŲ�� ������
        KitchenMgr kitchenMgr = KitchenMgr.Instance;

        if (kitchenMgr.mouseArea == DragArea.Bowl_Egg &&
           kitchenMgr.dragState == DragState.Normal)
        {
            //����������� ġŲ�� �巡����
            if (kitchenMgr.bowlEgg.AddChicken())
            {
                kitchenMgr.dragState = DragState.None;
                return;
            }
        }
        else if (kitchenMgr.mouseArea == DragArea.Tray_Egg && 
            kitchenMgr.dragState == DragState.Normal)
        {
            //����������� ġŲ�� �巡����
            if(kitchenMgr.trayEgg.AddChicken())
            {
                kitchenMgr.dragState = DragState.None;
                return;
            }
        }

        //�������� ġŲ�� ������
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
