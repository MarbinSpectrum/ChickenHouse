using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WakeupMsg : Mgr
{
    public static bool wakeUpFlag = false;
    [SerializeField] private TextMeshProUGUI msgText;
    public void SetUI()
    {
        gameObject.SetActive(true);
        wakeUpFlag = false;
        string strFormat = LanguageMgr.GetText("WAKE_UP_MSG");
        string str = string.Format(strFormat, gameMgr.playData.day);
        LanguageMgr.SetText(msgText, str);
    }

    public void CloseMsgBox()
    {
        gameObject.SetActive(false);
    }
}
