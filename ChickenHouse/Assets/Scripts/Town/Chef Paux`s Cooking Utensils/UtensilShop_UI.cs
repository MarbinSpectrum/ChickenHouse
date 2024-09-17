using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UtensilShop_UI : Mgr
{
    [SerializeField] private UtensilShopMenuSlot    shopMenuSlot;
    [SerializeField] private RectTransform          slotContents;
    [SerializeField] private UtensilPurchaseCheck   purchaseCheck;
    [SerializeField] private Tab                    tabInfo;
    [SerializeField] private TextMeshProUGUI        playerMoney;

    private struct Tab
    {
        public Image[]              tabImg;
        public TextMeshProUGUI[]    tabText;
        public Color                selectColor;
        public Color                deSelectColor;
        public Sprite               tabSelect;
        public Sprite               tabDeSelect;
    }

    private UtensilShopMenu nowMenu;
    private List<ShopItem>  itemList = new List<ShopItem>();
    private List<UtensilShopMenuSlot> shopMenus = new List<UtensilShopMenuSlot>();

    private const string MONEY_FORMAT = "{0:N0}<size=15>$</size>";

    public void SetUI()
    {
        nowMenu = UtensilShopMenu.Fryer_Buy;

        SelectMenu(nowMenu);
    }

    public void SelectMenu(int menuNum)
    {
        //인스펙터로 끌어서 사용하는 함수
        SelectMenu((UtensilShopMenu)menuNum);
        soundMgr.PlaySE(Sound.Btn_SE);
    }

    private void SelectMenu(UtensilShopMenu pMenu)
    {
        PlayData playData = gameMgr.playData;
        if (playData == null)
            return;
        string moneyStr = string.Format(MONEY_FORMAT, playData.money);
        LanguageMgr.SetText(playerMoney, moneyStr);
        nowMenu = pMenu;
        slotContents.anchoredPosition = Vector2.zero;
        for (int i = 0; i < tabInfo.tabImg.Length; i++)
        {
            if (i == (int)pMenu)
            {
                tabInfo.tabImg[i].sprite = tabInfo.tabSelect;
                tabInfo.tabText[i].color = tabInfo.selectColor;
            }
            else
            {
                tabInfo.tabImg[i].sprite = tabInfo.tabDeSelect;
                tabInfo.tabText[i].color = tabInfo.deSelectColor;
            }
        }

        void AddItemList(ShopItem pItem)
        {
            PlayData playData = gameMgr.playData;
            if (playData.hasItem[(int)pItem])
                return;
            itemList.Add(pItem);
        }

        itemList.Clear();
        switch (pMenu)
        {
            case UtensilShopMenu.Fryer_Buy:
                {
                    AddItemList(ShopItem.OIL_Zone_1);
                    AddItemList(ShopItem.OIL_Zone_2);
                    AddItemList(ShopItem.OIL_Zone_3);
                    AddItemList(ShopItem.OIL_Zone_4);
                }
                break;
            case UtensilShopMenu.Fryer_Add:
                {
                    AddItemList(ShopItem.NEW_OIL_ZONE_1);
                    AddItemList(ShopItem.NEW_OIL_ZONE_2);
                    AddItemList(ShopItem.NEW_OIL_ZONE_3);
                }
                break;  
        }

        shopMenus.ForEach((x) => x.gameObject.SetActive(false));
        for (int i = 0; i < itemList.Count; i++)
        {
            if(i >= shopMenus.Count)
            {
                UtensilShopMenuSlot slotMenu = Instantiate(shopMenuSlot, slotContents);
                shopMenus.Add(slotMenu);
            }

            shopMenus[i].SetData(itemList[i], (item) => ItemBuyCheckUI((ShopItem)item));
            shopMenus[i].gameObject.SetActive(true);
        }
    }

    private void ItemBuyCheckUI(ShopItem pItem)
    {
        purchaseCheck.SetUI(()=>
        {
            soundMgr.PlaySE(Sound.GetMoney_SE);

            PlayData playData = gameMgr.playData;
            playData.hasItem[(int)pItem] = true;

            ShopData shopData = shopMgr.GetShopData(pItem);
            int newMoney = (int)(shopData.money * (100f - gameMgr.playData.ShopSaleValue()) / 100f);
            playData.money -= newMoney;

            SelectMenu(nowMenu);
        });
    }
}
