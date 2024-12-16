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
    [SerializeField] private Color              notSelectColor;
    [SerializeField] private Color              notSelectMainQuest;
    [SerializeField] private Color              selectColor;
    private NoParaDel questFun;

    public void SetData(QuestData pQuest, bool pSelect, bool pEndQuest)
    {
        //����Ʈ�̸�
        LanguageMgr.SetString(diaryName, pQuest.questNameKey);

        if(pSelect)
        {
            //������
            if (QuestMgr.IsMainQuest(pQuest.quest))
                diaryName.color = notSelectMainQuest;
            else
                diaryName.color = selectColor;
            select.enabled  = true;
        }
        else
        {
            //�������� �ƴ�
            if(QuestMgr.IsMainQuest(pQuest.quest))
                diaryName.color = notSelectMainQuest;
            else
                diaryName.color = notSelectColor;
            select.enabled  = false;
        }

        //������ ������Ʈ��  ��Ȱ��ȭ
        diaryLine.enabled = !pEndQuest;
    }

    public void SetEventTrigger(NoParaDel fun)
    {
        //�ǿ� �̺�Ʈ ���
        questFun = fun;
    }

    public void CheckEventTrigger()
    {
        questFun?.Invoke();
    }
}
