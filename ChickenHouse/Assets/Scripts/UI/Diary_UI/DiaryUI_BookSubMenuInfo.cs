using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class DiaryUI_BookSubMenuInfo : Mgr
{
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemExplain;
    [SerializeField] private TextMeshProUGUI itemMoney;
    [SerializeField] private TextMeshProUGUI itemCost;
    [SerializeField] private RectTransform  rect;
    [SerializeField] private Image          face;
    [SerializeField] private RectTransform alcoholic;

    public void SetUI(Drink pDrink)
    {
        bool isAct = BookMgr.IsActDrink(pDrink);
        rect.gameObject.SetActive(isAct);
        if (isAct == false)
            return;
        DrinkData drinkData = subMenuMgr.GetDrinkData(pDrink);

        face.sprite = drinkData.img;
        face.GetComponent<RectTransform>().sizeDelta = new Vector2(55, 70);

        LanguageMgr.SetString(itemName, drinkData.nameKey);
        LanguageMgr.SetString(itemExplain, drinkData.infoKey);

        if (subMenuMgr.IsAlcohol(pDrink))
            alcoholic.gameObject.SetActive(true);
        else
            alcoholic.gameObject.SetActive(false);

        string moneyStr = LanguageMgr.GetMoneyStr(itemMoney.fontSize, drinkData.price);
        itemMoney.text = moneyStr;
        if (drinkData.cost > 0)
        {
            itemCost.gameObject.SetActive(true);
            string costStr = string.Format("-{0}", LanguageMgr.GetMoneyStr(itemCost.fontSize, drinkData.cost));
            itemCost.text = costStr;
        }
        else
        {
            itemCost.gameObject.SetActive(false);
        }
    }

    public void SetUI(SideMenu pSideMenu)
    {
        bool isAct = BookMgr.IsActSideMenu(pSideMenu);
        rect.gameObject.SetActive(isAct);
        if (isAct == false)
            return;
        SideMenuData sideMenuData = subMenuMgr.GetSideMenuData(pSideMenu);

        face.sprite = sideMenuData.img;
        face.GetComponent<RectTransform>().sizeDelta = new Vector2(55, 55);

        LanguageMgr.SetString(itemName, sideMenuData.nameKey);
        LanguageMgr.SetString(itemExplain, sideMenuData.infoKey);

        alcoholic.gameObject.SetActive(false);

        string moneyStr = LanguageMgr.GetMoneyStr(itemMoney.fontSize, sideMenuData.price);
        itemMoney.text = moneyStr;

        if (sideMenuData.cost > 0)
        {
            itemCost.gameObject.SetActive(true);
            string costStr = string.Format("-{0}", LanguageMgr.GetMoneyStr(itemCost.fontSize, sideMenuData.cost));
            itemCost.text = costStr;
        }
        else
        {
            itemCost.gameObject.SetActive(false);
        }
    }
}
