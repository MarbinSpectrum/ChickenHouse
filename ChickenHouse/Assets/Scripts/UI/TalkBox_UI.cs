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
    [SerializeField] private RectTransform      resultRect;
    public struct FailObj
    {
        public RectTransform slot;
        public TextMeshProUGUI textUI;
    }
    [SerializeField] private FailObj[] failText;

    private IEnumerator cor;
    private NoParaDel fun;

    public string talkStr { private set; get; }

    public void ShowText(string pStr, TalkBoxType pTalkBoxType, NoParaDel pFun)
    {
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

    public void ShowResult(bool spicyResult, bool chickenStateResult,
        bool drinkResult, bool sideMenuResult)
    {
        return;

        resultRect.gameObject.SetActive(true);

        //치킨 상태
        failText[0].slot.gameObject.SetActive(chickenStateResult == false);
        LanguageMgr.SetString(failText[0].textUI, "FAIL_CHICKEN");

        //양념 결과
        failText[1].slot.gameObject.SetActive(spicyResult == false);
        LanguageMgr.SetString(failText[1].textUI,"FAIL_SPICY");

        //음료 결과
        failText[2].slot.gameObject.SetActive(drinkResult == false);
        LanguageMgr.SetString(failText[2].textUI, "FAIL_DRINK");

        //사이드 결과
        failText[3].slot.gameObject.SetActive(sideMenuResult == false);
        LanguageMgr.SetString(failText[3].textUI, "FAIL_PICKLE");
    }

    public void CloseResult()
    {
        resultRect.gameObject.SetActive(false);
    }

    private IEnumerator RunCor(float delayTime)
    {
        List<string> tempList = new List<string>();
        int idx = 0;
        string  tagString   = string.Empty;
        bool    tagMode     = false;

        while (idx < talkStr.Length)
        {
            if(tagMode)
            {
                tagString += talkStr[idx];
                if (talkStr[idx] == '>')
                {
                    tempList.Add(tagString);
                    tagString = string.Empty;
                    tagMode = false;
                }
                idx++;
            }
            else
            {
                if (talkStr[idx] != '<')
                {
                    tempList.Add(talkStr[idx].ToString());
                    idx++;
                }
                else
                {
                    tagMode = true;
                }
            }
        }

        string tempString = string.Empty;
        for (int i = 0; i < tempList.Count; i++)
        {
            tempString += tempList[i];

            textUI.text = tempString;

            yield return new WaitForSeconds(delayTime);
        }

        fun?.Invoke();
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
    }

    public void CloseTalkBox()
    {
        obj.gameObject.SetActive(false);
        waitTalkBox.gameObject.SetActive(false);
        CloseResult();
    }

    public void ShowWaitTalkBox()
    {
        obj.gameObject.SetActive(false);
        waitTalkBox.gameObject.SetActive(true);
    }

    public void ShowOrderTalk()
    {
        //인스펙터에 끌어서 사용하는 함수임
        GuestMgr guestMgr = GuestMgr.Instance;
        if (guestMgr == null)
            return;
        guestMgr.TalkOrder();
        waitTalkBox.gameObject.SetActive(false);
    }
}
