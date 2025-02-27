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
    [SerializeField] private RectTransform      questRewardRect;
    [SerializeField] private TextMeshProUGUI    questReward;

    [SerializeField] private RectTransform      questListContent;
    [SerializeField] private DiaryUI_QuestListObj    questObj;

    private QuestData                       selectQuest     = null;
    private List<QuestData>                 quest           = new List<QuestData>();
    private List<DiaryUI_QuestListObj>      questListObj    = new List<DiaryUI_QuestListObj>();

    public void SetUI()
    {
        selectQuest = null;
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
        List<QuestData> sideQuest = new List<QuestData>();
        for (int i = 0; i < playData.quest.Length; i++)
        {
            if ((QuestState)playData.quest[i] != QuestState.Run)
            {
                //진행중이 아닌 퀘스트
                continue;
            }

            //진행중인 퀘스트 등록
            Quest q = (Quest)i;
            QuestData questData = questMgr.GetQuestData(q);
            if (QuestMgr.IsMainQuest(q))
                quest.Add(questData);
            else
                sideQuest.Add(questData);
        }
        for(int i = 0; i < sideQuest.Count; i++)
            quest.Add(sideQuest[i]);

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
            QuestData questData = quest[i];

            int idx = i;
            questListObj[i].gameObject.SetActive(true);
            questListObj[i].SetData(quest[i], isSelect, isEnd);
            questListObj[i].SetEventTrigger(() =>
            {
                SetUI(questData);
                ShowQuestInfo(questData);
                gameMgr.playData.questCheck[(int)questData.quest] = true;
                questListObj[idx].UpdateRedDot();
            });

            if(questData == pQuest)
            {
                gameMgr.playData.questCheck[(int)questData.quest] = true;
                questListObj[idx].UpdateRedDot();
            }
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
        string rewardText = string.Empty;
        QuestData questData = questMgr.GetQuestData(pQuest.quest);
        for (int i = 0; i < questData.rewards.Count; i++)
        {
            ShopItem shopItem = questData.rewards[i];
            if (shopItem == ShopItem.None)
                continue;
            ShopData shopData = shopMgr.GetShopData(shopItem);
            string shopItemStr = LanguageMgr.GetText(shopData.nameKey);
            rewardText += shopItemStr;
            if (i + 1 != questData.rewards.Count)
                rewardText += ", ";
        }
        switch (pQuest.quest)
        {
            case Quest.MainQuest_1:
                {
                    int day = Mathf.Max(0, QuestMgr.MAIN_QUEST_1_LIMIT_DAY - gameMgr.playData.day + 1);
                    strDetail += string.Format(LanguageMgr.GetText("QUEST_DETAIL_1"), day);
                    LanguageMgr.SetText(questDetail, strDetail);

                    questRewardRect.gameObject.SetActive(false);
                }
                break;
            case Quest.Event_0_Quest:
                {
                    LanguageMgr.SetText(questDetail, string.Empty);

                    LanguageMgr.SetText(questReward, rewardText);
                    questRewardRect.gameObject.SetActive(true);
                }
                break;
            case Quest.SpicyQuest_1:
                {
                    int nowChicken = Mathf.Min(QuestMgr.SPICY_QUEST_1_CNT, gameMgr.playData.questCnt[(int)Quest.SpicyQuest_1]);
                    strDetail += string.Format(LanguageMgr.GetText("QUEST_DETAIL_2"), nowChicken, QuestMgr.SPICY_QUEST_1_CNT);
                    LanguageMgr.SetText(questDetail, strDetail);

                    questRewardRect.gameObject.SetActive(true);
                    LanguageMgr.SetText(questReward, rewardText);
                }
                break;
            case Quest.SpicyQuest_2:
                {
                    int nowChicken = Mathf.Min(QuestMgr.SPICY_QUEST_2_CNT, gameMgr.playData.questCnt[(int)Quest.SpicyQuest_2]);
                    strDetail += string.Format(LanguageMgr.GetText("QUEST_DETAIL_2"), nowChicken, QuestMgr.SPICY_QUEST_2_CNT);
                    LanguageMgr.SetText(questDetail, strDetail);

                    LanguageMgr.SetText(questReward, rewardText);
                    questRewardRect.gameObject.SetActive(true);
                }
                break;
            case Quest.SeaOtterQuest:
                {
                    int nowProfit = Mathf.Min(QuestMgr.SEA_OTTER_QUEST_TARGET_MONEY, gameMgr.playData.questCnt[(int)Quest.SeaOtterQuest]);
                    strDetail += string.Format(LanguageMgr.GetText("QUEST_DETAIL_5"), nowProfit, QuestMgr.SEA_OTTER_QUEST_TARGET_MONEY);
                    LanguageMgr.SetText(questDetail, strDetail);

                    LanguageMgr.SetString(questReward, "SEAOTTER_QUEST_REWARD");
                    questRewardRect.gameObject.SetActive(true);
                }
                break;
            case Quest.DrinkQuest_1:
                {
                    int colaCnt = Mathf.Min(QuestMgr.DRINK_QUEST_1_CNT, gameMgr.playData.questCnt[(int)Quest.DrinkQuest_1]);
                    strDetail += string.Format(LanguageMgr.GetText("QUEST_DETAIL_3"), colaCnt, QuestMgr.DRINK_QUEST_1_CNT);
                    LanguageMgr.SetText(questDetail, strDetail);

                    LanguageMgr.SetText(questReward, rewardText);
                    questRewardRect.gameObject.SetActive(true);
                }
                break;
            case Quest.DrinkQuest_2:
                {
                    int beerCnt = Mathf.Min(QuestMgr.DRINK_QUEST_2_CNT, gameMgr.playData.questCnt[(int)Quest.DrinkQuest_2]);
                    strDetail += string.Format(LanguageMgr.GetText("QUEST_DETAIL_4"), beerCnt, QuestMgr.DRINK_QUEST_2_CNT);
                    LanguageMgr.SetText(questDetail, strDetail);

                    LanguageMgr.SetText(questReward, rewardText);
                    questRewardRect.gameObject.SetActive(true);
                }
                break;
        }
    }
}
