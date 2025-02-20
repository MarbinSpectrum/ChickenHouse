using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestTest : Mgr
{
    [SerializeField] private Guest guest;
    [SerializeField] private SpriteRenderer guestPos;
    
    public void Test()
    {
        //¼Õ´Ô»ý¼º
        IEnumerator Run()
        {
            guestMgr.RemoveAllGuest();

            GuestObj newGuest = guestMgr.GetGuestObj(guest);
            newGuest.gameObject.SetActive(true);
            newGuest.CloseTalkBox();
            newGuest.ShowGuest();
            newGuest.OrderGuest();
            newGuest.transform.position = guestPos.transform.position;
            newGuest.SetOrderSprite(guestPos.sortingOrder);
            newGuest.SetColor(guestPos.color);

            yield return new WaitForSeconds(1f);

            newGuest.TalkOrder();

        }
        StartCoroutine(Run());
    }
}
