using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KitchenSideMenuSetSlot : Mgr
{
    [SerializeField] private RectTransform      sideMenuRect;
    [SerializeField] private Image              sideMenuFace;
    [SerializeField] private Image              enterEffect;
    [SerializeField] private KitchenSideMenuDrag dragObj;
    private NoParaDel selectFun;
    private SideMenu sideMenu;
    public void SetUI(SideMenu pSideMenu, NoParaDel pSelect)
    {
        sideMenu = pSideMenu;
        selectFun = pSelect;

        bool isAct = (pSideMenu != SideMenu.None);
        sideMenuRect.gameObject.SetActive(isAct);

        SideMenuData sideMenuData = subMenuMgr.GetSideMenuData(pSideMenu);
        if (sideMenuData == null)
            return;
        sideMenuFace.sprite = sideMenuData.img;
        enterEffect.gameObject.SetActive(false);
    }

    public void Select() => selectFun?.Invoke();

    public void EnterEvent()
    {
        if (sideMenu != SideMenu.None)
            return;
        if (dragObj.sideMenu == SideMenu.None)
            return;

        if (gameMgr.playData != null)
        {
            for (MenuSetPos setCheck = MenuSetPos.SideMenu0;
             setCheck < MenuSetPos.SideMenuMAX; setCheck++)
            {
                SideMenu sideMenu = (SideMenu)gameMgr.playData.sideMenu[(int)setCheck];
                if (dragObj.sideMenu == sideMenu)
                {
                    //해당 메뉴는 이미 배치되어 있다.
                    return;
                }
            }
        }

        enterEffect.gameObject.SetActive(true);
    }

    public void ExitEvent()
    {
        enterEffect.gameObject.SetActive(false);
    }

    public void AddEvent()
    {
        if (dragObj.sideMenu == SideMenu.None)
            return;
        selectFun?.Invoke();
    }
}
