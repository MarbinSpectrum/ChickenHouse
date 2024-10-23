using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TownTalk : Mgr
{
    [SerializeField] private Image npcImg;
    [SerializeField] private TextMeshProUGUI talkText;
    [SerializeField] private TextMeshProUGUI talkName;
    [SerializeField] private float textDelay = 0.1f;

    private bool nowTalkFlag = false;
    private bool waitFlag = false;

    public void SetNPCSprite(Sprite spr)
    {
        //NPC 스프라이트 배치
        RectTransform rectTransform = npcImg.GetComponent<RectTransform>();

        float rectWidth = rectTransform.sizeDelta.x;
        float sprRate = (float)spr.texture.height / (float)spr.texture.width;
        float rectHeight = sprRate * rectWidth;

        rectTransform.sizeDelta = new Vector2(rectWidth, rectHeight);

        npcImg.sprite = spr;
    }

    public void StartTalk(string nameKey, List<string> keyList, NoParaDel fun = null)
    {
        gameObject.SetActive(true);

        LanguageMgr.SetString(talkName, nameKey);

        waitFlag = false;
        nowTalkFlag = false;
        IEnumerator Run()
        {
            foreach (string key in keyList)
            {
                waitFlag = true;
                nowTalkFlag = true;
                string str = LanguageMgr.GetText(key);
                string showStr = "";

                for(int i = 0; i < str.Length && nowTalkFlag; i++)
                {
                    if(i + 1 < str.Length && str[i] == '\\' && str[i+1] == 'n')
                    {
                        showStr += "\n";
                        LanguageMgr.SetText(talkText, showStr);
                        i++;
                        continue;
                    }

                    showStr += str[i];
                    LanguageMgr.SetText(talkText, showStr);

                    float waitTime = 0;
                    while(waitTime < textDelay && nowTalkFlag)
                    {
                        waitTime += Time.deltaTime;
                        yield return null;
                    }
                }

                nowTalkFlag = false;
                LanguageMgr.SetText(talkText, str);

                yield return new WaitWhile(() => (waitFlag));
            }
            fun.Invoke();
        }
        StartCoroutine(Run());
    }

    public void EndTalk()
    {
        gameObject.SetActive(false);
    }

    public void ClickTalkBox()
    {
        //인스펙터로 끌어다 쓰는함수임
        //대사 넘기기용으로 사용될듯함
        if(nowTalkFlag)
            nowTalkFlag = false;
        else if (waitFlag)
            waitFlag = false;
    }
}
