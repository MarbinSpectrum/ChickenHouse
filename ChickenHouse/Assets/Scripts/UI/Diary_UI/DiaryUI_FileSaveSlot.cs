using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class DiaryUI_FileSaveSlot : Mgr
{
    [SerializeField] private TextMeshProUGUI    questName;
    [SerializeField] private TextMeshProUGUI    ingameDay;
    [SerializeField] private TextMeshProUGUI    dayText;
    [SerializeField] private TextMeshProUGUI    timeText;
    [SerializeField] private TextMeshProUGUI    moneyText;


    [SerializeField] private RectTransform          dataRect;
    [SerializeField] private TextMeshProUGUI        empty;

    private DiaryUI_FileSaveMenu   fileSaveMenu;

    private int slotNum;

    private const string EMPTY = "EMPTY";
    private const string DAY_FORMAT = "{0:D4}-{1:D2}-{2:D2}";
    private const string TIME_FORMAT = "{0} {1:D2}:{2:D2}";

    private PlayData playData;

    public void SetUI(PlayData pPlayData,int pSlotNum, DiaryUI_FileSaveMenu pFileSaveMenu)
    {
        playData = pPlayData;
        slotNum = pSlotNum;
        fileSaveMenu = pFileSaveMenu;

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

            //진행중인 퀘스트 등록
            Quest q = (Quest)i;

            if(QuestMgr.IsMainQuest(q) == false)
            {
                continue;
            }

            QuestData questData = questMgr.GetQuestData(q);
            LanguageMgr.SetString(questName, questData.questNameKey);
            break;
        }
    }

    public void OpenSlotMenu()
    {
        //인스펙터로 끌어서 사용하는 함수
        fileSaveMenu.SetUI(slotNum, playData, this);
    }
}
