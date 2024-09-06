using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using Sirenix.OdinInspector;

public class MenuSet_UI : Mgr
{
    [SerializeField] private RectTransform          spicyTokenArea;
    private List<MenuSetSpicyToken>                 spicyTokens = new List<MenuSetSpicyToken>();
    [SerializeField] private MenuSetSpicyToken      spicyToken;
    [SerializeField] private MenuSetSpicyToken[]    spicyTokenPoint;
    [SerializeField] private MenuSetSpicyToken      dragSpicyToken;
    [SerializeField] private List<RectTransform>    dropSpicyList;

    [SerializeField] private RectTransform          drinkTokenArea;
    private List<MenuSetDrinkToken>                 drinkTokens = new List<MenuSetDrinkToken>();
    [SerializeField] private MenuSetDrinkToken      drinkToken;
    [SerializeField] private MenuSetDrinkToken[]    drinkTokenPoint;
    [SerializeField] private MenuSetDrinkToken      dragDrinkToken;
    [SerializeField] private List<RectTransform>    dropDrinkList;

    [SerializeField] private RectTransform          sideMenuTokenArea;
    private List<MenuSetSideMenuToken>              sideMenuTokens = new List<MenuSetSideMenuToken>();
    [SerializeField] private MenuSetSideMenuToken   sideMenuToken;
    [SerializeField] private MenuSetSideMenuToken[] sideMenuTokenPoint;
    [SerializeField] private MenuSetSideMenuToken   dragSideMenuToken;
    [SerializeField] private List<RectTransform>    dropSideMenuList;

    [SerializeField] private RectTransform          infoRect;
    [SerializeField] private TextMeshProUGUI        nameText;
    [SerializeField] private TextMeshProUGUI        infoText;
    [SerializeField] private TextMeshProUGUI        priceText;


    private ChickenSpicy[]  setSpicyState       = new ChickenSpicy[(int)MenuSetPos.SpicyMAX];
    private Drink[]         setDrinkState       = new Drink[(int)MenuSetPos.DrinkMAX];
    private SideMenu[]      setSideMenuState    = new SideMenu[(int)MenuSetPos.SideMenuMAX];

    public enum MenuSetDragType
    {
        None,
        Spicy,
        SideMenu,
        Drink
    }
    private int dragObj = 0;
    private MenuSetDragType nowDragType;

    public enum MenuSetPos
    {
        Spicy0      = 0,
        Spicy1      = 1,
        Spicy2      = 2,
        Spicy3      = 3,
        Spicy4      = 4,
        SpicyMAX,

        Drink0      = 0,
        Drink1      = 1,
        DrinkMAX,

        SideMenu0 = 0,
        SideMenuMAX,

        None        = -1,
    }
    private MenuSetPos dropPos = 0;

    private int maxSpicy = 5;
    private int maxDrink = 2;
    private int maxSideMenu = 1;

