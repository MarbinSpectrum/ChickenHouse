using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaryUI_Book : Mgr
{
    [SerializeField] private RectTransform[]            bookRect;
    [SerializeField] private DiaryUI_BookScroll_Init    bookScrollInit;
    [SerializeField] private DiaryUI_BookTag            bookTag;

    private List<Guest>         guestList = new List<Guest>();
    private List<ChickenSpicy>  spicyList = new List<ChickenSpicy>();

    private BookMenu nowMenu;

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

    public void SetUI(BookMenu pMenu)
    {
        bookTag.OnTag(pMenu);

        switch (pMenu)
        {
            case BookMenu.Guest:
                {
                    //손님 목록 갱신
                    guestList.Clear();
                    for (Guest guest = Guest.Fox; guest < Guest.MAX; guest++)
                        guestList.Add(guest);
                    bookScrollInit.SetGuestList(guestList);
                }
                break;
            case BookMenu.Seasoning:
                {
                    //손님 목록 갱신
                    spicyList.Clear();
                    for (ChickenSpicy spicy = ChickenSpicy.Hot; spicy < ChickenSpicy.MAX; spicy++)
                        spicyList.Add(spicy);
                    bookScrollInit.SetSpicyList(spicyList);
                }
                break;
        }
    }

}
