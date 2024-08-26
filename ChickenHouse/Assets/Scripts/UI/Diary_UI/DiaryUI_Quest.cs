using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DiaryUI_Quest : Mgr
{
    [SerializeField] private RectTransform[]    questRect;

    [SerializeField] private TextMeshProUGUI    questName;
    [SerializeField] private TextMeshProUGUI    questInfo;
    [SerializeField] private TextMeshProUGUI    questSummary;
    [SerializeField] private TextMeshProUGUI    questDetail;

    [SerializeField] private RectTransform      questListContent;
    [SerializeField] private DiaryUI_QuestListObj    questObj;

    private QuestData                       selectQuest     = null;
    private List<QuestData>                 quest           = new List<QuestData>();
    private List<DiaryUI_QuestListObj>      questListObj    = new List<DiaryUI_QuestListObj>();

    public void SetUI()
    {
        RefreshQuestList();

        QuestData firstQuest = quest.Count > 0 ? quest[0] : null;
        SetUI(firstQuest);
        ShowQuestInfo(firstQuest);
    }

    private void RefreshQuestList()
    {
        PlayData playData = gameMgr?.playData;
        if (playData == null)
            return;

        quest.Clear();
        for (int i = 0; i < playData.quest.Length; i++)
        {
            if (playData.quest[i] != 1)
            {
                //진행중이 아닌 퀘스트
                continue;
            }

            //진행중인 퀘스트 등록
            Quest q = (Quest)i;
            QuestData questData = questMgr.GetQuestData(q);
            quest.Add(questData);
        }
    }

    public void SetState(bool state)
    {
        for (int i = 0; i < questRect.Length; i++)
            questRect[i].gameObject.SetActive(state);
    }

    public void SetUI(QuestData pQuest)
    {
        //pQuest를 선택한 상태

        if (pQuest == selectQuest)
            return;

        RefreshQuestList();

        selectQuest = pQuest;

        questListObj.ForEach((x) => x.gameObject.SetActive(false));
        for (int i = 0; i < quest.Count; i++)
        {
            if (i >= questListObj.Count)
            {
                DiaryUI_QuestListObj questList = Instantiate(questObj, questListContent);
                questListObj.Add(questList);
            }

            bool isEnd = ((i + 1) == quest.Count);
            bool isSelect = (selectQuest == quest[i]);
            questListObj[i].gameObject.SetActive(true);
            questListObj[i].SetData(quest[i], isSelect, isEnd);
            questListObj[i].SetEventTrigger(() =>
            {
                SetUI(quest[i]);
                ShowQuestInfo(quest[i]);
            });
        }
    }

    private void ShowQuestInfo(QuestData pQuest)
    {
        if (pQuest == null)
            return;

        LanguageMgr.SetString(questName, pQuest.questNameKey);
        LanguageMgr.SetString(questInfo, pQuest.questInfoKey);
        LanguageMgr.SetString(questSummary, pQuest.questSummaryKey);

        string strDetail = string.Empty;
        switch(pQuest.quest)
        {
            case Quest.MainQuest_1:
                {
                    int day = Mathf.Max(0, 7 - gameMgr.playData.day + 1);
                    strDetail += string.Format(LanguageMgr.GetText("QUEST_DETAIL_1"), day);
                    strDetail += "\n";

                    long money = (int)Mathf.Min(GameMgr.TARGET_MONEY_1, gameMgr.playData.money);
                    strDetail += string.Format(LanguageMgr.GetText("QUEST_DETAIL_2"), money, GameMgr.TARGET_MONEY_1);

                    LanguageMgr.SetText(questDetail, strDetail);
                }
                break;
        }
    }
}
