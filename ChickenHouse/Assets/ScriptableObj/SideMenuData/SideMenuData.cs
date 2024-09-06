using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SideMenuData", menuName = "ScriptableObjects/SideMenu", order = 7)]
public class SideMenuData : ScriptableObject
{
    [Header("�̹���")]
    public Sprite img;
    [Header("�̸�")]
    public string nameKey;
    [Header("����")]
    public string infoKey;
    [Header("�ǸŰ���")]
    public int price;
}