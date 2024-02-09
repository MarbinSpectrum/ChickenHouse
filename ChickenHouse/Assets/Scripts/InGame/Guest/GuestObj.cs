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

    public virtual void OrderGuest()
    {
        talkBox.ShowText("�� ġŲ �Ѹ�����\n�ݶ� ��Ź�ؿ�.");
    }
}