    public void Init()
    {
        int drinkCnt = 0;
        for (Drink drink = Drink.Cola; drink < Drink.MAX; drink++)
            if (gameMgr.playData.hasItem[(int)drink])
                drinkCnt++;
        if (drinkCnt == 1)
            maxDrink = 1;
        else
            maxDrink = 2;

        int spicyCnt = 0;
        for (ChickenSpicy spicy = ChickenSpicy.Hot; spicy < ChickenSpicy.MAX; spicy++)
            if (gameMgr.playData.HasRecipe(spicy))
                spicyCnt++;
        if (spicyCnt >= 5)
            maxSpicy = 5;
        else
            maxSpicy = spicyCnt;

        setSpicyState[(int)MenuSetPos.Spicy0] = (ChickenSpicy)gameMgr.playData.spicyState[(int)MenuSetPos.Spicy0];
        setSpicyState[(int)MenuSetPos.Spicy1] = (ChickenSpicy)gameMgr.playData.spicyState[(int)MenuSetPos.Spicy1];
        setSpicyState[(int)MenuSetPos.Spicy2] = (ChickenSpicy)gameMgr.playData.spicyState[(int)MenuSetPos.Spicy2];
        setSpicyState[(int)MenuSetPos.Spicy3] = (ChickenSpicy)gameMgr.playData.spicyState[(int)MenuSetPos.Spicy3];
        setSpicyState[(int)MenuSetPos.Spicy4] = (ChickenSpicy)gameMgr.playData.spicyState[(int)MenuSetPos.Spicy4];
        dropSpicyList.ForEach((x) => x.gameObject.SetActive(false));
        for (int i = 0; i < maxSpicy; i++)
            dropSpicyList[i].gameObject.SetActive(true);

        setDrinkState[(int)MenuSetPos.Drink0] = (Drink)gameMgr.playData.drinkState[(int)MenuSetPos.Drink0];
        setDrinkState[(int)MenuSetPos.Drink1] = (Drink)gameMgr.playData.drinkState[(int)MenuSetPos.Drink1];
        dropDrinkList.ForEach((x) => x.gameObject.SetActive(false));
        for (int i = 0; i < maxDrink; i++)
            dropDrinkList[i].gameObject.SetActive(true);

        setSideMenuState[(int)MenuSetPos.SideMenu0] = (SideMenu)gameMgr.playData.sideMenuState[(int)MenuSetPos.SideMenu0];
        dropSideMenuList.ForEach((x) => x.gameObject.SetActive(false));
        for (int i = 0; i < maxSideMenu; i++)
            dropSideMenuList[i].gameObject.SetActive(true);

        RefreshToken();
    }

    private void Update()
    {
        if (nowDragType == MenuSetDragType.None)
        {
            dragSpicyToken.gameObject.SetActive(false);
            dragDrinkToken.gameObject.SetActive(false);
            dragSideMenuToken.gameObject.SetActive(false);
            dropPos = MenuSetPos.None;
        }
        else if (nowDragType == MenuSetDragType.Spicy)
        {
            dragSpicyToken.SetUI((ChickenSpicy)dragObj);
            dragSpicyToken.gameObject.SetActive(true);

            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dragSpicyToken.transform.position = new Vector3(pos.x, pos.y, 0);
        }
        else if (nowDragType == MenuSetDragType.Drink)
        {
            dragDrinkToken.SetUI((Drink)dragObj);
            dragDrinkToken.gameObject.SetActive(true);

            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dragDrinkToken.transform.position = new Vector3(pos.x, pos.y, 0);
        }
        else if (nowDragType == MenuSetDragType.SideMenu)
        {
            dragSideMenuToken.SetUI((SideMenu)dragObj);
            dragSideMenuToken.gameObject.SetActive(true);

            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dragSideMenuToken.transform.position = new Vector3(pos.x, pos.y, 0);
        }
    }

    private void RefreshToken()
    {
        RefreshSpicy();
        RefreshDrink();
        RefreshSideMenu();

        if (nowDragType == MenuSetDragType.None)
        {
            infoRect.gameObject.SetActive(false);
        }
        else if (nowDragType == MenuSetDragType.Spicy)
        {
            infoRect.gameObject.SetActive(true);
            SpicyData spicyData = spicyMgr.GetSpicyData((ChickenSpicy)dragObj);
            LanguageMgr.SetString(nameText, spicyData.nameKey);
            LanguageMgr.SetString(infoText, spicyData.infoKey);
            string priceStr = string.Format("{0:N0}$", spicyData.price);
            LanguageMgr.SetText(priceText, priceStr);
        }
        else if (nowDragType == MenuSetDragType.Drink)
        {
            infoRect.gameObject.SetActive(true);
            DrinkData drinkData = subMenuMgr.GetDrinkData((Drink)dragObj);
            LanguageMgr.SetString(nameText, drinkData.nameKey);
            LanguageMgr.SetString(infoText, drinkData.infoKey);
            string priceStr = string.Format("{0:N0}$", drinkData.price);
            LanguageMgr.SetText(priceText, priceStr);
        }
        else if (nowDragType == MenuSetDragType.SideMenu)
        {
            infoRect.gameObject.SetActive(true);
            SideMenuData sideMenuData = subMenuMgr.GetSideMenuData((SideMenu)dragObj);
            LanguageMgr.SetString(nameText, sideMenuData.nameKey);
            LanguageMgr.SetString(infoText, sideMenuData.infoKey);
            string priceStr = string.Format("{0:N0}$", sideMenuData.price);
            LanguageMgr.SetText(priceText, priceStr);
        }
    }

