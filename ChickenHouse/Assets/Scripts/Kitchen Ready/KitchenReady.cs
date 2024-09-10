using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenReady : Mgr
{
    [SerializeField] private RectTransform dontClick;
    [SerializeField] private RectTransform menuRect;
    [SerializeField] private RectTransform[] menuPos;

    [SerializeField] private StartGame startGame;

    [SerializeField] private KitchenSet_UI      kitchenSetUI;
    [SerializeField] private KitchenSetCheck    kitchenSetCheck;

    [SerializeField] private MenuSet_UI     menuSetUI;
    [SerializeField] private MenuSetCheck   menuSetCheck;

    public enum EUIPos
    {
        KitchenSet = 0,
        MenuSet = 1,
    }

    private void Start()
    {
        gameMgr.InitData();

#if UNITY_EDITOR
        gameMgr.LoadData();
#endif

        kitchenSetUI.Init();
        menuSetUI.Init();

        //직원을 보유하지 않으면 직원화면이 안나옴
        bool hasWorker = false;
        for (EWorker worker = EWorker.Worker_1; worker < EWorker.MAX; worker++)
        {
            if (gameMgr.playData.hasWorker[(int)worker])
            {
                hasWorker = true;
                break;
            }
        }

        //양념이 두개 이상 보유 중일때부터 메뉴화면이 나옴
        int hasSpicyCnt = 0;
        for (ChickenSpicy spicy = ChickenSpicy.Hot; spicy < ChickenSpicy.MAX; spicy++)
        {
            if (gameMgr.playData.hasItem[(int)spicy])
            {
                hasSpicyCnt++;
                break;
            }
        }

        if (hasWorker)
            menuRect.transform.position = menuPos[(int)EUIPos.KitchenSet].transform.position;
        else if (hasSpicyCnt >= 2)
            menuRect.transform.position = menuPos[(int)EUIPos.MenuSet].transform.position;
        else
        {
            gameObject.SetActive(false);
            startGame.Run();
        }
    }

    public void MoveToKitchenSetPos()
    {
        MoveDic(EUIPos.KitchenSet);
    }

    public void MovetoMenuSetPos()
    {
        int useWorkerCnt = kitchenSetUI.GetUseWorkerCnt();
        if (useWorkerCnt == 0)
        {
            //직원을 모두 배치 못한듯?
            kitchenSetCheck.SetUI(() => MoveDic(EUIPos.MenuSet));
        }
        else
        {
            //양념이 두개 이상 보유 중일때부터 메뉴화면이 나옴
            int hasSpicyCnt = 0;
            for (ChickenSpicy spicy = ChickenSpicy.Hot; spicy < ChickenSpicy.MAX; spicy++)
            {
                if (gameMgr.playData.hasItem[(int)spicy])
                {
                    hasSpicyCnt++;
                    break;
                }
            }

            if (hasSpicyCnt >= 2)
                MoveDic(EUIPos.MenuSet);
            else
                StartGame();
        }
    }

    private void MoveDic(EUIPos eUIPos)
    {
        int idx = (int)eUIPos;

        IEnumerator Run()
        {
            dontClick.gameObject.SetActive(true);
            float distance = 0;
            do
            {
                menuRect.transform.position = Vector3.Lerp(menuRect.transform.position, menuPos[idx].transform.position, 0.1f);
                distance = Vector3.Distance(menuRect.transform.position, menuPos[idx].transform.position);
                yield return null;
            }
            while (distance > 0.1f);
            menuRect.transform.position = menuPos[idx].transform.position;
            dontClick.gameObject.SetActive(false);
        }
        StartCoroutine(Run());
    }
    public void StartGame()
    {
        if(menuSetUI.GetUseSpicyCnt() <= 0)
        {
            //양념을 하나 이상 배치해야지 게임을 시작할 수 있다.
            menuSetCheck.SetUI("MENU_SET_CHECK_MSG_0");
            return;
        }

        if (menuSetUI.GetUseDrinkCnt() <= 0)
        {
            //음료를 하나 이상 배치해야지 게임을 시작할 수 있다.
            menuSetCheck.SetUI("MENU_SET_CHECK_MSG_1");
            return;
        }

        if (menuSetUI.GetUseSideMenuCnt() <= 0)
        {
            //사이드메뉴를 하나 이상 배치해야지 게임을 시작할 수 있다.
            menuSetCheck.SetUI("MENU_SET_CHECK_MSG_2");
            return;
        }

        kitchenSetUI.ApplyKitchenSet();
        menuSetUI.ApplySetMenu();

        IEnumerator Run()
        {
            dontClick.gameObject.SetActive(true);
            sceneMgr.ShowAnimation(SceneChangeAni.FADE, true);

            yield return new WaitForSeconds(1.5f);

            gameObject.SetActive(false);

            sceneMgr.ShowAnimation(SceneChangeAni.FADE, false);

            startGame.Run();
        }
        StartCoroutine(Run());
    }
}
