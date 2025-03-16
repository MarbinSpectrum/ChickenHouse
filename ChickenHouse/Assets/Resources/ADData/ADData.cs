using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ADData", menuName = "ScriptableObjects/AD", order = 9)]
public class ADData : ScriptableObject
{
    [Header("아이콘")]
    public Sprite img;
    [Header("이름")]
    public string nameKey;
    [Header("설명")]
    public string infoKey;
    [Header("판매가격")]
    public int price;
}