    private void RefreshSpicy()
    {
        PlayData playData = gameMgr.playData;
        List<ChickenSpicy> spicyList = new List<ChickenSpicy>();
        for (ChickenSpicy spicy = ChickenSpicy.Hot; spicy < ChickenSpicy.MAX; spicy++)
        {
            bool hasItem = playData.HasRecipe(spicy);
            if (hasItem == false)
            {
                //보유하지 않은 양념은 표시되지 않음
                continue;
            }

            bool setSpicy = false;
            foreach (ChickenSpicy checkSpicy in setSpicyState)
            {
                if (checkSpicy == spicy)
                {
                    setSpicy = true;
                    break;
                }
            }
            if (setSpicy)
            {
                //해당 양념은 이미 배치되어있음
                continue;
            }

            spicyList.Add(spicy);
        }

        spicyTokens.ForEach((x) => x.gameObject.SetActive(false));
        for (int i = 0; i < spicyList.Count; i++)
        {
            if (spicyTokens.Count <= i)
            {
                MenuSetSpicyToken newToken = Instantiate(spicyToken, spicyTokenArea);
                spicyTokens.Add(newToken);
            }
            float alphaValue = 1f;
            if (dragObj == (int)spicyList[i] && (nowDragType == MenuSetDragType.Spicy || nowDragType == MenuSetDragType.None))
            {
                //해당 직원은 드래그 상태
                alphaValue = 0f;
            }

            EventTrigger eventTrigger = spicyTokens[i].GetComponent<EventTrigger>();
            eventTrigger.triggers.Clear();

            EventTrigger.Entry endDrag = new EventTrigger.Entry();
            endDrag.eventID = EventTriggerType.EndDrag;
            endDrag.callback.AddListener((eventData) => DropToken(MenuSetDragType.Spicy));
            eventTrigger.triggers.Add(endDrag);

            EventTrigger.Entry beginDrag = new EventTrigger.Entry();
            beginDrag.eventID = EventTriggerType.BeginDrag;
            ChickenSpicy tempSpicy = spicyList[i];
            beginDrag.callback.AddListener((eventData) => DragToken((int)tempSpicy, MenuSetDragType.Spicy));
            eventTrigger.triggers.Add(beginDrag);

            spicyTokens[i].SetUI(spicyList[i], alphaValue);
            spicyTokens[i].gameObject.SetActive(true);
        }

        {
            //배치한 토큰 표시
            for (MenuSetPos sPos = MenuSetPos.Spicy0; sPos < MenuSetPos.SpicyMAX; sPos++)
            {
                int tempPos = (int)sPos;
                float alphaValue = ((dragObj == (int)setSpicyState[(int)sPos]) && nowDragType == MenuSetDragType.Spicy) ? 0 : 1;
                spicyTokenPoint[(int)sPos].SetUI(setSpicyState[(int)sPos], alphaValue);


                EventTrigger eventTrigger = spicyTokenPoint[(int)sPos].GetComponent<EventTrigger>();
                eventTrigger.triggers.Clear();

                EventTrigger.Entry pointerEnter = new EventTrigger.Entry();
                pointerEnter.eventID = EventTriggerType.PointerEnter;
                pointerEnter.callback.AddListener((eventData) => EnterTokenPoint(MenuSetDragType.Spicy, tempPos));
                eventTrigger.triggers.Add(pointerEnter);

                EventTrigger.Entry pointerExit = new EventTrigger.Entry();
                pointerExit.eventID = EventTriggerType.PointerExit;
                pointerExit.callback.AddListener((eventData) => ExitTokenPoint(MenuSetDragType.Spicy, tempPos));
                eventTrigger.triggers.Add(pointerExit);

                EventTrigger.Entry endDrag = new EventTrigger.Entry();
                endDrag.eventID = EventTriggerType.EndDrag;
                endDrag.callback.AddListener((eventData) => DropToken(MenuSetDragType.Spicy));
                eventTrigger.triggers.Add(endDrag);

                EventTrigger.Entry beginDrag = new EventTrigger.Entry();
                beginDrag.eventID = EventTriggerType.BeginDrag;
                beginDrag.callback.AddListener((eventData) => DragToken((int)setSpicyState[tempPos], MenuSetDragType.Spicy));
                eventTrigger.triggers.Add(beginDrag);
            }
        }
    }

