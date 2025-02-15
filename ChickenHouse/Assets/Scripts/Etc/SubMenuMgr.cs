using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubMenuMgr : AwakeSingleton<SubMenuMgr>
{
    private Dictionary<Drink, DrinkData>       drinkData       = new();
    private Dictionary<SideMenu, SideMenuData> sideMenuData    = new();

    private static bool init = false;

    protected override void Awake()
    {
        base.Awake();

        if (init)
            return;

        init = true;
        for (Drink drink = Drink.Cola; drink < Drink.MAX; drink++)
        {
            DrinkData dData = Resources.Load<DrinkData>($"DrinkData/{drink.ToString()}");
            if (dData == null)
                continue;
            drinkData.Add(drink, dData);
        }

        for (SideMenu sidemenu = SideMenu.ChickenRadish; sidemenu < SideMenu.MAX; sidemenu++)
        {
            SideMenuData sData = Resources.Load<SideMenuData>($"SideMenuData/{sidemenu.ToString()}");
            if (sData == null)
                continue;
            sideMenuData.Add(sidemenu, sData);
        }
    }

    public DrinkData GetDrinkData(Drink pDrink)
    {
        //음료 정보 얻기
        if (drinkData.ContainsKey(pDrink))
            return drinkData[pDrink];
        return null;
    }

    public int GetDrinkPrice(Drink pDrink)
    {
        //음료 가격
        DrinkData drinkData = GetDrinkData(pDrink);
        if (drinkData == null)
            return 0;
        return drinkData.price;
    }

    public DragState GetDrinkToDragState(Drink pDrink)
    {
        switch (pDrink)
        {
            case Drink.Cola:
                return DragState.Cola;
            case Drink.Beer:
                return DragState.Beer;
            case Drink.SuperPower:
                return DragState.SuperPower;
            case Drink.LoveMelon:
                return DragState.LoveMelon;
            case Drink.SodaSoda:
                return DragState.SodaSoda;
        }
        return DragState.None;
    }

    public Drink GetDragStateToDrink(DragState pDragState)
    {
        switch (pDragState)
        {
            case DragState.Cola:
                return Drink.Cola;
            case DragState.Beer:
                return Drink.Beer;
            case DragState.SuperPower:
                return Drink.SuperPower;
            case DragState.LoveMelon:
                return Drink.LoveMelon;
            case DragState.SodaSoda:
                return Drink.SodaSoda;
        }
        return Drink.None;
    }

    public bool IsAlcohol(Drink pDrink)
    {
        //알콜 음료인가?
        switch(pDrink)
        {
            case Drink.Beer:
                return true;             
        }
        return false;
    }

    public static Drink GetShopItemToDrink(ShopItem shopItem)
    {
        //ShopItem->Drink
        switch (shopItem)
        {
            case ShopItem.Cola:
                return Drink.Cola;
            case ShopItem.Beer:
                return Drink.Beer;
            case ShopItem.SuperPower:
                return Drink.SuperPower;
            case ShopItem.LoveMelon:
                return Drink.LoveMelon;
            case ShopItem.SodaSoda:
                return Drink.SodaSoda;

        }
        return Drink.None;
    }

    public static ShopItem GetDrinkToShopItem(Drink pDrink)
    {
        //Drink->ShopItem
        switch (pDrink)
        {
            case Drink.Cola:
                return ShopItem.Cola;
            case Drink.Beer:
                return ShopItem.Beer;
            case Drink.SuperPower:
                return ShopItem.SuperPower;
            case Drink.LoveMelon:
                return ShopItem.LoveMelon;
            case Drink.SodaSoda:
                return ShopItem.SodaSoda;

        }
        return ShopItem.None;
    }

    public SideMenuData GetSideMenuData(SideMenu pSideMenu)
    {
        //사이드 메뉴 정보 얻기
        if (sideMenuData.ContainsKey(pSideMenu))
            return sideMenuData[pSideMenu];
        return null;
    }

    public int GetSideMenuPrice(SideMenu pSideMenu)
    {
        //사이드메뉴 가격
        SideMenuData sideMenuData = GetSideMenuData(pSideMenu);
        if (sideMenuData == null)
            return 0;
        return sideMenuData.price;
    }

    public static DragState GetSideMenuToDragState(SideMenu pSideMenu)
    {
        //SideMenu -> DragState
        switch (pSideMenu)
        {
            case SideMenu.ChickenRadish:
                return DragState.Chicken_Radish;
            case SideMenu.Pickle:
                return DragState.Pickle;
            case SideMenu.Coleslaw:
                return DragState.Coleslaw;
            case SideMenu.CornSalad:
                return DragState.CornSalad;
            case SideMenu.FrenchFries:
                return DragState.FrenchFries;
            case SideMenu.ChickenNugget:
                return DragState.ChickenNugget;

        }
        return DragState.None;
    }

    public static SideMenu GetDragStateToSideMenu(DragState pDragState)
    {
        //DragState -> SideMenu
        switch (pDragState)
        {
            case DragState.Chicken_Radish:
                return SideMenu.ChickenRadish;
            case DragState.Pickle:
                return SideMenu.Pickle;
            case DragState.Coleslaw:
                return SideMenu.Coleslaw;
            case DragState.CornSalad:
                return SideMenu.CornSalad;
            case DragState.FrenchFries:
                return SideMenu.FrenchFries;
            case DragState.ChickenNugget:
                return SideMenu.ChickenNugget;
        }
        return SideMenu.None;
    }

    public static SideMenu GetShopItemToSideMenu(ShopItem shopItem)
    {
        //ShopItem -> SideMenu
        switch (shopItem)
        {
            case ShopItem.ChickenRadish:
                return SideMenu.ChickenRadish;
            case ShopItem.Pickle:
                return SideMenu.Pickle;
            case ShopItem.Coleslaw:
                return SideMenu.Coleslaw;
            case ShopItem.CornSalad:
                return SideMenu.CornSalad;
            case ShopItem.FrenchFries:
                return SideMenu.FrenchFries;
            case ShopItem.ChickenNugget:
                return SideMenu.ChickenNugget;
        }
        return SideMenu.None;
    }

    public static ShopItem GetSideMenuToShopItem(SideMenu pSideMenu)
    {
        //SideMenu -> ShopItem
        switch (pSideMenu)
        {
            case SideMenu.ChickenRadish:
                return ShopItem.ChickenRadish;
            case SideMenu.Pickle:
                return ShopItem.Pickle;
            case SideMenu.Coleslaw:
                return ShopItem.Coleslaw;
            case SideMenu.CornSalad:
                return ShopItem.CornSalad;
            case SideMenu.FrenchFries:
                return ShopItem.FrenchFries;
            case SideMenu.ChickenNugget:
                return ShopItem.ChickenNugget;
        }
        return ShopItem.None;
    }
}
