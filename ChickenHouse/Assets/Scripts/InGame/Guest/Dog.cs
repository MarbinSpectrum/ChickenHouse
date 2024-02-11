using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : GuestObj
{
    public override void OrderGuest()
    {
        List<string> sideMenuName = requireMenu.GetSideMenuName();
        string chickenName = requireMenu.GetChickenName();
        string showStr = string.Empty;

        if (sideMenuName.Count == 0)
        {
            showStr = LanguageMgr.GetText("DOG_ORDER_1");
            showStr = string.Format(showStr, chickenName);
        }
        else if (sideMenuName.Count == 1)
        {
            showStr = LanguageMgr.GetText("DOG_ORDER_2");
            showStr = string.Format(showStr, chickenName, sideMenuName[0]);
        }
        else if (sideMenuName.Count == 2)
        {
            showStr = LanguageMgr.GetText("DOG_ORDER_3");
            showStr = string.Format(showStr, chickenName, sideMenuName[0], sideMenuName[1]);
        }

        soundMgr.PlayLoopSE(Sound.Voice0_SE);
        animator.SetTrigger("Talk");
        talkBox.ShowText(showStr, () =>
        {
            soundMgr.StopLoopSE(Sound.Voice0_SE);
            animator.SetTrigger("TalkEnd");
        });
    }

    public override void ThankGuest()
    {
        soundMgr.PlayLoopSE(Sound.Voice0_SE);
        string showStr = LanguageMgr.GetText("DOG_THANK_YOU");
        animator.SetTrigger("Talk");
        talkBox.ShowText(showStr, () =>
        {
            soundMgr.StopLoopSE(Sound.Voice0_SE);
            animator.SetTrigger("TalkEnd");
        });
    }
}
