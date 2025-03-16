using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Beaverior_UI : Mgr
{
    [SerializeField] private BeaveriorSlot          shopMenuSlot;
    [SerializeField] private RectTransform          slotContents;
    [SerializeField] private BeaveriorPurchaseCheck purchaseCheck;
    [SerializeField] private Tab                    tabInfo;
    [SerializeField] private Money_UI               playerMoney;
    [SerializeField] Beaverior beaverior;

    private struct Tab
    {
        public Image[] tabImg;
        public TextMeshProUGUI[] tabText;
        public Color selectColor;
        public Color deSelectColor;
        public Sprite tabSelect;
        public Sprite tabDeSelect;
    }

    private InteriorTab nowTab;
    private List<BeaveriorSlot> itemMenu = new List<BeaveriorSlot>();

    public void SetUI()
    {
        nowTab = InteriorTab.Wall;

        SelectMenu(nowTab, true);
    }

    public void SelectMenu(int menuNum)
    {
        //인스펙터로 끌어서 사용하는 함수
        SelectMenu((InteriorTab)menuNum, true);
        soundMgr.PlaySE(Sound.Btn_SE);
    }

    private void SelectMenu(InteriorTab pMenu,bool moveTop)
    {

        PlayData playData = gameMgr.playData;
        if (playData == null)
            return;

        playerMoney.SetMoney(playData.money);
        nowTab = pMenu;
        beaverior.UpdateList(nowTab);
        if(moveTop)
            slotContents.anchoredPosition = Vector2.zero;
        for (int i = 0; i < tabInfo.tabImg.Length; i++)
        {
            if (i == (int)nowTab)
            {
                tabInfo.tabImg[i].sprite = tabInfo.tabSelect;
                tabInfo.tabText[i].color = tabInfo.selectColor;
                tabInfo.tabImg[i].transform.SetAsLastSibling();
            }
            else
            {
                tabInfo.tabImg[i].sprite = tabInfo.tabDeSelect;
                tabInfo.tabText[i].color = tabInfo.deSelectColor;
            }
        }

        itemMenu.ForEach((x) => x.gameObject.SetActive(false));
        for (int i = 0; i < beaverior.itemList.Count; i++)
        {
            if (i >= itemMenu.Count)
            {
                BeaveriorSlot slotMenu = Instantiate(shopMenuSlot, slotContents);
                itemMenu.Add(slotMenu);
            }

            itemMenu[i].SetData(beaverior.itemList[i], 
                (item) => OpenPurchaseCheck((InteriorItem)item),
                (item) => PurchaseBtn((InteriorItem)item));
            itemMenu[i].gameObject.SetActive(true);
        }
    }

    private void OpenPurchaseCheck(InteriorItem pInteriorItem)
    {
        bool isUse = gameMgr.playData.IsUseInterior(pInteriorItem);
        purchaseCheck.SetUI(pInteriorItem, isUse, () =>
        {
            //인스펙터로 끌어서 사용하는 함수
            PlayData playData = gameMgr.playData;
            if (playData == null)
                return;

            if (playData.hasInterior[(int)pInteriorItem])
            {
                playData.SetInterior(pInteriorItem);
                SelectMenu(nowTab, true);
                soundMgr.PlaySE(Sound.Btn_SE);
            }
            else
            {
                InteriorData interiorData = interiorMgr.GetInteriorData(pInteriorItem);
                if (interiorData == null)
                    return;

                int newMoney = (int)(interiorData.price * (100f - gameMgr.playData.ShopSaleValue()) / 100f);
                if (playData.money < newMoney)
                {
                    //돈이 부족하다.
                    return;
                }

                soundMgr.PlaySE(Sound.GetMoney_SE);

                playData.AddInterior(pInteriorItem);
                playData.money -= newMoney;
                playData.SetInterior(pInteriorItem);
                SelectMenu(nowTab, false);
            }
        });
    }

    private void PurchaseBtn(InteriorItem pInteriorItem)
    {
        //인스펙터로 끌어서 사용하는 함수
        PlayData playData = gameMgr.playData;
        if (playData == null)
            return;

        if (playData.hasInterior[(int)pInteriorItem])
        {
            playData.SetInterior(pInteriorItem);
            SelectMenu(nowTab, false);
            soundMgr.PlaySE(Sound.Btn_SE);
            return;
        }

        OpenPurchaseCheck(pInteriorItem);
    }
}
