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
    [SerializeField] private float              delayTime = 0.1f;
    public struct FailObj
    {
        public RectTransform slot;
        public TextMeshProUGUI textUI;
    }
    [SerializeField] private FailObj[] failText;

    private IEnumerator cor;
    private NoParaDel fun;

    public string talkStr { private set; get; }
    private string resultStr = string.Empty;

    public void ShowText(string pStr, TalkBoxType pTalkBoxType, NoParaDel pFun)
    {
        talkStr = pStr;
        waitTalkBox.gameObject.SetActive(false);

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
        cor = RunCor(delayTime);
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
        List<string>    tempList    = new List<string>();
        List<bool>      isTagText   = new List<bool>();
        LanguageMgr.SplitString(talkStr, ref tempList, ref isTagText);

        string tempString = string.Empty;

        textUI.text = string.Empty;
        obj.gameObject.SetActive(true);

        resultStr = string.Empty;

        RectTransform textRect = textUI.GetComponent<RectTransform>();
        yield return new WaitUntil(() => textRect.sizeDelta.x > 0);

        const float remainWidth = 5f;

        string newTextUIStr = string.Empty;
        for (int i = 0; i < tempList.Count; i++)
        {
            tempString += tempList[i];
            if (isTagText[i])
                continue;

            textUI.text = tempString;
            textUI.ForceMeshUpdate();

            float nowTextWidth = 0;
            string nowTextString = string.Empty;
            List<string> arrayStr = new List<string>();
            List<float> arrayWidth = new List<float>();
            int charIdx = 0;
            for(int j = 0; j <= i; j++)
            {
                if(isTagText[j])
                {
                    nowTextString += tempList[j];
                    if(tempList[j] == "\n")
                    {
                        arrayStr.Add(nowTextString);
                        arrayWidth.Add(nowTextWidth);
                        nowTextWidth = 0;
                        nowTextString = string.Empty;
                    }
                }
                else
                {
                    TMP_CharacterInfo charInfo = textUI.textInfo.characterInfo[charIdx];

                    float charWidth = Mathf.Abs(charInfo.topRight.x - charInfo.topLeft.x);
                    if (nowTextWidth + charWidth > textRect.sizeDelta.x && tempList[j] == " ")
                    {
                        nowTextString += "\n";
                        arrayStr.Add(nowTextString);
                        arrayWidth.Add(nowTextWidth);
                        nowTextWidth = 0;
                        nowTextString = string.Empty;
                    }
                    else
                    {
                        nowTextWidth += charWidth;
                        nowTextString += tempList[j];
                    }

                    charIdx++;
                }
            }

            if (nowTextString != string.Empty)
            {
                nowTextString += "\n";
                arrayStr.Add(nowTextString);
                arrayWidth.Add(nowTextWidth);
                nowTextWidth = 0;
                nowTextString = string.Empty;
            }

            if (i == tempList.Count - 1)
            {
                for (int j = arrayWidth.Count - 1; j >= 1; j--)
                {
                    if (arrayWidth.Count >= 2 && arrayWidth[j] < remainWidth)
                    {
                        arrayWidth[j - 1] += arrayWidth[j];
                        //substring을 넣는이유는 뒤에 붙어 있는 \n을 넣지않기 위해서임
                        arrayStr[j - 1] = arrayStr[j - 1].Substring(0, arrayStr[j - 1].Length - 1);
                        arrayStr[j - 1] += arrayStr[j];
                        arrayWidth.RemoveAt(j);
                        arrayStr.RemoveAt(j);
                    }
                }

                float avgNewFontSize = 0;


                for(int j = 0; j < arrayWidth.Count; j++)
                {
                    float newFontSize = Mathf.Min(textUI.fontSize, textUI.fontSize * textRect.sizeDelta.x / arrayWidth[j]);
                    avgNewFontSize += newFontSize;
                }
                avgNewFontSize /= arrayWidth.Count;

                for (int j = 0; j < arrayWidth.Count; j++)
                {
                    float newFontSize = Mathf.Min(avgNewFontSize, textUI.fontSize * textRect.sizeDelta.x / arrayWidth[j]);
                    arrayStr[j] = string.Format("<size={0}>{1}</size>", newFontSize, arrayStr[j]);
                }
            }

            newTextUIStr = string.Empty;
            for (int j = 0; j < arrayStr.Count; j++)
                newTextUIStr += arrayStr[j];
        }

        resultStr = newTextUIStr;
        LanguageMgr.SplitString(resultStr, ref tempList, ref isTagText);

        tempString = string.Empty;
        for (int i = 0; i < tempList.Count; i++)
        {
            tempString += tempList[i];
            if (isTagText[i])
                continue;

            textUI.text = tempString;
            yield return new WaitForSeconds(delayTime);
        }


        fun?.Invoke();
        fun = null;
    }

    public void SkipTalk()
    {
        if (cor != null)
        {
            StopCoroutine(cor);
            cor = null;
        }

        if (resultStr != string.Empty)
            textUI.text = resultStr;
        else
            textUI.text = talkStr;

        fun?.Invoke();
        fun = null;
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
        GuestSystem guestMgr = GuestSystem.Instance;
        if (guestMgr == null)
            return;
        guestMgr.TalkOrder();
        waitTalkBox.gameObject.SetActive(false);
    }
}
