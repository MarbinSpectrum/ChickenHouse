using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class KitchenSideMenuSlot : Mgr
{
    [SerializeField] private TextMeshProUGUI    sideMenuName;
    [SerializeField] private Image              sideMenuFace;

    [SerializeField] private RectTransform      selectRect;
    [SerializeField] private RectTransform      partyRect;

    private NoParaDel selectSideMenuFun;

    public void SetUI(SideMenu pSideMenu, bool pSelect, bool pIsParty, NoParaDel pSelectFun)
    {
        SideMenuData sideMenuData = subMenuMgr.GetSideMenuData(pSideMenu);
        if (sideMenuData == null)
        {
            gameObject.SetActive(false);
            return;
        }

        selectSideMenuFun = pSelectFun;

        gameObject.SetActive(true);
        partyRect.gameObject.SetActive(pIsParty);
        selectRect.gameObject.SetActive(pSelect);
        sideMenuFace.sprite = sideMenuData.img;
        LanguageMgr.SetString(sideMenuName, sideMenuData.nameKey, true);
    }

    public void SelectSideMenu() => selectSideMenuFun?.Invoke();
}
