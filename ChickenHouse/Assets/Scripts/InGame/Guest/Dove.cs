using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dove : GuestObj
{
    public override void OrderGuest(NoParaDel fun = null)
    {
        int menuIdx = requireMenu.menuIdx;

        talkStr = string.Empty;
        if (menuIdx == 0)
            talkStr = LanguageMgr.GetText("DOVE_ORDER_1");
        else if (menuIdx == 1)
            talkStr = LanguageMgr.GetText("DOVE_ORDER_2");
        else if (menuIdx == 2)
            talkStr = LanguageMgr.GetText("DOVE_ORDER_3");
        else if (menuIdx == 3)
            talkStr = LanguageMgr.GetText("DOVE_ORDER_4");
        else if (menuIdx == 4)
            talkStr = LanguageMgr.GetText("DOVE_ORDER_5");
        else if (menuIdx == 5)
            talkStr = LanguageMgr.GetText("DOVE_ORDER_6");
        else if (menuIdx == 6)
            talkStr = LanguageMgr.GetText("DOVE_ORDER_7");
        else if (menuIdx == 7)
            talkStr = LanguageMgr.GetText("DOVE_ORDER_8");
        else if (menuIdx == 8)
            talkStr = LanguageMgr.GetText("DOVE_ORDER_9");
        else if (menuIdx == 9)
            talkStr = LanguageMgr.GetText("DOVE_ORDER_10");
        else if (menuIdx == 10)
            talkStr = LanguageMgr.GetText("DOVE_ORDER_11");

        if (requireMenu.drink == Drink.Cola)
        {
            talkStr += "\n";
            talkStr += LanguageMgr.GetText("DOVE_SIDE_COLA");
        }
        else if (requireMenu.drink == Drink.Beer)
        {
            talkStr += "\n";
            talkStr += LanguageMgr.GetText("DOVE_SIDE_BEER");
        }

        if (requireMenu.sideMenu == SideMenu.Pickle)
        {
            talkStr += "\n";
            talkStr += LanguageMgr.GetText("DOVE_SIDE_PICKLE");
        }
    }

    public override void TalkOrder(NoParaDel fun = null)
    {
        soundMgr.PlayLoopSE(Sound.Voice11_SE);
        animator.SetTrigger("Talk");
        talkBox.ShowText(talkStr, TalkBoxType.Normal, () =>
        {
            soundMgr.StopLoopSE(Sound.Voice11_SE);
            animator.SetTrigger("TalkEnd");
            fun?.Invoke();
        });
    }

    public override void CloseTalkBox()
    {
        soundMgr.StopLoopSE(Sound.Voice11_SE);
        soundMgr.StopLoopSE(Sound.Voice12_SE);
        talkBox.CloseTalkBox();
    }

    public override void HappyGuest(NoParaDel fun = null)
    {
        talkStr = LanguageMgr.GetText("DOVE_HAPPY");

        soundMgr.PlayLoopSE(Sound.Voice11_SE);
        animator.SetTrigger("Happy");
        talkBox.ShowText(talkStr, TalkBoxType.Happy, () =>
        {
            soundMgr.StopLoopSE(Sound.Voice11_SE);
            animator.SetTrigger("TalkEnd");
            fun?.Invoke();
        });
    }

    public override void ThankGuest(NoParaDel fun = null)
    {
        talkStr = LanguageMgr.GetText("DOVE_THANK_YOU");

        soundMgr.PlayLoopSE(Sound.Voice12_SE);
        animator.SetTrigger("Talk");
        talkBox.ShowText(talkStr, TalkBoxType.Thank, () =>
        {
            soundMgr.StopLoopSE(Sound.Voice12_SE);
            animator.SetTrigger("TalkEnd");
            fun?.Invoke();
        });
    }

    public override void AngryGuest(NoParaDel fun = null)
    {
        talkStr = LanguageMgr.GetText("DOVE_ANGRY");

        soundMgr.PlayLoopSE(Sound.Voice11_SE);
        animator.SetTrigger("Angry");
        talkBox.ShowText(talkStr, TalkBoxType.Angry, () =>
        {
            soundMgr.StopLoopSE(Sound.Voice11_SE);
            fun?.Invoke();
        });
    }
}