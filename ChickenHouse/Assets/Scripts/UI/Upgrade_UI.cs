using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade_UI : Mgr
{
    [SerializeField] private LoopScrollInit scrollInit;
    [SerializeField] private RectTransform  body;
    [SerializeField] private GameObject     dontTouch;
    [SerializeField] private Money_UI       moneyUI;
    [SerializeField] private Button         gotoNextDay;
    public List<Upgrade> upgradeList { private set; get; } = new List<Upgrade>();
    private bool selectMenu = false;

    private void Awake()
    {
        gotoNextDay.onClick.RemoveAllListeners();
        gotoNextDay.onClick.AddListener(() =>
        {
            GotoNextDay();
        });
    }

    private void Start()
    {
        gameMgr.LoadData();

        SetMoney();
        SetUpgrade();
    }

    private void SetUpgrade()
    {
        upgradeList.Clear();

        upgradeList.Add(Upgrade.OIL_Zone_1);
        upgradeList.Add(Upgrade.Recipe_1);
        upgradeList.Add(Upgrade.Advertisement_1);
        upgradeList.Add(Upgrade.Worker_1);

        scrollInit.Init(upgradeList.Count);
    }

    public void SetMoney()
    {
        moneyUI.SetMoney(gameMgr.playData.money);
    }

    public void GotoNextDay()
    {
        if (selectMenu)
            return;
        dontTouch.gameObject.SetActive(true);
        selectMenu = true;
        gameMgr.playData.day += 1;
        gameMgr.SaveData();
        sceneMgr.SceneLoad(Scene.INGAME, SceneChangeAni.CIRCLE);
    }
}
