using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DiaryUI_BookSeasoningInfo : Mgr
{
    [SerializeField] private TextMeshProUGUI    itemName;
    [SerializeField] private TextMeshProUGUI    itemExplain;
    [SerializeField] private RectTransform      rect;
    [SerializeField] private SeasoningFace      seasoningFace;

    public void SetUI(ChickenSpicy pChickenSpicy)
    {
        bool isAct = bookMgr.IsActSpicy(pChickenSpicy);
        rect.gameObject.SetActive(isAct);
        seasoningFace.SetUI(pChickenSpicy);
        SpicyData spicyData = spicyMgr.GetSpicyData(pChickenSpicy);
        if (spicyData == null)
            return;
        LanguageMgr.SetString(itemName, spicyData.nameKey);
        LanguageMgr.SetString(itemExplain, spicyData.bookInfoKey);
    }

}
