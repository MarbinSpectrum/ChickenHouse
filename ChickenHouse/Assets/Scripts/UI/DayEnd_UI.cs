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

    private const int SUPPLIES_VAIUE = 10;
    private bool goNext = false;

    public void ShowResult()
    {
        soundMgr.StopBGM();

        baseRect.gameObject.SetActive(true);

        //매장 수익 표시
        int total = 0;
        LanguageMgr.SetText(title, string.Format(LanguageMgr.GetText("DAY_END"), gameMgr.playData.day));

        int storeRevenue = gameMgr.dayMoney;
        LanguageMgr.SetString(nameList[DayEndList.Store_Revenue], "STORE_REVENUE");
        LanguageMgr.SetText(infoList[DayEndList.Store_Revenue], string.Format("{0:N0} $", storeRevenue));
        total += storeRevenue;

        //임대료 표시
        int rentValue = 100;
        LanguageMgr.SetString(nameList[DayEndList.Rent], "RENT");
        LanguageMgr.SetText(infoList[DayEndList.Rent], string.Format("-{0:N0} $", rentValue));
        total -= rentValue;

        //재료값
        int suppliesUsed = gameMgr.sellChickenCnt * SUPPLIES_VAIUE;
        LanguageMgr.SetString(nameList[DayEndList.Supplies_Uesd], "SUPPLIES_UESD");
        LanguageMgr.SetText(infoList[DayEndList.Supplies_Uesd], string.Format("-{0:N0} $", suppliesUsed));
        total -= suppliesUsed;


        //순 이익
        LanguageMgr.SetString(nameList[DayEndList.Total_Profit], "TOTAL_PROFIT");
        LanguageMgr.SetText(infoList[DayEndList.Total_Profit], string.Format("{0:N0} $", total));

        nextBtn.onClick.RemoveAllListeners();
        nextBtn.onClick.AddListener(() => GoNext());
    }

    private void GoNext()
    {
        if (goNext)
            return;
        goNext = true;

        gameMgr.SaveData();
        sceneMgr.SceneLoad(Scene.UPGRADE, SceneChangeAni.CIRCLE);
    }
}
