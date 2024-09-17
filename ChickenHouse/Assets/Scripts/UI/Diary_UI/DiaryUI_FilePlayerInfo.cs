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

    private const string MONEY_FORMAT = "{0:N0}<size=15>$</size>";
    private const string PERCENT_FORMAT = "{0:#,###}<size=15>%</size>";
    private const string TIME_FORMAT = "{0:N2}<size=15>s</size>";
    private const string LV_FORMAT = "Lv {0}";
    private const string EXP_FORMAX = "{0}/{1}";

    public void SetUI(PlayData pPlayData)
    {
        if(pPlayData == null)
            return;

        //�������õ�
        string cookLvStr = string.Format(LV_FORMAT, pPlayData.cookLv);
        LanguageMgr.SetText(cookLv, cookLvStr);

        //�������õ� ����ġ
        if(pPlayData.cookLv < cookLvMgr.MAX_LV)
        {
            string cookExpStr = string.Format(EXP_FORMAX, pPlayData.cookExp, cookLvMgr.RequireExp(pPlayData.cookLv + 1));
            LanguageMgr.SetText(cookExp, cookExpStr);
            cookExp.gameObject.SetActive(true);
            cookExpBar.fillAmount = pPlayData.cookExp / cookLvMgr.RequireExp(pPlayData.cookLv + 1);
        }
        else
        {
            cookExp.gameObject.SetActive(false);
            cookExpBar.fillAmount = 1;
        }

        //�������õ� ����
        if (pPlayData.cookLv < cookLvMgr.MAX_LV)
        {
            LanguageMgr.SetText(cookLv, LvRewardText(pPlayData.cookLv+1));
            cookLv.gameObject.SetActive(true);
        }
        else
        {
            cookLv.gameObject.SetActive(false);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //�⺻ ġŲ ����
        string chickenPriceStr = string.Format(MONEY_FORMAT, pPlayData.ChickenPrice());
        LanguageMgr.SetText(chickenPrice, chickenPriceStr);

        //ġŲ ��� �� ���� ��
        string chickenResStr = string.Format(MONEY_FORMAT, pPlayData.DecreaseChickenRes());
        LanguageMgr.SetText(chickenResPrice, chickenResStr);

        //�帵ũ ��� �� ���� ��
        string drinkResStr = string.Format(MONEY_FORMAT, pPlayData.DecreaseDrinkRes());
        LanguageMgr.SetText(drinkResValue, drinkResStr);

        //��Ŭ ��� �� ���� ��
        string sideMenuResStr = string.Format(MONEY_FORMAT, pPlayData.DecreasePickleRes());
        LanguageMgr.SetText(sideMenuResValue, sideMenuResStr);

        //���� �ӵ�
        float workerSpeedValue = 100f + pPlayData.GetWorkerSpeedUpRate();
        if (workerSpeedValue == 0)
            LanguageMgr.SetText(workerSpeed, "0%");
        else
        {
            string workerSpeedStr = string.Format(PERCENT_FORMAT, workerSpeedValue);
            LanguageMgr.SetText(workerSpeed, workerSpeedStr);
        }

        //����������
        float incomeUpValue = pPlayData.GetPriceUpRate();
        if (incomeUpValue == 0)
            LanguageMgr.SetText(incomeUp, "0%");
        else
        {
            string incomeUpStr = string.Format(PERCENT_FORMAT, incomeUpValue);
            LanguageMgr.SetText(incomeUp, incomeUpStr);
        }

        //�������η�
        float shopSaleValue = pPlayData.ShopSaleValue();
        if (shopSaleValue == 0)
            LanguageMgr.SetText(saleValue, "0%");
        else
        {
            string shopSaleValueStr = string.Format(PERCENT_FORMAT, shopSaleValue);
            LanguageMgr.SetText(saleValue, shopSaleValueStr);
        }

        //�մ� �湮 �ӵ�
        float guestSpawnRateValue = gameMgr.playData.GuestDelayRate();
        if (guestSpawnRateValue == 0)
            LanguageMgr.SetText(guestSpawnRate, "0%");
        else
        {
            string guestSpawnRateValueStr = string.Format(PERCENT_FORMAT, guestSpawnRateValue);
            LanguageMgr.SetText(guestSpawnRate, guestSpawnRateValueStr);
        }

        //�մ� �γ��� ����
        float maxPatienceValue = gameMgr.playData.GuestPatience();
        if (maxPatienceValue == 0)
            LanguageMgr.SetText(guestSpawnRate, "0%");
        else
        {
            string maxPatienceValueStr = string.Format(PERCENT_FORMAT, maxPatienceValue);
            LanguageMgr.SetText(geustWait, maxPatienceValueStr);
        }

        //�մ� ��
        float tipRateValue = pPlayData.TipRate();
        if (tipRateValue == 0)
            LanguageMgr.SetText(guestTip, "0%");
        else
        {
            string tipValueStr = string.Format(PERCENT_FORMAT, pPlayData.TipRate());
            LanguageMgr.SetText(guestTip, tipValueStr);
        }

        //�Ӵ��
        string rentValueStr = string.Format(MONEY_FORMAT, pPlayData.RentValue());
        LanguageMgr.SetText(rentValue, rentValueStr);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //���� �ں�
        string moneyStr = string.Format(MONEY_FORMAT, pPlayData.money);
        LanguageMgr.SetText(money, moneyStr);
    }

    private string LvRewardText(int pLv)
    {
        string result = string.Empty;

        return result;
    }
}
