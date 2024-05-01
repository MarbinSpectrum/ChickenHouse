using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayData
{
    /** 일차 **/
    public int day = 1;
    /** 보유 자금 **/
    public long money;
    /** 보유 아이템 상태 **/
    public bool[] hasItem = new bool[(int)ShopItem.MAX];
    /** 사용 아이템 상태 **/
    public bool[] useItem = new bool[(int)ShopItem.MAX];

    public PlayData()
    {
        hasItem[(int)ShopItem.OIL_Zone_1] = true;
        useItem[(int)ShopItem.OIL_Zone_1] = true;
    }

    public int GetMenuValue(GuestReviews review, ChickenSpicy spicy0, ChickenSpicy spicy1, ChickenState chickenState,
        Drink pDrink, SideMenu pSideMenue)
    {
        //메뉴 가격
        int defaultValue = 300;

        int spicyValue0 = GetSpicyValue(spicy0);
        int spicyValue1 = GetSpicyValue(spicy1);
        int drinkValue  = 0;
        switch (pDrink)
        {
            case Drink.None:
                drinkValue = 0;
                break;
            case Drink.Cola:
                drinkValue = 50;
                break;
        }

        int sideMenuValue = 0;
        switch (pSideMenue)
        {
            case SideMenu.None:
                sideMenuValue = 0;
                break;
            case SideMenu.Pickle:
                sideMenuValue = 50;
                break;
        }

        int totalValue = defaultValue + spicyValue0 + spicyValue1 + drinkValue + sideMenuValue;

        int percent = 100;

        if (useItem[(int)ShopItem.OIL_Zone_3])
            percent += 20;
        else if (useItem[(int)ShopItem.OIL_Zone_4])
            percent += 40;

        int resultValue = (totalValue * percent) / 100;

        switch(review)
        {
            case GuestReviews.Bad:
                return 0;
            case GuestReviews.Normal:
                return resultValue;
            case GuestReviews.Happy:
                return (int)(resultValue * 1.5f);
        }

        return resultValue;
    }

    private int GetSpicyValue(ChickenSpicy spicy)
    {
        switch(spicy)
        {
            case ChickenSpicy.Not:
                return 0;
            case ChickenSpicy.Hot:
                return 50;
            case ChickenSpicy.Soy:
                return 100;
            case ChickenSpicy.Hell:
                return 150;
            case ChickenSpicy.Carbonara:
                return 200;
            case ChickenSpicy.BBQ:
                return 250;
        }
        return 0;
    }
}
