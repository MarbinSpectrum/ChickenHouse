using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayData
{
    //�ֹ� ���� Ʃ�丮��
    public bool tutoComplete1;
    //���� ��ġ Ʃ�丮��
    public bool tutoComplete2;
    //��� ��ġ Ʃ�丮��
    public bool tutoComplete3;
    //���� Ʃ�丮��
    public bool tutoComplete4;

    /** ������ ���� �ð� **/
    public int saveYear;
    public int saveMonth;
    public int saveDay;
    public int saveHour;
    public int saveMin;

    /** �����õ� **/
    public int cookLv;
    /** �����õ� ����ġ **/
    public int cookExp;

    /** ���� **/
    public int day = 1;
    /** ���� �ڱ� **/
    public long money;
    /** ���� ������ ���� **/
    public bool[] hasItem = new bool[(int)ShopItem.MAX];

    /** ���� ���� ���� **/
    public bool[] hasWorker = new bool[(int)EWorker.MAX];
    /** ���� ��ġ ���� **/
    public int[] workerPos = new int[(int)KitchenSetWorkerPos.MAX];

    /** ��� ��ġ ���� **/
    public int[] spicy = new int[(int)MenuSetPos.SpicyMAX];
    /** �帵ũ ��ġ ���� **/
    public int[] drink = new int[(int)MenuSetPos.DrinkMAX];
    /** ���̵� �޴� ��ġ ���� **/
    public int[] sideMenu = new int[(int)MenuSetPos.SideMenuMAX];


    /** ����Ʈ ����(0: ������� ,1: ������ ,2: �Ϸ�) **/
    public int[] quest      = new int[(int)Quest.MAX];
    /** ����Ʈ �������� **/
    public int[] questCnt   = new int[(int)Quest.MAX];
    /** ����Ʈ�� Ȯ���� ���� **/
    public bool[] questCheck = new bool[(int)Quest.MAX];

    public const int DEFAULT_CHICKEN_PRICE = 1000;
    public const int CHICKEN_RES_VAIUE = 100;
    public const int DEFAULT_RENT_PRICE = 1000;
    public const int TUP_RATE_VALUE = 50;
    public const int MAX_WORKER = 2;

    public PlayData()
    {
        cookLv = 1;

        hasItem[(int)ShopItem.Recipe_0] = true;
        spicy[(int)MenuSetPos.Spicy0] = (int)ChickenSpicy.Hot;

        hasItem[(int)ShopItem.Cola] = true;
        drink[(int)MenuSetPos.Drink0] = (int)Drink.Cola;

        hasItem[(int)ShopItem.ChickenRadish] = true;
        sideMenu[(int)MenuSetPos.SideMenu0] = (int)SideMenu.ChickenRadish;

        {
            hasItem[(int)ShopItem.Pickle] = true;
            hasItem[(int)ShopItem.Coleslaw] = true;
            hasItem[(int)ShopItem.CornSalad] = true;
            hasItem[(int)ShopItem.FrenchFries] = true;
            hasItem[(int)ShopItem.ChickenNugget] = true;
        }

        hasItem[(int)ShopItem.OIL_Zone_1] = true;

        quest[(int)Quest.MainQuest_1] = 1;
    }

    public int GetMenuValue(GuestReviews review, ChickenSpicy spicy0, ChickenSpicy spicy1, ChickenState chickenState,
        Drink pDrink, SideMenu pSideMenue)
    {
        //�޴� ����
        int defaultValue = ChickenPrice();

        int spicyValue0 = SpicyMgr.Instance.GetSpicyPrice(spicy0);
        int spicyValue1 = SpicyMgr.Instance.GetSpicyPrice(spicy1);
        int drinkValue  = SubMenuMgr.Instance.GetDrinkPrice(pDrink);
        int sideMenuValue = SubMenuMgr.Instance.GetSideMenuPrice(pSideMenue);

        int totalValue = defaultValue + spicyValue0 + spicyValue1 + drinkValue + sideMenuValue;

        int percent = 100 + (int)(GetTotalPriceUpRate());

        ShopItem nowOilZone = NowOilZone();
        if (nowOilZone == ShopItem.OIL_Zone_3)
            percent += 20;
        else if (nowOilZone == ShopItem.OIL_Zone_4)
            percent += 40;

        int resultValue = (int)(totalValue * (percent/100f));
        switch(review)
        {
            case GuestReviews.Bad:
                return 0;
            case GuestReviews.Normal:
                return resultValue;
        }

        return resultValue;
    }

    public float TipRate()
    {
        float rate = TUP_RATE_VALUE;
        CookLvMgr cookLvMgr = CookLvMgr.Instance;
        int lvValue = cookLvMgr.GetLvSumValue(CookLvStat.Tip, cookLv);
        rate += lvValue;

        return rate;
    }

    public int ChickenPrice()
    {
        int value = DEFAULT_CHICKEN_PRICE;
        CookLvMgr cookLvMgr = CookLvMgr.Instance;
        int lvValue = cookLvMgr.GetLvSumValue(CookLvStat.DecreaseDrinkRes, cookLv);
        value += lvValue;
        return value;
    }

    public float DecreaseChickenRes()
    {
        CookLvMgr cookLvMgr = CookLvMgr.Instance;
        int lvValue = cookLvMgr.GetLvSumValue(CookLvStat.DecreaseChickenRes, cookLv);
        return lvValue;
    }

    public float DecreaseDrinkRes()
    {
        CookLvMgr cookLvMgr = CookLvMgr.Instance;
        int lvValue = cookLvMgr.GetLvSumValue(CookLvStat.DecreaseDrinkRes, cookLv);
        return lvValue;
    }

    public float DecreasePickleRes()
    {
        CookLvMgr cookLvMgr = CookLvMgr.Instance;
        int lvValue = cookLvMgr.GetLvSumValue(CookLvStat.DecreasePickleRes, cookLv);
        return lvValue;
    }

    public float GetWorkerSpeedUpRate()
    {
        CookLvMgr cookLvMgr = CookLvMgr.Instance;
        int lvValue = cookLvMgr.GetLvSumValue(CookLvStat.WorkerSpeedUp, cookLv);
        return lvValue;
    }

    public float GetOilZoneSpeedRate()
    {
        //���׷��̵� �ӵ��� ���� ���� ����
        ShopItem nowOilZone = NowOilZone();
        if (nowOilZone == ShopItem.OIL_Zone_1)
            return 100f;
        else if (nowOilZone == ShopItem.OIL_Zone_2)
            return 140f;
        else if (nowOilZone == ShopItem.OIL_Zone_3)
            return 180f;
        else if (nowOilZone == ShopItem.OIL_Zone_4)
            return 260f;
        return 100;
    }

    public float GetPriceUpRate()
    {
        CookLvMgr cookLvMgr = CookLvMgr.Instance;
        int lvValue = cookLvMgr.GetLvSumValue(CookLvStat.IncomeUp, cookLv);
        return lvValue;
    }

    public float GetTotalPriceUpRate()
    {
        //���� ������
        float rate = GetPriceUpRate();
        ShopItem nowOilZone = NowOilZone();
        if (nowOilZone == ShopItem.OIL_Zone_3)
            rate += 20f;
        else if (nowOilZone == ShopItem.OIL_Zone_4)
            rate += 40f;

        if (hasItem[(int)ShopItem.Advertisement_2])
            rate += 10f;
        if (hasItem[(int)ShopItem.Advertisement_3])
            rate += 10f;
        if (hasItem[(int)ShopItem.Advertisement_4])
            rate += 10f;
        if (hasItem[(int)ShopItem.Advertisement_5])
            rate += 10f;
        return rate;
    }

    public float GuestPatience()
    {
        CookLvMgr cookLvMgr = CookLvMgr.Instance;
        int lvValue = cookLvMgr.GetLvSumValue(CookLvStat.GuestPatience, cookLv);
        return 100f + lvValue;
    }

    public float GuestSpawnSpeed()
    {
        CookLvMgr cookLvMgr = CookLvMgr.Instance;
        int lvValue = cookLvMgr.GetLvSumValue(CookLvStat.GuestSpawnSpeed, cookLv);
        return 100f + lvValue;
    }

    public float GuestTotalDelayRate()
    {
        //�Խ�Ʈ ������ ����
        float rate = GuestSpawnSpeed();

        if (hasItem[(int)ShopItem.Advertisement_5])
            rate += 5;
        if (hasItem[(int)ShopItem.Advertisement_4])
            rate += 7f;
        if (hasItem[(int)ShopItem.Advertisement_3])
            rate += 10f;
        if (hasItem[(int)ShopItem.Advertisement_2])
            rate += 15;
        if (hasItem[(int)ShopItem.Advertisement_1])
            rate += 20f;

        return rate;
    }

    public bool HasRecipe(ChickenSpicy pSpicy)
    {
        //�ش� ������ ����� �����Ǹ� ������ �ִ��� ����
        if (pSpicy == ChickenSpicy.None)
            return true;

        ShopItem shopItem = SpicyMgr.SpicyGetRecipe(pSpicy);
        if (shopItem == ShopItem.None)
            return false;

        return hasItem[(int)shopItem];
    }

    public bool HasDrink(Drink pDrink)
    {
        //�ش� ������ ���Ḧ ������ �ִ��� ����
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
        //�ش� ������ ���̵�޴��� ������ �ִ��� ����
        return hasItem[(int)SubMenuMgr.SideMenuGetShopItem(pSideMenu)];
    }

    public bool KitchenSetSpicy(ChickenSpicy pSpicy)
    {
        //�ش� ������ ����� ��ġ�� ����
        if (pSpicy == ChickenSpicy.None)
            return true;

        for (int i = 0; i < spicy.Length; i++)
        {
            ChickenSpicy tempSpicy = (ChickenSpicy)spicy[i];
            if (tempSpicy == pSpicy)
                return true;
        }
        return false;
    }

    public bool KitchenSetDrink(Drink pDrink)
    {
        //�ش� ������ ���Ḧ ��ġ�� ����
        for (int i = 0; i < drink.Length; i++)
        {
            Drink tempDrink = (Drink)drink[i];
            if (tempDrink == pDrink)
                return true;
        }
        return false;
    }

    public bool KitchenSetSideMenu(SideMenu pSideMenu)
    {
        //�ش� ������ ���̵�޴��� ��ġ�� ����
        for (int i = 0; i < sideMenu.Length; i++)
        {
            SideMenu tempSideMenu = (SideMenu)sideMenu[i];
            if (tempSideMenu == pSideMenu)
                return true;
        }
        return false;
    }

    public ShopItem NowOilZone()
    {
        if (hasItem[(int)ShopItem.OIL_Zone_4])
            return ShopItem.OIL_Zone_4;
        if (hasItem[(int)ShopItem.OIL_Zone_3])
            return ShopItem.OIL_Zone_3;
        if (hasItem[(int)ShopItem.OIL_Zone_2])
            return ShopItem.OIL_Zone_2;
        if (hasItem[(int)ShopItem.OIL_Zone_1])
            return ShopItem.OIL_Zone_1;
        return ShopItem.None;
    }

    public float ShopSaleValue()
    {
        //���� ���η�
        CookLvMgr cookLvMgr = CookLvMgr.Instance;
        int lvValue = cookLvMgr.GetLvSumValue(CookLvStat.ShopSale, cookLv);
        float value = lvValue;
        return value;
    }

    public int RentValue()
    {
        //�Ӵ��
        CookLvMgr cookLvMgr = CookLvMgr.Instance;
        int lvValue = cookLvMgr.GetLvSumValue(CookLvStat.Rent, cookLv);
        return Mathf.Max(0, DEFAULT_RENT_PRICE - lvValue);
    }

    public void GetShopItem(ShopItem pShopItem)
    {
        hasItem[(int)pShopItem] = true;
        ChickenSpicy chickenSpicy = SpicyMgr.RecipeGetSpicy(pShopItem);
        if (chickenSpicy != ChickenSpicy.None)
        {
            //����� ���� ���� ������ ���
            BookMgr.ActSpicyData(chickenSpicy);
        }
        Drink drink = SubMenuMgr.ShopItemGetDrink(pShopItem);
        if (drink != Drink.None)
        {
            //���Ḧ ���� ���� ������ ���
            BookMgr.ActDrinkData(drink);
        }
        SideMenu sideMenu = SubMenuMgr.ShopItemGetSideMenu(pShopItem);
        if (sideMenu != SideMenu.None)
        {
            //���̵�޴��� ���� ���� ������ ���
            BookMgr.ActSideMenuData(sideMenu);
        }
    }
}
