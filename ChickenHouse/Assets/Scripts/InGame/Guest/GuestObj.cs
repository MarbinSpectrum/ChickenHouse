using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GuestObj : Mgr
{
    /** �մ� ���� **/
    [SerializeField] protected  GuestData       guestData;
    /** �ִϸ����� **/
    [SerializeField] protected  Animator        animator;
    /** ��������Ʈ **/
    [SerializeField] protected SpriteRenderer   spriteRenderer;
    [SerializeField] protected  TalkBox_UI      talkBox;
    [SerializeField] public     Guest           guest;

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

    public virtual void HappyGuest(NoParaDel fun = null)
    {
        animator.SetTrigger("Talk");
        talkBox.ShowText("�� ġŲ �Ѹ�����\n�ݶ� ��Ź�ؿ�.", () =>
        {
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

    public GuestReviews ChickenPoint(ChickenSpicy spicy0, ChickenSpicy spicy1, ChickenState chickenState,
        Drink pDrink, SideMenu pSideMenue)
    {
        GuestReviews result = requireMenu.MenuPoint(guestData, spicy0, spicy1, chickenState, pDrink, pSideMenue);

        return result;
    }

    public void SetOrderSprite(int order)
    {
        spriteRenderer.sortingOrder = order;
    }

    public void SetColor(Color pColor)
    {
        spriteRenderer.color = pColor;
    }

    public string GetTalkText() => talkBox.talkStr;

    public int GetShowDay() => guestData.day;
}