    private void RefreshDrink()
    {
        PlayData playData = gameMgr.playData;
        List<Drink> drinkList = new List<Drink>();
        for (Drink drink = Drink.Cola; drink < Drink.MAX; drink++)
        {
            bool hasItem = playData.HasDrink(drink);
            if (hasItem == false)
            {
                //보유하지 않은 음료는 표시되지 않음
                continue;
            }

            bool setDrink = false;
            foreach (Drink checkDrink in setDrinkState)
            {
                if (checkDrink == drink)
                {
                    setDrink = true;
                    break;
                }
            }
            if (setDrink)
            {
                //해당 음료는 이미 배치되어있음
                continue;
            }

            drinkList.Add(drink);
        }

        drinkTokens.ForEach((x) => x.gameObject.SetActive(false));
        for (int i = 0; i < drinkList.Count; i++)
        {
            if (drinkTokens.Count <= i)
            {
                MenuSetDrinkToken newToken = Instantiate(drinkToken, drinkTokenArea);
                drinkTokens.Add(newToken);
            }
            float alphaValue = 1f;
            if (dragObj == (int)drinkList[i] && (nowDragType == MenuSetDragType.Drink || nowDragType == MenuSetDragType.None))
            {
                //해당 토큰은 드래그 상태
                alphaValue = 0f;
            }

            EventTrigger eventTrigger = drinkTokens[i].GetComponent<EventTrigger>();
            eventTrigger.triggers.Clear();

            EventTrigger.Entry endDrag = new EventTrigger.Entry();
            endDrag.eventID = EventTriggerType.EndDrag;
            endDrag.callback.AddListener((eventData) => DropToken(MenuSetDragType.Drink));
            eventTrigger.triggers.Add(endDrag);

            EventTrigger.Entry beginDrag = new EventTrigger.Entry();
            beginDrag.eventID = EventTriggerType.BeginDrag;
            Drink tempDrink = drinkList[i];
            beginDrag.callback.AddListener((eventData) => DragToken((int)tempDrink, MenuSetDragType.Drink));
            eventTrigger.triggers.Add(beginDrag);

            drinkTokens[i].SetUI(drinkList[i], alphaValue);
            drinkTokens[i].gameObject.SetActive(true);
        }

        {
            //배치한 토큰 표시
            for (MenuSetPos sPos = MenuSetPos.Drink0; sPos < MenuSetPos.DrinkMAX; sPos++)
            {
                int tempPos = (int)sPos;
                float alphaValue = ((dragObj == (int)setDrinkState[(int)sPos]) && nowDragType == MenuSetDragType.Drink)? 0 : 1;
                drinkTokenPoint[(int)sPos].SetUI(setDrinkState[(int)sPos], alphaValue);


                EventTrigger eventTrigger = drinkTokenPoint[(int)sPos].GetComponent<EventTrigger>();
                eventTrigger.triggers.Clear();

                EventTrigger.Entry pointerEnter = new EventTrigger.Entry();
                pointerEnter.eventID = EventTriggerType.PointerEnter;
                pointerEnter.callback.AddListener((eventData) => EnterTokenPoint(MenuSetDragType.Drink, tempPos));
                eventTrigger.triggers.Add(pointerEnter);

                EventTrigger.Entry pointerExit = new EventTrigger.Entry();
                pointerExit.eventID = EventTriggerType.PointerExit;
                pointerExit.callback.AddListener((eventData) => ExitTokenPoint(MenuSetDragType.Drink, tempPos));
                eventTrigger.triggers.Add(pointerExit);

                EventTrigger.Entry endDrag = new EventTrigger.Entry();
                endDrag.eventID = EventTriggerType.EndDrag;
                endDrag.callback.AddListener((eventData) => DropToken(MenuSetDragType.Drink));
                eventTrigger.triggers.Add(endDrag);

                EventTrigger.Entry beginDrag = new EventTrigger.Entry();
                beginDrag.eventID = EventTriggerType.BeginDrag;
                beginDrag.callback.AddListener((eventData) => DragToken((int)setDrinkState[tempPos], MenuSetDragType.Drink));
                eventTrigger.triggers.Add(beginDrag);
            }
        }
    }

