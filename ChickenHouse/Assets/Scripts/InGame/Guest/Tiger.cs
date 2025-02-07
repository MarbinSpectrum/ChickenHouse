using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiger : GuestObj
{
    public override void OrderGuest(NoParaDel fun = null)
    {
        int menuIdx = requireMenu.menuIdx;

        talkStr = string.Empty;
        if (menuIdx == 0)
            talkStr = LanguageMgr.GetText("TIGER_ORDER_1");
        else if (menuIdx == 1)
            talkStr = LanguageMgr.GetText("TIGER_ORDER_2");
        else if (menuIdx == 2)
            talkStr = LanguageMgr.GetText("TIGER_ORDER_3");
        else if (menuIdx == 3)
            talkStr = LanguageMgr.GetText("TIGER_ORDER_4");
        else if (menuIdx == 4)
            talkStr = LanguageMgr.GetText("TIGER_ORDER_5");
        else if (menuIdx == 5)
            talkStr = LanguageMgr.GetText("TIGER_ORDER_6");
        else if (menuIdx == 6)
            talkStr = LanguageMgr.GetText("TIGER_ORDER_7");
        else if (menuIdx == 7)
            talkStr = LanguageMgr.GetText("TIGER_ORDER_8");
        else if (menuIdx == 8)
            talkStr = LanguageMgr.GetText("TIGER_ORDER_9");
        else if (menuIdx == 9)
            talkStr = LanguageMgr.GetText("TIGER_ORDER_10");
        else if (menuIdx == 10)
            talkStr = LanguageMgr.GetText("TIGER_ORDER_11");

        if (requireMenu.drink == Drink.Cola)
        {
            talkStr += "\n";
            talkStr += LanguageMgr.GetText("TIGER_SIDE_COLA");
        }
        if (requireMenu.sideMenu == SideMenu.ChickenRadish)
        {
            talkStr += "\n";
            talkStr += LanguageMgr.GetText("TIGER_SIDE_PICKLE");
        }
    }

    public override void TalkOrder(NoParaDel fun = null)
    {
        soundMgr.PlayLoopSE(Sound.Voice13_SE);
        animator.SetTrigger("Talk");
        talkBox.ShowText(talkStr, TalkBoxType.Normal, () =>
        {
            soundMgr.StopLoopSE(Sound.Voice13_SE);
            animator.SetTrigger("TalkEnd");
            fun?.Invoke();
        });
    }

    public override void CloseTalkBox()
    {
        soundMgr.StopLoopSE(Sound.Voice13_SE);
        soundMgr.StopLoopSE(Sound.Voice14_SE);
        talkBox.CloseTalkBox();
    }

    public override void HappyGuest(NoParaDel fun = null)
    {
        talkStr = LanguageMgr.GetText("TIGER_HAPPY");

        soundMgr.PlayLoopSE(Sound.Voice13_SE);
        animator.SetTrigger("Happy");
        talkBox.ShowText(talkStr, TalkBoxType.Happy, () =>
        {
            soundMgr.StopLoopSE(Sound.Voice13_SE);
            animator.SetTrigger("TalkEnd");
            fun?.Invoke();
        });
    }

    public override void ThankGuest(NoParaDel fun = null)
    {
        talkStr = LanguageMgr.GetText("TIGER_THANK_YOU");

        soundMgr.PlayLoopSE(Sound.Voice13_SE);
        animator.SetTrigger("Talk");
        talkBox.ShowText(talkStr, TalkBoxType.Thank, () =>
        {
            soundMgr.StopLoopSE(Sound.Voice13_SE);
            animator.SetTrigger("TalkEnd");
            fun?.Invoke();
        });
    }

    public override void AngryGuest(NoParaDel fun = null)
    {
        talkStr = LanguageMgr.GetText("TIGER_ANGRY");

        soundMgr.PlayLoopSE(Sound.Voice14_SE);
        animator.SetTrigger("Angry");
        talkBox.ShowText(talkStr, TalkBoxType.Angry, () =>
        {
            soundMgr.StopLoopSE(Sound.Voice14_SE);
            fun?.Invoke();
        });
    }
}