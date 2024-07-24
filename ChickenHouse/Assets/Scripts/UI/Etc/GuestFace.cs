using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuestFace : Mgr
{
    [SerializeField] private Dictionary<Guest, Image> guestImgs = new Dictionary<Guest, Image>();

    private Guest gueset;

    public Image GetNowImg()
    {
        //현재 이미지 가져오기
        if (guestImgs.ContainsKey(gueset) == false)
            return null;

        return guestImgs[gueset];
    }

    public void SetUI(Guest pGuest)
    {
        gueset = pGuest;

        foreach (var pair in guestImgs)
        {
            Image img = pair.Value;
            img.gameObject.SetActive(false);
        }

        Image nowImg = GetNowImg();
        if (nowImg == null)
            return;

        nowImg.gameObject.SetActive(true);
    }
}
