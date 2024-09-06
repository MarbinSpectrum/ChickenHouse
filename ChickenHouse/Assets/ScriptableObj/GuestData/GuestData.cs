using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "GuestData", menuName = "ScriptableObjects/Guest", order = 1)]
public class GuestData : ScriptableObject
{
    [Header("초상화")]
    public Sprite face;
    [Header("도감 표지")]
    public Sprite bookImg;
    [Header("도감 이름")]
    public string bookNameKey;
    [Header("도감 설명")]
    public string bookInfoKey;
    [Header("등장 날짜")]
    public int day;
    [Header("술을 마쉬는 캐릭터 여부")]
    public bool canDrinkAlcohol;
    [Header("주문 메뉴")]
    public List<GuestMenu> goodChicken;
}

[System.Serializable]
public class GuestMenu
{
    public int openDay;

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

        if (openDay > playData.day)
            return false;

        if ((spicy0 == ChickenSpicy.Hot || spicy1 == ChickenSpicy.Hot) &&
            playData.KitchenSetSpicy(ChickenSpicy.Hot) == false)
            return false;
        if ((spicy0 == ChickenSpicy.Soy || spicy1 == ChickenSpicy.Soy) &&
            playData.KitchenSetSpicy(ChickenSpicy.Soy) == false)
            return false;
        if ((spicy0 == ChickenSpicy.Hell || spicy1 == ChickenSpicy.Hell) &&
             playData.KitchenSetSpicy(ChickenSpicy.Hell) == false)
            return false;
        if ((spicy0 == ChickenSpicy.Prinkle || spicy1 == ChickenSpicy.Prinkle) &&
            playData.KitchenSetSpicy(ChickenSpicy.Prinkle) == false)
            return false;
        if ((spicy0 == ChickenSpicy.Carbonara || spicy1 == ChickenSpicy.Carbonara) &&
           playData.KitchenSetSpicy(ChickenSpicy.Carbonara) == false)
            return false;
        if ((spicy0 == ChickenSpicy.BBQ || spicy1 == ChickenSpicy.BBQ) &&
             playData.KitchenSetSpicy(ChickenSpicy.BBQ) == false)
            return false;

        if(drink != Drink.None)
            if (playData.KitchenSetDrink(drink) == false)
                return false;

        if(sideMenu != SideMenu.None)
            if (playData.KitchenSetSideMenu(sideMenu) == false)
                return false;

        return true;
    }

    public SideMenu GetRandomSideMenu()
    {
        PlayData playData = GameMgr.Instance.playData;

        //랜덤하게 아무 사이드 메뉴나 반환
        List<SideMenu> sideMenus = new List<SideMenu>();
        for (SideMenu sideMenu = SideMenu.Pickle; sideMenu < SideMenu.MAX; sideMenu++)
        {
            if (playData.KitchenSetSideMenu(sideMenu) == false)
                continue;
            sideMenus.Add(sideMenu);
        }
        if (sideMenus.Count == 0)
            return SideMenu.None;
        int r = Random.Range(0, sideMenus.Count);
        return sideMenus[r];
    }

    public Drink GetRandomDrink(bool canDrinkAlcohol)
    {
        PlayData playData = GameMgr.Instance.playData;

        //랜덤하게 아무 음료나 반환
        List<Drink> drinks = new List<Drink>();
        for (Drink drink = Drink.Cola; drink < Drink.MAX; drink++)
        {
            if (playData.KitchenSetDrink(drink) == false)
                continue;
            bool isAlcohol = SubMenuMgr.Instance.IsAlcohol(drink);
            if (isAlcohol)
            {
                if (canDrinkAlcohol)
                    drinks.Add(drink);
                continue;
            }
            else
            {
                drinks.Add(drink);
            }
        }
        if (drinks.Count == 0)
            return Drink.None;
        int r = Random.Range(0, drinks.Count);
        return drinks[r];
    }
}