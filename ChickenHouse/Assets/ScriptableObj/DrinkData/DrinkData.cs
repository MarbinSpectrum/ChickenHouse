using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DrinkData", menuName = "ScriptableObjects/Drink", order = 6)]

public class DrinkData : ScriptableObject
{
    [Header("�̹���")]
    public Sprite img;
    [Header("�̸�")]
    public string nameKey;
    [Header("����")]
    public string infoKey;
    [Header("�ǸŰ���")]
    public int price;
    [Header("����")]
    public int cost;
}
