using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ShopData", menuName = "ScriptableObjects/Shop", order = 2)]
public class ShopData : ScriptableObject
{
    /** 스킬 아이콘 **/
    public Sprite   icon;

    /** 스킬 이름 KEY **/
    public string   nameKey;

    /** 스킬 설명 KEY **/
    public string   infoKey;

    /**업그레이드 가격 **/
    public int      money;
}