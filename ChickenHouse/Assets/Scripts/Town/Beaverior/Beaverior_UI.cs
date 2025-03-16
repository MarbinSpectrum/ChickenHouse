using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Beaverior_UI : Mgr
{
    [SerializeField] private BeaveriorSlot          shopMenuSlot;
    [SerializeField] private RectTransform          slotContents;
    [SerializeField] private BeaveriorPurchaseCheck purchaseCheck;
    [SerializeField] private Tab                    tabInfo;
    [SerializeField] private Money_UI               playerMoney;
    [SerializeField] Beaverior beaverior;

    private struct Tab
    {
        public Image[] tabImg;
        public TextMeshProUGUI[] tabText;
        public Color selectColor;
        public Color deSelectColor;
        public Sprite tabSelect;
        public Sprite tabDeSelect;
    }

    private List<BeaveriorSlot> contractMenu = new List<BeaveriorSlot>();

    public void SetUI()
    {
        SetMenu();
    }

    private void SetMenu()
    {

        PlayData playData = gameMgr.playData;
        if (playData == null)
            return;
        beaverior.UpdateList();

        playerMoney.SetMoney(playData.money);

        slotContents.anchoredPosition = Vector2.zero;
        for (int i = 0; i < tabInfo.tabImg.Length; i++)
        {
            if (i == (int)0)
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

        contractMenu.ForEach((x) => x.gameObject.SetActive(false));
        for (int i = 0; i < beaverior.itemList.Count; i++)
        {
            if (i >= contractMenu.Count)
            {
                BeaveriorSlot slotMenu = Instantiate(shopMenuSlot, slotContents);
                contractMenu.Add(slotMenu);
            }

            contractMenu[i].SetData(beaverior.itemList[i], (item) => ItemBuyCheckUI((ShopItem)item));
            contractMenu[i].gameObject.SetActive(true);
        }
    }

    private void ItemBuyCheckUI(ShopItem pItem)
    {
        purchaseCheck.SetUI(() =>
        {
            soundMgr.PlaySE(Sound.GetMoney_SE);

            PlayData playData = gameMgr.playData;
            playData.hasItem[(int)pItem] = true;

            ShopData shopData = shopMgr.GetShopData(pItem);
            int newMoney = (int)(shopData.money * (100f - gameMgr.playData.ShopSaleValue()) / 100f);
            playData.money -= newMoney;

            SetMenu();
        });
    }
}
