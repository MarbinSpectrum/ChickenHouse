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

    [SerializeField] LongNose longNose;
    private List<LongNoseContractSlot> contractMenu = new List<LongNoseContractSlot>();

    public void SetUI()
    {
        SetMenu();
    }

    private void SetMenu()
    {

        PlayData playData = gameMgr.playData;
        if (playData == null)
            return;
        longNose.UpdateList();

        string moneyStr = LanguageMgr.GetMoneyStr(playerMoney.fontSize, playData.money);
        LanguageMgr.SetText(playerMoney, moneyStr,true);

        slotContents.anchoredPosition = Vector2.zero;

        contractMenu.ForEach((x) => x.gameObject.SetActive(false));
        for (int i = 0; i < longNose.itemList.Count; i++)
        {
            if (i >= contractMenu.Count)
            {
                LongNoseContractSlot slotMenu = Instantiate(shopMenuSlot, slotContents);
                contractMenu.Add(slotMenu);
            }

            contractMenu[i].SetData(longNose.itemList[i], (item) => ItemBuyCheckUI((ShopItem)item));
            contractMenu[i].gameObject.SetActive(true);
        }
    }

    private void ItemBuyCheckUI(ShopItem pItem)
    {
        contractCheck.SetUI(() =>
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
