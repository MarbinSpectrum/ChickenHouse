using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class LongNoseContractSlot : Mgr
{
    private ADItem ADItem;

    [SerializeField] private TextMeshProUGUI    itemName;
    [SerializeField] private TextMeshProUGUI    itemInfo;
    [SerializeField] private Image              itemIcon;
    [SerializeField] private TextMeshProUGUI    itemCost;

    //아이템 구입 확인
    private OneParaDel fun;

    public void SetData(ADItem pADItem, OneParaDel pFun = null)
    {
        ADItem = pADItem;
        fun = pFun;

        ADData adData = adItemMgr.GetADData(ADItem);
        if (adData == null)
            return;
        PlayData playData = gameMgr.playData;
        if (playData == null)
            return;

        LanguageMgr.SetString(itemName, adData.nameKey);
        LanguageMgr.SetString(itemInfo, adData.infoKey);
        itemIcon.sprite = adData.img;

        int newMoney = (int)(adData.price * (100f - gameMgr.playData.ShopSaleValue()) / 100f);
        string moneyStr = string.Format(LanguageMgr.COMMA_FORMAT, newMoney);
        LanguageMgr.SetText(itemCost, moneyStr);
    }

    public void BuyItem()
    {
        //인스펙터로 끌어서 사용하는 함수
        PlayData playData = gameMgr.playData;
        if (playData == null)
            return;
        ADData adData = adItemMgr.GetADData(ADItem);
        if (adData == null)
            return;

        int newMoney = (int)(adData.price * (100f - gameMgr.playData.ShopSaleValue()) / 100f);
        if (playData.money < newMoney)
        {
            //돈이 부족하다.
            return;
        }

        fun?.Invoke(ADItem);
    }
}
