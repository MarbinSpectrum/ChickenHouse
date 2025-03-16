using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BeaveriorSlot : Mgr
{
    private InteriorItem interiorItem;

    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemCost;
    [SerializeField] private RectTransform itemSelect;
    [SerializeField] private RectTransform checkMark;
    [SerializeField] private RectTransform purchaseBtn;

    //아이템 구입 확인
    private OneParaDel purchaseCheckFun;
    //아이템 변경
    private OneParaDel buyBtnFun;

    public void SetData(InteriorItem pInteriorItem, OneParaDel pPurchaseCheckFun, OneParaDel pBuyBtnFun)
    {
        interiorItem = pInteriorItem;
        purchaseCheckFun = pPurchaseCheckFun;
        buyBtnFun = pBuyBtnFun;

        InteriorData interiorData = interiorMgr.GetInteriorData(pInteriorItem);
        if (interiorData == null)
            return;
        PlayData playData = gameMgr.playData;
        if (playData == null)
            return;

        LanguageMgr.SetString(itemName, interiorData.nameKey);
        itemIcon.sprite = interiorData.img;

        int newMoney = (int)(interiorData.price * (100f - gameMgr.playData.ShopSaleValue()) / 100f);
        string moneyStr = string.Format(LanguageMgr.COMMA_FORMAT, newMoney);
        LanguageMgr.SetText(itemCost, moneyStr);

        if (playData.hasInterior[(int)pInteriorItem])
        {
            itemSelect.gameObject.SetActive(true);
            itemCost.gameObject.SetActive(false);
        }
        else
        {
            itemSelect.gameObject.SetActive(false);
            itemCost.gameObject.SetActive(true);
        }

        if (playData.IsUseInterior(pInteriorItem))
        {
            checkMark.gameObject.SetActive(true);
            purchaseBtn.gameObject.SetActive(false);
        }
        else
        {
            checkMark.gameObject.SetActive(false);
            purchaseBtn.gameObject.SetActive(true);
        }
    }

    public void PurchaseCheck()
    {
        purchaseCheckFun?.Invoke(interiorItem);
    }

    public void BuyBtn()
    {
        buyBtnFun?.Invoke(interiorItem);
    }
}
