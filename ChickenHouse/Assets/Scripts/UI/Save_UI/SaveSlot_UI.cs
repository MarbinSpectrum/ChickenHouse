using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class SaveSlot_UI : Mgr
{
    [SerializeField] private TextMeshProUGUI    questName;
    [SerializeField] private TextMeshProUGUI    dayText;
    [SerializeField] private TextMeshProUGUI    ingameDay;
    [SerializeField] private TextMeshProUGUI    timeText;
    [SerializeField] private TextMeshProUGUI    moneyText;

    [SerializeField] private RectTransform      dataRect;
    [SerializeField] private TextMeshProUGUI    empty;

    [SerializeField] private GameRecord_UI        saveUI;

    private int slotNum;

    private const string EMPTY = "EMPTY";
    private const string DAY_FORMAT = "{0:D4}-{1:D2}-{2:D2}";
    private const string TIME_FORMAT = "{0} {1:D2}:{2:D2}";

    private PlayData playData;
    private bool canSave;
    private bool canLoad;

    public void SetUI(int pSlotNum, bool pCanSave, bool pCanLoad, PlayData pPlayData)
    {
        slotNum = pSlotNum;
        canSave = pCanSave;
        canLoad = pCanLoad;
        playData = pPlayData;

        //saveUI.gameObject.SetActive(false);

        if (pPlayData == null)
        {
            dataRect.gameObject.SetActive(false);
            empty.gameObject.SetActive(true);
            LanguageMgr.SetString(empty, EMPTY);
            return;
        }

        dataRect.gameObject.SetActive(true);
        empty.gameObject.SetActive(false);

        string inGameDay = string.Format(LanguageMgr.GetText("DAY"), pPlayData.day);
        LanguageMgr.SetText(ingameDay, inGameDay);

        string dayStr = string.Format(DAY_FORMAT, pPlayData.saveYear, pPlayData.saveMonth, pPlayData.saveDay);
        LanguageMgr.SetText(dayText, dayStr);

        string timeStr = string.Format(TIME_FORMAT, pPlayData.saveHour < 12 ? "AM" : "PM", pPlayData.saveHour, pPlayData.saveMin);
        LanguageMgr.SetText(timeText, timeStr);

        string moneyStr = LanguageMgr.GetMoneyStr(moneyText.fontSize, pPlayData.money);
        LanguageMgr.SetText(moneyText, moneyStr);

        LanguageMgr.SetText(questName, "");
        for (int i = 0; i < pPlayData.quest.Length; i++)
        {
            if (pPlayData.quest[i] != 1)
            {
                //진행중이 아닌 퀘스트
                continue;
            }

            if(QuestMgr.IsMainQuest((Quest)i) == false)
            {
                continue;
            }

            //진행중인 퀘스트 등록
            Quest q = (Quest)i;
            QuestData questData = questMgr.GetQuestData(q);
            LanguageMgr.SetString(questName, questData.questNameKey);
            break;
        }
    }

    public void OpenSlotMenu()
    {
        //인스펙터로 끌어서 사용하는 함수
        soundMgr.PlaySE(Sound.Btn_SE);
        if (canSave)
            saveUI.SetUI(slotNum, playData, this);
        else if(canLoad && playData != null)
            saveUI.SetUI(slotNum, playData, this);
    }
}
