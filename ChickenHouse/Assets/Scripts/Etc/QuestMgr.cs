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
        if(playData.quest[(int)pQuest] == 0)
        {
            playData.quest[(int)pQuest] = 1;
            playData.questCnt[(int)pQuest] = 0;
        }
    }

    public bool ClearCheck(Quest pQuest)
    {
        //해당 퀘스트 클리어여부
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
        //퀘스트 내용 적용
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
                case Quest.SpicyQuest_3:
                    {
                        //불닭치킨 10마리 팔기
                        int hellChicken = 0;
                        foreach (GuestMenu guestMenu in gameMgr.sellMenu)
                            if (guestMenu.spicy0 == ChickenSpicy.Hell || guestMenu.spicy1 == ChickenSpicy.Hell)
                                hellChicken++;
                        playData.questCnt[(int)quest] += hellChicken;
                    }
                    break;
                case Quest.DrinkQuest_1:
                    {
                        //콜라 20개 팔기
                        int colaCnt = 0;
                        foreach (GuestMenu guestMenu in gameMgr.sellMenu)
                            if (guestMenu.drink == Drink.Cola)
                                colaCnt++;
                        playData.questCnt[(int)quest] += colaCnt;
                    }
                    break;
                case Quest.DrinkQuest_2:
                    {
                        //맥주 20개 팔기
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
