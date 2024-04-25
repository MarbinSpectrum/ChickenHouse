using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GuestObj : Mgr
{
    /** ¼Õ´Ô Á¤º¸ **/
    [SerializeField] protected  GuestData       guestData;
    /** ¾Ö´Ï¸ŞÀÌÅÍ **/
    [SerializeField] protected  Animator        animator;
    /** ½ºÇÁ¶óÀÌÆ® **/
    [SerializeField] protected SpriteRenderer   spriteRenderer;
    [SerializeField] protected  TalkBox_UI      talkBox;
    [SerializeField] public     Guest           guest;

    protected RequireMenu requireMenu = new RequireMenu();

    public virtual void ShowGuest()
    {
        //¼Õ´Ô ³ª¿À°ÔÇÏ±â
        animator.SetBool("Show", true);
    }

    public virtual void LeaveGuest()
    {
        //¼Õ´Ô ¶°³ª°ÔÇÏ±â
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
        talkBox.ShowText("ÇÖ Ä¡Å² ÇÑ¸¶¸®¶û\nÄİ¶ó ºÎÅ¹ÇØ¿ä.", () =>
         {
             fun?.Invoke();
             animator.SetTrigger("TalkEnd");
         });
    }

    public virtual void HappyGuest(NoParaDel fun = null)
    {
        animator.SetTrigger("Talk");
        talkBox.ShowText("ÇÖ Ä¡Å² ÇÑ¸¶¸®¶û\nÄİ¶ó ºÎÅ¹ÇØ¿ä.", () =>
        {
            animator.SetTrigger("TalkEnd");
        });
    }

    public virtual void ThankGuest(NoParaDel fun = null)
    {
        animator.SetTrigger("Talk");
        talkBox.ShowText("ÇÖ Ä¡Å² ÇÑ¸¶¸®¶û\nÄİ¶ó ºÎÅ¹ÇØ¿ä.", () =>
        {
            animator.SetTrigger("TalkEnd");
        });
    }

    public virtual void AngryGuest(NoParaDel fun = null)
    {
        animator.SetTrigger("Angry");
        talkBox.ShowText("ÀÌ µı°Ô Ä¡Å²?", () =>
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
