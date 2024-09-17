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
            //Ʃ�丮���� ���� �Ϸ�ȵȵ�
            //Ȥ�ø𸣴� Ʃ�丮�� Ÿ�ֶ̹��� �۵��ϵ��� ���Ƴ���
            return;
        }

        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if(kitchenMgr.cameraObj.lookArea != LookArea.Kitchen)
        {
            //�ֹ��� �����ִ� ���¿����� ��ȣ �ۿ� ����
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
            //Ʃ�丮���� ���� �Ϸ�ȵȵ�
            //Ȥ�ø𸣴� Ʃ�丮�� Ÿ�ֶ̹��� �۵��ϵ��� ���Ƴ���
            return;
        }

        KitchenMgr kitchenMgr = KitchenMgr.Instance;

        //�������� ���ᰡ ������
        kitchenMgr.dragState = DragState.None;

        if (kitchenMgr.mouseArea == DragArea.SideMenu_Slot)
        {
            //���Ḧ �÷����´�.
            if (kitchenMgr.drinkSlot.Put_Drink())
            {
                return;
            }
        }
    }
}
