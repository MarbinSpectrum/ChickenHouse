using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Serval : GuestObj
{
    public override void OrderGuest(NoParaDel fun = null)
    {
        int menuIdx = requireMenu.menuIdx;

        talkStr = string.Empty;
        switch (menuIdx)
        {
            case 0:
                talkStr = LanguageMgr.GetText("SERVAL_ORDER_1");
                break;
            case 1:
                talkStr = LanguageMgr.GetText("SERVAL_ORDER_2");
                break;
            case 2:
                talkStr = LanguageMgr.GetText("SERVAL_ORDER_3");
                break;
            case 3:
                talkStr = LanguageMgr.GetText("SERVAL_ORDER_4");
                break;
            case 4:
                talkStr = LanguageMgr.GetText("SERVAL_ORDER_5");
                break;
            case 5:
                talkStr = LanguageMgr.GetText("SERVAL_ORDER_6");
                break;
            case 6:
                talkStr = LanguageMgr.GetText("SERVAL_ORDER_7");
                break;
            case 7:
                talkStr = LanguageMgr.GetText("SERVAL_ORDER_8");
                break;
            case 8:
                talkStr = LanguageMgr.GetText("SERVAL_ORDER_9");
                break;
            case 9:
                talkStr = LanguageMgr.GetText("SERVAL_ORDER_10");
                break;
            case 10:
                talkStr = LanguageMgr.GetText("SERVAL_ORDER_11");
                break;
        }

        switch (requireMenu.drink)
        { 
            case Drink.Anything:
                talkStr += "\n";
                talkStr += LanguageMgr.GetText("SERVAL_SIDE_ANY");
                break;
        }

        switch (requireMenu.sideMenu)
        {
            case SideMenu.ChickenRadish:
                talkStr += "\n";
                talkStr += LanguageMgr.GetText("SERVAL_SIDE_RADISH");
                break;
            case SideMenu.Pickle:
                talkStr += "\n";
                talkStr += LanguageMgr.GetText("SERVAL_SIDE_PICKLE");
                break;
            case SideMenu.Coleslaw:
                talkStr += "\n";
                talkStr += LanguageMgr.GetText("SERVAL_SIDE_COLESLAW");
                break;
            case SideMenu.CornSalad:
                talkStr += "\n";
                talkStr += LanguageMgr.GetText("SERVAL_SIDE_CORNSALAD");
                break;
            case SideMenu.FrenchFries:
                talkStr += "\n";
                talkStr += LanguageMgr.GetText("SERVAL_SIDE_FRENCHFRIES");
                break;
            case SideMenu.ChickenNugget:
                talkStr += "\n";
                talkStr += LanguageMgr.GetText("SERVAL_SIDE_CKICKENNUGGET");
                break;
        }
    }

    public override void TalkOrder(NoParaDel fun = null)
    {
        soundMgr.PlayLoopSE(Sound.Voice17_SE, 0.85f);
        animator.SetTrigger("Talk");
        talkBox.ShowText(talkStr, TalkBoxType.Normal, () =>
        {
            soundMgr.StopLoopSE(Sound.Voice17_SE);
            animator.SetTrigger("TalkEnd");
            fun?.Invoke();
        });
    }

    public override void CloseTalkBox()
    {
        soundMgr.StopLoopSE(Sound.Voice17_SE);
        soundMgr.StopLoopSE(Sound.Voice18_SE);
        talkBox.CloseTalkBox();
    }

    public override void HappyGuest(NoParaDel fun = null)
    {
        talkStr = LanguageMgr.GetText("SERVAL_HAPPY");

        soundMgr.PlayLoopSE(Sound.Voice17_SE, 0.85f);
        animator.SetTrigger("Happy");
        talkBox.ShowText(talkStr, TalkBoxType.Happy, () =>
        {
            soundMgr.StopLoopSE(Sound.Voice17_SE);
            animator.SetTrigger("TalkEnd");
            fun?.Invoke();
        });
    }

    public override void ThankGuest(NoParaDel fun = null)
    {
        talkStr = LanguageMgr.GetText("SERVAL_THANK_YOU");

        soundMgr.PlayLoopSE(Sound.Voice17_SE, 0.85f);
        animator.SetTrigger("Talk");
        talkBox.ShowText(talkStr, TalkBoxType.Thank, () =>
        {
            soundMgr.StopLoopSE(Sound.Voice17_SE);
            animator.SetTrigger("TalkEnd");
            fun?.Invoke();
        });
    }

    public override void AngryGuest(NoParaDel fun = null)
    {
        talkStr = LanguageMgr.GetText("SERVAL_ANGRY");

        soundMgr.PlayLoopSE(Sound.Voice18_SE, 0.85f);
        animator.SetTrigger("Angry");
        talkBox.ShowText(talkStr, TalkBoxType.Angry, () =>
        {
            soundMgr.StopLoopSE(Sound.Voice18_SE);
            fun?.Invoke();
        });
    }
}