    private void RefreshSideMenu()
    {
        PlayData playData = gameMgr.playData;
        List<SideMenu> sideMenuList = new List<SideMenu>();
        for (SideMenu sideMenu = SideMenu.Pickle; sideMenu < SideMenu.MAX; sideMenu++)
        {
            bool hasItem = playData.HasSideMenu(sideMenu);
            if (hasItem == false)
            {
                //보유하지 않은 양념은 표시되지 않음
                continue;
            }

            bool setSideMenu = false;
            foreach (SideMenu checkSideMenu in setSideMenuState)
            {
                if (checkSideMenu == sideMenu)
                {
                    setSideMenu = true;
                    break;
                }
            }
            if (setSideMenu)
            {
                //해당 사이드메뉴는 이미 배치되어있음
                continue;
            }

            sideMenuList.Add(sideMenu);
        }

        sideMenuTokens.ForEach((x) => x.gameObject.SetActive(false));
        for (int i = 0; i < sideMenuList.Count; i++)
        {
            if (sideMenuTokens.Count <= i)
            {
                MenuSetSideMenuToken newToken = Instantiate(sideMenuToken, sideMenuTokenArea);
                sideMenuTokens.Add(newToken);
            }
            float alphaValue = 1f;
            if (dragObj == (int)sideMenuList[i] && (nowDragType == MenuSetDragType.SideMenu || nowDragType == MenuSetDragType.None))
            {
                //해당 직원은 드래그 상태
                alphaValue = 0f;
            }

            EventTrigger eventTrigger = sideMenuTokens[i].GetComponent<EventTrigger>();
            eventTrigger.triggers.Clear();

            EventTrigger.Entry endDrag = new EventTrigger.Entry();
            endDrag.eventID = EventTriggerType.EndDrag;
            endDrag.callback.AddListener((eventData) => DropToken(MenuSetDragType.SideMenu));
            eventTrigger.triggers.Add(endDrag);

            EventTrigger.Entry beginDrag = new EventTrigger.Entry();
            beginDrag.eventID = EventTriggerType.BeginDrag;
            SideMenu tempSideMenu = sideMenuList[i];
            beginDrag.callback.AddListener((eventData) => DragToken((int)tempSideMenu, MenuSetDragType.SideMenu));
            eventTrigger.triggers.Add(beginDrag);

            sideMenuTokens[i].SetUI(sideMenuList[i], alphaValue);
            sideMenuTokens[i].gameObject.SetActive(true);
        }

        {
            //배치한 토큰 표시
            for (MenuSetPos sPos = MenuSetPos.SideMenu0; sPos < MenuSetPos.SideMenuMAX; sPos++)
            {
                int tempPos = (int)sPos;
                float alphaValue = ((dragObj == (int)setSideMenuState[(int)sPos]) && nowDragType == MenuSetDragType.SideMenu) ? 0 : 1;
                sideMenuTokenPoint[(int)sPos].SetUI(setSideMenuState[(int)sPos], alphaValue);


                EventTrigger eventTrigger = sideMenuTokenPoint[(int)sPos].GetComponent<EventTrigger>();
                eventTrigger.triggers.Clear();

                EventTrigger.Entry pointerEnter = new EventTrigger.Entry();
                pointerEnter.eventID = EventTriggerType.PointerEnter;
                pointerEnter.callback.AddListener((eventData) => EnterTokenPoint(MenuSetDragType.SideMenu, tempPos));
                eventTrigger.triggers.Add(pointerEnter);

                EventTrigger.Entry pointerExit = new EventTrigger.Entry();
                pointerExit.eventID = EventTriggerType.PointerExit;
                pointerExit.callback.AddListener((eventData) => ExitTokenPoint(MenuSetDragType.SideMenu, tempPos));
                eventTrigger.triggers.Add(pointerExit);

                EventTrigger.Entry endDrag = new EventTrigger.Entry();
                endDrag.eventID = EventTriggerType.EndDrag;
                endDrag.callback.AddListener((eventData) => DropToken(MenuSetDragType.SideMenu));
                eventTrigger.triggers.Add(endDrag);

                EventTrigger.Entry beginDrag = new EventTrigger.Entry();
                beginDrag.eventID = EventTriggerType.BeginDrag;
                beginDrag.callback.AddListener((eventData) => DragToken((int)setSideMenuState[tempPos], MenuSetDragType.SideMenu));
                eventTrigger.triggers.Add(beginDrag);
            }
        }
    }

