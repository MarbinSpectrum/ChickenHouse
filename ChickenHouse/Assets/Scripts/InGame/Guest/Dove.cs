using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dove : GuestObj
{
    public override void OrderGuest(NoParaDel fun = null)
    {
        List<string> sideMenuName = requireMenu.GetSideMenuName();
        string chickenName = requireMenu.GetChickenName();
        string showStr = string.Empty;

        if (sideMenuName.Count == 0)
        {
            showStr = LanguageMgr.GetText("CAT_ORDER_1");
            showStr = string.Format(showStr, chickenName);
        }
        else if (sideMenuName.Count == 1)
        {
            showStr = LanguageMgr.GetText("CAT_ORDER_2");
            showStr = string.Format(showStr, chickenName, sideMenuName[0]);
        }
        else if (sideMenuName.Count == 2)
        {
            showStr = LanguageMgr.GetText("CAT_ORDER_3");
            showStr = string.Format(showStr, chickenName, sideMenuName[0], sideMenuName[1]);
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