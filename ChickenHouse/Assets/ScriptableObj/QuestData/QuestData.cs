using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "QuestData", menuName = "ScriptableObjects/Quest", order = 4)]

public class QuestData : ScriptableObject
{
    [Header("����Ʈ ����")]
    public Quest quest;

    [Header("����Ʈ �̸�")]
    public string questNameKey;
    [Header("����Ʈ ����")]
    public string questInfoKey;
    [Header("����Ʈ ���")]
    public string questSummaryKey;
    [Header("���� ����Ʈ")]
    public Quest nextQuest;
    [Header("����")]
    public List<ShopItem> rewards;
}
