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
        talkBox.ShowText("핫 치킨 한마리랑\n콜라 부탁해요.", () =>
         {
             fun?.Invoke();
             animator.SetTrigger("TalkEnd");
         });
    }

    public virtual void ThankGuest(NoParaDel fun = null)
    {
        animator.SetTrigger("Talk");
        talkBox.ShowText("핫 치킨 한마리랑\n콜라 부탁해요.", () =>
        {
            animator.SetTrigger("TalkEnd");
        });
    }

    public virtual void AngryGuest(NoParaDel fun = null)
    {
        animator.SetTrigger("Angry");
        talkBox.ShowText("이 딴게 치킨?", () =>
        {
      
        });
    }

    public GuestReviews ChickenPoint(ChickenSpicy spicy0, ChickenSpicy spicy1, ChickenState chickenState,
                            bool hasDrink, bool hasPickle)
    {
        GuestReviews result = requireMenu.MenuPoint(guestData, spicy0, spicy1, chickenState, hasDrink, hasPickle);

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
}
