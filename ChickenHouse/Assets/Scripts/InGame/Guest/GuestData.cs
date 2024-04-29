using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "GuestData", menuName = "ScriptableObjects/Guest", order = 1)]
public class GuestData : ScriptableObject
{
    [Header("등장 날짜")]
    public int day;
    [Header("주문 메뉴")]
    public List<GuestMenu> goodChicken;
}

[System.Serializable]
public class GuestMenu
{
    /** 원하는 치킨 맛 **/
    public ChickenSpicy spicy0;
    public ChickenSpicy spicy1;

    /** 무조건 시키는 음료수 **/
    public Drink        drink;
    /** 무조건 시키는 사이드 메뉴 **/
    public SideMenu     sideMenu;

    public bool CanMakeChicken()
    {
        PlayData playData = GameMgr.Instance.playData;
        if ((spicy0 == ChickenSpicy.Soy || spicy1 == ChickenSpicy.Soy) &&
            playData.hasItem[(int)ShopItem.Recipe_1] == false)
            return false;
        if ((spicy0 == ChickenSpicy.Hell || spicy1 == ChickenSpicy.Hell) &&
            playData.hasItem[(int)ShopItem.Recipe_2] == false)
            return false;
        if ((spicy0 == ChickenSpicy.Prinkle || spicy1 == ChickenSpicy.Prinkle) &&
            playData.hasItem[(int)ShopItem.Recipe_3] == false)
            return false;
        if ((spicy0 == ChickenSpicy.Carbonara || spicy1 == ChickenSpicy.Carbonara) &&
            playData.hasItem[(int)ShopItem.Recipe_4] == false)
            return false;
        if ((spicy0 == ChickenSpicy.BBQ || spicy1 == ChickenSpicy.BBQ) &&
            playData.hasItem[(int)ShopItem.Recipe_5] == false)
            return false;

        return true;
    }
}