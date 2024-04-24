using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : GuestObj
{
    public override void OrderGuest(NoParaDel fun = null)
    {
        List<string> sideMenuName = requireMenu.GetSideMenuName();
        string chickenName = requireMenu.GetChickenName();
        string showStr = string.Empty;

        showStr = LanguageMgr.GetText("FOX_ORDER_1");

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
