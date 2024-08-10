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
    [SerializeField] private Dictionary<Guest, Sprite>  guestSprite = new Dictionary<Guest, Sprite>();

    private const float IMG_WIDTH   = 600;
    private const float IMG_HEIGHT  = 512;

    private float firstWidth;
    private float firstHeight;
    private bool initSize = false;

    public void SetUI(Guest pGuest)
    {
        bool isAct = bookMgr.IsActGuest(pGuest);
        rect.gameObject.SetActive(isAct);

        SetImgSize(pGuest);

        if (guestSprite.ContainsKey(pGuest) == false)
            return;
        Sprite sprite = guestSprite[pGuest];
        gusetImg.sprite = sprite;

        switch(pGuest)
        {
            case Guest.Fox:
                LanguageMgr.SetString(guestName, "BOOK_FOX_NAME");
                LanguageMgr.SetString(guestInfo, "BOOK_FOX_INFO");
                break;
            case Guest.Cat:
                LanguageMgr.SetString(guestName, "BOOK_CAT_NAME");
                LanguageMgr.SetString(guestInfo, "BOOK_CAT_INFO");
                break;
            case Guest.Dog:
                LanguageMgr.SetString(guestName, "BOOK_DOG_NAME");
                LanguageMgr.SetString(guestInfo, "BOOK_DOG_INFO");
                break;
            case Guest.Tiger:
                LanguageMgr.SetString(guestName, "BOOK_TIGER_NAME");
                LanguageMgr.SetString(guestInfo, "BOOK_TIGER_INFO");
                break;
            case Guest.Panda:
                LanguageMgr.SetString(guestName, "BOOK_PANDA_NAME");
                LanguageMgr.SetString(guestInfo, "BOOK_PANDA_INFO");
                break;
            case Guest.TasmanianDevil:
                LanguageMgr.SetString(guestName, "BOOK_TASMANIAN_NAME");
                LanguageMgr.SetString(guestInfo, "BOOK_TASMANIAN_INFO");
                break;
            case Guest.Flamingo:
                LanguageMgr.SetString(guestName, "BOOK_FLAMINGO_NAME");
                LanguageMgr.SetString(guestInfo, "BOOK_FLAMINGO_INFO");
                break;
            case Guest.RedPanda:
                LanguageMgr.SetString(guestName, "BOOK_REDPANDA_NAME");
                LanguageMgr.SetString(guestInfo, "BOOK_REDPANDA_INFO");
                break;
            case Guest.Lemur:
                LanguageMgr.SetString(guestName, "BOOK_LEMUR_NAME");
                LanguageMgr.SetString(guestInfo, "BOOK_LEMUR_INFO");
                break;
            case Guest.Rabbit:
                LanguageMgr.SetString(guestName, "BOOK_RABBIT_NAME");
                LanguageMgr.SetString(guestInfo, "BOOK_RABBIT_INFO");
                break;
            case Guest.Dove:
                LanguageMgr.SetString(guestName, "BOOK_DOVE_NAME");
                LanguageMgr.SetString(guestInfo, "BOOK_DOVE_INFO");
                break;
            case Guest.VirginiaOpossum:
                LanguageMgr.SetString(guestName, "BOOK_OPOSSUM_NAME");
                LanguageMgr.SetString(guestInfo, "BOOK_OPOSSUM_INFO");
                break;
            case Guest.Deer:
                LanguageMgr.SetString(guestName, "BOOK_DEER_NAME");
                LanguageMgr.SetString(guestInfo, "BOOK_DEER_INFO");
                break;
            case Guest.SeaOtter:
                LanguageMgr.SetString(guestName, "BOOK_SEAOTTER_NAME");
                LanguageMgr.SetString(guestInfo, "BOOK_SEAOTTER_INFO");
                break;
        }
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

        if (guestSprite.ContainsKey(pGuest) == false)
            return;
        Sprite sprite = guestSprite[pGuest];

        float tartGetRate = (float)sprite.texture.height / (float)sprite.texture.width;
        float newWidth = firstWidth;
        float newHeight = firstWidth * tartGetRate;

        imgRect.sizeDelta = new Vector2(newWidth, newHeight);
    }
}
