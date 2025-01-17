using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequireMenu
{
    //�մ��� ���ϴ� �޴��� ���� ������ ��� ��ü�Դϴ�.

    /** ��Ŭ�� �䱸�� Ȯ�� **/
    private const int SIDEMENU_PER = 40;

    /** �ݶ� �䱸�� Ȯ�� **/
    private const int DRINK_PER = 40;

    /** ���ϴ� ġŲ �ҽ� **/
    public ChickenSpicy[] chickenSpicy { get; private set; } = new ChickenSpicy[2];
    /** �ݶ� �ʿ� ���� **/
    public Drink drink { get; private set; }
    /** ��Ŭ �ʿ� ���� **/
    public SideMenu sideMenu { get; private set; }

    public int menuIdx { get; private set; }

    public bool CreateMenu(GuestData pGuestData, float nowTime, bool isTuto)
    {
        chickenSpicy ??= new ChickenSpicy[2];
        if (isTuto)
        {
            menuIdx = 0;
            GuestMenu guestMenu = pGuestData.goodChicken[menuIdx];

            //ġŲ �� ����
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

            //ġŲ �� ����
            ChickenSpicy spicy0 = (ChickenSpicy)Mathf.Min((int)guestMenu.spicy0, (int)guestMenu.spicy1);
            ChickenSpicy spicy1 = (ChickenSpicy)Mathf.Max((int)guestMenu.spicy0, (int)guestMenu.spicy1);
            chickenSpicy[0] = spicy0;
            chickenSpicy[1] = spicy1;

            //���� ���ᰡ ������ �����ϰ� ���Ѵ�.
            drink = guestMenu.drink;
            if (drink == Drink.None)
            {
                int randomValue = Random.Range(0, 100);
                if (randomValue < DRINK_PER)
                    drink = guestMenu.GetRandomDrink(pGuestData.canDrinkAlcohol,pGuestData.anythingDrink);
            }

            //���� ���̵� �޴��� ������ �����ϰ� ���Ѵ�.
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
        //�մ��� ������ ġŲ ����
        int maxPoint = 2;
        if (drink != Drink.None)
            maxPoint++;
        if (sideMenu != SideMenu.None)
            maxPoint++;

        int point = 0;

        //--------------------------------------------------------------------------------
        //��� �˻�
        ChickenSpicy spicy0 = (ChickenSpicy)Mathf.Min((int)pSpicy0, (int)pSpicy1);
        ChickenSpicy spicy1 = (ChickenSpicy)Mathf.Max((int)pSpicy0, (int)pSpicy1);
        if (spicy0 == chickenSpicy[0])
            point++;
        if (spicy1 == chickenSpicy[1])
            point++;

        //--------------------------------------------------------------------------------
        //���̵峪 �帵ũ �˻�
        if (CheckDrink(drink))
            point += 1;
        if (CheckSide(sideMenu))
            point += 1;

        //ġŲ���°˻�
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
        //ġŲ �˻�
        if (pChickenState == ChickenState.GoodChicken)
            return true;
        return false;
    }

    public bool CheckSpicy(ChickenSpicy pSpicy0, ChickenSpicy pSpicy1)
    {
        //��� �˻�
        ChickenSpicy spicy0 = (ChickenSpicy)Mathf.Min((int)pSpicy0, (int)pSpicy1);
        ChickenSpicy spicy1 = (ChickenSpicy)Mathf.Max((int)pSpicy0, (int)pSpicy1);
        if (spicy0 == chickenSpicy[0] && spicy1 == chickenSpicy[1])
            return true;
        return false;
    }

    public bool CheckDrink(Drink pDrink)
    {
        //�帵ũ �˻�
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
        //���̵� �˻�
        if (sideMenu == pSideMenu && sideMenu != SideMenu.None)
            return true;
        if (sideMenu == SideMenu.None)
            return true;
        return false;
    }
}
