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
    [SerializeField] private Money_UI               playerMoney;
    [SerializeField]private ChefPauxsCookingUtensils chefPauxsCookingUtensils;

    private struct Tab
    {
        public Image[]              tabImg;
        public TextMeshProUGUI[]    tabText;
        public Color                selectColor;
        public Color                deSelectColor;
        public Sprite               tabSelect;
        public Sprite               tabDeSelect;
    }

    private UtensilShopTab nowTab;
    private List<UtensilShopMenuSlot> shopMenus = new List<UtensilShopMenuSlot>();

    public void SetUI()
    {
        nowTab = UtensilShopTab.Fryer_Buy;

        SelectMenu(nowTab);
    }

    public void SelectMenu(int menuNum)
    {
        //인스펙터로 끌어서 사용하는 함수
        SelectMenu((UtensilShopTab)menuNum);
        soundMgr.PlaySE(Sound.Btn_SE);
    }

    private void SelectMenu(UtensilShopTab pMenu)
    {
        PlayData playData = gameMgr.playData;
        if (playData == null)
            return;

        playerMoney.SetMoney(playData.money);

        nowTab = pMenu;
        slotContents.anchoredPosition = Vector2.zero;
        for (int i = 0; i < tabInfo.tabImg.Length; i++)
        {
            if (i == (int)pMenu)
            {
                tabInfo.tabImg[i].sprite = tabInfo.tabSelect;
                tabInfo.tabText[i].color = tabInfo.selectColor;
                tabInfo.tabImg[i].transform.SetAsLastSibling();
            }
            else
            {
                tabInfo.tabImg[i].sprite = tabInfo.tabDeSelect;
                tabInfo.tabText[i].color = tabInfo.deSelectColor;
            }
        }

        chefPauxsCookingUtensils.UpdateList(pMenu);

        shopMenus.ForEach((x) => x.gameObject.SetActive(false));
        for (int i = 0; i < chefPauxsCookingUtensils.itemList.Count; i++)
        {
            if(i >= shopMenus.Count)
            {
                UtensilShopMenuSlot slotMenu = Instantiate(shopMenuSlot, slotContents);
                shopMenus.Add(slotMenu);
            }

            shopMenus[i].SetData(chefPauxsCookingUtensils.itemList[i], (item) => ItemBuyCheckUI((ShopItem)item));
            shopMenus[i].gameObject.SetActive(true);
        }
    }

    private void ItemBuyCheckUI(ShopItem pItem)
    {
        purchaseCheck.SetUI(()=>
        {
            soundMgr.PlaySE(Sound.GetMoney_SE);

            gameMgr.playData.AddShopItem(pItem);

            ShopData shopData = shopMgr.GetShopData(pItem);
            int newMoney = (int)(shopData.money * (100f - gameMgr.playData.ShopSaleValue()) / 100f);
            gameMgr.playData.money -= newMoney;

            SelectMenu(nowTab);
        });
    }
}
