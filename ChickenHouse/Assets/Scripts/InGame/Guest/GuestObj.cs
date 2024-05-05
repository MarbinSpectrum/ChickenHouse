using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GuestObj : Mgr
{
    /** 손님 정보 **/
    [SerializeField] protected  GuestData       guestData;
    /** 애니메이터 **/
    [SerializeField] protected  Animator        animator;
    /** 스프라이트 **/
    [SerializeField] protected SpriteRenderer   spriteRenderer;
    [SerializeField] protected  TalkBox_UI      talkBox;
    [SerializeField] public     Guest           guest;

    protected RequireMenu requireMenu = new RequireMenu();
    protected string talkStr;

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

    public virtual bool CreateMenu(float orderTime)
    {
        bool isTuto = tutoMgr.tutoComplete == false;

        return requireMenu.CreateMenu(guestData, orderTime, isTuto);
    }

    public virtual void CloseTalkBox()
    {
        talkBox.CloseTalkBox();
    }

    public virtual void OrderGuest(NoParaDel fun = null)
    {
        //주문내력에 해당하는 talkStr을 만듬
    }

    public virtual void TalkOrder(NoParaDel fun = null)
    {
        animator.SetTrigger("Talk");
        talkBox.ShowText("핫 치킨 한마리랑\n콜라 부탁해요.", TalkBoxType.Normal, () =>
        {
            fun?.Invoke();
            animator.SetTrigger("TalkEnd");
        });
    }

    public virtual void HappyGuest(NoParaDel fun = null)
    {
        animator.SetTrigger("Talk");
        talkBox.ShowText("핫 치킨 한마리랑\n콜라 부탁해요.", TalkBoxType.Happy, () =>
        {
            fun?.Invoke();
            animator.SetTrigger("TalkEnd");
        });
    }

    public virtual void ThankGuest(NoParaDel fun = null)
    {
        animator.SetTrigger("Talk");
        talkBox.ShowText("핫 치킨 한마리랑\n콜라 부탁해요.", TalkBoxType.Normal, () =>
        {
            animator.SetTrigger("TalkEnd");
        });
    }

    public virtual void AngryGuest(NoParaDel fun = null)
    {
        animator.SetTrigger("Angry");
        talkBox.ShowText("이 딴게 치킨?", TalkBoxType.Angry, () =>
        {
            fun?.Invoke();
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

    public string GetTalkText() => talkStr;

    public Sprite GetGuestFace() => guestData.face;

    public int GetShowDay() => guestData.day;

    public void SkipTalk() => talkBox.SkipTalk();
}
