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
        //����Ʈ�̸�
        LanguageMgr.SetString(diaryName, pQuest.questNameKey);

        if(pSelect)
        {
            //������
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
        eventTrigger.onClick.RemoveAllListeners();
        eventTrigger.onClick.AddListener(() =>
        {
            fun?.Invoke();
        });
    }
}
