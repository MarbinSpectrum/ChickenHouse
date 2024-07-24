using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestMgr : AwakeSingleton<QuestMgr>
{
    [SerializeField] private Dictionary<Quest, QuestData> questData = new Dictionary<Quest, QuestData>();

    public QuestData GetQuestData(Quest pQuest)
    {
        //상점 정보 얻기
        if (questData.ContainsKey(pQuest))
            return questData[pQuest];
        return null;
    }
}
