using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpicyData", menuName = "ScriptableObjects/Spicy", order = 5)]
public class SpicyData : ScriptableObject
{
    [Header("��� �̹���")]
    public Sprite img;
    [Header("�̸�")]
    public string nameKey;
    [Header("����")]
    public string infoKey;
    [Header("�ǸŰ���")]
    public int price;
    [Header("���� ����")]
    public string bookInfoKey;
    [Header("����ġ")]
    public int exp;
}
