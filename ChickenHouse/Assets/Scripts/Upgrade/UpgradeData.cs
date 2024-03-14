using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "UpgradeData", menuName = "ScriptableObjects/Upgrade", order = 2)]
public class UpgradeData : ScriptableObject
{
    /** ��ų ������ **/
    public Sprite icon;

    /** ��ų �̸� KEY**/
    public string nameKey;

    /** ��ų ���� KEY**/
    public string infoKey;
}