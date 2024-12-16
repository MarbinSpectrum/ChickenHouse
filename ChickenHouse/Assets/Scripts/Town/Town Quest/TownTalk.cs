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
        //NPC ��������Ʈ ��ġ
        RectTransform rectTransform = npcImg.GetComponent<RectTransform>();

        float rectWidth = rectTransform.sizeDelta.x;
        float sprRate = (float)spr.texture.height / (float)spr.texture.width;
        float rectHeight = sprRate * rectWidth;

        rectTransform.sizeDelta = new Vector2(rectWidth, rectHeight);

        npcImg.sprite = spr;
    }

    public void StartTalk(string nameKey, List<string> keyList,Sprite npcSprites0, Sprite npcSprites1,
        Sound sound,NoParaDel fun = null)
    {
        gameObject.SetActive(true);

        LanguageMgr.SetString(talkName, nameKey);
        SetNPCSprite(npcSprites0);

        waitFlag = false;
        nowTalkFlag = false;
        IEnumerator Run()
        {
            foreach (string key in keyList)
            {
                soundMgr.PlayLoopSE(sound);
                waitFlag = true;
                nowTalkFlag = true;
                string str = LanguageMgr.GetText(key);
                string showStr = "";

                for(int i = 0; i < str.Length && nowTalkFlag; i++)
                {
                    if(i%2== 0)
                        SetNPCSprite(npcSprites0);
                    else
                        SetNPCSprite(npcSprites1);

                    if (i + 1 < str.Length && str[i] == '\\' && str[i+1] == 'n')
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

                soundMgr.StopLoopSE(sound);
                nowTalkFlag = false;
                SetNPCSprite(npcSprites0);
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
        //�ν����ͷ� ����� �����Լ���
        //��� �ѱ������� ���ɵ���
        if(nowTalkFlag)
            nowTalkFlag = false;
        else if (waitFlag)
            waitFlag = false;
    }
}
