using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "GuestData", menuName = "ScriptableObjects/Guest", order = 1)]
public class GuestData : ScriptableObject
{
    [Header("�ʻ�ȭ")]
    public Sprite face;
    [Header("���� ǥ��")]
    public Sprite bookImg;
    [Header("���� �̸�")]
    public string bookNameKey;
    [Header("���� ����")]
    public string bookInfoKey;
    [Header("���� ��¥")]
    public int day;
    [Header("���� ������ ĳ���� ����")]
    public bool canDrinkAlcohol;
    [Header("�ƹ��ų� ������ ĳ���� ����")]
    public bool anythingDrink;
    [Header("�ֹ� �޴�")]
    public List<GuestMenu> goodChicken;
}

[System.Serializable]
public class GuestMenu
{
    public int openDay;

    /** ���ϴ� ġŲ �� **/
    public ChickenSpicy spicy0;
    public ChickenSpicy spicy1;

    /** ������ ��Ű�� ����� **/
    public Drink        drink;
    /** ������ ��Ű�� ���̵� �޴� **/
    public SideMenu     sideMenu;

    public bool CanMakeChicken()
    {
        PlayData playData = GameMgr.Instance.playData;
        int playDataDay = 1;
        if (playData != null)
            playDataDay = playData.day;

        if (openDay > playDataDay)
            return false;

        if (spicy0 == ChickenSpicy.Hot || spicy1 == ChickenSpicy.Hot)
            if (playData == null || playData.KitchenSetSpicy(ChickenSpicy.Hot) == false)
                return false;
        if (spicy0 == ChickenSpicy.Soy || spicy1 == ChickenSpicy.Soy)
            if (playData == null || playData.KitchenSetSpicy(ChickenSpicy.Soy) == false)
                return false;
        if (spicy0 == ChickenSpicy.Hell || spicy1 == ChickenSpicy.Hell)
            if (playData == null || playData.KitchenSetSpicy(ChickenSpicy.Hell) == false)
                return false;
        if (spicy0 == ChickenSpicy.Prinkle || spicy1 == ChickenSpicy.Prinkle)
            if (playData == null || playData.KitchenSetSpicy(ChickenSpicy.Prinkle) == false)
                return false;
        if (spicy0 == ChickenSpicy.Carbonara || spicy1 == ChickenSpicy.Carbonara)
            if (playData == null || playData.KitchenSetSpicy(ChickenSpicy.Carbonara) == false)
                return false;
        if (spicy0 == ChickenSpicy.BBQ || spicy1 == ChickenSpicy.BBQ)
            if (playData == null || playData.KitchenSetSpicy(ChickenSpicy.BBQ) == false)
                return false;

        if (drink != Drink.None)
            if (playData == null || playData.KitchenSetDrink(drink) == false)
                return false;

        if(sideMenu != SideMenu.None)
            if (playData == null || playData.KitchenSetSideMenu(sideMenu) == false)
                return false;

        return true;
    }

    public SideMenu GetRandomSideMenu()
    {
        PlayData playData = GameMgr.Instance.playData;

        //�����ϰ� �ƹ� ���̵� �޴��� ��ȯ
        List<SideMenu> sideMenus = new List<SideMenu>();
        for (SideMenu sideMenu = SideMenu.ChickenRadish; sideMenu < SideMenu.MAX; sideMenu++)
        {
            if (playData == null || playData.KitchenSetSideMenu(sideMenu) == false)
                continue;
            sideMenus.Add(sideMenu);
        }
        if (sideMenus.Count == 0)
            return SideMenu.None;
        int r = Random.Range(0, sideMenus.Count);
        return sideMenus[r];
    }

    public Drink GetRandomDrink(bool canDrinkAlcohol, bool canAnything)
    {
        if (canAnything)
            return Drink.Anything;

        PlayData playData = GameMgr.Instance.playData;

        //�����ϰ� �ƹ� ���ᳪ ��ȯ
        List<Drink> drinks = new List<Drink>();
        for (Drink drink = Drink.Cola; drink < Drink.MAX; drink++)
        {
            if (playData == null || playData.KitchenSetDrink(drink) == false)
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