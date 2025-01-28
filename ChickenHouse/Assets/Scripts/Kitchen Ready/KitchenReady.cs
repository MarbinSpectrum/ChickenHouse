using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenReady : Mgr
{
    [SerializeField] private StaffReady staffReady;
    [SerializeField] private MenuReady  menuReady;
    [SerializeField] private StartGame  startGame;
    [SerializeField] private RectTransform dontClick;

    public struct MovePosList
    {
        public RectTransform leftPos;
        public RectTransform rightPos;
        public RectTransform centerPos;
    }
    [SerializeField] private MovePosList movePosList;

    public struct MoveUI
    {
        public RectTransform goMenuUIBtn;
        public RectTransform goStaffUIBtn;
        public RectTransform startGameBtn;
        public RectTransform uiRect;
    }
    [SerializeField] private MoveUI moveUI;

    private bool actStaffUI = false;
    private bool actMenuUI = false;

    private void Start()
    {
        gameMgr.InitData();
        if(gameMgr.playData == null)
        {
            gameObject.SetActive(false);
            startGame.Run();
            return;
        }

        staffReady.SetUI();
        menuReady.SetUI();

        bool hasWorker = false;
        for (EWorker eWorker = EWorker.Worker_1; eWorker < EWorker.MAX; eWorker++)
        {
            if (gameMgr.playData.hasWorker[(int)eWorker])
            {
                //직원보유중이면 리스트 생성
                hasWorker = true;
                break;
            }
        }
       

        int hasSpicyCnt = 0;
        for (ChickenSpicy eSpicy = ChickenSpicy.Hot; eSpicy < ChickenSpicy.MAX; eSpicy++)
        {
            if (gameMgr.playData.HasRecipe(eSpicy))
            {
                //양념 보유갯수 카운트
                hasSpicyCnt++;
            }
        }

        int hasDrinkCnt = 0;
        for (Drink eDrink = Drink.Cola; eDrink < Drink.MAX; eDrink++)
        {
            if (gameMgr.playData.HasDrink(eDrink))
            {
                //음료 보유갯수 카운트
                hasDrinkCnt++;
            }
        }

        int hasSideMenuCnt = 0;
        for (SideMenu eSideMenu = SideMenu.Pickle; eSideMenu < SideMenu.MAX; eSideMenu++)
        {
            if (gameMgr.playData.HasSideMenu(eSideMenu))
            {
                //사이드메뉴 보유갯수 카운트
                hasSideMenuCnt++;
            }
        }
        actStaffUI = hasWorker;
        actMenuUI = (hasDrinkCnt > 1 || hasSideMenuCnt > 1 || hasDrinkCnt > 1);

        if (actStaffUI)
        {
            MoveStaffUI(0);
            moveUI.uiRect.gameObject.SetActive(true);
            moveUI.goMenuUIBtn.gameObject.SetActive(actMenuUI);
            moveUI.goStaffUIBtn.gameObject.SetActive(false);
            moveUI.startGameBtn.gameObject.SetActive(actMenuUI == false);
            return;
        }

        if (actMenuUI)
        {
            MoveMenuUI(0);
            moveUI.uiRect.gameObject.SetActive(true);
            moveUI.goMenuUIBtn.gameObject.SetActive(false);
            moveUI.goStaffUIBtn.gameObject.SetActive(actStaffUI);
            moveUI.startGameBtn.gameObject.SetActive(true);
            return;
        }

        gameObject.SetActive(false);
        startGame.Run();
    }

    public void MoveStaffUI(float pDelay)
    {
        //직원 UI 이동
        if(pDelay == 0)
        {
            dontClick.gameObject.SetActive(false);
            staffReady.transform.position = movePosList.centerPos.position;
            staffReady.gameObject.SetActive(true);
            menuReady.gameObject.SetActive(false);

            moveUI.uiRect.gameObject.SetActive(true);
            moveUI.goMenuUIBtn.gameObject.SetActive(actMenuUI);
            moveUI.goStaffUIBtn.gameObject.SetActive(false);
            moveUI.startGameBtn.gameObject.SetActive(actMenuUI == false);

            return;
        }

        dontClick.gameObject.SetActive(true);
        moveUI.uiRect.gameObject.SetActive(false);
        staffReady.transform.position   = movePosList.leftPos.position;
        menuReady.transform.position    = movePosList.centerPos.position;
        staffReady.gameObject.SetActive(true);
        menuReady.gameObject.SetActive(true);

        IEnumerator Run()
        {
            float timeDelay = pDelay;
            float lerpValue = 0;
            while (timeDelay > 0)
            {
                lerpValue += Time.deltaTime;
                staffReady.transform.position = 
                    Vector3.Lerp(movePosList.leftPos.position, movePosList.centerPos.position,
                    lerpValue/pDelay);
                menuReady.transform.position =
                    Vector3.Lerp(movePosList.centerPos.position, movePosList.rightPos.position,
                    lerpValue / pDelay);

                timeDelay -= Time.deltaTime;
                yield return null;
            }

            staffReady.transform.position = movePosList.centerPos.position;
            menuReady.transform.position = movePosList.rightPos.position;
            staffReady.gameObject.SetActive(true);
            menuReady.gameObject.SetActive(false);
 
            yield return new WaitForSeconds(0.5f);

            dontClick.gameObject.SetActive(false);
            moveUI.uiRect.gameObject.SetActive(true);
            moveUI.goMenuUIBtn.gameObject.SetActive(actMenuUI);
            moveUI.goStaffUIBtn.gameObject.SetActive(false);
            moveUI.startGameBtn.gameObject.SetActive(actMenuUI == false);
        }
        StartCoroutine(Run());
    }

    public void MoveMenuUI(float pDelay)
    {
        //메뉴 UI 이동
        if (pDelay == 0)
        {
            dontClick.gameObject.SetActive(false);
            menuReady.transform.position = movePosList.centerPos.position;
            menuReady.gameObject.SetActive(true);
            staffReady.gameObject.SetActive(false);

            moveUI.uiRect.gameObject.SetActive(true);
            moveUI.goMenuUIBtn.gameObject.SetActive(false);
            moveUI.goStaffUIBtn.gameObject.SetActive(actStaffUI);
            moveUI.startGameBtn.gameObject.SetActive(true);

            return;
        }

        dontClick.gameObject.SetActive(true);
        moveUI.uiRect.gameObject.SetActive(false);
        staffReady.transform.position = movePosList.centerPos.position;
        menuReady.transform.position = movePosList.rightPos.position;
        staffReady.gameObject.SetActive(true);
        menuReady.gameObject.SetActive(true);

        IEnumerator Run()
        {

            float timeDelay = pDelay;
            float lerpValue = 0;
            while (timeDelay > 0)
            {
                lerpValue += Time.deltaTime;
                staffReady.transform.position =
                    Vector3.Lerp(movePosList.centerPos.position, movePosList.leftPos.position,
                    lerpValue / pDelay);
                menuReady.transform.position =
                    Vector3.Lerp(movePosList.rightPos.position, movePosList.centerPos.position,
                    lerpValue / pDelay);

                timeDelay -= Time.deltaTime;
                yield return null;
            }

            staffReady.transform.position = movePosList.leftPos.position;
            menuReady.transform.position = movePosList.centerPos.position;
            staffReady.gameObject.SetActive(false);
            menuReady.gameObject.SetActive(true);

            yield return new WaitForSeconds(0.5f);

            dontClick.gameObject.SetActive(false);
            moveUI.uiRect.gameObject.SetActive(true);
            moveUI.goMenuUIBtn.gameObject.SetActive(false);
            moveUI.goStaffUIBtn.gameObject.SetActive(actStaffUI);
            moveUI.startGameBtn.gameObject.SetActive(true);
        }
        StartCoroutine(Run());
    }

    public void StartGame()
    {
        gameObject.SetActive(false);
        startGame.Run();
    }
}
