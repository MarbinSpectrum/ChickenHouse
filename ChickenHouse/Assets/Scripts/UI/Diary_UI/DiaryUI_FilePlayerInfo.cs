using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DiaryUI_FilePlayerInfo : Mgr
{
    [SerializeField] private TextMeshProUGUI chickenPrice;
    [SerializeField] private TextMeshProUGUI chickenResPrice;
    [SerializeField] private TextMeshProUGUI drinkResValue;
    [SerializeField] private TextMeshProUGUI sideMenuResValue;
    [SerializeField] private TextMeshProUGUI cookSpeed;
    [SerializeField] private TextMeshProUGUI incomeUp;
    [SerializeField] private TextMeshProUGUI saleValue;
    [SerializeField] private TextMeshProUGUI guestRate;
    [SerializeField] private TextMeshProUGUI geustWait;
    [SerializeField] private TextMeshProUGUI guestTip;
    [SerializeField] private TextMeshProUGUI rentValue;
    [SerializeField] private TextMeshProUGUI money;

    private const string MONEY_FORMAT = "{0:N0}<size=15>$</size>";
    private const string PERCENT_FORMAT = "{0:#,###}<size=15>%</size>";
    private const string TIME_FORMAT = "{0:N2}<size=15>s</size>";

    public void SetUI(PlayData pPlayData)
    {

        //기본 치킨 가격
        string chickenPriceStr = string.Format(MONEY_FORMAT, PlayData.DEFAULT_CHICKEN_PRICE);
        LanguageMgr.SetText(chickenPrice, chickenPriceStr);

        //치킨 재료 값
        string chickenResStr = string.Format(MONEY_FORMAT, PlayData.CHICKEN_RES_VAIUE);
        LanguageMgr.SetText(chickenResPrice, chickenResStr);

        //드링크 재료 값
        string drinkResStr = string.Format(MONEY_FORMAT, PlayData.DRINK_RES_VAIUE);
        LanguageMgr.SetText(drinkResValue, drinkResStr);

        //사이드 메뉴 재료 값
        string sideMenuResStr = string.Format(MONEY_FORMAT, PlayData.SIDE_MENU_RES_VAIUE);
        LanguageMgr.SetText(sideMenuResValue, sideMenuResStr);

        //조리 속도
        float cookSpeedValue = pPlayData.GetOilZoneSpeedRate() * 100f;
        string cookSpeedStr = string.Format(PERCENT_FORMAT, cookSpeedValue);
        LanguageMgr.SetText(cookSpeed, cookSpeedStr);

        //수익증가률
        float incomeUpValue = pPlayData.GetPriceUpRate() * 100f;
        if (incomeUpValue == 0)
            LanguageMgr.SetText(incomeUp, "0%");
        else
        {
            string incomeUpStr = string.Format(PERCENT_FORMAT, incomeUpValue);
            LanguageMgr.SetText(incomeUp, incomeUpStr);
        }

        //상점할인률
        float shopSaleValue = pPlayData.ShopSaleValue() * 100f;
        if (shopSaleValue == 0)
            LanguageMgr.SetText(saleValue, "0%");
        else
        {
            string shopSaleValueStr = string.Format(PERCENT_FORMAT, shopSaleValue);
            LanguageMgr.SetText(saleValue, shopSaleValueStr);
        }

        //손님 방문 주기
        float guestDelayValue = GuestSystem.GUEST_DELAY_TIME * gameMgr.playData.GuestDelayRate();
        string guestDelayValueStr = string.Format(TIME_FORMAT, guestDelayValue);
        LanguageMgr.SetText(guestRate, guestDelayValueStr);

        //손님 인내심 상한
        float maxPatienceValue = Memo_UI.MAX_TIME * gameMgr.playData.GuestDelayRate();
        string maxPatienceValueStr = string.Format(TIME_FORMAT, maxPatienceValue);
        LanguageMgr.SetText(geustWait, maxPatienceValueStr);

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
        string rentValueStr = string.Format(MONEY_FORMAT, pPlayData.RentValue());
        LanguageMgr.SetText(rentValue, rentValueStr);


        string moneyStr = string.Format(MONEY_FORMAT, pPlayData.money);
        LanguageMgr.SetText(money, moneyStr);
    }
}
