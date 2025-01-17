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
    [SerializeField] private Image              redDot;
    [SerializeField] private TextMeshProUGUI    diaryName;
    [SerializeField] private Color              notSelectColor;
    [SerializeField] private Color              notSelectMainQuest;
    [SerializeField] private Color              selectColor;
    private NoParaDel questFun;
    private QuestData questData;

    public void SetData(QuestData pQuest, bool pSelect, bool pEndQuest)
    {
        questData = pQuest;

        //퀘스트이름
        LanguageMgr.SetString(diaryName, questData.questNameKey);

        if(pSelect)
        {
            //선택중
            if (QuestMgr.IsMainQuest(questData.quest))
                diaryName.color = notSelectMainQuest;
            else
                diaryName.color = selectColor;
            select.enabled  = true;
        }
        else
        {
            //선택중이 아님
            if(QuestMgr.IsMainQuest(questData.quest))
                diaryName.color = notSelectMainQuest;
            else
                diaryName.color = notSelectColor;
            select.enabled  = false;
        }

        //마지막 오브젝트는  비활성화
        diaryLine.enabled = !pEndQuest;

        UpdateRedDot();
    }

    public void UpdateRedDot()
    {
        bool isAct = (gameMgr.playData.questCheck[(int)questData.quest] == false);
        redDot.gameObject.SetActive(isAct);
    }

    public void SetEventTrigger(NoParaDel fun)
    {
        //탭에 이벤트 등록
        questFun = fun;
    }

    public void CheckEventTrigger()
    {
        questFun?.Invoke();
    }
}
