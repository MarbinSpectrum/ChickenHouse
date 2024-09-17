using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DrinkData", menuName = "ScriptableObjects/Drink", order = 6)]

public class DrinkData : ScriptableObject
{
    [Header("이미지")]
    public Sprite img;
    [Header("이름")]
    public string nameKey;
    [Header("설명")]
    public string infoKey;
    [Header("판매가격")]
    public int price;
    [Header("원가")]
    public int cost;
}
