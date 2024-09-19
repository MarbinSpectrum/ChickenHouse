using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DiaryUI_BookScrollObj : Mgr
{
    [SerializeField] private List<DiaryUI_BookGuestSlot>        guestSlot       = new List<DiaryUI_BookGuestSlot>();
    [SerializeField] private List<DiaryUI_BookSeasoningSlot>    seasoningSlot   = new List<DiaryUI_BookSeasoningSlot>();
    [SerializeField] private List<DiaryUI_BookSubMenuSlot>      subMenuSlot     = new List<DiaryUI_BookSubMenuSlot>();
    [SerializeField] private RectTransform                      thisRect;
    [SerializeField] private RectTransform                      headerRect;
    [SerializeField] private TextMeshProUGUI                    headerText;

    public void SetData(Guest pGuest0, NoParaDel fun0, Guest pGuest1, NoParaDel fun1, Guest pGuest2, NoParaDel fun2, Guest pGuest3, NoParaDel fun3)
    {
        //¼Õ´Ô°´Ã¼ ¼¼ÆÃ
        Init();

        if (pGuest0 != Guest.None)
        {
            guestSlot[0].SetData(pGuest0);
            guestSlot[0].gameObject.SetActive(true);
            guestSlot[0].SetClickEvent(fun0);
        }
        if (pGuest1 != Guest.None)
        {
            guestSlot[1].SetData(pGuest1);
            guestSlot[1].gameObject.SetActive(true);
            guestSlot[1].SetClickEvent(fun1);
        }
        if (pGuest2 != Guest.None)
        {
            guestSlot[2].SetData(pGuest2);
            guestSlot[2].gameObject.SetActive(true);
            guestSlot[2].SetClickEvent(fun2);
        }
        if (pGuest3 != Guest.None)
        {
            guestSlot[3].SetData(pGuest3);
            guestSlot[3].gameObject.SetActive(true);
            guestSlot[3].SetClickEvent(fun3);
        }

        thisRect.sizeDelta = new(thisRect.sizeDelta.x, 80);
    }

    public void SetData(ChickenSpicy pSpicy0, NoParaDel fun0, ChickenSpicy pSpicy1, NoParaDel fun1, ChickenSpicy pSpicy2, NoParaDel fun2, ChickenSpicy pSpicy3, NoParaDel fun3)
    {
        //¾ç³ä°´Ã¼ ¼¼ÆÃ
        Init();

        if (pSpicy0 != ChickenSpicy.Not)
        {
            seasoningSlot[0].SetData(pSpicy0);
            seasoningSlot[0].gameObject.SetActive(true);
            seasoningSlot[0].SetClickEvent(fun0);
        }
        if (pSpicy1 != ChickenSpicy.Not)
        {
            seasoningSlot[1].SetData(pSpicy1);
            seasoningSlot[1].gameObject.SetActive(true);
            seasoningSlot[1].SetClickEvent(fun1);
        }
        if (pSpicy2 != ChickenSpicy.Not)
        {
            seasoningSlot[2].SetData(pSpicy2);
            seasoningSlot[2].gameObject.SetActive(true);
            seasoningSlot[2].SetClickEvent(fun2);
        }
        if (pSpicy3 != ChickenSpicy.Not)
        {
            seasoningSlot[3].SetData(pSpicy3);
            seasoningSlot[3].gameObject.SetActive(true);
            seasoningSlot[3].SetClickEvent(fun3);
        }

        thisRect.sizeDelta = new(thisRect.sizeDelta.x, 80);
    }

    public void SetData(DiaryUI_Book.EtcList etcList)
    {
        //±âÅ¸ ¼¼ÆÃ
        Init();

        if(etcList.isDrinkHeader)
        {
            headerRect.gameObject.SetActive(true);
            LanguageMgr.SetString(headerText, "DRINK_HEADER");
            thisRect.sizeDelta = new(thisRect.sizeDelta.x, 45);
        }
        else if (etcList.isSideMenuHeader)
        {
            headerRect.gameObject.SetActive(true);
            LanguageMgr.SetString(headerText, "PICKLE_HEADER");
            thisRect.sizeDelta = new(thisRect.sizeDelta.x, 45);
        }
        else if(etcList.drinks.Count > 0)
        {
            for(int i = 0; i < 4; i++)
            {
                if (etcList.drinks[i] != Drink.None)
                {
                    subMenuSlot[i].SetData(etcList.drinks[i]);
                    subMenuSlot[i].gameObject.SetActive(true);
                    subMenuSlot[i].SetClickEvent(etcList.funs[i]);
                }
            }
            thisRect.sizeDelta = new(thisRect.sizeDelta.x, 80);
        }
        else if (etcList.sideMenus.Count > 0)
        {
            for (int i = 0; i < 4; i++)
            {
                if (etcList.sideMenus[i] != SideMenu.None)
                {
                    subMenuSlot[i].SetData(etcList.sideMenus[i]);
                    subMenuSlot[i].gameObject.SetActive(true);
                    subMenuSlot[i].SetClickEvent(etcList.funs[i]);
                }
            }
            thisRect.sizeDelta = new(thisRect.sizeDelta.x, 80);
        }
    }

    private void Init()
    {
        foreach (DiaryUI_BookGuestSlot slot in guestSlot)
            slot.gameObject.SetActive(false);
        foreach (DiaryUI_BookSeasoningSlot slot in seasoningSlot)
            slot.gameObject.SetActive(false);
        foreach (DiaryUI_BookSubMenuSlot slot in subMenuSlot)
            slot.gameObject.SetActive(false);
        headerRect.gameObject.SetActive(false);
    }
}
