using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : Mgr
{
    private void Start()
    {
        gameMgr.LoadData();

        GuestMgr guestMgr = GuestMgr.Instance;
        if(guestMgr != null)
        {
            guestMgr.ui.nowMoney.SetMoney(gameMgr.playData.money);
            guestMgr.StartGuestCycle();
            soundMgr.PlayBGM(Sound.InGame_BG);
        }
    }
}
