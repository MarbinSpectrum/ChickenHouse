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
            //Ʃ�丮���� ���� �Ϸ�ȵȵ�
            //Ȥ�ø𸣴� Ʃ�丮�� Ÿ�ֶ̹��� �۵��ϵ��� ���Ƴ���
            return;
        }

        if(tableChicken.hasChicken == false)
        {
            //ġŲ�� �÷����� �۵���
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
            //���� ����ؼ� ī���ͷ� �̵�����
            GiveChicken(true);
        }
        else
        {
            //������ ������� �ʾұ⿡ ī���ͷ� �̵��ؼ� ���
            kitchenMgr.cameraObj.ChangeLook(LookArea.Counter, () =>
            {
                GiveChicken(false);
                kitchenMgr.ui.memo.CloseTriggerBox();
                kitchenMgr.ui.workerUI.OffBox();
            });
        }


    }
}
