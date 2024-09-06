using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class StartGame : Mgr
{
    [SerializeField] private RectTransform dayStartAni;

    public void Run()
    {
        GuestSystem guestSystem = GuestSystem.Instance;
        if(guestSystem != null)
        {
            guestSystem.Init();
        }

        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if (kitchenMgr != null)
        {
            kitchenMgr.Init();
        }
      
        soundMgr.PlayBGM(Sound.InGame_BG);
        dayStartAni.gameObject.SetActive(true);
    }
}
