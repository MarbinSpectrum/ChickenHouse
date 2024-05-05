using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirginiaOpossum : GuestObj
{
    public override void OrderGuest(NoParaDel fun = null)
    {
        int menuIdx = requireMenu.menuIdx;

        talkStr = string.Empty;
        if (menuIdx == 0)
        {
            talkStr = LanguageMgr.GetText("OPOSSUM_ORDER_1");
            if (requireMenu.drink == Drink.Cola)
            {
                talkStr += "\n";
                talkStr += LanguageMgr.GetText("OPOSSUM_SIDE_COLA");
            }
            if (requireMenu.sideMenu == SideMenu.Pickle)
            {
                talkStr += "\n";
                talkStr += LanguageMgr.GetText("OPOSSUM_SIDE_PICKLE");
            }
        }
        else if (menuIdx == 1)
        {
            talkStr = LanguageMgr.GetText("OPOSSUM_ORDER_2");
            if (requireMenu.drink == Drink.Cola)
            {
                talkStr += "\n";
                talkStr += LanguageMgr.GetText("OPOSSUM_SIDE_COLA");
            }
            if (requireMenu.sideMenu == SideMenu.Pickle)
            {
                talkStr += "\n";
                talkStr += LanguageMgr.GetText("OPOSSUM_SIDE_PICKLE");
            }
        }
        else if (menuIdx == 2)
        {
            talkStr = LanguageMgr.GetText("OPOSSUM_ORDER_3");
            if (requireMenu.drink == Drink.Cola)
            {
                talkStr += "\n";
                talkStr += LanguageMgr.GetText("OPOSSUM_SIDE_COLA");
            }
            if (requireMenu.sideMenu == SideMenu.Pickle)
            {
                talkStr += "\n";
                talkStr += LanguageMgr.GetText("OPOSSUM_SIDE_PICKLE");
            }
        }
    }

    public override void TalkOrder(NoParaDel fun = null)
    {
        soundMgr.PlayLoopSE(Sound.Voice7_SE);
        animator.SetTrigger("Talk");
        talkBox.ShowText(talkStr, TalkBoxType.Normal, () =>
        {
            soundMgr.StopLoopSE(Sound.Voice7_SE);
            animator.SetTrigger("TalkEnd");
            fun?.Invoke();
        });
    }

    public override void CloseTalkBox()
    {
        soundMgr.StopLoopSE(Sound.Voice7_SE);
        soundMgr.StopLoopSE(Sound.Voice8_SE);
        talkBox.CloseTalkBox();
    }

    public override void HappyGuest(NoParaDel fun = null)
    {
        talkStr = LanguageMgr.GetText("OPOSSUM_HAPPY");

        soundMgr.PlayLoopSE(Sound.Voice7_SE);
        animator.SetTrigger("Happy");
        talkBox.ShowText(talkStr, TalkBoxType.Happy, () =>
        {
            soundMgr.StopLoopSE(Sound.Voice7_SE);
            animator.SetTrigger("TalkEnd");
            fun?.Invoke();
        });
    }

    public override void ThankGuest(NoParaDel fun = null)
    {
        talkStr = LanguageMgr.GetText("OPOSSUM_THANK_YOU");

        soundMgr.PlayLoopSE(Sound.Voice7_SE);
        animator.SetTrigger("Talk");
        talkBox.ShowText(talkStr, TalkBoxType.Normal, () =>
        {
            soundMgr.StopLoopSE(Sound.Voice7_SE);
            animator.SetTrigger("TalkEnd");
            fun?.Invoke();
        });
    }

    public override void AngryGuest(NoParaDel fun = null)
    {
        talkStr = LanguageMgr.GetText("OPOSSUM_ANGRY");

        soundMgr.PlayLoopSE(Sound.Voice8_SE);
        animator.SetTrigger("Angry");
        talkBox.ShowText(talkStr, TalkBoxType.Angry, () =>
        {
            soundMgr.StopLoopSE(Sound.Voice8_SE);
            fun?.Invoke();
        });
    }
}