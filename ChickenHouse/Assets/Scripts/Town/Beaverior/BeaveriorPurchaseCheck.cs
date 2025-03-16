using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BeaveriorPurchaseCheck : Mgr
{
    [SerializeField] private CounterBG_UI       counterBG;
    [SerializeField] private TextMeshProUGUI    itemName;
    [SerializeField] private TextMeshProUGUI    priceText;
    [SerializeField] private TextMeshProUGUI    selectText;
    [SerializeField] private RectTransform      btnObj;
    private NoParaDel fun;


    public void SetUI(InteriorItem pInteriorItem,bool pIsUse, NoParaDel pFun)
    {
        InteriorData interiorData = interiorMgr.GetInteriorData(pInteriorItem);
        LanguageMgr.SetString(itemName, interiorData.nameKey);

        if (gameMgr.playData.hasInterior[(int)pInteriorItem])
        {
            priceText.gameObject.SetActive(false);
            selectText.gameObject.SetActive(true);
        }
        else
        {
            priceText.gameObject.SetActive(true);
            selectText.gameObject.SetActive(false);
            string moneyStr = string.Format(LanguageMgr.COMMA_FORMAT, interiorData.price);
            LanguageMgr.SetText(priceText, moneyStr);
        }
        btnObj.gameObject.SetActive(pIsUse == false);
        counterBG.SetInteriorBeaveriorUI(pInteriorItem, CounterBG.CounterTime.Lunch);

        fun = pFun;
        gameObject.SetActive(true);
    }

    public void OpenYes()
    {
        //인스펙터로 끌어서 사용하는 함수
        soundMgr.PlaySE(Sound.Btn_SE);
        gameObject.SetActive(false);
        fun?.Invoke();
    }

    public void OpenNo()
    {
        //인스펙터로 끌어서 사용하는 함수
        soundMgr.PlaySE(Sound.Btn_SE);
        gameObject.SetActive(false);

    }
}
