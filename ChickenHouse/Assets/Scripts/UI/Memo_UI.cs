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
        //인스펙터에서 끌어서 사용하는 함수임
        //메모지를 확대함
        animator.Play("Open");
    }

    public void CloseMemo()
    {
        //인스펙터에서 끌어서 사용하는 함수임
        //메모지를 닫음
        animator.Play("Close");
    }
}
