using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade_UI : Mgr
{
    [SerializeField] private UpgradeSlot_UI upgradeSlot;
    [SerializeField] private LoopScrollInit scrollInit;
    [SerializeField] private RectTransform  body;
    [SerializeField] private GameObject     dontTouch;
    [SerializeField] private Money_UI       moneyUI;

    public List<Upgrade> upgradeList { private set; get; } = new List<Upgrade>();
    private bool selectMenu = false;

    private void Start()
    {
        gameMgr.LoadData();

        SetMoney();
        SetUpgrade();
    }

    private void SetUpgrade()
    {
        bool HasUpgrade(Upgrade upgrade)
        {
            if (gameMgr.playData.upgradeState[(int)upgrade])
            {
                //해당 업그레이드가 완료되어있음
                return true;
            }

            //업그레이드가 완료 안되어있음
            return false;
        }

        upgradeList.Clear();

        upgradeList.Add(Upgrade.OIL_Zone_1);
        upgradeList.Add(Upgrade.Recipe_1);
        upgradeList.Add(Upgrade.Advertisement_1);

        scrollInit.Init(upgradeList.Count);
    }

    public void SetMoney()
    {
        moneyUI.SetMoney(gameMgr.playData.money);
    }

    public void SelectUpgrade(Upgrade pUpgrade)
    {
        if (selectMenu)
            return;
        dontTouch.gameObject.SetActive(true);
        selectMenu = true;
        gameMgr.playData.upgradeState[(int)pUpgrade] = true;
        gameMgr.playData.day += 1;
        gameMgr.SaveData();
        sceneMgr.SceneLoad(Scene.INGAME, SceneChangeAni.CIRCLE);
    }
}
