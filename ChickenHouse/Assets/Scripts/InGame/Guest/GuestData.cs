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
}