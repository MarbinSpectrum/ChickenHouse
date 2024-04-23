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
            guestMgr.Init();
        }

        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if (kitchenMgr != null)
        {
            kitchenMgr.Init();
        }

        soundMgr.PlayBGM(Sound.InGame_BG);
    }
}
