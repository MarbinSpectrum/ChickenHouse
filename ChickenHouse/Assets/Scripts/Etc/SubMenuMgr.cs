using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubMenuMgr : AwakeSingleton<SubMenuMgr>
{
    [SerializeField] private Dictionary<Drink, DrinkData>       drinkData       = new Dictionary<Drink, DrinkData>();
    [SerializeField] private Dictionary<SideMenu, SideMenuData> sideMenuData    = new Dictionary<SideMenu, SideMenuData>();

    public DrinkData GetDrinkData(Drink pDrink)
    {
        //���� ���� ���
        if (drinkData.ContainsKey(pDrink))
            return drinkData[pDrink];
        return null;
    }

    public int GetDrinkPrice(Drink pDrink)
    {
        //���� ����
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
        //���� �����ΰ�?
        switch(pDrink)
        {
            case Drink.Beer:
                return true;             
        }
        return false;
    }

    public static Drink ShopItemGetDrink(ShopItem shopItem)
    {
        //�����ۿ� �ش��ϴ� �����ȯ
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
        //���̵� �޴� ���� ���
        if (sideMenuData.ContainsKey(pSideMenu))
            return sideMenuData[pSideMenu];
        return null;
    }

    public int GetSideMenuPrice(SideMenu pSideMenu)
    {
        //���̵�޴� ����
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
        //�����ۿ� �ش��ϴ� ���̵�޴���ȯ
        switch (shopItem)
        {
            case ShopItem.Pickle:
                return SideMenu.ChickenRadish;
        }
        return SideMenu.None;
    }
}
