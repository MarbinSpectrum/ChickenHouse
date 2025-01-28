using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuReady : Mgr
{
    [SerializeField] private SpicyReady spicyReady;
    [SerializeField] private DrinkReady drinkReady;
    [SerializeField] private SideMenuReady sideMenuReady;

    public void SetUI()
    {
        spicyReady.SelectSpicyCancelBtn();
        drinkReady.SelectDrinkCancelBtn();
        sideMenuReady.SelectSideMenuCancelBtn();

        spicyReady.SetUI();
        drinkReady.SetUI();
        sideMenuReady.SetUI();
    }

    public void AllCancel()
    {
        spicyReady.SelectSpicyCancelBtn();
        drinkReady.SelectDrinkCancelBtn();
        sideMenuReady.SelectSideMenuCancelBtn();
    }
}
