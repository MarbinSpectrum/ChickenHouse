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
    /** 업그레이드 상태 **/
    public bool[] upgradeState = new bool[(int)Upgrade.MAX];

    public int GetMenuValue(GuestReviews review, ChickenSpicy spicy0, ChickenSpicy spicy1, ChickenState chickenState,
        bool hasDrink, bool hasPickle)
    {
        //메뉴 가격
        int defaultValue = 300;

        int spicyValue0 = GetSpicyValue(spicy0);
        int spicyValue1 = GetSpicyValue(spicy1);
        int drinkValue  = hasDrink ? 50 : 0;
        int pickleValue = hasPickle ? 50 : 0;

        int totalValue = defaultValue + spicyValue0 + spicyValue1 + drinkValue + pickleValue;

        int percent = 100;
        if (upgradeState[(int)Upgrade.Recipe_1])
            percent += 20;
        if (upgradeState[(int)Upgrade.Recipe_2])
            percent += 20;
        if (upgradeState[(int)Upgrade.Recipe_3])
            percent += 20;
        if (upgradeState[(int)Upgrade.Recipe_4])
            percent += 20;

        int resultValue = (totalValue * percent) / 100;

        switch(review)
        {
            case GuestReviews.Bad:
                return 0;
            case GuestReviews.Normal:
                return resultValue;
            case GuestReviews.Good:
                return (int)(resultValue * 1.5f);
            case GuestReviews.Happy:
                return (int)(resultValue * 2f);
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
            case ChickenSpicy.BBQ:
                return 200;
        }
        return 0;
    }
}
