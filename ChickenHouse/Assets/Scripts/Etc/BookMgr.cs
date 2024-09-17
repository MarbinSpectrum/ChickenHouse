using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookMgr : AwakeSingleton<BookMgr>
{
    private const string GUEST_KEY = "GUEST_{0}";
    private const string SPICY_KEY = "SPICY_{0}";

    protected override void Awake()
    {
        SetSingleton();

        ActSpicyData(ChickenSpicy.Hot);
    }

    public bool IsActGuest(Guest pGuest)
    {
        int isAct = PlayerPrefs.GetInt(string.Format(GUEST_KEY, pGuest));
        return isAct == 1;
    }

    public void ActGuestData(Guest pGuest)
    {
        PlayerPrefs.SetInt(string.Format(GUEST_KEY, pGuest), 1);
    }

    public bool IsActSpicy(ChickenSpicy pChickenSpicy)
    {
        int isAct = PlayerPrefs.GetInt(string.Format(SPICY_KEY, pChickenSpicy));
        return isAct == 1;
    }

    public void ActSpicyData(ChickenSpicy pChickenSpicy)
    {
        PlayerPrefs.SetInt(string.Format(SPICY_KEY, pChickenSpicy), 1);
    }
}
