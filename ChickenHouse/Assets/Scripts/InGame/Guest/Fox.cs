using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : GuestObj
{
    public override void OrderGuest(NoParaDel fun = null)
    {
        int menuIdx = requireMenu.menuIdx;

        talkStr = string.Empty;
        switch(menuIdx)
        {
            case 0:
                talkStr = LanguageMgr.GetText("FOX_ORDER_1");
                break;
            case 1:
                talkStr = LanguageMgr.GetText("FOX_ORDER_2");
                break;
            case 2:
                talkStr = LanguageMgr.GetText("FOX_ORDER_3");
                break;
            case 3:
                talkStr = LanguageMgr.GetText("FOX_ORDER_4");
                break;
            case 4:
                talkStr = LanguageMgr.GetText("FOX_ORDER_5");
                break;
            case 5:
                talkStr = LanguageMgr.GetText("FOX_ORDER_6");
                break;
            case 6:
                talkStr = LanguageMgr.GetText("FOX_ORDER_7");
                break;
            case 7:
                talkStr = LanguageMgr.GetText("FOX_ORDER_8");
                break;
            case 8:
                talkStr = LanguageMgr.GetText("FOX_ORDER_9");
                break;
            case 9:
                talkStr = LanguageMgr.GetText("FOX_ORDER_10");
                break;
            case 10:
                talkStr = LanguageMgr.GetText("FOX_ORDER_11");
                break;
        }

        switch(requireMenu.drink)
        {
            case Drink.Cola:
                if (menuIdx == 0)
                {
                    //대사에 콜라를 달라는 대사가 달려있음
                    break;
                }
                talkStr += "\n";
                talkStr += LanguageMgr.GetText("FOX_SIDE_COLA");
                break;
            case Drink.SuperPower:
                talkStr += "\n";
                talkStr += LanguageMgr.GetText("FOX_SIDE_SUPERPOWER");
                break;
            case Drink.LoveMelon:
                talkStr += "\n";
                talkStr += LanguageMgr.GetText("FOX_SIDE_LOVEMELON");
                break;
            case Drink.SodaSoda:
                talkStr += "\n";
                talkStr += LanguageMgr.GetText("FOX_SIDE_SODASODA");
                break;
        }

        switch (requireMenu.sideMenu)
        {
            case SideMenu.ChickenRadish:
                if (menuIdx == 2)
                {
                    //대사에 치킨무를 달라는 대사가 달려있음
                    break;
                }
                talkStr += "\n";
                talkStr += LanguageMgr.GetText("FOX_SIDE_RADISH");
                break;
            case SideMenu.Pickle:
                talkStr += "\n";
                talkStr += LanguageMgr.GetText("FOX_SIDE_PICKLE");
                break;
            case SideMenu.Coleslaw:
                talkStr += "\n";
                talkStr += LanguageMgr.GetText("FOX_SIDE_COLESLAW");
                break;
            case SideMenu.CornSalad:
                talkStr += "\n";
                talkStr += LanguageMgr.GetText("FOX_SIDE_CORNSALAD");
                break;
            case SideMenu.FrenchFries:
                talkStr += "\n";
                talkStr += LanguageMgr.GetText("FOX_SIDE_FRENCHFRIES");
                break;
            case SideMenu.ChickenNugget:
                talkStr += "\n";
                talkStr += LanguageMgr.GetText("FOX_SIDE_CKICKENNUGGET");
                break;
        }

    }

    public override void TalkOrder(NoParaDel fun = null)
    {
        soundMgr.PlayLoopSE(Sound.Voice2_SE, 1f);
        animator.SetTrigger("Talk");
        talkBox.ShowText(talkStr, TalkBoxType.Normal, () =>
        {
            soundMgr.StopLoopSE(Sound.Voice2_SE);
            animator.SetTrigger("TalkEnd");
            fun?.Invoke();
        });
    }

    public override void CloseTalkBox()
    {
        soundMgr.StopLoopSE(Sound.Voice1_SE);
        soundMgr.StopLoopSE(Sound.Voice2_SE);
        talkBox.CloseTalkBox();
    }

    public override void HappyGuest(NoParaDel fun = null)
    {
        talkStr = LanguageMgr.GetText("FOX_HAPPY");

        soundMgr.PlayLoopSE(Sound.Voice1_SE, 1f);
        animator.SetTrigger("Happy");
        talkBox.ShowText(talkStr, TalkBoxType.Happy, () =>
        {
            soundMgr.StopLoopSE(Sound.Voice1_SE);
            animator.SetTrigger("TalkEnd");
            fun?.Invoke();
        });
    }

    public override void ThankGuest(NoParaDel fun = null)
    {
        talkStr = LanguageMgr.GetText("FOX_THANK_YOU");

        soundMgr.PlayLoopSE(Sound.Voice1_SE, 1f);
        animator.SetTrigger("Talk");
        talkBox.ShowText(talkStr, TalkBoxType.Thank, () =>
        {
            soundMgr.StopLoopSE(Sound.Voice1_SE);
            animator.SetTrigger("TalkEnd");
            fun?.Invoke();
        });
    }

    public override void AngryGuest(NoParaDel fun = null)
    {
        talkStr = LanguageMgr.GetText("FOX_ANGRY");

        animator.SetTrigger("Angry");
        talkBox.ShowText(talkStr, TalkBoxType.Angry, () =>
        {
            fun?.Invoke();
        });
    }
}
