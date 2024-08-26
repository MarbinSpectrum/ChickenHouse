using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetMoney_UI : Mgr
{
    [SerializeField] private Animation ani;
    [SerializeField] private TextMeshProUGUI textUI;
    public void RunAni(int num)
    {
        textUI.text = string.Format("{0:N0} $", num);
        ani.Play();

        soundMgr.PlaySE(Sound.GetMoney_SE);
    }
}
