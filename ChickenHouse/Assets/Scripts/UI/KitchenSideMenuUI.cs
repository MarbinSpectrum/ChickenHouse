using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenSideMenuUI : Mgr
{
    [SerializeField] private Animator menuUI;
    [SerializeField] private RectTransform contents;
    [SerializeField] private float actX;

    /** 드링크 **/
    [SerializeField] private List<DrinkObj> drinks = new List<DrinkObj>();
    /** 피클 **/
    [SerializeField] private List<SideMenuObj> sideMenus = new List<SideMenuObj>();

    [SerializeField] private RectTransform drinktEmpty;
    [SerializeField] private RectTransform sideMenuEmpty;

    private void Update()
    {
        bool isKitchen = (KitchenMgr.Instance.cameraObj.lookArea == LookArea.Kitchen);
        if (contents.anchoredPosition.x < -contents.sizeDelta.x + actX && isKitchen)
            menuUI.SetBool("Show", true);
        else
            menuUI.SetBool("Show", false);
        menuUI.gameObject.SetActive(KitchenMgr.Instance.cameraObj.runAni == false);
    }

    public void UpdateSlot()
    {
        /////////////////////////////////////////////////////////////////////////////////
        //음료 & 사이드메뉴 세팅
        drinktEmpty.gameObject.SetActive(true);
        drinks.ForEach((x) => x.gameObject.SetActive(false));
        List<Drink> actDrink = new List<Drink>();
        for (int i = 0; i < (int)MenuSetPos.DrinkMAX; i++)
        {
            if (gameMgr.playData == null)
                continue;
            Drink isDrink = (Drink)gameMgr.playData.drink[i];
            if (isDrink == Drink.None)
                continue;
            actDrink.Add(isDrink);
            drinktEmpty.gameObject.SetActive(false);
        }
        for (int i = 0; i < actDrink.Count; i++)
        {
            drinks[i].SetObj(actDrink[i]);
            drinks[i].gameObject.SetActive(true);
        }

        sideMenuEmpty.gameObject.SetActive(true);
        sideMenus.ForEach((x) => x.gameObject.SetActive(false));
        for (int i = 0; i < sideMenus.Count; i++)
        {
            if (gameMgr.playData == null)
                continue;

            for (int j = 0; j < gameMgr.playData.sideMenu.Length; j++)
            {
                SideMenu sideMenu = (SideMenu)gameMgr.playData.sideMenu[j];
                if (sideMenu == SideMenu.None || sideMenus[i].SideMenu != sideMenu)
                    continue;
                sideMenus[i].gameObject.SetActive(true);
                sideMenuEmpty.gameObject.SetActive(false);
                break;
            }
        }

    }
}
