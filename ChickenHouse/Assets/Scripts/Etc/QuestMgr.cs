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
        //상점 정보 얻기
        if (questData.ContainsKey(pQuest))
            return questData[pQuest];
        return null;
    }

    public void AddQuest(Quest pQuest)
    {
        //퀘스트 등록
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
        //타운에서 보상을 받는 퀘스트
        switch (pQuest)
        {
            case Quest.SeaOtterQuest:
                return true;
        }

        return false;
    }

    public static bool ClearCheck(Quest pQuest)
    {
        //해당 퀘스트 클리어여부
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
        //해당 퀘스트가 메인 퀘스트인가
        switch (pQuest)
        {
            case Quest.MainQuest_1:
                return true;
        }
        return false;
    }

    public void QuestApply()
    {
        //퀘스트 내용 적용
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
                        //치킨 5마리 팔기
                        playData.questCnt[(int)quest] += gameMgr.sellMenu.Count;
                    }
                    break;
                case Quest.SpicyQuest_2:
                    {
                        //간장치킨 5마리 팔기
                        int soyChicken = 0;
                        foreach(GuestMenu guestMenu in gameMgr.sellMenu)
                            if (guestMenu.spicy0 == ChickenSpicy.Soy || guestMenu.spicy1 == ChickenSpicy.Soy)
                                soyChicken++;
                        playData.questCnt[(int)quest] += soyChicken;
                    }
                    break;
                case Quest.SeaOtterQuest:
                    {
                        //매출 20.000C 달성
                        playData.questCnt[(int)quest] = Mathf.Max(playData.questCnt[(int)quest],gameMgr.dayMoney);
                    }
                    break;
                case Quest.DrinkQuest_1:
                    {
                        //콜라 10개 팔기
                        int colaCnt = 0;
                        foreach (GuestMenu guestMenu in gameMgr.sellMenu)
                            if (guestMenu.drink == Drink.Cola)
                                colaCnt++;
                        playData.questCnt[(int)quest] += colaCnt;
                    }
                    break;
                case Quest.DrinkQuest_2:
                    {
                        //맥주 10개 팔기
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
