using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestMgr : AwakeSingleton<QuestMgr>
{
    [SerializeField] private Dictionary<Quest, QuestData> questData = new Dictionary<Quest, QuestData>();

    public static int TARGET_MONEY_1 = 100000;
    public static int MAIN_QUEST_1_LIMIT_DAY = 7;
    public static int SPICY_QUEST_1_CNT = 5;
    public static int SPICY_QUEST_2_CNT = 5;
    public static int SPICY_QUEST_3_CNT = 10;
    public static int DRINK_QUEST_1_CNT = 20;
    public static int DRINK_QUEST_2_CNT = 20;

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
        if(playData.quest[(int)pQuest] == 0)
        {
            playData.quest[(int)pQuest] = 1;
            playData.questCnt[(int)pQuest] = 0;
        }
    }

    public bool ClearCheck(Quest pQuest)
    {
        //�ش� ����Ʈ Ŭ�����
        GameMgr gameMgr = GameMgr.Instance;
        PlayData playData = gameMgr.playData;
        switch (pQuest)
        {
            case Quest.MainQuest_1:
                return playData.money >= TARGET_MONEY_1;
            case Quest.SpicyQuest_1:
                return playData.questCnt[(int)pQuest] >= SPICY_QUEST_1_CNT;
            case Quest.SpicyQuest_2:
                return playData.questCnt[(int)pQuest] >= SPICY_QUEST_2_CNT;
            case Quest.SpicyQuest_3:
                return playData.questCnt[(int)pQuest] >= SPICY_QUEST_3_CNT;
            case Quest.DrinkQuest_1:
                return playData.questCnt[(int)pQuest] >= DRINK_QUEST_1_CNT;
            case Quest.DrinkQuest_2:
                return playData.questCnt[(int)pQuest] >= DRINK_QUEST_2_CNT;
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
            if (playData.quest[(int)quest] != 1)
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
                case Quest.SpicyQuest_3:
                    {
                        //�Ҵ�ġŲ 10���� �ȱ�
                        int hellChicken = 0;
                        foreach (GuestMenu guestMenu in gameMgr.sellMenu)
                            if (guestMenu.spicy0 == ChickenSpicy.Hell || guestMenu.spicy1 == ChickenSpicy.Hell)
                                hellChicken++;
                        playData.questCnt[(int)quest] += hellChicken;
                    }
                    break;
                case Quest.DrinkQuest_1:
                    {
                        //�ݶ� 20�� �ȱ�
                        int colaCnt = 0;
                        foreach (GuestMenu guestMenu in gameMgr.sellMenu)
                            if (guestMenu.drink == Drink.Cola)
                                colaCnt++;
                        playData.questCnt[(int)quest] += colaCnt;
                    }
                    break;
                case Quest.DrinkQuest_2:
                    {
                        //���� 20�� �ȱ�
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
