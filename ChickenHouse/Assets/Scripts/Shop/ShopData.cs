using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ShopData", menuName = "ScriptableObjects/Shop", order = 2)]
public class ShopData : ScriptableObject
{
    public Sprite   icon;
    public string   nameKey;
    public string   infoKey;
    public int      money;
}