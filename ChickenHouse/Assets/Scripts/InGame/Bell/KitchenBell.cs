using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenBell : Mgr
{
    [SerializeField] private TutoObj tutoObj;
    [SerializeField] private TableChickenSlot tableChicken;
    [SerializeField] private TableDrinkSlot tableDrinkSlot;
    [SerializeField] private TablePickleSlot tablePickleSlot;

    public void CompleteBtn()
    {

        if (tutoMgr.tutoComplete == false && tutoMgr.nowTuto != Tutorial.Tuto_8)
        {            
            //Ʃ�丮���� ���� �Ϸ�ȵȵ�
            //Ȥ�ø𸣴� Ʃ�丮�� Ÿ�ֶ̹��� �۵��ϵ��� ���Ƴ���
            return;
        }

        if(tableChicken.hasChicken == false)
        {
            //ġŲ�� �÷����� �۵���
            return;
        }

        GuestMgr guestMgr = GuestMgr.Instance;
        if (guestMgr.guestcnt <= 0)
            return;

        soundMgr.PlaySE(Sound.NewOrder_SE);

        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        kitchenMgr.ui.goCounter.CloseBtn();

        kitchenMgr.cameraObj.ChangeLook(LookArea.Counter, () =>
        {
            Drink drink = tableDrinkSlot.hasDrink ? Drink.Cola : Drink.None;
            SideMenu sideMenu = tablePickleSlot.hasPickle ? SideMenu.Pickle : SideMenu.None;
            ChickenSpicy spicy0 = (ChickenSpicy)Mathf.Min((int)tableChicken.source0, (int)tableChicken.source1);
            ChickenSpicy spicy1 = (ChickenSpicy)Mathf.Max((int)tableChicken.source0, (int)tableChicken.source1);
            GuestMgr guestMgr = GuestMgr.Instance;
            guestMgr.GiveChicken(spicy0, spicy1, tableChicken.chickenState,
              drink, sideMenu);

            tableChicken.Init();
            tableDrinkSlot.Init();
            tablePickleSlot.Init();
            kitchenMgr.ui.memo.RemoveMemo(0);
            //guestMgr.ui.goKitchen.OpenBtn();
        });


        kitchenMgr.cameraObj.lookArea = LookArea.Counter;
        kitchenMgr.ui.memo.CloseTriggerBox();
        kitchenMgr.ui.workerUI.OffBox();
    }
}
