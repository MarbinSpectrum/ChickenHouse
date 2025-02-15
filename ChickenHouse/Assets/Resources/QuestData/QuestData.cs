using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "QuestData", menuName = "ScriptableObjects/Quest", order = 4)]

public class QuestData : ScriptableObject
{
    [Header("퀘스트 종류")]
    public Quest quest;

    [Header("퀘스트 이름")]
    public string questNameKey;
    [Header("퀘스트 내용")]
    public string questInfoKey;
    [Header("퀘스트 요약")]
    public string questSummaryKey;
    [Header("다음 퀘스트")]
    public Quest nextQuest;
    [Header("보상")]
    public List<ShopItem> rewards;
}
