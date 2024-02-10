using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GuestObj : Mgr
{
    /** �մ� ���� **/
    [SerializeField] protected GuestData    guestData;
    /** �ִϸ����� **/
    [SerializeField] protected Animator     animator;

    [SerializeField] protected TalkBox_UI   talkBox;

    protected RequireMenu requireMenu = new RequireMenu();

    public virtual void ShowGuest()
    {
        //�մ� �������ϱ�
        animator.SetBool("Show", true);
    }

    public virtual void LeaveGuest()
    {
        //�մ� �������ϱ�
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
        talkBox.ShowText("�� ġŲ �Ѹ�����\n�ݶ� ��Ź�ؿ�.",()=>
        {
            animator.SetTrigger("TalkEnd");
        });
    }

    public virtual void ThankGuest()
    {
        animator.SetTrigger("Talk");
        talkBox.ShowText("�� ġŲ �Ѹ�����\n�ݶ� ��Ź�ؿ�.", () =>
        {
            animator.SetTrigger("TalkEnd");
        });
    }
}
