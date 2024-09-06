using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSetSideMenuToken : Mgr
{
    [SerializeField] private Image          sideMenuFace;
    [SerializeField] private CanvasGroup    canvasGroup;
    [SerializeField] private MenuSet_UI     menuSetUI;

    private SideMenu sideMenu = SideMenu.None;

    public void SetUI(SideMenu pDrink, float pAlpha = 1)
    {
        if (pDrink == SideMenu.None)
        {
            canvasGroup.alpha = 0;
            return;
        }

        sideMenu = pDrink;

        SideMenuData drinkData = subMenuMgr.GetSideMenuData(sideMenu);
        sideMenuFace.sprite = drinkData.img;
        canvasGroup.alpha = pAlpha;
    }

    public void DragToken()
    {
        //�ν����Ϳ��� ��� ����ϴ� �Լ���
        menuSetUI.DragToken((int)sideMenu, MenuSet_UI.MenuSetDragType.SideMenu);
    }
}
