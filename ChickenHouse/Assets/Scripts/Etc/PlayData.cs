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

    /** 퀘스트 상태(0: 진행안함 ,1: 진행중 ,2: 완료) **/
    public int[] quest      = new int[(int)Quest.MAX];
    /** 퀘스트 진행정도 **/
    public int[] questCnt   = new int[(int)Quest.MAX];
    
    public const int DEFAULT_CHICKEN_VALUE = 300;
    public const int CHICKEN_RES_VAIUE = 100;
    public const int DRINK_RES_VAIUE = 100;
    public const int SIDE_MENU_RES_VAIUE = 100;

    public PlayData()
    {
        hasItem[(int)ShopItem.OIL_Zone_1] = true;
        useItem[(int)ShopItem.OIL_Zone_1] = true;
        quest[(int)Quest.MainQuest_1] = 1;

    }

    public int GetMenuValue(GuestReviews review, ChickenSpicy spicy0, ChickenSpicy spicy1, ChickenState chickenState,
        Drink pDrink, SideMenu pSideMenue)
    {
        //메뉴 가격
        int defaultValue = DEFAULT_CHICKEN_VALUE;

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

    public float GetCookSpeedRate()
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

        if (useItem[(int)ShopItem.OIL_Zone_3])
            rate += 0.2f;
        else if (useItem[(int)ShopItem.OIL_Zone_4])
            rate += 0.4f;

        ResumeData resumeData = GetNowWorkerData();
        if (resumeData != null)
        {
            if (resumeData.skill.Contains(WorkerSkill.WorkerSkill_2))
                rate += 0.2f;
        }
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

        ResumeData resumeData = GetNowWorkerData();
        if (resumeData != null)
        {
            if (resumeData.skill.Contains(WorkerSkill.WorkerSkill_3))
                upgradeRate -= 0.07f;
        }
        return upgradeRate * dayRate;
    }

    private int GetSpicyValue(ChickenSpicy spicy)
    {
        //양념장 가격
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

    public ShopItem GetNowWorker()
    {
        //현재 사용중인 손님
        ShopItem shopItem = ShopItem.None;
        if (hasItem[(int)ShopItem.Worker_1] && useItem[(int)ShopItem.Worker_1])
            shopItem = ShopItem.Worker_1;
        else if (hasItem[(int)ShopItem.Worker_2] && useItem[(int)ShopItem.Worker_2])
            shopItem = ShopItem.Worker_2;
        else if (hasItem[(int)ShopItem.Worker_3] && useItem[(int)ShopItem.Worker_3])
            shopItem = ShopItem.Worker_3;
        else if (hasItem[(int)ShopItem.Worker_4] && useItem[(int)ShopItem.Worker_4])
            shopItem = ShopItem.Worker_4;
        else if (hasItem[(int)ShopItem.Worker_5] && useItem[(int)ShopItem.Worker_5])
            shopItem = ShopItem.Worker_5;
        else if (hasItem[(int)ShopItem.Worker_6] && useItem[(int)ShopItem.Worker_6])
            shopItem = ShopItem.Worker_6;

        return shopItem;
    }

    public float WorkerSpeed()
    {
        float value = 0;
        return value;
    }

    public float ShopSaleValue()
    {
        float value = 0;
        return value;
    }

    public int RentValue()
    {
        return 100;
    }

    public ResumeData GetNowWorkerData()
    {
        //현재 사용주인 손님 정보
        ShopItem shopItem = GetNowWorker();

        ResumeData resumeData = ShopMgr.Instance?.GetResumeData(shopItem);
        return resumeData;
    }
}
