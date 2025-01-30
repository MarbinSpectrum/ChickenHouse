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
    [SerializeField] private DiaryUI_BookSubMenuInfo    subMenuInfo;

    public List<Guest>          guestList { private set; get; } = new List<Guest>();
    public List<ChickenSpicy>   spicyList { private set; get; } = new List<ChickenSpicy>();
    public List<EtcList>        etcList   { private set; get; } = new List<EtcList>();

    public class EtcList
    {
        public List<Drink>      drinks             { private set; get; } = new List<Drink>();
        public List<SideMenu>   sideMenus          { private set; get; } = new List<SideMenu>();
        public List<NoParaDel>  funs    { private set; get; } = new List<NoParaDel>(); 
        public bool isDrinkHeader       { private set; get; } = false;
        public bool isSideMenuHeader    { private set; get; } = false;
        public EtcList(Drink pDrink0, NoParaDel fun0, Drink pDrink1, NoParaDel fun1, Drink pDrink2, NoParaDel fun2, Drink pDrink3, NoParaDel fun3)
        {
            drinks.Add(pDrink0);
            drinks.Add(pDrink1);
            drinks.Add(pDrink2);
            drinks.Add(pDrink3);
            funs.Add(fun0);
            funs.Add(fun1);
            funs.Add(fun2);
            funs.Add(fun3);
        }

        public EtcList(SideMenu pSideMenu0, NoParaDel fun0, SideMenu pSideMenu1, NoParaDel fun1, SideMenu pSideMenu2, NoParaDel fun2, SideMenu pSideMenu3, NoParaDel fun3)
        {
            sideMenus.Add(pSideMenu0);
            sideMenus.Add(pSideMenu1);
            sideMenus.Add(pSideMenu2);
            sideMenus.Add(pSideMenu3);
            funs.Add(fun0);
            funs.Add(fun1);
            funs.Add(fun2);
            funs.Add(fun3);
        }

        public EtcList(int headerType)
        {
            if (headerType == 1)
                isDrinkHeader = true;
            else if (headerType == 2)
                isSideMenuHeader = true;
        }
    }


    private BookMenu            nowMenu;
    public  Guest               selectGuestSlot { private set; get; }
    public  ChickenSpicy        selectSpicySlot { private set; get; }
    public  Drink               selectDrinkSlot { private set; get; }
    public  SideMenu            selectSideMenuSlot { private set; get; }



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

    public void SetSelectEtc(Drink pDrink)
    {
        selectDrinkSlot = pDrink;
        selectSideMenuSlot = SideMenu.None;
        subMenuInfo.SetUI(pDrink);
    }

    public void SetSelectEtc(SideMenu pSideMenu)
    {
        selectDrinkSlot = Drink.None;
        selectSideMenuSlot = pSideMenu;
        subMenuInfo.SetUI(pSideMenu);
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
                    SetSelectEtc(SideMenu.None);

                    //손님 목록 갱신
                    guestList.Clear();
                    for (Guest guest = Guest.Fox; guest < Guest.MAX; guest++)
                        guestList.Add(guest);
                    bookScrollInit.SetMode(BookMenu.Guest);
                }
                break;
            case BookMenu.Seasoning:
                {
                    SetSelectSeasoning(ChickenSpicy.Hot);
                    SetSelectGuest(Guest.None);
                    SetSelectEtc(SideMenu.None);

                    //양념 목록 갱신
                    spicyList.Clear();
                    for (ChickenSpicy spicy = ChickenSpicy.Hot; spicy < ChickenSpicy.MAX; spicy++)
                        spicyList.Add(spicy);
                    bookScrollInit.SetMode(BookMenu.Seasoning);
                }
                break;
            case BookMenu.Etc:
                {
                    SetSelectSeasoning(ChickenSpicy.None);
                    SetSelectGuest(Guest.None);
                    SetSelectEtc(Drink.Cola);

                    //기타 갱신
                    etcList.Clear();
                    etcList.Add(new EtcList(1));
                    {
                        int cnt = Mathf.CeilToInt(((int)Drink.MAX - 1) / (float)4);
                        for (int i = 0; i < cnt; i++)
                        {
                            Drink[] drinks = new Drink[4];
                            for(int j = 0; j < 4; j++)
                            {
                                drinks[j] = Drink.Cola + i * 4 + j;
                                drinks[j] = drinks[j] >= Drink.MAX ? Drink.None : drinks[j];
                            }

                            EtcList etcObj = new EtcList(
                                drinks[0], () =>
                                {
                                    SetSelectEtc(drinks[0]);
                                    bookScrollInit.UpdateCells(BookMenu.Etc);
                                },
                                drinks[1], () =>
                                {
                                    SetSelectEtc(drinks[1]);
                                    bookScrollInit.UpdateCells(BookMenu.Etc);
                                },
                                drinks[2], () =>
                                {
                                    SetSelectEtc(drinks[2]);
                                    bookScrollInit.UpdateCells(BookMenu.Etc);
                                },
                                drinks[3], () =>
                                {
                                    SetSelectEtc(drinks[3]);
                                    bookScrollInit.UpdateCells(BookMenu.Etc);
                                });
                            etcList.Add(etcObj);
                        }
                    }

                    etcList.Add(new EtcList(2));
                    {
                        int cnt = Mathf.CeilToInt(((int)SideMenu.MAX - 1) / (float)4);
                        for (int i = 0; i < cnt; i++)
                        {
                            SideMenu[] sideMenus = new SideMenu[4];
                            for (int j = 0; j < 4; j++)
                            {
                                sideMenus[j] = SideMenu.Pickle + i * 4 + j;
                                sideMenus[j] = sideMenus[j] >= SideMenu.MAX ? SideMenu.None : sideMenus[j];
                            }

                            EtcList etcObj = new EtcList(
                                sideMenus[0], () =>
                                {
                                    SetSelectEtc(sideMenus[0]);
                                    bookScrollInit.UpdateCells(BookMenu.Etc);
                                },
                                sideMenus[1], () =>
                                {
                                    SetSelectEtc(sideMenus[1]);
                                    bookScrollInit.UpdateCells(BookMenu.Etc);
                                },
                                sideMenus[2], () =>
                                {
                                    SetSelectEtc(sideMenus[2]);
                                    bookScrollInit.UpdateCells(BookMenu.Etc);
                                },
                                sideMenus[3], () =>
                                {
                                    SetSelectEtc(sideMenus[3]);
                                    bookScrollInit.UpdateCells(BookMenu.Etc);
                                });
                            etcList.Add(etcObj);
                        }
                    }

                    bookScrollInit.SetMode(BookMenu.Etc);
                }
                break;
        }
    }


}
