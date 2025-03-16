using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayData
{
    //주방 조리 튜토리얼
    public bool tutoComplete1;
    //직원 배치 튜토리얼
    public bool tutoComplete2;
    //양념 배치 튜토리얼
    public bool tutoComplete3;
    //마을 튜토리얼
    public bool tutoComplete4;

    /** 저장한 실제 시간 **/
    public int saveYear;
    public int saveMonth;
    public int saveDay;
    public int saveHour;
    public int saveMin;

    /** 장사숙련도 **/
    public int cookLv;
    /** 장사숙련도 경험치 **/
    public int cookExp;

    /** 일차 **/
    public int day = 1;
    /** 보유 자금 **/
    public long money;
    /** 보유 아이템 상태 **/
    public bool[] hasItem = new bool[(int)ShopItem.MAX];
    public bool[] hasDrink = new bool[(int)Drink.MAX];
    public bool[] hasSideMenu = new bool[(int)SideMenu.MAX];
    public bool[] hasSpicy = new bool[(int)ChickenSpicy.MAX];
    public bool[] hasAD = new bool[(int)ADItem.MAX];

    /** 직원 보유 상태 **/
    public bool[] hasWorker = new bool[(int)EWorker.MAX];
    /** 직원 배치 상태 **/
    public int[] workerPos = new int[(int)KitchenSetWorkerPos.MAX];

    /** 양념 배치 상태 **/
    public int[] spicy = new int[(int)MenuSetPos.SpicyMAX];
    /** 드링크 배치 상태 **/
    public int[] drink = new int[(int)MenuSetPos.DrinkMAX];
    /** 사이드 메뉴 배치 상태 **/
    public int[] sideMenu = new int[(int)MenuSetPos.SideMenuMAX];


    /** 퀘스트 상태(0: 진행안함 ,1: 진행중 ,2: 완료) **/
    public int[] quest      = new int[(int)Quest.MAX];
    /** 퀘스트 진행정도 **/
    public int[] questCnt   = new int[(int)Quest.MAX];
    /** 퀘스트를 확인한 여부 **/
    public bool[] questCheck = new bool[(int)Quest.MAX];

    /** 인테리어 상태 **/
    public int useInteriorWall;
    public int useInteriorDesk;
    public int useInteriorFloor;
    public int useInteriorTable;
    public bool[] hasInterior = new bool[(int)InteriorItem.MAX];

    public const int DEFAULT_CHICKEN_PRICE = 1000;
    public const int CHICKEN_RES_VAIUE = 100;
    public const int DEFAULT_RENT_PRICE = 1000;
    public const int TUP_RATE_VALUE = 50;
    public const int MAX_WORKER = 2;

    public PlayData()
    {
        cookLv = 1;

        hasSpicy[(int)ChickenSpicy.Hot] = true;
        spicy[(int)MenuSetPos.Spicy0] = (int)ChickenSpicy.Hot;

        hasDrink[(int)Drink.Cola] = true;
        drink[(int)MenuSetPos.Drink0] = (int)Drink.Cola;

        hasSideMenu[(int)SideMenu.ChickenRadish] = true;
        sideMenu[(int)MenuSetPos.SideMenu0] = (int)SideMenu.ChickenRadish;

        hasItem[(int)ShopItem.OIL_Zone_1] = true;

        hasInterior[(int)InteriorItem.Interior_Wall_0] = true;
        hasInterior[(int)InteriorItem.Interior_Desk_0] = true;
        hasInterior[(int)InteriorItem.Interior_Floor_0] = true;
        hasInterior[(int)InteriorItem.Interior_Table_0] = true;
        useInteriorWall = (int)InteriorItem.Interior_Wall_0;
        useInteriorDesk = (int)InteriorItem.Interior_Desk_0;
        useInteriorFloor = (int)InteriorItem.Interior_Floor_0;
        useInteriorTable = (int)InteriorItem.Interior_Table_0;

        quest[(int)Quest.MainQuest_1] = 1;
    }

    public int GetMenuValue(GuestReviews review, ChickenSpicy spicy0, ChickenSpicy spicy1, ChickenState chickenState,
        Drink pDrink, SideMenu pSideMenue)
    {
        //메뉴 가격
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

    public float DecreaseChickenRate()
    {
        CookLvMgr cookLvMgr = CookLvMgr.Instance;
        int lvValue = cookLvMgr.GetLvSumValue(CookLvStat.DecreaseChickenRes, cookLv);
        return lvValue;
    }

    public float DecreaseDrinkRate()
    {
        CookLvMgr cookLvMgr = CookLvMgr.Instance;
        int lvValue = cookLvMgr.GetLvSumValue(CookLvStat.DecreaseDrinkRes, cookLv);
        return lvValue;
    }

    public float DecreaseSideMenuRate()
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
        //업그레이드 속도에 따라서 상태 설정
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
        //수익 증가률
        float rate = GetPriceUpRate();
        ShopItem nowOilZone = NowOilZone();
        if (nowOilZone == ShopItem.OIL_Zone_3)
            rate += 20f;
        else if (nowOilZone == ShopItem.OIL_Zone_4)
            rate += 40f;

        if (hasAD[(int)ADItem.Advertisement_2])
            rate += 10f;
        if (hasAD[(int)ADItem.Advertisement_3])
            rate += 10f;
        if (hasAD[(int)ADItem.Advertisement_4])
            rate += 10f;
        if (hasAD[(int)ADItem.Advertisement_5])
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
        //게스트 딜레이 배율
        float rate = GuestSpawnSpeed();

        if (hasAD[(int)ADItem.Advertisement_5])
            rate += 5;
        if (hasAD[(int)ADItem.Advertisement_4])
            rate += 7f;
        if (hasAD[(int)ADItem.Advertisement_3])
            rate += 10f;
        if (hasAD[(int)ADItem.Advertisement_2])
            rate += 15;
        if (hasAD[(int)ADItem.Advertisement_1])
            rate += 20f;

        return rate;
    }

    public bool HasRecipe(ChickenSpicy pSpicy)
    {
        //해당 종류의 양념의 레시피를 가지고 있는지 여부
        return hasSpicy[(int)pSpicy];
    }

    public bool HasDrink(Drink pDrink)
    {
        //해당 종류의 음료를 가지고 있는지 여부
        return hasDrink[(int)pDrink];
    }

    public bool HasSideMenu(SideMenu pSideMenu)
    {
        //해당 종류의 사이드메뉴를 가지고 있는지 여부
        return hasSideMenu[(int)pSideMenu];
    }

    public bool KitchenSetSpicy(ChickenSpicy pSpicy)
    {
        //해당 종류의 양념을 배치한 상태
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
        //해당 종류의 음료를 배치한 상태
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
        //해당 종류의 사이드메뉴를 배치한 상태
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
        //상점 할인률
        CookLvMgr cookLvMgr = CookLvMgr.Instance;
        int lvValue = cookLvMgr.GetLvSumValue(CookLvStat.ShopSale, cookLv);
        float value = lvValue;
        return value;
    }

    public int RentValue()
    {
        //임대료
        CookLvMgr cookLvMgr = CookLvMgr.Instance;
        int lvValue = cookLvMgr.GetLvSumValue(CookLvStat.Rent, cookLv);
        return Mathf.Max(0, DEFAULT_RENT_PRICE - lvValue);
    }

    public void AddQuestReward(QuestData.QuestRewardData pRewardData)
    {
        switch(pRewardData.getRewardType)
        {
            case QuestData.ERewardType.ShopItem:
                AddShopItem((ShopItem)pRewardData.GetQuestReward());
                return;
            case QuestData.ERewardType.Spicy:
                AddSpicyItem((ChickenSpicy)pRewardData.GetQuestReward());
                return;
            case QuestData.ERewardType.Drink:
                AddDrink((Drink)pRewardData.GetQuestReward());
                return;
            case QuestData.ERewardType.SideMenu:
                AddSideMenu((SideMenu)pRewardData.GetQuestReward());
                return;
            case QuestData.ERewardType.AD_Item:
                AddADItem((ADItem)pRewardData.GetQuestReward());
                return;
            case QuestData.ERewardType.InteriorItem:
                AddInterior((InteriorItem)pRewardData.GetQuestReward());
                return;
        }
    }

    public void AddShopItem(ShopItem pShopItem)
    {
        hasItem[(int)pShopItem] = true;
    }

    public void AddSpicyItem(ChickenSpicy pChickenSpicy)
    {
        hasSpicy[(int)pChickenSpicy] = true;
        //양념을 새로 얻음 도감에 등록
        BookMgr.ActSpicyData(pChickenSpicy);
    }

    public void AddDrink(Drink pDrink)
    {
        hasDrink[(int)pDrink] = true;
        //음료를 새로 얻음 도감에 등록
        BookMgr.ActDrinkData(pDrink);
    }

    public void AddSideMenu(SideMenu pSideMenu)
    {
        hasSideMenu[(int)pSideMenu] = true;
        //사이드메뉴를 새로 얻음 도감에 등록
        BookMgr.ActSideMenuData(pSideMenu);
    }

    public void AddADItem(ADItem pADItem)
    {
        //광고 등록
        hasAD[(int)pADItem] = true;
    }

    public void AddInterior(InteriorItem pInteriorItem)
    {
        //인테리어 등록
        hasInterior[(int)pInteriorItem] = true;
    }

    public void SetInterior(InteriorItem pInteriorItem)
    {
        //해당 인테리어 용품을 적용
        InteriorTab interiorTab = InteriorMgr.GetInteriorTab(pInteriorItem);
        switch (interiorTab)
        {
            case InteriorTab.Wall:
                useInteriorWall = (int)pInteriorItem;
                break;
            case InteriorTab.Table:
                useInteriorTable = (int)pInteriorItem;
                break;
            case InteriorTab.Floor:
                useInteriorFloor = (int)pInteriorItem;
                break;
            case InteriorTab.Desk:
                useInteriorDesk = (int)pInteriorItem;
                break;
        }
    }

    public bool IsUseInterior(InteriorItem pInteriorItem)
    {
        //해당 인테리어 용품을 사용중인지 체크
        InteriorTab interiorTab = InteriorMgr.GetInteriorTab(pInteriorItem);
        switch (interiorTab)
        {
            case InteriorTab.Wall:
                return useInteriorWall == (int)pInteriorItem;
            case InteriorTab.Table:
                return useInteriorTable == (int)pInteriorItem;
            case InteriorTab.Floor:
                return useInteriorFloor == (int)pInteriorItem;
            case InteriorTab.Desk:
                return useInteriorDesk == (int)pInteriorItem;
        }
        return false;
    }
}
