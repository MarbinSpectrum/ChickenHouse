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

        //���� ���� ǥ��
        int total = 0;
        LanguageMgr.SetText(title, string.Format(LanguageMgr.GetText("DAY_END"), gameMgr.playData.day));

        int storeRevenue = gameMgr.dayMoney;
        LanguageMgr.SetString(nameList[DayEndList.Store_Revenue], "STORE_REVENUE");
        LanguageMgr.SetText(infoList[DayEndList.Store_Revenue], string.Format("{0:N0} $", storeRevenue));
        total += storeRevenue;

        //�Ӵ�� ǥ��
        int rentValue = gameMgr.playData.RentValue();
        LanguageMgr.SetString(nameList[DayEndList.Rent], "RENT");
        LanguageMgr.SetText(infoList[DayEndList.Rent], string.Format("-{0:N0} $", rentValue));
        total -= rentValue;

        //��ᰪ
        int suppliesUsed = 0;
        foreach(GuestMenu guestMenu in gameMgr.sellMenu)
        {
            if (guestMenu.sideMenu != SideMenu.None)
                suppliesUsed += PlayData.SIDE_MENU_RES_VAIUE;
            if (guestMenu.drink != Drink.None)
                suppliesUsed += PlayData.DRINK_RES_VAIUE;
            suppliesUsed += PlayData.CHICKEN_RES_VAIUE;
        }

        LanguageMgr.SetString(nameList[DayEndList.Supplies_Uesd], "SUPPLIES_UESD");
        LanguageMgr.SetText(infoList[DayEndList.Supplies_Uesd], string.Format("-{0:N0} $", suppliesUsed));        
        total -= suppliesUsed;

        //�Ƹ�����Ʈ �����
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
            LanguageMgr.SetText(infoList[DayEndList.Salary], string.Format("-{0:N0} $", workerSalary));
            total -= workerSalary;
        }
        else
        {
            nameList[DayEndList.Salary].gameObject.SetActive(false);
            infoList[DayEndList.Salary].gameObject.SetActive(false);
        }

        total = Mathf.Max(0, total);

        //�� ����
        LanguageMgr.SetString(nameList[DayEndList.Total_Profit], "TOTAL_PROFIT");
        LanguageMgr.SetText(infoList[DayEndList.Total_Profit], string.Format("{0:N0} $", total));

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

        gameMgr.playData.day++;
        gameMgr.DayEndEvent();

#if DEMO
        if (gameMgr.playData.day == 7)
        {
            gameMgr.playData.money += addValue;
            sceneMgr.SceneLoad(Scene.DEMO, false, false, SceneChangeAni.CIRCLE);
            return;
        }   
#endif

        gameMgr.playData.money += addValue;
        sceneMgr.SceneLoad(Scene.TOWN,true, true, SceneChangeAni.CIRCLE);
    }
}
