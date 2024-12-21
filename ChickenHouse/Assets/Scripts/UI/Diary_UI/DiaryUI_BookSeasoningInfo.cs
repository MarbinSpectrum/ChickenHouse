using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DiaryUI_BookSeasoningInfo : Mgr
{
    [SerializeField] private TextMeshProUGUI    itemName;
    [SerializeField] private TextMeshProUGUI    itemExplain;
    [SerializeField] private TextMeshProUGUI    itemMoney;
    [SerializeField] private RectTransform      rect;
    [SerializeField] private SeasoningFace      seasoningFace;

    public void SetUI(ChickenSpicy pChickenSpicy)
    {
        bool isAct = BookMgr.IsActSpicy(pChickenSpicy);
        rect.gameObject.SetActive(isAct);
        seasoningFace.SetUI(pChickenSpicy);
        SpicyData spicyData = spicyMgr.GetSpicyData(pChickenSpicy);
        if (spicyData == null)
            return;
        LanguageMgr.SetString(itemName, spicyData.nameKey);
        LanguageMgr.SetString(itemExplain, spicyData.bookInfoKey);
        string moneyStr = LanguageMgr.GetMoneyStr(itemMoney.fontSize, spicyData.price);
        itemMoney.text = moneyStr;
    }

}
