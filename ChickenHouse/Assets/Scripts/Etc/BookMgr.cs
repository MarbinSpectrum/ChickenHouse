using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookMgr : AwakeSingleton<BookMgr>
{
    private const string GUEST_KEY = "GUEST_{0}";
    private const string SPICY_KEY = "SPICY_{0}";
    private const string DRINK_KEY = "DRINK_{0}";
    private const string SIDEMENU_KEY = "SIDEMENU_{0}";

    protected override void Awake()
    {
        SetSingleton();

        ActSpicyData(ChickenSpicy.Hot);
        ActDrinkData(Drink.Cola);
        ActSideMenuData(SideMenu.ChickenRadish);
    }

    public static bool IsActGuest(Guest pGuest)
    {
        int isAct = PlayerPrefs.GetInt(string.Format(GUEST_KEY, pGuest));
        return isAct == 1;
    }

    public static void ActGuestData(Guest pGuest)
    {
        PlayerPrefs.SetInt(string.Format(GUEST_KEY, pGuest), 1);
    }

    public static bool IsActSpicy(ChickenSpicy pChickenSpicy)
    {
        int isAct = PlayerPrefs.GetInt(string.Format(SPICY_KEY, pChickenSpicy));
        return isAct == 1;
    }

    public static void ActSpicyData(ChickenSpicy pChickenSpicy)
    {
        PlayerPrefs.SetInt(string.Format(SPICY_KEY, pChickenSpicy), 1);
    }

    public static bool IsActDrink(Drink pDrink)
    {
        int isAct = PlayerPrefs.GetInt(string.Format(DRINK_KEY, pDrink));
        return isAct == 1;
    }

    public static void ActDrinkData(Drink pDrink)
    {
        PlayerPrefs.SetInt(string.Format(DRINK_KEY, pDrink), 1);
    }

    public static bool IsActSideMenu(SideMenu pSideMenu)
    {
        int isAct = PlayerPrefs.GetInt(string.Format(SIDEMENU_KEY, pSideMenu));
        return isAct == 1;
    }

    public static void ActSideMenuData(SideMenu pSideMenu)
    {
        PlayerPrefs.SetInt(string.Format(SIDEMENU_KEY, pSideMenu), 1);
    }
}
