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
    [SerializeField] private RectTransform      memoTriggerBox;

    private void Start()
    {
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if (kitchenMgr == null)
            return;

        //�ʱ� �޸��� ���� ����
        if(kitchenMgr.cameraObj.lookArea == LookArea.Counter)
        {
            memoTriggerBox.gameObject.SetActive(false);
            memoImg.gameObject.SetActive(false);
        }
        else
        {
            memoTriggerBox.gameObject.SetActive(true);
            memoImg.gameObject.SetActive(true);
        }
    }

    public void OpenTriggerBox(string str)
    {
        memoTriggerBox.gameObject.SetActive(true);
        memoImg.gameObject.SetActive(true);
        memoText.text = str;
        CloseMemo();
    }

    public void CloseTriggerBox()
    {
        memoTriggerBox.gameObject.SetActive(false);
        memoImg.gameObject.SetActive(false);
    }

    public void OpenMemo()
    {
        //�ν����Ϳ��� ��� ����ϴ� �Լ���
        //�޸����� Ȯ����
        animator.Play("Open");
        memoImg.gameObject.SetActive(false);
    }

    public void CloseMemo()
    {
        //�ν����Ϳ��� ��� ����ϴ� �Լ���
        //�޸����� ����
        animator.Play("Close");
        memoImg.gameObject.SetActive(true);
    }
}
