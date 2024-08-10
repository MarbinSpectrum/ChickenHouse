using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayData
{
    /** ������ ���� �ð� **/
    public int saveYear;
    public int saveMonth;
    public int saveDay;
    public int saveHour;
    public int saveMin;

    /** ���� **/
    public int day = 1;
    /** ���� �ڱ� **/
    public long money;
    /** ���� ������ ���� **/
    public bool[] hasItem = new bool[(int)ShopItem.MAX];
    /** ��� ������ ���� **/
    public bool[] useItem = new bool[(int)ShopItem.MAX];

    /** ����Ʈ ����(0: ������� ,1: ������ ,2: �Ϸ�) **/
    public int[] quest      = new int[(int)Quest.MAX];
    /** ����Ʈ �������� **/
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
        //�޴� ����
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
        //���׷��̵� �ӵ��� ���� ���� ����
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
        //���� ������
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
        //�Խ�Ʈ ������ ����
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
        //����� ����
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
        //���� ������� �մ�
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
        //���� ������� �մ� ����
        ShopItem shopItem = GetNowWorker();

        ResumeData resumeData = ShopMgr.Instance?.GetResumeData(shopItem);
        return resumeData;
    }
}
