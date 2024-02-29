using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GuestObj : Mgr
{
    /** �մ� ���� **/
    [SerializeField] protected GuestData guestData;
    /** �ִϸ����� **/
    [SerializeField] protected Animator animator;

    [SerializeField] protected TalkBox_UI talkBox;

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

    public virtual void CreateMenu(float orderTime)
    {
        bool isTuto = tutoMgr.tutoComplete == false;

        requireMenu.CreateMenu(guestData, orderTime, isTuto);
    }

    public virtual void CloseTalkBox()
    {
        talkBox.CloseTalkBox();
    }

    public virtual void OrderGuest(NoParaDel fun = null)
    {
        animator.SetTrigger("Talk");
        talkBox.ShowText("�� ġŲ �Ѹ�����\n�ݶ� ��Ź�ؿ�.", () =>
         {
             fun?.Invoke();
             animator.SetTrigger("TalkEnd");
         });
    }

    public virtual void ThankGuest(NoParaDel fun = null)
    {
        animator.SetTrigger("Talk");
        talkBox.ShowText("�� ġŲ �Ѹ�����\n�ݶ� ��Ź�ؿ�.", () =>
        {
            animator.SetTrigger("TalkEnd");
        });
    }

    public virtual void AngryGuest(NoParaDel fun = null)
    {
        animator.SetTrigger("Angry");
        talkBox.ShowText("�� ���� ġŲ?", () =>
        {
      
        });
    }

    public float ChickenPoint(int chickenCnt, ChickenSpicy spicy0, ChickenSpicy spicy1, ChickenState chickenState,
                            bool hasDrink, bool hasPickle)
    {
        //�մ��� ������ ġŲ ����

        //�⺻ ġŲ ����
        float defaultPoint = gameMgr.playData.GetDefaultPoint();

        float point = requireMenu.MenuPoint(guestData, defaultPoint, chickenCnt, spicy0, spicy1, chickenState, hasDrink, hasPickle);

        return point;
    }
}
