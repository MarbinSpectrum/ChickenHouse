using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class KitchenDrinkSlot : Mgr
{
    [SerializeField] private TextMeshProUGUI    drinkName;
    [SerializeField] private Image              drinkFace;

    [SerializeField] private RectTransform selectRect;
    [SerializeField] private RectTransform partyRect;

    private NoParaDel selectDrinkFun;

    public void SetUI(Drink pDrink, bool pSelect, bool pIsParty, NoParaDel pSelectFun)
    {
        DrinkData drinkData = subMenuMgr.GetDrinkData(pDrink);
        if (drinkData == null)
        {
            gameObject.SetActive(false);
            return;
        }

        selectDrinkFun = pSelectFun;

        gameObject.SetActive(true);
        partyRect.gameObject.SetActive(pIsParty);
        selectRect.gameObject.SetActive(pSelect);
        drinkFace.sprite = drinkData.img;
        LanguageMgr.SetString(drinkName, drinkData.nameKey);
    }

    public void SelectDrinkMenu() => selectDrinkFun?.Invoke();
}
