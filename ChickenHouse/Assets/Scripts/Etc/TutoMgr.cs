using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TutoMgr : AwakeSingleton<TutoMgr>
{
    public Tutorial nowTuto     { private set; get; }

    public bool tutoComplete;

    private TutoText tutoText;

    protected override void Awake()
    {
        base.Awake();
#if (TEST_TUTO == false)
        tutoComplete = PlayerPrefs.GetInt("TUTO", 0) == 1;
#endif

    }

    //---------------------------------------------------------------------------------------------

    public void RegistTutoText(TutoText pTutoText)
    {
        //튜토리얼 텍스트 등록
        tutoText = pTutoText;
    }

    public void ShowText(Tutorial tutoType)
    {
        if (tutoText == null)
            return;
        tutoText.ShowText(tutoType);
    }

    public void CloseText() => tutoText.CloseText();

    //---------------------------------------------------------------------------------------------

    public void SetTuto(Tutorial tutoType)
    {
        nowTuto = tutoType;
    }
}
