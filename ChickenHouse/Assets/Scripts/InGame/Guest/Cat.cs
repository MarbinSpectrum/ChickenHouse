using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : GuestObj
{
    public override void OrderGuest(NoParaDel fun = null)
    {
        int menuIdx = requireMenu.menuIdx;

        string showStr = string.Empty;
        if (menuIdx == 0)
        {
            showStr = LanguageMgr.GetText("CAT_ORDER_1");
            if (requireMenu.drink == Drink.Cola)
            {
                showStr += "\n";
                showStr += LanguageMgr.GetText("CAT_SIDE_COLA");
            }
            if (requireMenu.sideMenu == SideMenu.Pickle)
            {
                showStr += "\n";
                showStr += LanguageMgr.GetText("CAT_SIDE_PICKLE");
            }
        }
        else if (menuIdx == 1)
        {
            showStr = LanguageMgr.GetText("CAT_ORDER_2");
            if (requireMenu.drink == Drink.Cola)
            {
                showStr += "\n";
                showStr += LanguageMgr.GetText("CAT_SIDE_COLA");
            }
            if (requireMenu.sideMenu == SideMenu.Pickle)
            {
                showStr += "\n";
                showStr += LanguageMgr.GetText("CAT_SIDE_PICKLE");
            }
        }
        else if (menuIdx == 2)
        {
            showStr = LanguageMgr.GetText("CAT_ORDER_3");
            if (requireMenu.drink == Drink.Cola)
            {
                showStr += "\n";
                showStr += LanguageMgr.GetText("CAT_SIDE_COLA");
            }
            if (requireMenu.sideMenu == SideMenu.Pickle)
            {
                showStr += "\n";
                showStr += LanguageMgr.GetText("CAT_SIDE_PICKLE");
            }
        }

        soundMgr.PlayLoopSE(Sound.Voice1_SE);
        animator.SetTrigger("Talk");
        talkBox.ShowText(showStr, () =>
        {
            soundMgr.StopLoopSE(Sound.Voice1_SE);
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
        string showStr = LanguageMgr.GetText("CAT_HAPPY");

        soundMgr.PlayLoopSE(Sound.Voice2_SE);
        animator.SetTrigger("Happy");
        talkBox.ShowText(showStr, () =>
        {
            soundMgr.StopLoopSE(Sound.Voice2_SE);
            animator.SetTrigger("TalkEnd");
            fun?.Invoke();
        });
    }

    public override void ThankGuest(NoParaDel fun = null)
    {
        string showStr = LanguageMgr.GetText("CAT_THANK_YOU");

        soundMgr.PlayLoopSE(Sound.Voice2_SE);
        animator.SetTrigger("Talk");
        talkBox.ShowText(showStr, () =>
        {
            soundMgr.StopLoopSE(Sound.Voice2_SE);
            animator.SetTrigger("TalkEnd");
            fun?.Invoke();
        });
    }

    public override void AngryGuest(NoParaDel fun = null)
    {
        string showStr = LanguageMgr.GetText("CAT_ANGRY");

        soundMgr.PlayLoopSE(Sound.Voice1_SE);
        animator.SetTrigger("Angry");
        talkBox.ShowText(showStr, () =>
        {
            soundMgr.StopLoopSE(Sound.Voice1_SE);
            fun?.Invoke();
        });
    }
}
