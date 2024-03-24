using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class Memo_UI : Mgr
{
    [SerializeField] private TextMeshProUGUI    memoText;
    [SerializeField] private Animator           animator;
    [SerializeField] private RectTransform      memoTriggerBox;

    public void OpenTriggerBox(string str)
    {
        memoTriggerBox.gameObject.SetActive(true);
        memoText.text = str;
        CloseMemo();
    }

    public void CloseTriggerBox()
    {
        memoTriggerBox.gameObject.SetActive(false);
    }

    public void OpenMemo()
    {
        //�ν����Ϳ��� ��� ����ϴ� �Լ���
        //�޸����� Ȯ����
        animator.Play("Open");
    }

    public void CloseMemo()
    {
        //�ν����Ϳ��� ��� ����ϴ� �Լ���
        //�޸����� ����
        animator.Play("Close");
    }
}