    public void DragToken(int pDragObj, MenuSetDragType pDrayType)
    {
        if (nowDragType != MenuSetDragType.None)
            return;
        dragObj = pDragObj;
        nowDragType = pDrayType;
        RefreshToken();
    }

    public void DropToken(MenuSetDragType pDrayType)
    {
        nowDragType = MenuSetDragType.None;
        switch(pDrayType)
        {
            case MenuSetDragType.Spicy:
                {
                    MenuSetPos prevPos = MenuSetPos.None;
                    for (MenuSetPos sPos = MenuSetPos.Spicy0; sPos < MenuSetPos.SpicyMAX; sPos++)
                    {
                        if (setSpicyState[(int)sPos] == (ChickenSpicy)dragObj)
                        {
                            prevPos = sPos;
                            setSpicyState[(int)prevPos] = ChickenSpicy.None;
                        }
                    }

                    int useSpicyCnt = GetUseSpicyCnt();

                    if (dropPos != MenuSetPos.None)
                    {
                        if (useSpicyCnt >= maxSpicy && setSpicyState[(int)dropPos] != ChickenSpicy.None)
                        {
                            if (prevPos != MenuSetPos.None)
                            {
                                //스왑처리
                                setSpicyState[(int)prevPos] = setSpicyState[(int)dropPos];
                            }
                            setSpicyState[(int)dropPos] = (ChickenSpicy)dragObj;
                        }
                        else if (useSpicyCnt < maxSpicy)
                        {
                            if (prevPos != MenuSetPos.None)
                            {
                                //스왑처리
                                setSpicyState[(int)prevPos] = setSpicyState[(int)dropPos];
                            }
                            setSpicyState[(int)dropPos] = (ChickenSpicy)dragObj;
                        }
                    }
                }
                break;
            case MenuSetDragType.Drink:
                {
                    MenuSetPos prevPos = MenuSetPos.None;
                    for (MenuSetPos sPos = MenuSetPos.Drink0; sPos < MenuSetPos.DrinkMAX; sPos++)
                    {
                        if (setDrinkState[(int)sPos] == (Drink)dragObj)
                        {
                            prevPos = sPos;
                            setDrinkState[(int)prevPos] = Drink.None;
                        }
                    }

                    int useDrinkCnt = GetUseDrinkCnt();

                    if (dropPos != MenuSetPos.None)
                    {
                        if (useDrinkCnt >= maxDrink && setDrinkState[(int)dropPos] != Drink.None)
                        {
                            if (prevPos != MenuSetPos.None)
                            {
                                //스왑처리
                                setDrinkState[(int)prevPos] = setDrinkState[(int)dropPos];
                            }
                            setDrinkState[(int)dropPos] = (Drink)dragObj;
                        }
                        else if (useDrinkCnt < maxDrink)
                        {
                            if (prevPos != MenuSetPos.None)
                            {
                                //스왑처리
                                setDrinkState[(int)prevPos] = setDrinkState[(int)dropPos];
                            }
                            setDrinkState[(int)dropPos] = (Drink)dragObj;
                        }
                    }
                }
                break;
            case MenuSetDragType.SideMenu:
                {
                    MenuSetPos prevPos = MenuSetPos.None;
                    for (MenuSetPos sPos = MenuSetPos.SideMenu0; sPos < MenuSetPos.SideMenuMAX; sPos++)
                    {
                        if (setSideMenuState[(int)sPos] == (SideMenu)dragObj)
                        {
                            prevPos = sPos;
                            setSideMenuState[(int)prevPos] = SideMenu.None;
                        }
                    }

                    int useSideMenuCnt = GetUseSideMenuCnt();

                    if (dropPos != MenuSetPos.None)
                    {
                        if (useSideMenuCnt >= maxSideMenu && setSideMenuState[(int)dropPos] != SideMenu.None)
                        {
                            if (prevPos != MenuSetPos.None)
                            {
                                //스왑처리
                                setSideMenuState[(int)prevPos] = setSideMenuState[(int)dropPos];
                            }
                            setSideMenuState[(int)dropPos] = (SideMenu)dragObj;
                        }
                        else if (useSideMenuCnt < maxSideMenu)
                        {
                            if (prevPos != MenuSetPos.None)
                            {
                                //스왑처리
                                setSideMenuState[(int)prevPos] = setSideMenuState[(int)dropPos];
                            }
                            setSideMenuState[(int)dropPos] = (SideMenu)dragObj;
                        }
                    }
                }
                break;
        }

        dragObj = 0;
        RefreshToken();
    }

