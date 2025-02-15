using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class StartGame : Mgr
{
    [SerializeField] private RectTransform  dayStartAni;
    [SerializeField] private RectTransform  dontClick;

    [SerializeField] private TutoObj        event0_Tuto;
    [SerializeField] private Event0_UI      event0_UI;

    public void Run()
    {
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if (kitchenMgr != null)
            kitchenMgr.Init();

        GuestSystem guestSystem = GuestSystem.Instance;
        guestSystem.ui.timer.gameObject.SetActive(true);
        guestSystem.ui.nowMoney.gameObject.SetActive(true);
        guestSystem.ui.pauseBtn.gameObject.SetActive(true);

        if (gameMgr.playData != null && (QuestState)gameMgr.playData.quest[(int)Quest.Event_0_Quest] == QuestState.Run)
        {
            guestSystem.ui.timer.SetEventMode(true);
            dontClick.gameObject.SetActive(true);
            //이벤트0 튜토리얼
            event0_UI.gameObject.SetActive(true);
            soundMgr.PlayBGM(Sound.Event_0_BG);
            event0_Tuto.PlayTuto();
        }
        else
        {
            if (guestSystem != null)
                guestSystem.Init();
            guestSystem.ui.timer.RunTimer();
            guestSystem.ui.timer.SetEventMode(false);

            if (kitchenMgr != null)
                kitchenMgr.RunWorker(true);

            soundMgr.PlayBGM(Sound.InGame_BG);
            dayStartAni.gameObject.SetActive(true);
        }
    }

    public void Event0Start()
    {
        GuestSystem guestSystem = GuestSystem.Instance;
        if (guestSystem != null)
            guestSystem.Init();

        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if (kitchenMgr != null)
            kitchenMgr.RunWorker(true);

        guestSystem.ui.timer.gameObject.SetActive(true);
        guestSystem.ui.nowMoney.gameObject.SetActive(true);
        guestSystem.ui.pauseBtn.gameObject.SetActive(true);

        event0_UI.SetUI();
        dontClick.gameObject.SetActive(false);
        dayStartAni.gameObject.SetActive(true);
    }
}
