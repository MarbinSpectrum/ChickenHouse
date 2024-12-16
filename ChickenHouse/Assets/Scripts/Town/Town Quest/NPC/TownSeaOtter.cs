using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownSeaOtter : TownTalkObj
{
    public override void Init()
    {
        UpdateTalkBox();
    }

    private void UpdateTalkBox()
    {
        if ((QuestState)gameMgr.playData.quest[(int)Quest.SeaOtterQuest] == QuestState.Not)
        {
            questTalkBox.SetState(QuestState.Not);
            questTalkBox.gameObject.SetActive(true);
        }
        else if ((QuestState)gameMgr.playData.quest[(int)Quest.SeaOtterQuest] == QuestState.Run)
        {
            if (QuestMgr.ClearCheck(Quest.SeaOtterQuest))
                questTalkBox.SetState(QuestState.Complete);
            else
                questTalkBox.SetState(QuestState.Run);
            questTalkBox.gameObject.SetActive(true);
        }
        else
            questTalkBox.gameObject.SetActive(false);
    }

    public override void StartTalk()
    {
        if ((QuestState)gameMgr.playData.quest[(int)Quest.SeaOtterQuest] == QuestState.Not)
        {
            List<string> talkKey = new List<string>();
            talkKey.Add("SEAOTTER_QUEST_TALK_0");
            talkKey.Add("SEAOTTER_QUEST_TALK_1");
            talkKey.Add("SEAOTTER_QUEST_TALK_2");
            talkKey.Add("SEAOTTER_QUEST_TALK_3");
            talkKey.Add("SEAOTTER_QUEST_TALK_4");
            talkKey.Add("SEAOTTER_QUEST_TALK_5");
            talkKey.Add("SEAOTTER_QUEST_TALK_6");
            townTalk.StartTalk("BOOK_SEAOTTER_NAME", talkKey, npcSprites0, npcSprites1,
                Sound.Voice23_SE, () =>
            {
                gameMgr.playData.quest[(int)Quest.SeaOtterQuest] = (int)QuestState.Run;
                townTalk.EndTalk();
                UpdateTalkBox();
            });
        }
        else if ((QuestState)gameMgr.playData.quest[(int)Quest.SeaOtterQuest] == QuestState.Run 
            && QuestMgr.ClearCheck(Quest.SeaOtterQuest))
        {
            List<string> talkKey = new List<string>();
            talkKey.Add("SEAOTTER_QUEST_TALK_7");

            IEnumerator Run()
            {
                rewardItem.gameObject.SetActive(true);
                QuestData questData = questMgr.GetQuestData(Quest.SeaOtterQuest);

                //보상 표시 및 보상 적용
                for (int i = 0; i < questData.rewards.Count; i++)
                {
                    bool rewardWaitFlag = false;
                    rewardItem.gameObject.SetActive(true);
                    ShopData shopData = shopMgr.GetShopData(questData.rewards[i]);
                    rewardItem.SetUI(shopData, () => rewardWaitFlag = true);
                    yield return new WaitUntil(() => rewardWaitFlag);
                    rewardItem.gameObject.SetActive(false);

                    gameMgr.playData.GetShopItem(questData.rewards[i]);
                }
            }

            townTalk.StartTalk("BOOK_SEAOTTER_NAME", talkKey, npcSprites0, npcSprites1, 
                Sound.Voice23_SE, () =>
            {
                gameMgr.playData.quest[(int)Quest.SeaOtterQuest] = (int)QuestState.Complete;
                townTalk.EndTalk();
                UpdateTalkBox();

                StartCoroutine(Run());
            });
        }
    }
}

