using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KitchenSpicyInfo : Mgr
{
    [SerializeField] private SeasoningFace      spicyFace;
    [SerializeField] private TextMeshProUGUI    spicyName;
    [SerializeField] private TextMeshProUGUI    spicyInfo;

    public void SetUI(ChickenSpicy pChickenSpicy)
    {
        SpicyData spicyData = spicyMgr.GetSpicyData(pChickenSpicy);
        if (spicyData == null)
        {
            gameObject.SetActive(false);
            return;
        }

        if(pChickenSpicy == ChickenSpicy.None)
        {
            spicyFace.gameObject.SetActive(false);
            spicyName.gameObject.SetActive(false);
            spicyInfo.gameObject.SetActive(false);
            return;
        }
        gameObject.SetActive(true);
        spicyFace.SetUI(pChickenSpicy);
        LanguageMgr.SetString(spicyName, spicyData.nameKey);
        LanguageMgr.SetString(spicyInfo, spicyData.infoKey);
        spicyFace.gameObject.SetActive(true);
        spicyName.gameObject.SetActive(true);
        spicyInfo.gameObject.SetActive(true);
    }
}

