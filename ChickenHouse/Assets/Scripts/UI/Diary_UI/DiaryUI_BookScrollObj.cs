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

    private List<Guest>             guestList   = new List<Guest>();
    private List<ChickenSpicy>      spicyList   = new List<ChickenSpicy>();
    private List<NoParaDel>         funList     = new List<NoParaDel>();
    private DiaryUI_Book.EtcList    ectList     = null;

    public void SetData(Guest pGuest0, NoParaDel fun0, Guest pGuest1, NoParaDel fun1, Guest pGuest2, NoParaDel fun2, Guest pGuest3, NoParaDel fun3)
    {
        //손님객체 세팅
        Init();

        guestList.Add(pGuest0);
        guestList.Add(pGuest1);
        guestList.Add(pGuest2);
        guestList.Add(pGuest3);
        funList.Add(fun0);
        funList.Add(fun1);
        funList.Add(fun2);
        funList.Add(fun3);

        UpdateGuestSlot();
    }

    public void UpdateGuestSlot()
    {
        //오브젝트를 손님데이터에 맞게 갱신
        for(int i = 0; i < guestList.Count; i++)
        {
            if (guestList[i] == Guest.None)
                continue;

            guestSlot[i].SetData(guestList[i]);
            guestSlot[i].gameObject.SetActive(true);
            guestSlot[i].SetClickEvent(funList[i]);
        }

        thisRect.sizeDelta = new(thisRect.sizeDelta.x, 80);
    }

    public void SetData(ChickenSpicy pSpicy0, NoParaDel fun0, ChickenSpicy pSpicy1, NoParaDel fun1, ChickenSpicy pSpicy2, NoParaDel fun2, ChickenSpicy pSpicy3, NoParaDel fun3)
    {
        //양념객체 세팅
        Init();

        spicyList.Add(pSpicy0);
        spicyList.Add(pSpicy1);
        spicyList.Add(pSpicy2);
        spicyList.Add(pSpicy3);
        funList.Add(fun0);
        funList.Add(fun1);
        funList.Add(fun2);
        funList.Add(fun3);

        UpdateSpicySlot();
    }

    public void UpdateSpicySlot()
    {
        //오브젝트를 양념데이터에 맞게 갱신
        for (int i = 0; i < spicyList.Count; i++)
        {
            if (spicyList[i] == ChickenSpicy.Not)
                continue;

            seasoningSlot[i].SetData(spicyList[i]);
            seasoningSlot[i].gameObject.SetActive(true);
            seasoningSlot[i].SetClickEvent(funList[i]);
        }
        thisRect.sizeDelta = new(thisRect.sizeDelta.x, 80);
    }

    public void SetData(DiaryUI_Book.EtcList pEtcList)
    {
        //기타 세팅
        Init();
        ectList = pEtcList;

        UpdateEtcSlot();
    }

    public void UpdateEtcSlot()
    {
        //오브젝트를 기타데이터에 맞게 갱신
        if (ectList == null)
            return;

        if (ectList.isDrinkHeader)
        {
            headerRect.gameObject.SetActive(true);
            LanguageMgr.SetString(headerText, "DRINK_HEADER");
            thisRect.sizeDelta = new(thisRect.sizeDelta.x, 45);
        }
        else if (ectList.isSideMenuHeader)
        {
            headerRect.gameObject.SetActive(true);
            LanguageMgr.SetString(headerText, "PICKLE_HEADER");
            thisRect.sizeDelta = new(thisRect.sizeDelta.x, 45);
        }
        else if (ectList.drinks.Count > 0)
        {
            for (int i = 0; i < 4; i++)
            {
                if (ectList.drinks[i] != Drink.None)
                {
                    subMenuSlot[i].SetData(ectList.drinks[i]);
                    subMenuSlot[i].gameObject.SetActive(true);
                    subMenuSlot[i].SetClickEvent(ectList.funs[i]);
                }
            }
            thisRect.sizeDelta = new(thisRect.sizeDelta.x, 80);
        }
        else if (ectList.sideMenus.Count > 0)
        {
            for (int i = 0; i < 4; i++)
            {
                if (ectList.sideMenus[i] != SideMenu.None)
                {
                    subMenuSlot[i].SetData(ectList.sideMenus[i]);
                    subMenuSlot[i].gameObject.SetActive(true);
                    subMenuSlot[i].SetClickEvent(ectList.funs[i]);
                }
            }
            thisRect.sizeDelta = new(thisRect.sizeDelta.x, 80);
        }
    }

    private void Init()
    {
        guestList.Clear();
        spicyList.Clear();
        ectList = null;
        funList.Clear();

        foreach (DiaryUI_BookGuestSlot slot in guestSlot)
            slot.gameObject.SetActive(false);
        foreach (DiaryUI_BookSeasoningSlot slot in seasoningSlot)
            slot.gameObject.SetActive(false);
        foreach (DiaryUI_BookSubMenuSlot slot in subMenuSlot)
            slot.gameObject.SetActive(false);
        headerRect.gameObject.SetActive(false);
    }
}
