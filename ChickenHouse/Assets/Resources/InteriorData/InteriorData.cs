using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InteriorData", menuName = "ScriptableObjects/Interior", order = 8)]
public class InteriorData : ScriptableObject
{
    [Header("아이콘")]
    public Sprite img;
    [Header("오브젝트 이미지")]
    public Sprite objImg1;
    public Sprite objImg2;

    [Header("이름")]
    public string nameKey;
    [Header("판매가격")]
    public int price;
}
