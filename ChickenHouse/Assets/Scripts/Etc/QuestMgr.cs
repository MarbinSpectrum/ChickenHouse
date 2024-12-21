using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestMgr : AwakeSingleton<QuestMgr>
{
    [SerializeField] private Dictionary<Quest, QuestData> questData = new Dictionary<Quest, QuestData>();

    public static int TARGET_MONEY_1 = 50000;
    public static int MAIN_QUEST_1_LIMIT_DAY = 7;
    public static int EVENT0_CNT = 10;
    public static int EVENT0_DAY = 4;
    public static int SPICY_QUEST_1_CNT = 5;
    public static int SPICY_QUEST_2_CNT = 5;
    public static int SEA_OTTER_QUEST_TARGET_MONEY = 20000;
    public static int DRINK_QUEST_1_CNT = 10;
    public static int DRINK_QUEST_2_CNT = 10;

    public QuestData GetQuestData(Quest pQuest)
    {
        //���� ���� ���
        if (questData.ContainsKey(pQuest))
            return questData[pQuest];
        return null;
    }

    public void AddQuest(Quest pQuest)
    {
        //����Ʈ ���
        if (pQuest == Quest.None)
            return;

        GameMgr gameMgr = GameMgr.Instance;
        PlayData playData = gameMgr.playData;
        if((QuestState)playData.quest[(int)pQuest] == QuestState.Not)
        {
            playData.quest[(int)pQuest] = (int)QuestState.Run;
            playData.questCnt[(int)pQuest] = 0;
        }
    }

    public static bool TownRewardQuest(Quest pQuest)
    {
        //Ÿ��� ������ �޴� ����Ʈ
        switch (pQuest)
        {
            case Quest.SeaOtterQuest:
                return true;
        }

        return false;
    }

    public static bool ClearCheck(Quest pQuest)
    {
        //�ش� ����Ʈ Ŭ�����
        GameMgr gameMgr = GameMgr.Instance;
        PlayData playData = gameMgr.playData;
        switch (pQuest)
        {
            case Quest.MainQuest_1:
                return playData.money >= TARGET_MONEY_1;
            case Quest.Event_0_Quest:
                return playData.questCnt[(int)pQuest] >= EVENT0_CNT;
            case Quest.SpicyQuest_1:
                return playData.questCnt[(int)pQuest] >= SPICY_QUEST_1_CNT;
            case Quest.SpicyQuest_2:
                return playData.questCnt[(int)pQuest] >= SPICY_QUEST_2_CNT;
            case Quest.SeaOtterQuest:
                return playData.questCnt[(int)pQuest] >= SEA_OTTER_QUEST_TARGET_MONEY;
            case Quest.DrinkQuest_1:
                return playData.questCnt[(int)pQuest] >= DRINK_QUEST_1_CNT;
            case Quest.DrinkQuest_2:
                return playData.questCnt[(int)pQuest] >= DRINK_QUEST_2_CNT;
        }

        return false;
    }

    public static bool IsMainQuest(Quest pQuest)
    {
        //�ش� ����Ʈ�� ���� ����Ʈ�ΰ�
        switch (pQuest)
        {
            case Quest.MainQuest_1:
                return true;
        }
        return false;
    }

    public void QuestApply()
    {
        //����Ʈ ���� ����
        GameMgr gameMgr     = GameMgr.Instance;
        PlayData playData   = gameMgr.playData;
        for (Quest quest = Quest.MainQuest_1; quest < Quest.MAX; quest++)
        {
            if ((QuestState)playData.quest[(int)quest] != QuestState.Run)
                continue;
            switch (quest)
            {
                case Quest.SpicyQuest_1:
                    {
                        //ġŲ 5���� �ȱ�
                        playData.questCnt[(int)quest] += gameMgr.sellMenu.Count;
                    }
                    break;
                case Quest.SpicyQuest_2:
                    {
                        //����ġŲ 5���� �ȱ�
                        int soyChicken = 0;
                        foreach(GuestMenu guestMenu in gameMgr.sellMenu)
                            if (guestMenu.spicy0 == ChickenSpicy.Soy || guestMenu.spicy1 == ChickenSpicy.Soy)
                                soyChicken++;
                        playData.questCnt[(int)quest] += soyChicken;
                    }
                    break;
                case Quest.SeaOtterQuest:
                    {
                        //���� 20.000C �޼�
                        playData.questCnt[(int)quest] = Mathf.Max(playData.questCnt[(int)quest],gameMgr.dayMoney);
                    }
                    break;
                case Quest.DrinkQuest_1:
                    {
                        //�ݶ� 10�� �ȱ�
                        int colaCnt = 0;
                        foreach (GuestMenu guestMenu in gameMgr.sellMenu)
                            if (guestMenu.drink == Drink.Cola)
                                colaCnt++;
                        playData.questCnt[(int)quest] += colaCnt;
                    }
                    break;
                case Quest.DrinkQuest_2:
                    {
                        //���� 10�� �ȱ�
                        int beerCnt = 0;
                        foreach (GuestMenu guestMenu in gameMgr.sellMenu)
                            if (guestMenu.drink == Drink.Beer)
                                beerCnt++;
                        playData.questCnt[(int)quest] += beerCnt;
                    }
                    break;
            }
        }
    }
}
