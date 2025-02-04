using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KitchenDrinkSetSlot : Mgr
{
    [SerializeField] private RectTransform          drinkRect;
    [SerializeField] private Image                  drinkFace;
    [SerializeField] private Image                  enterEffect;
    [SerializeField] private KitchenDrinkDrag       dragObj;
    private NoParaDel selectFun;
    private Drink drink;
    public void SetUI(Drink pDrink, NoParaDel pSelect)
    {
        selectFun = pSelect;
        drink = pDrink;

        bool isAct = (pDrink != Drink.None);
        drinkRect.gameObject.SetActive(isAct);

        DrinkData drinkData = subMenuMgr.GetDrinkData(pDrink);
        if (drinkData == null)
            return;

        drinkFace.sprite = drinkData.img;
        enterEffect.gameObject.SetActive(false);
    }

    public void Select() => selectFun?.Invoke();

    public void EnterEvent()
    {
        if (drink != Drink.None)
            return;
        if (dragObj.drink == Drink.None)
            return;

        if (gameMgr.playData != null)
        {
            for (MenuSetPos setCheck = MenuSetPos.Drink0;
              setCheck < MenuSetPos.DrinkMAX; setCheck++)
            {
                Drink drink = (Drink)gameMgr.playData.drink[(int)setCheck];
                if (dragObj.drink == drink)
                {
                    return;
                }
            }
        }

        enterEffect.gameObject.SetActive(true);
    }

    public void ExitEvent()
    {
        enterEffect.gameObject.SetActive(false);
    }

    public void AddEvent()
    {
        if (dragObj.drink == Drink.None)
            return;
        selectFun?.Invoke();
    }
}
