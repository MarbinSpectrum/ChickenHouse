using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LongNoseContractAD_UI : Mgr
{
    [SerializeField] private LongNoseContractSlot   shopMenuSlot;
    [SerializeField] private RectTransform          slotContents;
    [SerializeField] private LongNoseContractCheck  contractCheck;
    [SerializeField] private Money_UI               playerMoney;

    [SerializeField] LongNose longNose;
    private List<LongNoseContractSlot> contractMenu = new List<LongNoseContractSlot>();

    public void SetUI()
    {
        SetMenu();
    }

    private void SetMenu()
    {

        PlayData playData = gameMgr.playData;
        if (playData == null)
            return;
        longNose.UpdateList();

        playerMoney.SetMoney(playData.money);

        slotContents.anchoredPosition = Vector2.zero;

        contractMenu.ForEach((x) => x.gameObject.SetActive(false));
        for (int i = 0; i < longNose.itemList.Count; i++)
        {
            if (i >= contractMenu.Count)
            {
                LongNoseContractSlot slotMenu = Instantiate(shopMenuSlot, slotContents);
                contractMenu.Add(slotMenu);
            }

            contractMenu[i].SetData(longNose.itemList[i], (item) => ItemBuyCheckUI((ADItem)item));
            contractMenu[i].gameObject.SetActive(true);
        }
    }

    private void ItemBuyCheckUI(ADItem pADItem)
    {
        contractCheck.SetUI(() =>
        {
            soundMgr.PlaySE(Sound.GetMoney_SE);

            gameMgr.playData.AddADItem(pADItem);

            ADData adData = adItemMgr.GetADData(pADItem);
            int newMoney = (int)(adData.price * (100f - gameMgr.playData.ShopSaleValue()) / 100f);
            gameMgr.playData.money -= newMoney;

            SetMenu();
        });
    }
}
