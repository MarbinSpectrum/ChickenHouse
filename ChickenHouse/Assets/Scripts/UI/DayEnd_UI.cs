using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DayEnd_UI : Mgr
{
    [SerializeField] private TextMeshProUGUI    title;
    [SerializeField] private RectTransform      baseRect;
    [SerializeField] private Button             nextBtn;

    [SerializeField] private Dictionary<DayEndList, TextMeshProUGUI> nameList = new Dictionary<DayEndList, TextMeshProUGUI>();

    [SerializeField] private Dictionary<DayEndList, TextMeshProUGUI> infoList = new Dictionary<DayEndList, TextMeshProUGUI>();

    private bool goNext = false;

    private int addValue = 0;

    public void ShowResult()
    {
        soundMgr.StopBGM();

        baseRect.gameObject.SetActive(true);

        //매장 수익 표시
        int total = 0;
        LanguageMgr.SetText(title, string.Format(LanguageMgr.GetText("DAY_END"), gameMgr.playData.day));

        int storeRevenue = gameMgr.dayMoney;
        LanguageMgr.SetString(nameList[DayEndList.Store_Revenue], "STORE_REVENUE");
        string storeRevenueStr = LanguageMgr.GetMoneyStr(infoList[DayEndList.Store_Revenue].fontSize, storeRevenue);
        LanguageMgr.SetText(infoList[DayEndList.Store_Revenue], storeRevenueStr);
        total += storeRevenue;

        //임대료 표시
        int rentValue = gameMgr.playData.RentValue();
        LanguageMgr.SetString(nameList[DayEndList.Rent], "RENT");
        string rentValueStr = LanguageMgr.GetMoneyStr(infoList[DayEndList.Rent].fontSize, rentValue);
        LanguageMgr.SetText(infoList[DayEndList.Rent], string.Format("-{0}", rentValueStr));
        total -= rentValue;

        //재료값
        int suppliesUsed = 0;
        foreach(GuestMenu guestMenu in gameMgr.sellMenu)
        {
            if (guestMenu.sideMenu != SideMenu.None)
            {
                SideMenuData sideMenuData = subMenuMgr.GetSideMenuData(guestMenu.sideMenu);
                suppliesUsed += Mathf.Max(0,sideMenuData.cost - (int)gameMgr.playData.DecreasePickleRes());
            }
            if (guestMenu.drink != Drink.None)
            {
                DrinkData drinkData = subMenuMgr.GetDrinkData(guestMenu.drink);
                suppliesUsed += Mathf.Max(0, drinkData.cost - (int)gameMgr.playData.DecreaseDrinkRes());
            }
            suppliesUsed += Mathf.Max(0, PlayData.CHICKEN_RES_VAIUE - (int)gameMgr.playData.DecreaseChickenRes());
        }

        LanguageMgr.SetString(nameList[DayEndList.Supplies_Uesd], "SUPPLIES_UESD");
        string suppliesUsedStr = LanguageMgr.GetMoneyStr(infoList[DayEndList.Supplies_Uesd].fontSize, suppliesUsed);
        LanguageMgr.SetText(infoList[DayEndList.Supplies_Uesd], string.Format("-{0}", suppliesUsedStr));        
        total -= suppliesUsed;

        //아르바이트 고용비용
        WorkerData resumeData0 = workerMgr.GetWorkerData((EWorker)gameMgr.playData.workerPos[(int)KitchenSet_UI.KitchenSetWorkerPos.KitchenWorker0]);
        WorkerData resumeData1 = workerMgr.GetWorkerData((EWorker)gameMgr.playData.workerPos[(int)KitchenSet_UI.KitchenSetWorkerPos.KitchenWorker1]);
        WorkerData resumeData2 = workerMgr.GetWorkerData((EWorker)gameMgr.playData.workerPos[(int)KitchenSet_UI.KitchenSetWorkerPos.CounterWorker]);
        int salary = 0;
        if(resumeData0 != null)
            salary += resumeData0.salary;
        if (resumeData1 != null)
            salary += resumeData1.salary;
        if (resumeData2 != null)
            salary += resumeData2.salary;
        if (salary > 0)
        {
            int workerSalary = (int)(gameMgr.dayMoney * salary / 100f);
            nameList[DayEndList.Salary].gameObject.SetActive(true);
            infoList[DayEndList.Salary].gameObject.SetActive(true);
            LanguageMgr.SetString(nameList[DayEndList.Salary], "WORKER_SALARY");
            string workerSalaryStr = LanguageMgr.GetMoneyStr(infoList[DayEndList.Salary].fontSize, workerSalary);
            LanguageMgr.SetText(infoList[DayEndList.Salary], string.Format("-{0}", workerSalaryStr));
            total -= workerSalary;
        }
        else
        {
            nameList[DayEndList.Salary].gameObject.SetActive(false);
            infoList[DayEndList.Salary].gameObject.SetActive(false);
        }

        total = Mathf.Max(0, total);

        //순 이익
        LanguageMgr.SetString(nameList[DayEndList.Total_Profit], "TOTAL_PROFIT");
        string totalStr = LanguageMgr.GetMoneyStr(infoList[DayEndList.Total_Profit].fontSize, total);
        LanguageMgr.SetText(infoList[DayEndList.Total_Profit], totalStr);

        nextBtn.onClick.RemoveAllListeners();
        nextBtn.onClick.AddListener(() => GoNext());

        addValue = total;

        GuestSystem guestMgr = GuestSystem.Instance;
        guestMgr.ui.nowMoney.SetMoney(gameMgr.playData.money + addValue);
    }

    private void GoNext()
    {
        if (goNext)
            return;
        goNext = true;
        gameMgr.playData.money += addValue;
        gameMgr.playData.day++;
        gameMgr.DayEndEvent();

        if ((QuestState)gameMgr.playData.quest[(int)Quest.Event_0_Quest] == QuestState.Not && 
            gameMgr.playData.day == QuestMgr.EVENT0_DAY)
        {
            sceneMgr.SceneLoad(Scene.EVENT_0, true, false, SceneChangeAni.CIRCLE);
            return;
        }
        else if ((QuestState)gameMgr.playData.quest[(int)Quest.Event_0_Quest] == QuestState.Run)
        {
            sceneMgr.SceneLoad(Scene.EVENT_0, false, false, SceneChangeAni.CIRCLE);
            return;
        }
        else if ((QuestState)gameMgr.playData.quest[(int)Quest.MainQuest_1] == QuestState.Run &&
            gameMgr.playData.day == QuestMgr.MAIN_QUEST_1_LIMIT_DAY + 1)
        {
            if (QuestMgr.ClearCheck(Quest.MainQuest_1))
            {
#if DEMO
                sceneMgr.SceneLoad(Scene.DEMO, false, false, SceneChangeAni.CIRCLE);
#endif
            }
            else
            {
                sceneMgr.SceneLoad(Scene.BAD_END, true, false, SceneChangeAni.CIRCLE);
            }
            return;
        }

        sceneMgr.SceneLoad(Scene.TOWN, true, true, SceneChangeAni.CIRCLE);
    }
}
