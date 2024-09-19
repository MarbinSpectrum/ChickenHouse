using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DiaryUI_BookGuestInfo : Mgr
{
    [SerializeField] private RectTransform rect;
    [SerializeField] private RectTransform              imgRect;
    [SerializeField] private Image                      gusetImg;
    [SerializeField] private TextMeshProUGUI            guestName;
    [SerializeField] private TextMeshProUGUI            guestInfo;
    [SerializeField] private RectTransform              drinkable;

    private const float IMG_WIDTH   = 600;
    private const float IMG_HEIGHT  = 512;

    private float firstWidth;
    private float firstHeight;
    private bool initSize = false;

    public void SetUI(Guest pGuest)
    {
        bool isAct = BookMgr.IsActGuest(pGuest);
        rect.gameObject.SetActive(isAct);

        SetImgSize(pGuest);

        GuestData guestData = guestMgr.GetGuestData(pGuest);
        if (guestData == null)
            return;
        gusetImg.sprite = guestData.bookImg;

        LanguageMgr.SetString(guestName, guestData.bookNameKey);
        LanguageMgr.SetString(guestInfo, guestData.bookInfoKey);

        if (guestData.canDrinkAlcohol)
            drinkable.gameObject.SetActive(true);
        else
            drinkable.gameObject.SetActive(false);
    }

    private void SetImgSize(Guest pGuest)
    {
        //이미지 사이즈 조절
        if (initSize == false)
        {
            firstWidth  = imgRect.sizeDelta.x;
            firstHeight = imgRect.sizeDelta.y;
            initSize = true;
        }

        GuestData guestData = guestMgr.GetGuestData(pGuest);
        if (guestData == null)
            return;
        Sprite sprite = guestData.bookImg;
        float tartGetRate = (float)sprite.texture.height / (float)sprite.texture.width;
        float newWidth = firstWidth;
        float newHeight = firstWidth * tartGetRate;

        imgRect.sizeDelta = new Vector2(newWidth, newHeight);
    }
}
