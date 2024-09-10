using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TutoMgr : AwakeSingleton<TutoMgr>
{
    public Tutorial nowTuto     { private set; get; }

    //주방 조리 튜토리얼
    public bool tutoComplete1;

    //직원 배치 튜토리얼
    public bool tutoComplete2;

    //양념 배치 튜토리얼
    public bool tutoComplete3;

    private TutoText tutoText;

    protected override void Awake()
    {
        base.Awake();
#if (TEST_TUTO == false)
        tutoComplete1 = PlayerPrefs.GetInt("TUTO_1", 0) == 1;
        tutoComplete2 = PlayerPrefs.GetInt("TUTO_2", 0) == 1;
        tutoComplete3 = PlayerPrefs.GetInt("TUTO_3", 0) == 1;
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