    public void EnterTokenPoint(MenuSetDragType pDrayType, int pArea)
    {
        //인스펙터에서 끌어서 사용하는 함수임
        if (dragObj == 0)
            return;

        if (nowDragType != pDrayType)
            return;

        switch(pDrayType)
        {
            case MenuSetDragType.Spicy:
                {
                    int useSpicyCnt = GetUseSpicyCnt();
                    bool isSetToken = false;
                    foreach (ChickenSpicy checkSpicy in setSpicyState)
                    {
                        if (checkSpicy == (ChickenSpicy)dragObj)
                            isSetToken = true;
                    }

                    if (useSpicyCnt >= maxSpicy && setSpicyState[pArea] == ChickenSpicy.None && isSetToken == false)
                    {
                        //더이상 못넣음
                        return;
                    }

                    spicyTokenPoint[pArea].SetUI((ChickenSpicy)dragObj, 0.5f);
                }
                break;
            case MenuSetDragType.Drink:
                {
                    int useDrinkCnt = GetUseDrinkCnt();
                    bool isSetToken = false;
                    foreach (Drink checkDrink in setDrinkState)
                    {
                        if (checkDrink == (Drink)dragObj)
                            isSetToken = true;
                    }

                    if (useDrinkCnt >= maxDrink && setDrinkState[pArea] == Drink.None && isSetToken == false)
                    {
                        //더이상 못넣음
                        return;
                    }

                    drinkTokenPoint[pArea].SetUI((Drink)dragObj, 0.5f);
                }
                break;
            case MenuSetDragType.SideMenu:
                {
                    int useSideMenuCnt = GetUseSideMenuCnt();
                    bool isSetToken = false;
                    foreach (SideMenu checkSideMenu in setSideMenuState)
                    {
                        if (checkSideMenu == (SideMenu)dragObj)
                            isSetToken = true;
                    }

                    if (useSideMenuCnt >= maxSideMenu && setSideMenuState[pArea] == SideMenu.None && isSetToken == false)
                    {
                        //더이상 못넣음
                        return;
                    }

                    sideMenuTokenPoint[pArea].SetUI((SideMenu)dragObj, 0.5f);
                }
                break;
        }

        dropPos = (MenuSetPos)pArea;
    }

