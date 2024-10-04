using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class DiaryUI_FileSaveSlot : Mgr
{
    [SerializeField] private TextMeshProUGUI    questName;
    [SerializeField] private TextMeshProUGUI    dayText;
    [SerializeField] private TextMeshProUGUI    timeText;
    [SerializeField] private TextMeshProUGUI    moneyText;

    [SerializeField] private RectTransform          dataRect;
    [SerializeField] private TextMeshProUGUI        empty;

    [SerializeField] private DiaryUI_FileSaveMenu   fileSaveMenu;

    [SerializeField] private int slotNum;

    private const string EMPTY = "EMPTY";
    private const string DAY_FORMAT = "{0:D4}-{1:D2}-{2:D2}";
    private const string TIME_FORMAT = "{0} {1:D2}:{2:D2}";
    private const string MONEY_FORMAT = "{0:N0}$";

    private PlayData playData;

    public void SetUI(PlayData pPlayData)
    {
        playData = pPlayData;

        fileSaveMenu.gameObject.SetActive(false);

        if (pPlayData == null)
        {
            dataRect.gameObject.SetActive(false);
            empty.gameObject.SetActive(true);
            LanguageMgr.SetString(empty, EMPTY);
            return;
        }

        dataRect.gameObject.SetActive(true);
        empty.gameObject.SetActive(false);

        string dayStr = string.Format(DAY_FORMAT, pPlayData.saveYear, pPlayData.saveMonth, pPlayData.saveDay);
        LanguageMgr.SetText(dayText, dayStr);

        string timeStr = string.Format(TIME_FORMAT, pPlayData.saveHour < 12 ? "AM" : "PM", pPlayData.saveHour, pPlayData.saveMin);
        LanguageMgr.SetText(timeText, timeStr);

        string moneyStr = string.Format(MONEY_FORMAT,pPlayData.money);
        LanguageMgr.SetText(moneyText, moneyStr);

        LanguageMgr.SetText(questName, "");
        for (int i = 0; i < pPlayData.quest.Length; i++)
        {
            if (pPlayData.quest[i] != 1)
            {
                //�������� �ƴ� ����Ʈ
                continue;
            }

            //�������� ����Ʈ ���
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
        //�ν����ͷ� ��� ����ϴ� �Լ�
        fileSaveMenu.SetUI(slotNum, playData, this);
    }
}
