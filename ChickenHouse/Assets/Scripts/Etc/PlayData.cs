using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayData
{
    /** 저장한 실제 시간 **/
    public int saveYear;
    public int saveMonth;
    public int saveDay;
    public int saveHour;
    public int saveMin;

    /** 일차 **/
    public int day = 1;
    /** 보유 자금 **/
    public long money;
    /** 보유 아이템 상태 **/
    public bool[] hasItem = new bool[(int)ShopItem.MAX];
    /** 사용 아이템 상태 **/
    public bool[] useItem = new bool[(int)ShopItem.MAX];

    /** 직원 보유 상태 **/
    public bool[] hasWorker = new bool[(int)EWorker.MAX];
    /** 직원 배치 상태 **/
    public int[] workerPos = new int[(int)KitchenSet_UI.KitchenSetWorkerPos.MAX];

    /** 양념 배치 상태 **/
    public int[] spicyState = new int[(int)MenuSet_UI.MenuSetPos.SpicyMAX];
    /** 드링크 배치 상태 **/
    public int[] drinkState = new int[(int)MenuSet_UI.MenuSetPos.DrinkMAX];
    /** 사이드 메뉴 배치 상태 **/
    public int[] sideMenuState = new int[(int)MenuSet_UI.MenuSetPos.SideMenuMAX];


    /** 퀘스트 상태(0: 진행안함 ,1: 진행중 ,2: 완료) **/
    public int[] quest      = new int[(int)Quest.MAX];
    /** 퀘스트 진행정도 **/
    public int[] questCnt   = new int[(int)Quest.MAX];
    
    public const int DEFAULT_CHICKEN_PRICE = 600;
    public const int CHICKEN_RES_VAIUE = 100;
    public const int DRINK_RES_VAIUE = 100;
    public const int SIDE_MENU_RES_VAIUE = 100;

    public PlayData()
    {
        hasItem[(int)ShopItem.Recipe_0] = true;

        hasItem[(int)ShopItem.Cola] = true;

        hasItem[(int)ShopItem.Pickle] = true;

        hasItem[(int)ShopItem.OIL_Zone_1] = true;
        useItem[(int)ShopItem.OIL_Zone_1] = true;

        hasWorker[(int)EWorker.Worker_1] = true;

        quest[(int)Quest.MainQuest_1] = 1;

    }

    public int GetMenuValue(GuestReviews review, ChickenSpicy spicy0, ChickenSpicy spicy1, ChickenState chickenState,
        Drink pDrink, SideMenu pSideMenue)
    {
        //메뉴 가격
        int defaultValue = DEFAULT_CHICKEN_PRICE;

        int spicyValue0 = SpicyMgr.Instance.GetSpicyPrice(spicy0);
        int spicyValue1 = SpicyMgr.Instance.GetSpicyPrice(spicy1);
        int drinkValue  = SubMenuMgr.Instance.GetDrinkPrice(pDrink);
        int sideMenuValue = SubMenuMgr.Instance.GetSideMenuPrice(pSideMenue);

        int totalValue = defaultValue + spicyValue0 + spicyValue1 + drinkValue + sideMenuValue;

        int percent = 100 + (int)(GetPriceUpRate()*100);

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
                return (int)(resultValue * (1f + TipRate()));
        }

        return resultValue;
    }

    public float TipRate()
    {
        float rate = 0.5f;
        return rate;
    }

    public float GetOilZoneSpeedRate()
    {
        //업그레이드 속도에 따라서 상태 설정
        if (useItem[(int)ShopItem.OIL_Zone_4])
        {
            return 2.6f;
        }
        else if (useItem[(int)ShopItem.OIL_Zone_3])
        {
            return 1.8f;
        }
        else if (useItem[(int)ShopItem.OIL_Zone_2])
        {
            return 1.4f;
        }
        else if (useItem[(int)ShopItem.OIL_Zone_1])
        {
            return 1f;
        }
        return 1;
    }

    public float GetPriceUpRate()
    {
        //수익 증가률
        float rate = 0;

        if (useItem[(int)ShopItem.OIL_Zone_4])
            rate += 0.4f;
        else if (useItem[(int)ShopItem.OIL_Zone_3])
            rate += 0.2f;

        if (hasItem[(int)ShopItem.Advertisement_2])
            rate += 0.1f;
        if (hasItem[(int)ShopItem.Advertisement_3])
            rate += 0.1f;
        if (hasItem[(int)ShopItem.Advertisement_4])
            rate += 0.1f;
        if (hasItem[(int)ShopItem.Advertisement_5])
            rate += 0.1f;
        return rate;
    }

    public float GuestDelayRate()
    {
        //게스트 딜레이 배율
        float dayRate = 1;
        if (day == 1)
            dayRate = 1;
        else if (day == 2)
            dayRate = 0.95f;
        else if (day == 3)
            dayRate = 0.9f;
        else if (day == 4)
            dayRate = 0.85f;
        else
            dayRate = 0.8f;

        float upgradeRate = 1;
        if (hasItem[(int)ShopItem.Advertisement_5])
            upgradeRate -= 0.05f;
        if (hasItem[(int)ShopItem.Advertisement_4])
            upgradeRate -= 0.07f;
        if (hasItem[(int)ShopItem.Advertisement_3])
            upgradeRate -= 0.1f;
        if (hasItem[(int)ShopItem.Advertisement_2])
            upgradeRate -= 0.15f;
        if (hasItem[(int)ShopItem.Advertisement_1])
            upgradeRate -= 0.2f;

        return upgradeRate * dayRate;
    }

    public bool HasRecipe(ChickenSpicy pSpicy)
    {
        //해당 종류의 양념의 레시피를 가지고 있는지 여부
        switch(pSpicy)
        {
            case ChickenSpicy.None:
                return true;
            case ChickenSpicy.Hot:
                return hasItem[(int)ShopItem.Recipe_0];
            case ChickenSpicy.Soy:
                return hasItem[(int)ShopItem.Recipe_1];
            case ChickenSpicy.Hell:
                return hasItem[(int)ShopItem.Recipe_2];
            case ChickenSpicy.Prinkle:
                return hasItem[(int)ShopItem.Recipe_3];
            case ChickenSpicy.Carbonara:
                return hasItem[(int)ShopItem.Recipe_4];
            case ChickenSpicy.BBQ:
                return hasItem[(int)ShopItem.Recipe_5];
        }
        return false;
    }

    public bool HasDrink(Drink pDrink)
    {
        //해당 종류의 음료를 가지고 있는지 여부
        switch (pDrink)
        {
            case Drink.Cola:
                return hasItem[(int)ShopItem.Cola];
            case Drink.Beer:
                return hasItem[(int)ShopItem.Beer];
        }
        return false;
    }

    public bool HasSideMenu(SideMenu pSideMenu)
    {
        //해당 종류의 사이드메뉴를 가지고 있는지 여부
        switch (pSideMenu)
        {
            case SideMenu.Pickle:
                return hasItem[(int)ShopItem.Pickle];
        }
        return false;
    }

    public bool KitchenSetSpicy(ChickenSpicy pSpicy)
    {
        //해당 종류의 양념을 배치한 상태
        if (pSpicy == ChickenSpicy.None)
            return true;

        for (int i = 0; i < spicyState.Length; i++)
        {
            ChickenSpicy spicy = (ChickenSpicy)spicyState[i];
            if (spicy == pSpicy)
                return true;
        }
        return false;
    }

    public bool KitchenSetDrink(Drink pDrink)
    {
        //해당 종류의 음료를 배치한 상태
        for (int i = 0; i < drinkState.Length; i++)
        {
            Drink drink = (Drink)drinkState[i];
            if (drink == pDrink)
                return true;
        }
        return false;
    }

    public bool KitchenSetSideMenu(SideMenu pSideMenu)
    {
        //해당 종류의 사이드메뉴를 배치한 상태
        for (int i = 0; i < sideMenuState.Length; i++)
        {
            SideMenu sideMenu = (SideMenu)sideMenuState[i];
            if (sideMenu == pSideMenu)
                return true;
        }
        return false;
    }

    public float ShopSaleValue()
    {
        //상점 할인률
        float value = 0;
        return value;
    }

    public int RentValue()
    {
        //임대료
        return 100;
    }
}
