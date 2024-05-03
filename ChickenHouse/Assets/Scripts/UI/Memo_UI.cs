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
    [SerializeField] private Image              memoImg;
    [SerializeField] private RectTransform      memoLabel;
    [SerializeField] private RectTransform      memoTriggerBox;
    [SerializeField] private Image              deep;

    private void Start()
    {
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if (kitchenMgr == null)
            return;

        //초기 메모지 상태 설정
        if(kitchenMgr.cameraObj.lookArea == LookArea.Counter)
        {
            memoTriggerBox.gameObject.SetActive(false);
            memoImg.gameObject.SetActive(false);
            memoLabel.gameObject.SetActive(false);
            deep.gameObject.SetActive(false);
        }
        else
        {
            memoTriggerBox.gameObject.SetActive(true);
            memoImg.gameObject.SetActive(true);
            memoLabel.gameObject.SetActive(true);
            deep.gameObject.SetActive(false);
        }
    }

    public void OpenTriggerBox(string str)
    {
        memoTriggerBox.gameObject.SetActive(true);
        memoImg.gameObject.SetActive(true);
        memoLabel.gameObject.SetActive(true);
        memoText.text = str;
        CloseMemo();
    }

    public void CloseTriggerBox()
    {
        memoTriggerBox.gameObject.SetActive(false);
        memoImg.gameObject.SetActive(false);
        memoLabel.gameObject.SetActive(false);
    }

    public void OpenMemo()
    {
        //인스펙터에서 끌어서 사용하는 함수임
        //메모지를 확대함
        animator.Play("Open");
        deep.gameObject.SetActive(true);
        memoImg.gameObject.SetActive(false);
        memoLabel.gameObject.SetActive(false);
    }

    public void CloseMemo()
    {
        //인스펙터에서 끌어서 사용하는 함수임
        //메모지를 닫음
        animator.Play("Close");
        deep.gameObject.SetActive(false);
        memoImg.gameObject.SetActive(true);
        memoLabel.gameObject.SetActive(true);
    }
}
