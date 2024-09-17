using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class DiaryUI_QuestListObj : Mgr
{
    [SerializeField] private Image              select;
    [SerializeField] private Image              diaryLine;
    [SerializeField] private TextMeshProUGUI    diaryName;
    [SerializeField] private Button             eventTrigger;
    [SerializeField] private Color              notSelectColor;
    [SerializeField] private Color              notSelectMainQuest;
    [SerializeField] private Color              selectColor;

    public void SetData(QuestData pQuest, bool pSelect, bool pEndQuest)
    {
        //퀘스트이름
        LanguageMgr.SetString(diaryName, pQuest.questNameKey);

        if(pSelect)
        {
            //선택중
            diaryName.color = selectColor;
            select.enabled  = true;
        }
        else
        {
            //선택중이 아님
            if(QuestMgr.IsMainQuest(pQuest.quest))
                diaryName.color = notSelectMainQuest;
            else
                diaryName.color = notSelectColor;
            select.enabled  = false;
        }

        //마지막 오브젝트는  비활성화
        diaryLine.enabled = !pEndQuest;
    }

    public void SetEventTrigger(NoParaDel fun)
    {
        //탭에 이벤트 등록
        eventTrigger.onClick.RemoveAllListeners();
        eventTrigger.onClick.AddListener(() =>
        {
            fun?.Invoke();
        });
    }
}
