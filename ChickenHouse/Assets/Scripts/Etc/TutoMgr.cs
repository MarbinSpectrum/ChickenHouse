using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TutoMgr : AwakeSingleton<TutoMgr>
{
    public Tutorial nowTuto     { private set; get; }

    private TutoText tutoText;
    public bool runTutoFlag { private set; get; } = false;

    protected override void Awake()
    {
        base.Awake();
    }

    //---------------------------------------------------------------------------------------------

    public bool CanTuto()
    {
        if (tutoText != null)
            return true;
        return false;
    }

    public void RegistTutoText(TutoText pTutoText)
    {
        //튜토리얼 텍스트 등록
        tutoText = pTutoText;
    }

    public void ShowText(Tutorial tutoType)
    {
        if (CanTuto() == false)
            return;
        tutoText.ShowText(tutoType);
    }

    public void CloseText() => tutoText.CloseText();

    //---------------------------------------------------------------------------------------------

    public void SetTuto(Tutorial tutoType)
    {
        nowTuto = tutoType;
    }

    public static int GetTutoGroup(Tutorial tutoType)
    {
        switch(tutoType)
        {
            case Tutorial.Tuto_0:
            case Tutorial.Tuto_1:
            case Tutorial.Tuto_2:
            case Tutorial.Tuto_3:
            case Tutorial.Tuto_4:
            case Tutorial.Tuto_5:
            case Tutorial.Tuto_6:
            case Tutorial.Tuto_7:
            case Tutorial.Tuto_8_0:
            case Tutorial.Tuto_8_1:
            case Tutorial.Tuto_8_2:
            case Tutorial.Tuto_9_0:
            case Tutorial.Tuto_9_1:
            case Tutorial.Tuto_10:
            case Tutorial.Tuto_11:
            case Tutorial.Tuto_12:
                return 1;
            case Tutorial.Worker_Tuto_1_0:
            case Tutorial.Worker_Tuto_1_1:
            case Tutorial.Worker_Tuto_2_0:
            case Tutorial.Worker_Tuto_2_1:
            case Tutorial.Worker_Tuto_2_2:
                return 2;
            case Tutorial.Menu_Tuto_1:
            case Tutorial.Menu_Tuto_2_0:
            case Tutorial.Menu_Tuto_2_1:
            case Tutorial.Menu_Tuto_2_2:
                return 3;
            case Tutorial.Town_Tuto_1:
            case Tutorial.Town_Tuto_2:
            case Tutorial.Town_Tuto_3:
            case Tutorial.Town_Tuto_4:
            case Tutorial.Town_Tuto_5:
            case Tutorial.Town_Tuto_6:
            case Tutorial.Town_Tuto_7:
            case Tutorial.Town_Tuto_8:
            case Tutorial.Town_Tuto_9:
            case Tutorial.Town_Tuto_10:
            case Tutorial.Town_Tuto_11:
            case Tutorial.Town_Tuto_12:
            case Tutorial.Town_Tuto_13:
                return 4;
            case Tutorial.Event_0_Tuto_1:
            case Tutorial.Event_0_Tuto_2:
            case Tutorial.Event_0_Tuto_3:
            case Tutorial.Event_0_Tuto_4:
            case Tutorial.Event_0_Tuto_5:
            case Tutorial.Event_0_Tuto_6:
                return 5;
        }

        return 0;
    }

    public bool NowRunTuto()
    {
        //현재 튜토리얼 중인지여부를 반환
        PlayData playData = GameMgr.Instance?.playData;
        if (playData == null)
            return false;

        int tutoNum = GetTutoGroup(nowTuto);
        if (tutoNum == 1 && playData.tutoComplete1 == false)
            return true;
        if (tutoNum == 2 && playData.tutoComplete2 == false)
            return true;
        if (tutoNum == 3 && playData.tutoComplete3 == false)
            return true;
        if (tutoNum == 4 && playData.tutoComplete4 == false)
            return true;

        return false;
    }

    public void SetTutoFlag(bool pState) => runTutoFlag = pState;
}
