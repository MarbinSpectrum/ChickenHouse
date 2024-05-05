using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop_UI : Mgr
{
    [SerializeField] private LoopScrollInit scrollInit;
    [SerializeField] private RectTransform  body;
    [SerializeField] private List<Animator> shopMenu;
    [SerializeField] private List<Button>   shopMenuBtn;
    [SerializeField] private GameObject     dontTouch;
    [SerializeField] private Money_UI       moneyUI;
    [SerializeField] private Button         gotoNextDay;
    [SerializeField] private Resume_UI      resumeUI;
    [SerializeField] private GetNewSpicy    newSpicy;

    public enum ShopMenu
    {
        Spicy       ,
        Worker      ,
        Advertise   ,
        kichenTool  ,
        MAX         ,
    }
    private ShopMenu nowMenu;

    public List<ShopItem> shopItemList { private set; get; } = new List<ShopItem>();
    private bool selectMenu = false;

    private void Awake()
    {
        gotoNextDay.onClick.RemoveAllListeners();
        gotoNextDay.onClick.AddListener(() =>
        {
            GotoNextDay();
        });

        for (ShopMenu menu = ShopMenu.Spicy; menu < ShopMenu.MAX; menu++)
        {
            int idx = (int)menu;
            shopMenuBtn[idx].onClick.RemoveAllListeners();
            shopMenuBtn[idx].onClick.AddListener(() =>
            {
                SetUpgrade((ShopMenu)idx);
            });
        }

        nowMenu = ShopMenu.Worker;
    }

    private void Start()
    {
        gameMgr.LoadData();

        soundMgr.PlayBGM(Sound.Shop_BG);

        SetMoney();
        SetUpgrade();
        newSpicy.SetSpicy();
    }

    private void SetUpgrade(ShopMenu pShopMenu)
    {
        nowMenu = pShopMenu;
        SetUpgrade();
    }

    private void SetUpgrade()
    {
        shopItemList.Clear();
        for (ShopMenu menu = ShopMenu.Spicy; menu < ShopMenu.MAX; menu++)
        {
            int idx = (int)menu;
            if(nowMenu == menu)
                shopMenu[idx].SetBool("Select", true);
            else
                shopMenu[idx].SetBool("Select", false);
        }

        switch(nowMenu)
        {
            case ShopMenu.Spicy:
                {
                    shopItemList.Add(ShopItem.Recipe_1);
                    shopItemList.Add(ShopItem.Recipe_2);
                    shopItemList.Add(ShopItem.Recipe_3);
                    shopItemList.Add(ShopItem.Recipe_4);
                    shopItemList.Add(ShopItem.Recipe_5);
                }
                break;
            case ShopMenu.kichenTool:
                {
                    shopItemList.Add(ShopItem.OIL_Zone_1);
                    shopItemList.Add(ShopItem.OIL_Zone_2);
                    shopItemList.Add(ShopItem.OIL_Zone_3);
                    shopItemList.Add(ShopItem.OIL_Zone_4);
                }
                break;
            case ShopMenu.Advertise:
                {
                    shopItemList.Add(ShopItem.Advertisement_1);
                    shopItemList.Add(ShopItem.Advertisement_2);
                    shopItemList.Add(ShopItem.Advertisement_3);
                    shopItemList.Add(ShopItem.Advertisement_4);
                    shopItemList.Add(ShopItem.Advertisement_5);
                }
                break;
            case ShopMenu.Worker:
                {
                    shopItemList.Add(ShopItem.Worker_1);
                    shopItemList.Add(ShopItem.Worker_2);
                    shopItemList.Add(ShopItem.Worker_3);
                    shopItemList.Add(ShopItem.Worker_4);
                    shopItemList.Add(ShopItem.Worker_5);
                    shopItemList.Add(ShopItem.Worker_6);
                }
                break;
        }

        scrollInit.Init(shopItemList.Count);
    }

    public void SetResume(ShopItem pShopItem)
    {
        ResumeData resumeData =shopMgr.GetResumeData(pShopItem);
        if (resumeData == null)
            return;
        resumeUI.SetData(resumeData);
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
