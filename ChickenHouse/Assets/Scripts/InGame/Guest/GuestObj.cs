using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GuestObj : Mgr
{
    /** 손님 정보 **/
    [SerializeField] protected GuestData    guestData;
    /** 애니메이터 **/
    [SerializeField] protected Animator     animator;

    [SerializeField] protected TalkBox_UI   talkBox;

    protected RequireMenu requireMenu = new RequireMenu();

    public virtual void ShowGuest()
    {
        //손님 나오게하기
        animator.SetBool("Show", true);
    }

    public virtual void LeaveGuest()
    {
        //손님 떠나게하기
        animator.SetBool("Show", false);
    }

    public virtual void CreateMenu()
    {
        requireMenu.CreateMenu(guestData, 0);
    }

    public void CloseTalkBox()
    {
        talkBox.CloseTalkBox();
    }

    public virtual void OrderGuest()
    {
        animator.SetTrigger("Talk");
        talkBox.ShowText("핫 치킨 한마리랑\n콜라 부탁해요.",()=>
        {
            animator.SetTrigger("TalkEnd");
        });
    }

    public virtual void ThankGuest()
    {
        animator.SetTrigger("Talk");
        talkBox.ShowText("핫 치킨 한마리랑\n콜라 부탁해요.", () =>
        {
            animator.SetTrigger("TalkEnd");
        });
    }
}
