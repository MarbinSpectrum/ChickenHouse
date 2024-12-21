using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class LongNoseContractSlot : Mgr
{
    private ShopItem shopItem;

    [SerializeField] private TextMeshProUGUI    itemName;
    [SerializeField] private TextMeshProUGUI    itemInfo;
    [SerializeField] private Image              itemIcon;
    [SerializeField] private TextMeshProUGUI    itemCost;
    private struct TextColor
    {
        public Color badColor;
        public Color goodColor;
    }
    [SerializeField] private TextColor costColor;

    //������ ���� Ȯ��
    private OneParaDel fun;

    public void SetData(ShopItem pShopItem, OneParaDel pFun = null)
    {
        shopItem = pShopItem;
        fun = pFun;

        ShopData shopData = shopMgr.GetShopData(shopItem);
        if (shopData == null)
            return;
        PlayData playData = gameMgr.playData;
        if (playData == null)
            return;

        LanguageMgr.SetString(itemName, shopData.nameKey);
        LanguageMgr.SetString(itemInfo, shopData.infoKey);
        itemIcon.sprite = shopData.icon;

        int newMoney = (int)(shopData.money * (100f - gameMgr.playData.ShopSaleValue()) / 100f);
        string moneyStr = LanguageMgr.GetMoneyStr(itemCost.fontSize, newMoney);
        LanguageMgr.SetText(itemCost, moneyStr);
        if (playData.money >= shopData.money)
            itemCost.color = costColor.goodColor;
        else
            itemCost.color = costColor.badColor;
    }

    public void BuyItem()
    {
        //�ν����ͷ� ��� ����ϴ� �Լ�
        PlayData playData = gameMgr.playData;
        if (playData == null)
            return;
        ShopData shopData = shopMgr.GetShopData(shopItem);
        if (shopData == null)
            return;

        int newMoney = (int)(shopData.money * (100f - gameMgr.playData.ShopSaleValue()) / 100f);
        if (playData.money < newMoney)
        {
            //���� �����ϴ�.
            return;
        }

        fun?.Invoke(shopItem);
    }
}
