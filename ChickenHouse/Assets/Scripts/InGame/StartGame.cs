using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class StartGame : Mgr
{
    [SerializeField] private List<Oil_Zone> oilMachines = new List<Oil_Zone>();
    [SerializeField] private List<GameObject> chickenPackslots = new List<GameObject>();

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
        foreach(Oil_Zone oilZone in oilMachines)
        {
            oilZone.Init();
        }
        if (gameMgr.playData.hasItem[(int)ShopItem.NEW_OIL_ZONE_1])
        {
            chickenPackslots[1].gameObject.SetActive(true);
            oilMachines[1].gameObject.SetActive(true);
        }
        else
        {
            oilMachines[1].gameObject.SetActive(false);
            chickenPackslots[1].gameObject.SetActive(false);
        }

        if (gameMgr.playData.hasItem[(int)ShopItem.NEW_OIL_ZONE_2])
        {
            chickenPackslots[2].gameObject.SetActive(true);
            oilMachines[2].gameObject.SetActive(true);
        }
        else
        {
            oilMachines[2].gameObject.SetActive(false);
            chickenPackslots[2].gameObject.SetActive(false);
        }

        if (gameMgr.playData.hasItem[(int)ShopItem.NEW_OIL_ZONE_3])
        {
            chickenPackslots[3].gameObject.SetActive(true);
            oilMachines[3].gameObject.SetActive(true);
        }
        else
        {
            oilMachines[3].gameObject.SetActive(false);
            chickenPackslots[3].gameObject.SetActive(false);
        }

        soundMgr.PlayBGM(Sound.InGame_BG);
    }
}
