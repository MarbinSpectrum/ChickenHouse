using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamingo : GuestObj
{
    public override void OrderGuest(NoParaDel fun = null)
    {
        int menuIdx = requireMenu.menuIdx;

        string showStr = string.Empty;
        if (menuIdx == 0)
        {
            showStr = LanguageMgr.GetText("FLAMINGO_ORDER_1");
            if (requireMenu.drink == Drink.Cola)
            {
                showStr += "\n";
                showStr += LanguageMgr.GetText("FLAMINGO_SIDE_COLA");
            }
            if (requireMenu.sideMenu == SideMenu.Pickle)
            {
                showStr += "\n";
                showStr += LanguageMgr.GetText("FLAMINGO_SIDE_PICKLE");
            }
        }
        else if (menuIdx == 1)
        {
            showStr = LanguageMgr.GetText("FLAMINGO_ORDER_2");
            if (requireMenu.drink == Drink.Cola)
            {
                showStr += "\n";
                showStr += LanguageMgr.GetText("FLAMINGO_SIDE_COLA");
            }
            if (requireMenu.sideMenu == SideMenu.Pickle)
            {
                showStr += "\n";
                showStr += LanguageMgr.GetText("FLAMINGO_SIDE_PICKLE");
            }
        }
        else if (menuIdx == 2)
        {
            showStr = LanguageMgr.GetText("FLAMINGO_ORDER_3");
            if (requireMenu.drink == Drink.Cola)
            {
                showStr += "\n";
                showStr += LanguageMgr.GetText("FLAMINGO_SIDE_COLA");
            }
            if (requireMenu.sideMenu == SideMenu.Pickle)
            {
                showStr += "\n";
                showStr += LanguageMgr.GetText("FLAMINGO_SIDE_PICKLE");
            }
        }

        soundMgr.PlayLoopSE(Sound.Voice9_SE);
        animator.SetTrigger("Talk");
        talkBox.ShowText(showStr, TalkBoxType.Normal, () =>
        {
            soundMgr.StopLoopSE(Sound.Voice9_SE);
            animator.SetTrigger("TalkEnd");
            fun?.Invoke();
        });
    }

    public override void CloseTalkBox()
    {
        soundMgr.StopLoopSE(Sound.Voice9_SE);
        soundMgr.StopLoopSE(Sound.Voice10_SE);
        talkBox.CloseTalkBox();
    }

    public override void HappyGuest(NoParaDel fun = null)
    {
        string showStr = LanguageMgr.GetText("FLAMINGO_HAPPY");

        soundMgr.PlayLoopSE(Sound.Voice9_SE);
        animator.SetTrigger("Happy");
        talkBox.ShowText(showStr, TalkBoxType.Happy, () =>
        {
            soundMgr.StopLoopSE(Sound.Voice9_SE);
            animator.SetTrigger("TalkEnd");
            fun?.Invoke();
        });
    }

    public override void ThankGuest(NoParaDel fun = null)
    {
        string showStr = LanguageMgr.GetText("FLAMINGO_THANK_YOU");

        soundMgr.PlayLoopSE(Sound.Voice10_SE);
        animator.SetTrigger("Talk");
        talkBox.ShowText(showStr, TalkBoxType.Normal, () =>
        {
            soundMgr.StopLoopSE(Sound.Voice10_SE);
            animator.SetTrigger("TalkEnd");
            fun?.Invoke();
        });
    }

    public override void AngryGuest(NoParaDel fun = null)
    {
        string showStr = LanguageMgr.GetText("FLAMINGO_ANGRY");

        soundMgr.PlayLoopSE(Sound.Voice9_SE);
        animator.SetTrigger("Angry");
        talkBox.ShowText(showStr, TalkBoxType.Angry, () =>
        {
            soundMgr.StopLoopSE(Sound.Voice9_SE);
            fun?.Invoke();
        });
    }
}