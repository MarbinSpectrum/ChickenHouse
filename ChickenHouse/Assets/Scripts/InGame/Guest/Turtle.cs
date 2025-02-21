using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turtle : GuestObj
{
    public override void OrderGuest(NoParaDel fun = null)
    {
        int menuIdx = requireMenu.menuIdx;

        talkStr = string.Empty;
        switch (menuIdx)
        {
            case 0:
                talkStr = LanguageMgr.GetText("TURTLE_ORDER_1");
                break;
            case 1:
                talkStr = LanguageMgr.GetText("TURTLE_ORDER_2");
                break;
            case 2:
                talkStr = LanguageMgr.GetText("TURTLE_ORDER_3");
                break;
            case 3:
                talkStr = LanguageMgr.GetText("TURTLE_ORDER_4");
                break;
            case 4:
                talkStr = LanguageMgr.GetText("TURTLE_ORDER_5");
                break;
            case 5:
                talkStr = LanguageMgr.GetText("TURTLE_ORDER_6");
                break;
            case 6:
                talkStr = LanguageMgr.GetText("TURTLE_ORDER_7");
                break;
            case 7:
                talkStr = LanguageMgr.GetText("TURTLE_ORDER_8");
                break;
            case 8:
                talkStr = LanguageMgr.GetText("TURTLE_ORDER_9");
                break;
            case 9:
                talkStr = LanguageMgr.GetText("TURTLE_ORDER_10");
                break;
            case 10:
                talkStr = LanguageMgr.GetText("TURTLE_ORDER_11");
                break;
        }

        switch (requireMenu.drink)
        {
            case Drink.Cola:
                talkStr += "\n";
                talkStr += LanguageMgr.GetText("TURTLE_SIDE_COLA");
                break;
            case Drink.Beer:
                talkStr += "\n";
                talkStr += LanguageMgr.GetText("TURTLE_SIDE_BEER");
                break;
            case Drink.SuperPower:
                talkStr += "\n";
                talkStr += LanguageMgr.GetText("TURTLE_SIDE_SUPERPOWER");
                break;
            case Drink.LoveMelon:
                talkStr += "\n";
                talkStr += LanguageMgr.GetText("TURTLE_SIDE_LOVEMELON");
                break;
            case Drink.SodaSoda:
                talkStr += "\n";
                talkStr += LanguageMgr.GetText("TURTLE_SIDE_SODASODA");
                break;
        }

        switch (requireMenu.sideMenu)
        {
            case SideMenu.ChickenRadish:
                talkStr += "\n";
                talkStr += LanguageMgr.GetText("TURTLE_SIDE_RADISH");
                break;
            case SideMenu.Pickle:
                talkStr += "\n";
                talkStr += LanguageMgr.GetText("TURTLE_SIDE_PICKLE");
                break;
            case SideMenu.Coleslaw:
                talkStr += "\n";
                talkStr += LanguageMgr.GetText("TURTLE_SIDE_COLESLAW");
                break;
            case SideMenu.CornSalad:
                talkStr += "\n";
                talkStr += LanguageMgr.GetText("TURTLE_SIDE_CORNSALAD");
                break;
            case SideMenu.FrenchFries:
                talkStr += "\n";
                talkStr += LanguageMgr.GetText("TURTLE_SIDE_FRENCHFRIES");
                break;
            case SideMenu.ChickenNugget:
                talkStr += "\n";
                talkStr += LanguageMgr.GetText("TURTLE_SIDE_CKICKENNUGGET");
                break;
        }
    }

    public override void TalkOrder(NoParaDel fun = null)
    {
        soundMgr.PlayLoopSE(Sound.Voice31_SE, 0.85f);
        animator.SetTrigger("Talk");
        talkBox.ShowText(talkStr, TalkBoxType.Normal, () =>
        {
            soundMgr.StopLoopSE(Sound.Voice31_SE);
            animator.SetTrigger("TalkEnd");
            fun?.Invoke();
        });
    }

    public override void CloseTalkBox()
    {
        soundMgr.StopLoopSE(Sound.Voice31_SE);
        talkBox.CloseTalkBox();
    }

    public override void HappyGuest(NoParaDel fun = null)
    {
        talkStr = LanguageMgr.GetText("TURTLE_HAPPY");

        soundMgr.PlayLoopSE(Sound.Voice31_SE, 0.85f);
        animator.SetTrigger("Happy");
        talkBox.ShowText(talkStr, TalkBoxType.Happy, () =>
        {
            soundMgr.StopLoopSE(Sound.Voice31_SE);
            animator.SetTrigger("TalkEnd");
            fun?.Invoke();
        });
    }

    public override void ThankGuest(NoParaDel fun = null)
    {
        talkStr = LanguageMgr.GetText("TURTLE_THANK_YOU");

        soundMgr.PlayLoopSE(Sound.Voice31_SE, 0.85f);
        animator.SetTrigger("Talk");
        talkBox.ShowText(talkStr, TalkBoxType.Thank, () =>
        {
            soundMgr.StopLoopSE(Sound.Voice31_SE);
            animator.SetTrigger("TalkEnd");
            fun?.Invoke();
        });
    }

    public override void AngryGuest(NoParaDel fun = null)
    {
        talkStr = LanguageMgr.GetText("TURTLE_ANGRY");

        soundMgr.PlayLoopSE(Sound.Voice31_SE, 0.85f);
        animator.SetTrigger("Angry");
        talkBox.ShowText(talkStr, TalkBoxType.Angry, () =>
        {
            soundMgr.StopLoopSE(Sound.Voice31_SE);
            fun?.Invoke();
        });
    }
}
