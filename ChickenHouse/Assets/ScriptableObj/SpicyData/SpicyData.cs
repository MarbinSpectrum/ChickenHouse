using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpicyData", menuName = "ScriptableObjects/Spicy", order = 5)]
public class SpicyData : ScriptableObject
{
    [Header("양념 이미지")]
    public Sprite img;
    [Header("이름")]
    public string nameKey;
    [Header("설명")]
    public string infoKey;
    [Header("판매가격")]
    public int price;
    [Header("도감 설명")]
    public string bookInfoKey;
    [Header("경험치")]
    public int exp;
}
