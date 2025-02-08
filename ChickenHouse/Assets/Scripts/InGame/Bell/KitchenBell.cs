using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenBell : Mgr
{
    [SerializeField] private TutoObj tutoObj;
    [SerializeField] private TableChickenSlot tableChicken;
    [SerializeField] private TableDrinkSlot tableDrinkSlot;
    [SerializeField] private TableSideMenuSlot tablePickleSlot;

    public void CompleteBtn()
    {

        if (gameMgr.playData != null && gameMgr.playData.tutoComplete1 == false && tutoMgr.nowTuto != Tutorial.Tuto_12)
        {            
            //튜토리얼이 아직 완료안된듯
            //혹시모르니 튜토리얼 타이밍때만 작동하도록 막아놓자
            return;
        }

        if(tableChicken.hasChicken == false)
        {
            //치킨을 올려야지 작동됨
            return;
        }

        GuestSystem guestMgr = GuestSystem.Instance;
        if (guestMgr.guestcnt <= 0)
            return;

        soundMgr.PlaySE(Sound.NewOrder_SE);

        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        kitchenMgr.ui.goCounter.CloseBtn();

        void GiveChicken(bool useWorker)
        {
            Drink drink = tableDrinkSlot.drink;
            SideMenu sideMenu = tablePickleSlot.pickle;
            ChickenSpicy spicy0 = (ChickenSpicy)Mathf.Min((int)tableChicken.source0, (int)tableChicken.source1);
            ChickenSpicy spicy1 = (ChickenSpicy)Mathf.Max((int)tableChicken.source0, (int)tableChicken.source1);

            guestMgr.GiveChicken(spicy0, spicy1, tableChicken.chickenState,
              drink, sideMenu, useWorker);

            tableChicken.Init();
            tableDrinkSlot.Init();
            tablePickleSlot.Init();
            kitchenMgr.ui.memo.RemoveMemo(0);
        }

        if (gameMgr.playData != null && (EWorker)gameMgr.playData.workerPos[(int)KitchenSetWorkerPos.CounterWorker] != EWorker.None)
        {
            //직원 사용해서 카운터로 이동안함
            GiveChicken(true);
        }
        else
        {
            //직원을 사용하지 않았기에 카운터로 이동해서 계산
            kitchenMgr.cameraObj.ChangeLook(LookArea.Counter, () =>
            {
                GiveChicken(false);
                kitchenMgr.ui.memo.CloseTriggerBox();
                kitchenMgr.ui.workerUI.OffBox();
            });
        }


    }
}
