using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequireMenu
{
    //손님이 원하는 메뉴에 대한 정보가 담긴 객체입니다.

    /** 피클을 요구할 확률 **/
    private const int SIDEMENU_PER = 40;

    /** 콜라를 요구할 확률 **/
    private const int DRINK_PER = 40;

    /** 원하는 치킨 소스 **/
    public ChickenSpicy[] chickenSpicy { get; private set; } = new ChickenSpicy[2];
    /** 콜라 필요 여부 **/
    public Drink drink { get; private set; }
    /** 피클 필요 여부 **/
    public SideMenu sideMenu { get; private set; }

    public int menuIdx { get; private set; }

    public bool CreateMenu(GuestData pGuestData, float nowTime, bool isTuto)
    {
        chickenSpicy ??= new ChickenSpicy[2];
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
                if (randomValue < DRINK_PER)
                    drink = guestMenu.GetRandomDrink(pGuestData.canDrinkAlcohol,pGuestData.anythingDrink);
            }

            //고정 사이드 메뉴가 없으면 랜덤하게 정한다.
            sideMenu = guestMenu.sideMenu;
            if (sideMenu == SideMenu.None)
            {
                int randomValue = Random.Range(0, 100);
                if (randomValue < SIDEMENU_PER)
                    sideMenu = guestMenu.GetRandomSideMenu();
            }

            return true;
        }
    }

    public GuestReviews MenuPoint(ChickenSpicy pSpicy0, ChickenSpicy pSpicy1, 
        ChickenState pChickenState, Drink pDrink, SideMenu pSideMenu)
    {
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
        if (CheckDrink(drink))
            point += 1;
        if (CheckSide(sideMenu))
            point += 1;

        //치킨상태검사
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

    public bool CheckChickenState(ChickenState pChickenState)
    {
        //치킨 검사
        if (pChickenState == ChickenState.GoodChicken)
            return true;
        return false;
    }

    public bool CheckSpicy(ChickenSpicy pSpicy0, ChickenSpicy pSpicy1)
    {
        //양념 검사
        ChickenSpicy spicy0 = (ChickenSpicy)Mathf.Min((int)pSpicy0, (int)pSpicy1);
        ChickenSpicy spicy1 = (ChickenSpicy)Mathf.Max((int)pSpicy0, (int)pSpicy1);
        if (spicy0 == chickenSpicy[0] && spicy1 == chickenSpicy[1])
            return true;
        return false;
    }

    public bool CheckDrink(Drink pDrink)
    {
        //드링크 검사
        if (drink == pDrink && drink != Drink.None)
            return true;
        if (drink == Drink.None)
            return true;
        if (drink == Drink.Anything)
            return true;
        return false;
    }

    public bool CheckSide(SideMenu pSideMenu)
    {
        //사이드 검사
        if (sideMenu == pSideMenu && sideMenu != SideMenu.None)
            return true;
        if (sideMenu == SideMenu.None)
            return true;
        return false;
    }
}
