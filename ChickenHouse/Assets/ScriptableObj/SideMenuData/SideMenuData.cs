using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SideMenuData", menuName = "ScriptableObjects/SideMenu", order = 7)]
public class SideMenuData : ScriptableObject
{
    [Header("이미지")]
    public Sprite img;
    [Header("이름")]
    public string nameKey;
    [Header("설명")]
    public string infoKey;
    [Header("판매가격")]
    public int price;
}