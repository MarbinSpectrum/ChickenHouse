using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DiaryUI_FilePlayerInfo : Mgr
{
    [SerializeField] private TextMeshProUGUI cookLv;
    [SerializeField] private TextMeshProUGUI cookExp;
    [SerializeField] private Image           cookExpBar;
    [SerializeField] private TextMeshProUGUI cookReward;

    [SerializeField] private TextMeshProUGUI chickenPrice;
    [SerializeField] private TextMeshProUGUI chickenResPrice;
    [SerializeField] private TextMeshProUGUI drinkResValue;
    [SerializeField] private TextMeshProUGUI sideMenuResValue;
    [SerializeField] private TextMeshProUGUI workerSpeed;
    [SerializeField] private TextMeshProUGUI incomeUp;
    [SerializeField] private TextMeshProUGUI saleValue;
    [SerializeField] private TextMeshProUGUI guestSpawnRate;
    [SerializeField] private TextMeshProUGUI geustWait;
    [SerializeField] private TextMeshProUGUI guestTip;
    [SerializeField] private TextMeshProUGUI rentValue;
    [SerializeField] private TextMeshProUGUI money;

    private const string PERCENT_FORMAT = "{0:#,###}<size=15>%</size>";
    private const string TIME_FORMAT = "{0:N2}<size=15>s</size>";
    private const string LV_FORMAT = "Lv {0}";
    private const string EXP_FORMAX = "{0}/{1}";

    public void SetUI(PlayData pPlayData)
    {
        if(pPlayData == null)
            return;

        //조리숙련도
        string cookLvStr = string.Format(LV_FORMAT, pPlayData.cookLv);
        LanguageMgr.SetText(cookLv, cookLvStr);

        //조리숙련도 경험치
        if(pPlayData.cookLv < cookLvMgr.MAX_LV)
        {
            string cookExpStr = string.Format(EXP_FORMAX, pPlayData.cookExp, cookLvMgr.RequireExp(pPlayData.cookLv + 1));
            LanguageMgr.SetText(cookExp, cookExpStr);
            cookExp.gameObject.SetActive(true);
            cookExpBar.fillAmount = pPlayData.cookExp / (float)cookLvMgr.RequireExp(pPlayData.cookLv + 1);
        }
        else
        {
            cookExp.gameObject.SetActive(false);
            cookExpBar.fillAmount = 1;
        }

        //조리숙련도 보상
        if (pPlayData.cookLv < cookLvMgr.MAX_LV)
        {
            LanguageMgr.SetText(cookReward, LvRewardText(pPlayData.cookLv+1));
            cookReward.gameObject.SetActive(true);
        }
        else
        {
            cookReward.gameObject.SetActive(false);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //기본 치킨 가격
        string chickenPriceStr = LanguageMgr.GetMoneyStr(chickenPrice.fontSize, pPlayData.ChickenPrice());
        LanguageMgr.SetText(chickenPrice, chickenPriceStr);

        //치킨 재료 값 감소 값
        float decreaseChickenValue = pPlayData.DecreaseChickenRate();
        if (decreaseChickenValue == 0)
            LanguageMgr.SetText(chickenResPrice, "0%");
        else
        {
            string decreaseChickenStr = string.Format(PERCENT_FORMAT, decreaseChickenValue);
            LanguageMgr.SetText(chickenResPrice, decreaseChickenStr);
        }

        //드링크 재료 값 감소 값
        float decreaseDrinkValue = pPlayData.DecreaseDrinkRate();
        if (decreaseDrinkValue == 0)
            LanguageMgr.SetText(drinkResValue, "0%");
        else
        {
            string decreaseDrinkStr = string.Format(PERCENT_FORMAT, decreaseDrinkValue);
            LanguageMgr.SetText(drinkResValue, decreaseDrinkStr);
        }

        //사이드메뉴 값 감소 률
        float decreaseSideMenuValue = pPlayData.DecreaseSideMenuRate();
        if (decreaseSideMenuValue == 0)
            LanguageMgr.SetText(sideMenuResValue, "0%");
        else
        {
            string decreaseSideMenuStr = string.Format(PERCENT_FORMAT, decreaseSideMenuValue);
            LanguageMgr.SetText(sideMenuResValue, decreaseSideMenuStr);
        }


        //직원 속도
        float workerSpeedValue = 100f + pPlayData.GetWorkerSpeedUpRate();
        if (workerSpeedValue == 0)
            LanguageMgr.SetText(workerSpeed, "0%");
        else
        {
            string workerSpeedStr = string.Format(PERCENT_FORMAT, workerSpeedValue);
            LanguageMgr.SetText(workerSpeed, workerSpeedStr);
        }

        //수익증가률
        float incomeUpValue = pPlayData.GetPriceUpRate()+100f;
        if (incomeUpValue == 0)
            LanguageMgr.SetText(incomeUp, "0%");
        else
        {
            string incomeUpStr = string.Format(PERCENT_FORMAT, incomeUpValue);
            LanguageMgr.SetText(incomeUp, incomeUpStr);
        }

        //상점할인률
        float shopSaleValue = pPlayData.ShopSaleValue();
        if (shopSaleValue == 0)
            LanguageMgr.SetText(saleValue, "0%");
        else
        {
            string shopSaleValueStr = string.Format(PERCENT_FORMAT, shopSaleValue);
            LanguageMgr.SetText(saleValue, shopSaleValueStr);
        }

        //손님 방문 속도
        float guestSpawnRateValue = gameMgr.playData.GuestSpawnSpeed();
        if (guestSpawnRateValue == 0)
            LanguageMgr.SetText(guestSpawnRate, "0%");
        else
        {
            string guestSpawnRateValueStr = string.Format(PERCENT_FORMAT, guestSpawnRateValue);
            LanguageMgr.SetText(guestSpawnRate, guestSpawnRateValueStr);
        }

        //손님 인내심 상한
        float maxPatienceValue = gameMgr.playData.GuestPatience();
        if (maxPatienceValue == 0)
            LanguageMgr.SetText(guestSpawnRate, "0%");
        else
        {
            string maxPatienceValueStr = string.Format(PERCENT_FORMAT, maxPatienceValue);
            LanguageMgr.SetText(geustWait, maxPatienceValueStr);
        }

        //손님 팁
        float tipRateValue = pPlayData.TipRate();
        if (tipRateValue == 0)
            LanguageMgr.SetText(guestTip, "0%");
        else
        {
            string tipValueStr = string.Format(PERCENT_FORMAT, pPlayData.TipRate());
            LanguageMgr.SetText(guestTip, tipValueStr);
        }

        //임대료
        string rentValueStr = LanguageMgr.GetMoneyStr(rentValue.fontSize, pPlayData.RentValue());
        LanguageMgr.SetText(rentValue, rentValueStr);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //보유 자본
        string moneyStr = LanguageMgr.GetMoneyStr(money.fontSize, pPlayData.money);
        LanguageMgr.SetText(money, moneyStr);
    }

    private string LvRewardText(int pLv)
    {
        CookLvMgr.LvData lvData = cookLvMgr.GetLvData(pLv);
        switch(lvData.cookLvStat)
        {
            case CookLvStat.IncreaseChickenPrice:
                {
                    string strFormat = LanguageMgr.GetText("COOK_LV_REWARD_1");
                    string str = string.Format(strFormat, pLv, lvData.value);
                    return str;
                }
            case CookLvStat.DecreaseChickenRes:
                {
                    string strFormat = LanguageMgr.GetText("COOK_LV_REWARD_2");
                    string str = string.Format(strFormat, pLv, lvData.value);
                    return str;
                }
            case CookLvStat.DecreaseDrinkRes:
                {
                    string strFormat = LanguageMgr.GetText("COOK_LV_REWARD_3");
                    string str = string.Format(strFormat, pLv, lvData.value);
                    return str;
                }
            case CookLvStat.DecreasePickleRes:
                {
                    string strFormat = LanguageMgr.GetText("COOK_LV_REWARD_4");
                    string str = string.Format(strFormat, pLv, lvData.value);
                    return str;
                }
            case CookLvStat.WorkerSpeedUp:
                {
                    string strFormat = LanguageMgr.GetText("COOK_LV_REWARD_5");
                    string str = string.Format(strFormat, pLv, lvData.value);
                    return str;
                }
            case CookLvStat.IncomeUp:
                {
                    string strFormat = LanguageMgr.GetText("COOK_LV_REWARD_6");
                    string str = string.Format(strFormat, pLv, lvData.value);
                    return str;
                }
            case CookLvStat.ShopSale:
                {
                    string strFormat = LanguageMgr.GetText("COOK_LV_REWARD_7");
                    string str = string.Format(strFormat, pLv, lvData.value);
                    return str;
                }
            case CookLvStat.GuestSpawnSpeed:
                {
                    string strFormat = LanguageMgr.GetText("COOK_LV_REWARD_8");
                    string str = string.Format(strFormat, pLv, lvData.value);
                    return str;
                }
            case CookLvStat.GuestPatience:
                {
                    string strFormat = LanguageMgr.GetText("COOK_LV_REWARD_9");
                    string str = string.Format(strFormat, pLv, lvData.value);
                    return str;
                }
            case CookLvStat.Tip:
                {
                    string strFormat = LanguageMgr.GetText("COOK_LV_REWARD_10");
                    string str = string.Format(strFormat, pLv, lvData.value);
                    return str;
                }
            case CookLvStat.Rent:
                {
                    string strFormat = LanguageMgr.GetText("COOK_LV_REWARD_11");
                    string str = string.Format(strFormat, pLv, lvData.value);
                    return str;
                }
        }

        return string.Empty;
    }
}
