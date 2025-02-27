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
    protected string talkStr;

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

    public virtual bool CreateMenu(float orderTime)
    {
        bool isTuto = (gameMgr.playData != null && gameMgr.playData.tutoComplete1 == false);

        return requireMenu.CreateMenu(guestData, orderTime, isTuto);
    }

    public virtual void CloseTalkBox()
    {
        talkBox.CloseTalkBox();
    }

    public virtual void OrderGuest(NoParaDel fun = null)
    {
        //�ֹ����¿� �ش��ϴ� talkStr�� ����
    }

    public virtual void TalkOrder(NoParaDel fun = null)
    {
        animator.SetTrigger("Talk");
        talkBox.ShowText("�� ġŲ �Ѹ�����\n�ݶ� ��Ź�ؿ�.", TalkBoxType.Normal, () =>
        {
            fun?.Invoke();
            animator.SetTrigger("TalkEnd");
        });
    }

    public virtual void HappyGuest(NoParaDel fun = null)
    {
        animator.SetTrigger("Talk");
        talkBox.ShowText("�� ġŲ �Ѹ�����\n�ݶ� ��Ź�ؿ�.", TalkBoxType.Happy, () =>
        {
            fun?.Invoke();
            animator.SetTrigger("TalkEnd");
        });
    }

    public virtual void ThankGuest(NoParaDel fun = null)
    {
        animator.SetTrigger("Talk");
        talkBox.ShowText("�� ġŲ �Ѹ�����\n�ݶ� ��Ź�ؿ�.", TalkBoxType.Normal, () =>
        {
            animator.SetTrigger("TalkEnd");
        });
    }

    public virtual void AngryGuest(NoParaDel fun = null)
    {
        animator.SetTrigger("Angry");
        talkBox.ShowText("�� ���� ġŲ?", TalkBoxType.Angry, () =>
        {
            fun?.Invoke();
        });
    }

    public virtual void WaitGuest()
    {
        talkBox.ShowWaitTalkBox();
    }


    public GuestReviews ChickenPoint(ChickenSpicy spicy0, ChickenSpicy spicy1, ChickenState chickenState,
        Drink pDrink, SideMenu pSideMenu)
    {
        GuestReviews result = requireMenu.MenuPoint(spicy0, spicy1, chickenState, pDrink, pSideMenu);

        return result;
    }

    public bool CheckChickenState(ChickenState chickenState)
    {
        bool result = requireMenu.CheckChickenState(chickenState);

        return result;
    }

    public bool CheckSpicy(ChickenSpicy spicy0, ChickenSpicy spicy1)
    {
        bool result = requireMenu.CheckSpicy(spicy0, spicy1);

        return result;
    }

    public bool CheckDrink(Drink pDrink)
    {
        bool result = requireMenu.CheckDrink(pDrink);

        return result;
    }

    public bool CheckSide(SideMenu pSideMenue)
    {
        bool result = requireMenu.CheckSide(pSideMenue);

        return result;
    }

    public void ShowResult(bool spicyResult, bool chickenStateResult, bool drinkResult, bool sideMenuResult) =>
        talkBox.ShowResult(spicyResult, chickenStateResult, drinkResult, sideMenuResult);

    public void CloseResult() => talkBox.CloseResult();

    public void SetOrderSprite(int order)
    {
        spriteRenderer.sortingOrder = order;
    }

    public void SetColor(Color pColor)
    {
        spriteRenderer.color = pColor;
    }

    public string GetTalkText() => talkStr;

    public Sprite GetGuestFace() => guestData.face;

    public int GetShowDay() => guestData.day;

    public void SkipTalk()
    {
        talkBox.SkipTalk();
    }
}
