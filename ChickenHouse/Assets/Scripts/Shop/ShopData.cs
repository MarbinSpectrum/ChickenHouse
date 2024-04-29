using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ShopData", menuName = "ScriptableObjects/Shop", order = 2)]
public class ShopData : ScriptableObject
{
    /** ��ų ������ **/
    public Sprite   icon;

    /** ��ų �̸� KEY **/
    public string   nameKey;

    /** ��ų ���� KEY **/
    public string   infoKey;

    /**���׷��̵� ���� **/
    public int      money;
}