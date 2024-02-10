using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : GuestObj
{
    public override void OrderGuest()
    {
        List<string> sideMenuName = requireMenu.GetSideMenuName();
        string chickenName = requireMenu.GetChickenName();
        string showStr = string.Empty;

        if (sideMenuName.Count == 0)
        {
            showStr = LanguageMgr.GetText("FOX_ORDER_1");
            showStr = string.Format(showStr, chickenName);
        }
        else if (sideMenuName.Count == 1)
        {
            showStr = LanguageMgr.GetText("FOX_ORDER_2");
            showStr = string.Format(showStr, chickenName, sideMenuName[0]);
        }
        else if (sideMenuName.Count == 2)
        {
            showStr = LanguageMgr.GetText("FOX_ORDER_3");
            showStr = string.Format(showStr, chickenName, sideMenuName[0], sideMenuName[1]);
        }

        animator.SetTrigger("Talk");
        talkBox.ShowText(showStr, () =>
        {
            animator.SetTrigger("TalkEnd");
        });
    }

    public override void ThankGuest()
    {
        string showStr = LanguageMgr.GetText("FOX_THANK_YOU");
        animator.SetTrigger("Talk");
        talkBox.ShowText(showStr, () =>
        {
            animator.SetTrigger("TalkEnd");
        });
    }
}