    public void ExitTokenPoint(MenuSetDragType pDrayType, int pArea)
    {
        //인스펙터에서 끌어서 사용하는 함수임
        if ((ChickenSpicy)dragObj == ChickenSpicy.None)
            return;

        if (nowDragType != pDrayType)
            return;

        switch (pDrayType)
        {
            case MenuSetDragType.Spicy:
                {
                    spicyTokenPoint[pArea].SetUI(setSpicyState[pArea], 1f);
                }
                break;
            case MenuSetDragType.Drink:
                {
                    drinkTokenPoint[pArea].SetUI(setDrinkState[pArea], 1f);
                }
                break;
            case MenuSetDragType.SideMenu:
                {
                    sideMenuTokenPoint[pArea].SetUI(setSideMenuState[pArea], 1f);
                }
                break;
        }

        dropPos = MenuSetPos.None;
    }

    public int GetUseSpicyCnt()
    {
        //사용한 양념 갯수
        int useSpicyCnt = 0;
        foreach (ChickenSpicy checkSpicy in setSpicyState)
        {
            if (checkSpicy != ChickenSpicy.None)
                useSpicyCnt++;
        }
        return useSpicyCnt;
    }

    public int GetUseDrinkCnt()
    {
        //사용한 음료 갯수
        int useDrinkCnt = 0;
        foreach (Drink checkDrink in setDrinkState)
        {
            if (checkDrink != Drink.None)
                useDrinkCnt++;
        }
        return useDrinkCnt;
    }

    public int GetUseSideMenuCnt()
    {
        //사용한 사이드메뉴 갯수
        int useSideMenuCnt = 0;
        foreach (SideMenu checkSideMenu in setSideMenuState)
        {
            if (checkSideMenu != SideMenu.None)
                useSideMenuCnt++;
        }
        return useSideMenuCnt;
    }

    public void ApplySetMenu()
    {
        gameMgr.playData.spicyState[(int)MenuSetPos.Spicy0] = (int)setSpicyState[(int)MenuSetPos.Spicy0];
        gameMgr.playData.spicyState[(int)MenuSetPos.Spicy1] = (int)setSpicyState[(int)MenuSetPos.Spicy1];
        gameMgr.playData.spicyState[(int)MenuSetPos.Spicy2] = (int)setSpicyState[(int)MenuSetPos.Spicy2];
        gameMgr.playData.spicyState[(int)MenuSetPos.Spicy3] = (int)setSpicyState[(int)MenuSetPos.Spicy3];
        gameMgr.playData.spicyState[(int)MenuSetPos.Spicy4] = (int)setSpicyState[(int)MenuSetPos.Spicy4];

        gameMgr.playData.drinkState[(int)MenuSetPos.Drink0] = (int)setDrinkState[(int)MenuSetPos.Drink0];
        gameMgr.playData.drinkState[(int)MenuSetPos.Drink1] = (int)setDrinkState[(int)MenuSetPos.Drink1];

        gameMgr.playData.sideMenuState[(int)MenuSetPos.SideMenu0] = (int)setSideMenuState[(int)MenuSetPos.SideMenu0];
    }
}
