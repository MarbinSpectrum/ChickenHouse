using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequireMenu
{
    //손님이 원하는 메뉴에 대한 정보가 담긴 객체입니다.

    /** 피클을 요구할 확률 **/
    private const int PICKLE_PER = 40;

    /** 콜라를 요구할 확률 **/
    private const int COLA_PER = 40;

    /** 원하는 치킨 소스 **/
    public ChickenSpicy[] chickenSpicy { get; private set; } = new ChickenSpicy[2];
    /** 콜라 필요 여부 **/
    public Drink drink { get; private set; }
    /** 피클 필요 여부 **/
    public SideMenu sideMenu { get; private set; }

    public int menuIdx { get; private set; }

    public bool CreateMenu(GuestData pGuestData, float nowTime,bool isTuto)
    {
        if (isTuto)
        {
            menuIdx = 0;
            GuestMenu guestMenu = pGuestData.goodChicken[menuIdx];

            //치킨 맛 결정
            ChickenSpicy spicy0 = (ChickenSpicy)Mathf.Min((int)guestMenu.spicy0, (int)guestMenu.spicy1);
            ChickenSpicy spicy1 = (ChickenSpicy)Mathf.Max((int)guestMenu.spicy0, (int)guestMenu.spicy1);
            chickenSpicy[0] = spicy0;
            chickenSpicy[1] = spicy1;

            drink = Drink.Cola;
            sideMenu = SideMenu.Pickle;
            return true;
        }
        else
        {
            List<int> randomList = new List<int>();
            for(int i = 0; i < pGuestData.goodChicken.Count; i++)
                if (pGuestData.goodChicken[i].CanMakeChicken())
                    randomList.Add(i);

            if (randomList.Count == 0)
                return false;

            menuIdx = randomList[Random.Range(0, randomList.Count)];
            GuestMenu guestMenu = pGuestData.goodChicken[menuIdx];

            //치킨 맛 결정
            ChickenSpicy spicy0 = (ChickenSpicy)Mathf.Min((int)guestMenu.spicy0, (int)guestMenu.spicy1);
            ChickenSpicy spicy1 = (ChickenSpicy)Mathf.Max((int)guestMenu.spicy0, (int)guestMenu.spicy1);
            chickenSpicy[0] = spicy0;
            chickenSpicy[1] = spicy1;

            //고정 음료가 없으면 랜덤하게 정한다.
            drink = guestMenu.drink;
            if (drink == Drink.None)
            {
                int randomValue = Random.Range(0, 100);
                if (randomValue < COLA_PER)
                    drink = Drink.Cola;
            }

            //고정 사이드 메뉴가 없으면 랜덤하게 정한다.
            sideMenu = guestMenu.sideMenu;
            if (sideMenu == SideMenu.None)
            {
                int randomValue = Random.Range(0, 100);
                if (randomValue < PICKLE_PER)
                    sideMenu = SideMenu.Pickle;
            }

            return true;
        }
    }

    public string GetChickenName()
    {
        ChickenSpicy spicy0 = (ChickenSpicy)Mathf.Min((int)chickenSpicy[0], (int)chickenSpicy[1]);
        ChickenSpicy spicy1 = (ChickenSpicy)Mathf.Max((int)chickenSpicy[0], (int)chickenSpicy[1]);

        //주문한 치킨 이름 반환
        switch (spicy0)
        {
            case ChickenSpicy.None:
                {
                    switch (spicy1)
                    {
                        case ChickenSpicy.None:
                            return LanguageMgr.GetText("FRIED_CHICKEN");
                        case ChickenSpicy.Hot:
                            return LanguageMgr.GetText("HOT_AND_FRIED_CHICKEN");
                        case ChickenSpicy.Soy:
                            return LanguageMgr.GetText("SOY_AND_FRIED_CHICKEN");
                        case ChickenSpicy.Hell:
                            return LanguageMgr.GetText("HELL_AND_FRIED_CHICKEN");
                        case ChickenSpicy.Prinkle:
                            return LanguageMgr.GetText("PRINKLE_AND_FRIED_CHICKEN");
                        case ChickenSpicy.BBQ:
                            return LanguageMgr.GetText("BBQ_AND_FRIED_CHICKEN");
                    }
                }
                break;
            case ChickenSpicy.Hot:
                {
                    switch (spicy1)
                    {
                        case ChickenSpicy.Hot:
                            return LanguageMgr.GetText("HOT_SPICY_CHICKEN");
                        case ChickenSpicy.Soy:
                            return LanguageMgr.GetText("HOT_AND_SOY_CHICKEN");
                        case ChickenSpicy.Hell:
                            return LanguageMgr.GetText("HELL_AND_HOT_CHICKEN");
                        case ChickenSpicy.Prinkle:
                            return LanguageMgr.GetText("HOT_AND_PRINKLE_CHICKEN");
                        case ChickenSpicy.BBQ:
                            return LanguageMgr.GetText("HOT_AND_BBQ_CHICKEN");
                    }
                }
                break;
            case ChickenSpicy.Soy:
                {
                    switch (spicy1)
                    {
                        case ChickenSpicy.Soy:
                            return LanguageMgr.GetText("SOY_CHICKEN");
                        case ChickenSpicy.Hell:
                            return LanguageMgr.GetText("HELL_AND_SOY_CHICKEN");
                        case ChickenSpicy.Prinkle:
                            return LanguageMgr.GetText("SOY_AND_PRINKLE_CHICKEN");
                        case ChickenSpicy.BBQ:
                            return LanguageMgr.GetText("SOY_AND_BBQ_CHICKEN");
                    }
                }
                break;
            case ChickenSpicy.Hell:
                {
                    switch (spicy1)
                    {
                        case ChickenSpicy.Hell:
                            return LanguageMgr.GetText("HELL_CHICKEN");
                        case ChickenSpicy.Prinkle:
                            return LanguageMgr.GetText("PRINKLE_AND_HELL_CHICKEN");
                        case ChickenSpicy.BBQ:
                            return LanguageMgr.GetText("BBQ_AND_HELL_CHICKEN");
                    }
                }
                break;
            case ChickenSpicy.Prinkle:
                {
                    switch (spicy1)
                    {
                        case ChickenSpicy.Prinkle:
                            return LanguageMgr.GetText("PRINKLE_CHICKEN");
                        case ChickenSpicy.BBQ:
                            return LanguageMgr.GetText("PRINKLE_AND_BBQ_CHICKEN");
                    }
                }
                break;
            case ChickenSpicy.BBQ:
                {
                    switch (spicy1)
                    {
                        case ChickenSpicy.BBQ:
                            return LanguageMgr.GetText("BBQ_CHICKEN");
                    }
                }
                break;
        }
        return string.Empty;
    }


    public GuestReviews MenuPoint(GuestData pGuestData, 
        ChickenSpicy pSpicy0, ChickenSpicy pSpicy1, ChickenState pChickenState, Drink pDrink, SideMenu pSideMenu)
    {
        GameMgr gameMgr = GameMgr.Instance;

        //손님이 생각한 치킨 점수
        int maxPoint = 2;
        if (drink != Drink.None)
            maxPoint++;
        if (sideMenu != SideMenu.None)
            maxPoint++;

        int point = 0;

        //--------------------------------------------------------------------------------
        //양념 검사
        ChickenSpicy spicy0 = (ChickenSpicy)Mathf.Min((int)pSpicy0, (int)pSpicy1);
        ChickenSpicy spicy1 = (ChickenSpicy)Mathf.Max((int)pSpicy0, (int)pSpicy1);
        if (spicy0 == chickenSpicy[0])
            point++;
        if (spicy1 == chickenSpicy[1])
            point++;

        //--------------------------------------------------------------------------------
        //사이드나 드링크 검사
        if (drink == pDrink && drink != Drink.None)
            point += 1;
        if (sideMenu == pSideMenu && sideMenu != SideMenu.None)
            point += 1;

        if (pChickenState == ChickenState.BadChicken_0)
            point -= 2;
        else if (pChickenState == ChickenState.BadChicken_1)
            point -= 1;
        else if (pChickenState == ChickenState.BadChicken_2)
            point -= 2;
        else if (pChickenState == ChickenState.NotCook)
            point -= 2;

        if (maxPoint == 4)
        {
            if(point == 4)
                return GuestReviews.Happy;
            else if (point == 3 || point == 2)
                return GuestReviews.Normal;
            else
                return GuestReviews.Bad;
        }
        else if (maxPoint == 3)
        {
            if (point == 3)
                return GuestReviews.Happy;
            else if (point == 2 || point == 1)
                return GuestReviews.Normal;
            else
                return GuestReviews.Bad;
        }
        else
        {
            if (point == 2)
                return GuestReviews.Happy;
            else if (point == 1)
                return GuestReviews.Normal;
            else
                return GuestReviews.Bad;
        }
    }
}
