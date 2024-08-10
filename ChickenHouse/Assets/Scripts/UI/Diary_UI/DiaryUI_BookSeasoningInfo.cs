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
        switch(pChickenSpicy)
        {
            case ChickenSpicy.Hot:
                LanguageMgr.SetString(itemName, "HOT_SPICY");
                LanguageMgr.SetString(itemExplain, "BOOK_HOT_SPICY");
                break;
            case ChickenSpicy.Soy:
                LanguageMgr.SetString(itemName, "SOY_SPICY");
                LanguageMgr.SetString(itemExplain, "BOOK_SOY_SPICY");
                break;
            case ChickenSpicy.Hell:
                LanguageMgr.SetString(itemName, "HELL_SPICY");
                LanguageMgr.SetString(itemExplain, "BOOK_HELL_SPICY");
                break;
            case ChickenSpicy.Prinkle:
                LanguageMgr.SetString(itemName, "PRINKLE_SPICY");
                LanguageMgr.SetString(itemExplain, "BOOK_PRINKLE_SPICY");
                break;
            case ChickenSpicy.BBQ:
                LanguageMgr.SetString(itemName, "BBQ_SPICY");
                LanguageMgr.SetString(itemExplain, "BOOK_BBQ_SPICY");
                break;
            case ChickenSpicy.Carbonara:
                LanguageMgr.SetString(itemName, "CARBONARA_SPICY");
                LanguageMgr.SetString(itemExplain, "BOOK_CARBONARA_SPICY");
                break;
        }

    }

}
