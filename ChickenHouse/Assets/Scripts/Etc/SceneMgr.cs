using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneMgr : AwakeSingleton<SceneMgr>
{
    [SerializeField] private Dictionary<SceneChangeAni,Animator> changeList = new Dictionary<SceneChangeAni, Animator>();
    [SerializeField] private RectTransform      saveAni;
    [SerializeField] private RectTransform      saveCheckUI;
    [SerializeField] private RewardItem         rewardItem;

    private bool dayEndCheckFlag = false;
    private bool saveDataFlag = false;

    public void SceneLoad(Scene scene, bool questCheck, bool dayEnd, SceneChangeAni changeAni = SceneChangeAni.NOT)
    {
        StartCoroutine(RunSceneLoad(scene, questCheck, dayEnd, changeAni));
    }

    private IEnumerator RunSceneLoad(Scene scene, bool questCheck, bool dayEnd, SceneChangeAni changeAni)
    {
        //씬이동 코루틴
        if(changeList.ContainsKey(changeAni))
            changeList[changeAni].Play("On");

        yield return new WaitForSeconds(1f);

        if (questCheck)
        {
            GameMgr     gameMgr     = GameMgr.Instance;
            PlayData    playData    = gameMgr.playData;
            QuestMgr    questMgr    = QuestMgr.Instance;
            ShopMgr     shopMgr     = ShopMgr.Instance;
            List<ShopItem>  rewardList  = new List<ShopItem>();
            List<Quest>     nextQuest   = new List<Quest>();
            for(Quest quest = Quest.MainQuest_1; quest < Quest.MAX; quest++)
            {
                //클리어한 퀘스트의 다음 퀘스트와, 보상을 정리한다.
                if ((QuestState)playData.quest[(int)quest] != QuestState.Run)
                    continue;
                if (QuestMgr.TownRewardQuest(quest))
                    continue;
                if (QuestMgr.IsMainQuest(quest))
                    continue;
                if(QuestMgr.ClearCheck(quest))
                {
                    playData.quest[(int)quest] = (int)QuestState.Complete;
                    QuestData       questData   = questMgr.GetQuestData(quest);
                    List<ShopItem>  rewardItems = questData.rewards;
                    Quest           getNewQuest = questData.nextQuest;
                    foreach(ShopItem reward in rewardItems)
                        rewardList.Add(reward);
                    nextQuest.Add(getNewQuest);
                }
            }

            //새로운 퀘스트 등록
            for(int i = 0; i < nextQuest.Count; i++)
                questMgr.AddQuest(nextQuest[i]);

            //보상 표시 및 보상 적용
            for (int i = 0; i < rewardList.Count; i++)
            {
                bool rewardWaitFlag = false;
                rewardItem.gameObject.SetActive(true);
                ShopData shopData = shopMgr.GetShopData(rewardList[i]);
                rewardItem.SetUI(shopData,() => rewardWaitFlag = true);
                yield return new WaitUntil(() => rewardWaitFlag);
                rewardItem.gameObject.SetActive(false);

                playData.GetShopItem(rewardList[i]);
            }
        }

        saveDataFlag = false;
        if (dayEnd)
        {
            dayEndCheckFlag = false;

            saveCheckUI.gameObject.SetActive(true);

            yield return new WaitUntil(() => dayEndCheckFlag);

            saveCheckUI.gameObject.SetActive(false);

        }

        if(saveDataFlag)
        {
            //저장확인용 플래그
            saveAni.gameObject.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            saveAni.gameObject.SetActive(false);
        }

        SoundMgr soundMgr = SoundMgr.Instance;
        soundMgr.StopSE();
        soundMgr.StopBGM();

        SceneManager.LoadScene((int)scene);

        if (changeList.ContainsKey(changeAni))
            changeList[changeAni].Play("Off");
    }

    public void ShowAnimation(SceneChangeAni changeAni,bool show)
    {
        if (changeList.ContainsKey(changeAni))
            changeList[changeAni].Play(show ? "On" : "Off");
    }

    public void SaveCheckYes()
    {
        GameMgr gameMgr = GameMgr.Instance;
        gameMgr.OpenRecordUI(true,false, (saveSlot) =>
        {
            int slotNum = (int)saveSlot;
            gameMgr.selectSaveSlot = slotNum;
            dayEndCheckFlag = true;
            saveDataFlag = true;
            gameMgr.CloseRecordUI();
        });
    }

    public void SaveCheckNo()
    {
        dayEndCheckFlag = true;
    }
}
