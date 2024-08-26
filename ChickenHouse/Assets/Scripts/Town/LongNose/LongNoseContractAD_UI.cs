using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LongNoseContractAD_UI : Mgr
{
    [SerializeField] private LongNoseContractSlot   shopMenuSlot;
    [SerializeField] private RectTransform          slotContents;
    [SerializeField] private LongNoseContractCheck  contractCheck;
    [SerializeField] private TextMeshProUGUI        playerMoney;

    private List<ShopItem> itemList = new List<ShopItem>();
    private List<LongNoseContractSlot> contractMenu = new List<LongNoseContractSlot>();

    private const string MONEY_FORMAT = "{0:N0}<size=15>$</size>";

    public void SetUI()
    {
        SetMenu();
    }

    private void SetMenu()
    {
        PlayData playData = gameMgr.playData;
        if (playData == null)
            return;
        string moneyStr = string.Format(MONEY_FORMAT, playData.money);
        LanguageMgr.SetText(playerMoney, moneyStr);

        slotContents.anchoredPosition = Vector2.zero;

        void AddItemList(ShopItem pItem)
        {
            PlayData playData = gameMgr.playData;
            if (playData.hasItem[(int)pItem])
                return;
            itemList.Add(pItem);
        }

        itemList.Clear();
        AddItemList(ShopItem.Advertisement_1);
        AddItemList(ShopItem.Advertisement_2);
        AddItemList(ShopItem.Advertisement_3);
        AddItemList(ShopItem.Advertisement_4);
        AddItemList(ShopItem.Advertisement_5);

        contractMenu.ForEach((x) => x.gameObject.SetActive(false));
        for (int i = 0; i < itemList.Count; i++)
        {
            if (i >= contractMenu.Count)
            {
                LongNoseContractSlot slotMenu = Instantiate(shopMenuSlot, slotContents);
                contractMenu.Add(slotMenu);
            }

            contractMenu[i].SetData(itemList[i], (item) => ItemBuyCheckUI((ShopItem)item));
            contractMenu[i].gameObject.SetActive(true);
        }
    }

    private void ItemBuyCheckUI(ShopItem pItem)
    {
        contractCheck.SetUI(() =>
        {
            PlayData playData = gameMgr.playData;
            playData.hasItem[(int)pItem] = true;
            gameMgr.SaveData();

            SetMenu();
        });
    }
}
