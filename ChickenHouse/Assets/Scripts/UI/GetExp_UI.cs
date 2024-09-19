using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GetExp_UI : Mgr
{
    [SerializeField] private TextMeshProUGUI    lvText;
    [SerializeField] private TextMeshProUGUI    expText;
    [SerializeField] private Image              expBar;
    private const string LV_FORMAT = "Lv {0}";
    private const string EXP_FORMAT = "{0}/{1}";

    public void SetUI(int pExp, NoParaDel pFun)
    {
        int nowLv = gameMgr.playData.cookLv;
        int nowExp = gameMgr.playData.cookExp;

        if (nowLv == cookLvMgr.MAX_LV)
        {
            pFun?.Invoke();
            return;
        }
        else
        {
            IEnumerator ExpAni(int nowExp, int targetExp, int requireExp)
            {
                float fromAmount = (float)nowExp / (float)requireExp;
                float targetAmount = (float)targetExp / (float)requireExp;
                do
                {
                    float lerpValue = Mathf.Lerp(fromAmount, targetAmount, 0.01f);
                    int lerpExpValue = (int)Mathf.Lerp(nowExp, targetExp, 0.01f);
                    fromAmount = lerpValue;
                    nowExp = lerpExpValue;
                    expBar.fillAmount = lerpValue;
                    LanguageMgr.SetText(expText, string.Format(EXP_FORMAT, nowExp, requireExp));

                    if (Mathf.Abs(fromAmount - targetAmount) < 0.01f)
                        break;
                    yield return null;

                } while (true);
                expBar.fillAmount = targetAmount;
                LanguageMgr.SetText(expText, string.Format(EXP_FORMAT, targetExp, requireExp));

            }

            IEnumerator Run()
            {
                int requireExp = cookLvMgr.RequireExp(nowLv + 1);
                int tempExp = pExp;
                expBar.fillAmount = (float)nowExp / (float)requireExp;

                while (tempExp > 0)
                {
                    if (nowLv < cookLvMgr.MAX_LV)
                    {
                        if (nowExp + tempExp >= requireExp)
                        {
                            yield return StartCoroutine(ExpAni(nowExp, requireExp, requireExp));
                            tempExp -= (requireExp - nowExp);
                            nowExp = 0;

                            nowLv++;
                            requireExp = cookLvMgr.RequireExp(nowLv + 1);

                        }
                        else
                        {
                            yield return StartCoroutine(ExpAni(nowExp, tempExp, requireExp));
                            nowExp = tempExp;
                            tempExp = 0;
                        }
                        yield return new WaitForSeconds(0.1f);
                    }
                    else
                    {
                        break;
                    }
                }

                gameMgr.playData.cookLv = nowLv;

                pFun?.Invoke();
            }
            StartCoroutine(Run());
        }

    }
}
