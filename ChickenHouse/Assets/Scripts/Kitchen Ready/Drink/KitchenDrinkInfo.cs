using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KitchenDrinkInfo : Mgr
{
    [SerializeField] private Image              drinkFace;
    [SerializeField] private TextMeshProUGUI    drinkName;
    [SerializeField] private TextMeshProUGUI    drinkInfo;

    public void SetUI(Drink pDrink)
    {
        DrinkData drinkData = subMenuMgr.GetDrinkData(pDrink);
        if (drinkData == null)
        {
            gameObject.SetActive(false);
            return;
        }

        if (pDrink == Drink.None)
        {
            drinkFace.gameObject.SetActive(false);
            drinkName.gameObject.SetActive(false);
            drinkInfo.gameObject.SetActive(false);
            return;
        }
        gameObject.SetActive(true);
        drinkFace.sprite = drinkData.img;
        LanguageMgr.SetString(drinkName, drinkData.nameKey);
        LanguageMgr.SetString(drinkInfo, drinkData.infoKey);
        drinkFace.gameObject.SetActive(true);
        drinkName.gameObject.SetActive(true);
        drinkInfo.gameObject.SetActive(true);
    }
}
