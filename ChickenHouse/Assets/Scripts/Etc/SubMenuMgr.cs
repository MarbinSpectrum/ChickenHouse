using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubMenuMgr : AwakeSingleton<SubMenuMgr>
{
    [SerializeField] private Dictionary<Drink, DrinkData>       drinkData       = new Dictionary<Drink, DrinkData>();
    [SerializeField] private Dictionary<SideMenu, SideMenuData> sideMenuData    = new Dictionary<SideMenu, SideMenuData>();

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

    public Drink GetDragStateDrink(DragState pDragState)
    {
        switch (pDragState)
        {
            case DragState.Cola:
                return Drink.Cola;
            case DragState.Beer:
                return Drink.Beer;
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

    public static Drink ShopItemGetDrink(ShopItem shopItem)
    {
        //아이템에 해당하는 음료반환
        switch (shopItem)
        {
            case ShopItem.Cola:
                return Drink.Cola;
            case ShopItem.Beer:
                return Drink.Beer;
        }
        return Drink.None;
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

    public SideMenu GetDragStateSideMenu(DragState pDragState)
    {
        switch(pDragState)
        {
            case DragState.Chicken_Radish:
                return SideMenu.ChickenRadish;
        }
        return SideMenu.None;
    }

    public static SideMenu ShopItemGetSideMenu(ShopItem shopItem)
    {
        //아이템에 해당하는 사이드메뉴반환
        switch (shopItem)
        {
            case ShopItem.Pickle:
                return SideMenu.ChickenRadish;
        }
        return SideMenu.None;
    }
}
