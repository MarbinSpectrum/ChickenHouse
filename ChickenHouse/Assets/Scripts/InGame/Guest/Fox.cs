using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : GuestObj
{
    public override void OrderGuest(NoParaDel fun = null)
    {
        int menuIdx = requireMenu.menuIdx;

        string showStr = string.Empty;
        if (menuIdx == 0)
        {
            showStr = LanguageMgr.GetText("FOX_ORDER_1");
            if (requireMenu.sideMenu == SideMenu.Pickle)
            {
                showStr += "\n";
                showStr += LanguageMgr.GetText("FOX_SIDE_PICKLE");
            }
        }
        else if (menuIdx == 1)
        {
            showStr = LanguageMgr.GetText("FOX_ORDER_2");
            if (requireMenu.drink == Drink.Cola)
            {
                showStr += "\n";
                showStr += LanguageMgr.GetText("FOX_SIDE_COLA");
            }
            if (requireMenu.sideMenu == SideMenu.Pickle)
            {
                showStr += "\n";
                showStr += LanguageMgr.GetText("FOX_SIDE_PICKLE");
            }
        }
        else if (menuIdx == 2)
        {
            showStr = LanguageMgr.GetText("FOX_ORDER_3");
            if (requireMenu.drink == Drink.Cola)
            {
                showStr += "\n";
                showStr += LanguageMgr.GetText("FOX_SIDE_COLA");
            }
        }

        soundMgr.PlayLoopSE(Sound.Voice2_SE);
        animator.SetTrigger("Talk");
        talkBox.ShowText(showStr, () =>
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
        string showStr = LanguageMgr.GetText("FOX_HAPPY");

        soundMgr.PlayLoopSE(Sound.Voice1_SE);
        animator.SetTrigger("Happy");
        talkBox.ShowText(showStr, () =>
        {
            soundMgr.StopLoopSE(Sound.Voice1_SE);
            animator.SetTrigger("TalkEnd");
            fun?.Invoke();
        });
    }

    public override void ThankGuest(NoParaDel fun = null)
    {
        string showStr = LanguageMgr.GetText("FOX_THANK_YOU");

        soundMgr.PlayLoopSE(Sound.Voice1_SE);
        animator.SetTrigger("Talk");
        talkBox.ShowText(showStr, () =>
        {
            soundMgr.StopLoopSE(Sound.Voice1_SE);
            animator.SetTrigger("TalkEnd");
            fun?.Invoke();
        });
    }

    public override void AngryGuest(NoParaDel fun = null)
    {
        string showStr = LanguageMgr.GetText("FOX_ANGRY");

        animator.SetTrigger("Angry");
        talkBox.ShowText(showStr, () =>
        {
            fun?.Invoke();
        });
    }
}
