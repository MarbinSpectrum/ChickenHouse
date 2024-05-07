using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequireMenu
{
    //�մ��� ���ϴ� �޴��� ���� ������ ��� ��ü�Դϴ�.

    /** ��Ŭ�� �䱸�� Ȯ�� **/
    private const int PICKLE_PER = 40;

    /** �ݶ� �䱸�� Ȯ�� **/
    private const int COLA_PER = 40;

    /** ���ϴ� ġŲ �ҽ� **/
    public ChickenSpicy[] chickenSpicy { get; private set; } = new ChickenSpicy[2];
    /** �ݶ� �ʿ� ���� **/
    public Drink drink { get; private set; }
    /** ��Ŭ �ʿ� ���� **/
    public SideMenu sideMenu { get; private set; }

    public int menuIdx { get; private set; }

    public bool CreateMenu(GuestData pGuestData, float nowTime,bool isTuto)
    {
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
                if (randomValue < COLA_PER)
                    drink = Drink.Cola;
            }

            //���� ���̵� �޴��� ������ �����ϰ� ���Ѵ�.
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

        //�ֹ��� ġŲ �̸� ��ȯ
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
