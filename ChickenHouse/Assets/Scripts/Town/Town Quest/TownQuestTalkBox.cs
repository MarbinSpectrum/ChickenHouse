using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TownQuestTalkBox : Mgr
{
    [SerializeField] private RectTransform notQuest;
    [SerializeField] private RectTransform runQuest;
    [SerializeField] private RectTransform completeQuest;

    public void SetState(QuestState pQuestState)
    {
        notQuest.gameObject.SetActive(false);
        runQuest.gameObject.SetActive(false);
        completeQuest.gameObject.SetActive(false);

        switch (pQuestState)
        {
            case QuestState.Not:
                notQuest.gameObject.SetActive(true);
                break;
            case QuestState.Run:
                runQuest.gameObject.SetActive(true);
                break;
            case QuestState.Complete:
                completeQuest.gameObject.SetActive(true);
                break;
        }
    }
}
