using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaryUI_Book : Mgr
{
    [SerializeField] private RectTransform[]            bookRect;
    [SerializeField] private DiaryUI_BookScroll_Init    bookScrollInit;
    [SerializeField] private DiaryUI_BookTag            bookTag;
    [SerializeField] private DiaryUI_BookSeasoningInfo  seasoningInfo;
    [SerializeField] private DiaryUI_BookGuestInfo      guestInfo;

    private List<Guest>         guestList = new List<Guest>();
    private List<ChickenSpicy>  spicyList = new List<ChickenSpicy>();

    private BookMenu            nowMenu;
    public  Guest               selectGuestSlot { private set; get; }
    public  ChickenSpicy        selectSpicySlot { private set; get; }

    public void SetUI()
    {
        nowMenu = BookMenu.Guest;
        SetUI(nowMenu);
    }

    public void SetState(bool state)
    {
        for (int i = 0; i < bookRect.Length; i++)
            bookRect[i].gameObject.SetActive(state);
    }

    public void SetUI(int menuIdx)
    {
        //인스펙터로 끌어다 쓰는 함수임
        SetUI((BookMenu)menuIdx);
    }

    public void SetSelectGuest(Guest pGuest)
    {
        selectGuestSlot = pGuest;
        guestInfo.SetUI(pGuest);
    }
    public void SetSelectSeasoning(ChickenSpicy pSpicy)
    {
        selectSpicySlot = pSpicy;
        seasoningInfo.SetUI(selectSpicySlot);
    }

    public void SetUI(BookMenu pMenu)
    {
        bookTag.OnTag(pMenu);

        switch (pMenu)
        {
            case BookMenu.Guest:
                {
                    SetSelectSeasoning(ChickenSpicy.None);
                    SetSelectGuest(Guest.Fox);

                    //손님 목록 갱신
                    guestList.Clear();
                    for (Guest guest = Guest.Fox; guest < Guest.MAX; guest++)
                        guestList.Add(guest);
                    bookScrollInit.SetGuestList(guestList);
                }
                break;
            case BookMenu.Seasoning:
                {
                    SetSelectSeasoning(ChickenSpicy.Hot);
                    SetSelectGuest(Guest.None);

                    //양념 목록 갱신
                    spicyList.Clear();
                    for (ChickenSpicy spicy = ChickenSpicy.Hot; spicy < ChickenSpicy.MAX; spicy++)
                        spicyList.Add(spicy);
                    bookScrollInit.SetSpicyList(spicyList);
                }
                break;
        }
    }

}
