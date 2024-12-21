using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetMoney_UI : Mgr
{
    [SerializeField] private Animator ani;
    [SerializeField] private TextMeshProUGUI textUI;
    [SerializeField] private Animator tipAni;
    [SerializeField] private TextMeshProUGUI tipTtextUI;

    public void RunAni(int num,int tip)
    {
        string moneyStr = LanguageMgr.GetMoneyStr(textUI.fontSize, num);
        textUI.text = moneyStr;
        ani.SetTrigger("Show");

        if(tip != 0)
        {
            string tipStr = LanguageMgr.GetMoneyStr(tipTtextUI.fontSize, tip);
            tipTtextUI.text = string.Format("{0} +{1}", LanguageMgr.GetText("TIP"), tipStr);
            tipAni.SetTrigger("Show");
        }

        soundMgr.PlaySE(Sound.GetMoney_SE);
    }
}
