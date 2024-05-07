using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TalkBox_UI : Mgr
{
    [SerializeField] private Sprite normalBox;
    [SerializeField] private Color  normalColor;
    [SerializeField] private Sprite angryBox;
    [SerializeField] private Color  angryColor;
    [SerializeField] private Sprite thankBox;
    [SerializeField] private Color  thankColor;
    [SerializeField] private Sprite happyBox;
    [SerializeField] private Color  happyColor;

    [SerializeField] private Image              talkBox;
    [SerializeField] private TextMeshProUGUI    textUI;
    [SerializeField] private GameObject         obj;
    [SerializeField] private Image              hearthImg;
    [SerializeField] private RectTransform      waitTalkBox;

    private IEnumerator cor;
    private NoParaDel fun;

    public string talkStr { private set; get; }

    public void ShowText(string pStr, TalkBoxType pTalkBoxType, NoParaDel pFun)
    {
        GuestMgr.Instance.skipTalkBtn.gameObject.SetActive(true);

        talkStr = pStr;
        waitTalkBox.gameObject.SetActive(false);
        obj.gameObject.SetActive(true);

        switch(pTalkBoxType)
        {
            case TalkBoxType.Normal:
                {
                    hearthImg.gameObject.SetActive(false);
                    talkBox.sprite = normalBox;
                    textUI.color = normalColor;
                }
                break;
            case TalkBoxType.Angry:
                {
                    hearthImg.gameObject.SetActive(false);
                    talkBox.sprite = angryBox;
                    textUI.color = angryColor;
                }
                break;
            case TalkBoxType.Thank:
                {
                    hearthImg.gameObject.SetActive(false);
                    talkBox.sprite = thankBox;
                    textUI.color = thankColor;
                }
                break;
            case TalkBoxType.Happy:
                {
                    hearthImg.gameObject.SetActive(true);
                    talkBox.sprite = happyBox;
                    textUI.color = happyColor;
                }
                break;
        }

        fun = pFun;
        if (cor != null)
        {
            StopCoroutine(cor);
            cor = null;
        }
        cor = RunCor(0.1f);
        StartCoroutine(cor);
    }

    private IEnumerator RunCor(float delayTime)
    {
        for (int i = 1; i <= talkStr.Length; i++)
        {
            string front = talkStr.Substring(0, i);
            string tail = talkStr.Substring(i, talkStr.Length - i);

            tail = "<color=#FFFFFF00>" + tail + "</color>";
            front += tail;

            textUI.text = front;

            yield return new WaitForSeconds(delayTime);
        }

        fun?.Invoke();
        GuestMgr.Instance.skipTalkBtn.gameObject.SetActive(false);
    }

    public void SkipTalk()
    {
        if (cor != null)
        {
            StopCoroutine(cor);
            cor = null;
        }
        textUI.text = talkStr;
        fun?.Invoke();
        GuestMgr.Instance.skipTalkBtn.gameObject.SetActive(false);
    }

    public void CloseTalkBox()
    {
        obj.gameObject.SetActive(false);
        waitTalkBox.gameObject.SetActive(false);
        GuestMgr.Instance.skipTalkBtn.gameObject.SetActive(false);
    }

    public void ShowWaitTalkBox()
    {
        obj.gameObject.SetActive(false);
        waitTalkBox.gameObject.SetActive(true);
    }

}
