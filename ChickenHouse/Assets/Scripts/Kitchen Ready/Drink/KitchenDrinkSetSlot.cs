using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KitchenDrinkSetSlot : Mgr
{
    [SerializeField] private RectTransform  drinkRect;
    [SerializeField] private Image          drinkFace;
    private NoParaDel selectFun;

    public void SetUI(Drink pDrink, NoParaDel pSelect)
    {
        selectFun = pSelect;

        bool isAct = (pDrink != Drink.None);
        drinkRect.gameObject.SetActive(isAct);

        DrinkData drinkData = subMenuMgr.GetDrinkData(pDrink);
        if (drinkData == null)
            return;

        drinkFace.sprite = drinkData.img;
    }

    public void Select() => selectFun?.Invoke();
}
