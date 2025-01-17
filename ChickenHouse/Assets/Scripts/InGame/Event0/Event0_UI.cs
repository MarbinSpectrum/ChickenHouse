using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Event0_UI : Mgr
{
    private int playerCnt = 0;
    private int enemyCnt = 0;
    [SerializeField] private float enemyChickenMakeTime = 45;
    private float time = 0;
    private bool run = false;
    [SerializeField] private Image playerGauge;
    [SerializeField] private Image enemyGauge;
    [SerializeField] private TextMeshProUGUI    talkText;
    [SerializeField] private RectTransform      talkBox;
    private List<int> talkList = new List<int>();

    public Event_0_Battle_Result battleResult { private set; get; } = Event_0_Battle_Result.None;

    public void SetUI()
    {
        battleResult = Event_0_Battle_Result.None;
        playerCnt = 0;
        enemyCnt = 0;
        UpdateCnt();
        run = true;
    }

    public void Update()
    {
        if (run == false)
            return;
        time += Time.deltaTime;
        if(time >=enemyChickenMakeTime)
        {
            time = 0;
            AddEnemyCnt();
        }
    }

    public void AddPlayerCnt()
    {
        if (battleResult != Event_0_Battle_Result.None)
            return;
        playerCnt++;
        playerCnt = Mathf.Min(QuestMgr.EVENT0_CNT, playerCnt);
        gameMgr.playData.questCnt[(int)Quest.Event_0_Quest] = playerCnt;
        UpdateCnt();
        ResultCheck();
    }

    private IEnumerator ShowTalkBox()
    {
        if (talkList.Count == 5)
            talkList.Clear();
        List<int> randomTalk = new List<int>();
        for(int i = 0; i < 5; i++)
        {
            if (talkList.Contains(i))
                continue;
            randomTalk.Add(i);
        }

        if(randomTalk.Count > 0)
        {
            int r = Random.Range(0, randomTalk.Count);
            int talkNum = randomTalk[r];
            talkList.Add(talkNum);
            talkBox.gameObject.SetActive(true);
            soundMgr.PlayLoopSE(Sound.Voice28_SE);
            string talkStr = string.Empty;
            string newTextUIStr = string.Empty;
            switch (talkNum)
            {
                case 0:
                    talkStr = LanguageMgr.GetText("PIG_TALK_1");
                    newTextUIStr = LanguageMgr.GetSmartText(talkStr, 4, talkText);
                    talkText.text = newTextUIStr;
                    yield return new WaitForSeconds(5f);
                    break;
                case 1:
                    talkStr = LanguageMgr.GetText("PIG_TALK_2");
                    newTextUIStr = LanguageMgr.GetSmartText(talkStr, 4, talkText);
                    talkText.text = newTextUIStr;
                    yield return new WaitForSeconds(5f);
                    break;
                case 2:
                    talkStr = LanguageMgr.GetText("PIG_TALK_3");
                    newTextUIStr = LanguageMgr.GetSmartText(talkStr, 4, talkText);
                    talkText.text = newTextUIStr;
                    yield return new WaitForSeconds(7f);
                    break;
                case 3:
                    talkStr = LanguageMgr.GetText("PIG_TALK_4");
                    newTextUIStr = LanguageMgr.GetSmartText(talkStr, 4, talkText);
                    talkText.text = newTextUIStr;
                    yield return new WaitForSeconds(8.5f);
                    break;
                case 4:
                    talkStr = LanguageMgr.GetText("PIG_TALK_5");
                    newTextUIStr = LanguageMgr.GetSmartText(talkStr, 4, talkText);
                    talkText.text = newTextUIStr;
                    yield return new WaitForSeconds(7f);
                    break;
            }
            soundMgr.StopLoopSE(Sound.Voice28_SE);
            talkBox.gameObject.SetActive(false);
        }
    }

    public void AddEnemyCnt()
    {
        if (battleResult != Event_0_Battle_Result.None)
            return;
        StartCoroutine(ShowTalkBox());
        enemyCnt++;
        enemyCnt = Mathf.Min(QuestMgr.EVENT0_CNT, enemyCnt);
        UpdateCnt();
        ResultCheck();
    }

    private void UpdateCnt()
    {
        float playerValue = playerCnt / (float)QuestMgr.EVENT0_CNT;
        float enemyValue = enemyCnt / (float)QuestMgr.EVENT0_CNT;

        playerGauge.fillAmount = playerValue;
        enemyGauge.fillAmount = enemyValue;
    }

    private void ResultCheck()
    {
        if (playerCnt >= QuestMgr.EVENT0_CNT)
            battleResult = Event_0_Battle_Result.Win;
        else if (enemyCnt >= QuestMgr.EVENT0_CNT)
            battleResult = Event_0_Battle_Result.Lose;
        else
            battleResult = Event_0_Battle_Result.None;
    }
}
