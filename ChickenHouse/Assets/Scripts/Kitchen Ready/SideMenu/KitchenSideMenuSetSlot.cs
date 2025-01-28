using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KitchenSideMenuSetSlot : Mgr
{
    [SerializeField] private RectTransform  sideMenuRect;
    [SerializeField] private Image          sideMenuFace;
    private NoParaDel selectFun;

    public void SetUI(SideMenu pSideMenu, NoParaDel pSelect)
    {
        selectFun = pSelect;

        bool isAct = (pSideMenu != SideMenu.None);
        sideMenuRect.gameObject.SetActive(isAct);

        SideMenuData sideMenuData = subMenuMgr.GetSideMenuData(pSideMenu);
        if (sideMenuData == null)
            return;
        sideMenuFace.sprite = sideMenuData.img;
    }

    public void Select() => selectFun?.Invoke();
}
