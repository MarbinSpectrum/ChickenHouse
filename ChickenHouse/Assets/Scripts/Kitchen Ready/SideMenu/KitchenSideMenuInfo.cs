using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KitchenSideMenuInfo : Mgr
{
    [SerializeField] private Image              sideMenuFace;
    [SerializeField] private TextMeshProUGUI    sideMenuName;
    [SerializeField] private TextMeshProUGUI    sideMenuInfo;

    public void SetUI(SideMenu pSideMenu)
    {
        SideMenuData sideMenuData = subMenuMgr.GetSideMenuData(pSideMenu);
        if (sideMenuData == null)
        {
            gameObject.SetActive(false);
            return;
        }

        if (pSideMenu == SideMenu.None)
        {
            sideMenuFace.gameObject.SetActive(false);
            sideMenuName.gameObject.SetActive(false);
            sideMenuInfo.gameObject.SetActive(false);
            return;
        }
        gameObject.SetActive(true);
        sideMenuFace.sprite = sideMenuData.img;
        LanguageMgr.SetString(sideMenuName, sideMenuData.nameKey);
        LanguageMgr.SetString(sideMenuInfo, sideMenuData.infoKey);
        sideMenuFace.gameObject.SetActive(true);
        sideMenuName.gameObject.SetActive(true);
        sideMenuInfo.gameObject.SetActive(true);
    }
}
