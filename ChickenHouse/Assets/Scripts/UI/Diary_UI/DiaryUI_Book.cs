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
        //�ν����ͷ� ����� ���� �Լ���
        SetUI((BookMenu)menuIdx);
    }

    public void SetUI(BookMenu pMenu)
    {
        bookTag.OnTag(pMenu);

        switch (pMenu)
        {
            case BookMenu.Guest:
                {
                    //�մ� ��� ����
                    guestList.Clear();
                    for (Guest guest = Guest.Fox; guest < Guest.MAX; guest++)
                        guestList.Add(guest);
                    bookScrollInit.SetGuestList(guestList);
                }
                break;
            case BookMenu.Seasoning:
                {
                    //�մ� ��� ����
                    spicyList.Clear();
                    for (ChickenSpicy spicy = ChickenSpicy.Hot; spicy < ChickenSpicy.MAX; spicy++)
                        spicyList.Add(spicy);
                    bookScrollInit.SetSpicyList(spicyList);
                }
                break;
        }
    }

}
