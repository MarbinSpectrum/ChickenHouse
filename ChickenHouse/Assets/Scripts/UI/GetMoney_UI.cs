using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetMoney_UI : Mgr
{
    [SerializeField] private Animation ani;
    [SerializeField] private TextMeshProUGUI textUI;
    [SerializeField] private Animation tipAni;
    [SerializeField] private TextMeshProUGUI tipTtextUI;

    public void RunAni(int num,int tip)
    {
        textUI.text = string.Format("{0:N0} $", num);
        ani.Play();

        if(tip != 0)
        {
            tipTtextUI.text = string.Format("{0} +{1:N0} $", LanguageMgr.GetText("TIP"), num);
            tipAni.Play();
        }

        soundMgr.PlaySE(Sound.GetMoney_SE);
    }
}
