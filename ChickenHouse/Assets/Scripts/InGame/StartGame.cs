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
            guestMgr.StartGuestCycle();
            soundMgr.PlayBGM(Sound.InGame_BG);
        }
    }
}
