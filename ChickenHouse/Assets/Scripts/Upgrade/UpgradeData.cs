using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "UpgradeData", menuName = "ScriptableObjects/Upgrade", order = 2)]
public class UpgradeData : ScriptableObject
{
    /** 스킬 아이콘 **/
    public Sprite icon;

    /** 스킬 이름 KEY**/
    public string nameKey;

    /** 스킬 설명 KEY**/
    public string infoKey;
}