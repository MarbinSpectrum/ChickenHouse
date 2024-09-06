using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSetDrinkToken : Mgr
{
    [SerializeField] private Image          drinkFace;
    [SerializeField] private CanvasGroup    canvasGroup;
    [SerializeField] private MenuSet_UI     menuSetUI;

    private Drink drink = Drink.None;

    public void SetUI(Drink pDrink, float pAlpha = 1)
    {
        if (pDrink == Drink.None)
        {
            canvasGroup.alpha = 0;
            return;
        }

        drink = pDrink;

        DrinkData drinkData = subMenuMgr.GetDrinkData(drink);
        drinkFace.sprite = drinkData.img;
        canvasGroup.alpha = pAlpha;
    }

    public void DragToken()
    {
        //�ν����Ϳ��� ��� ����ϴ� �Լ���
        menuSetUI.DragToken((int)drink, MenuSet_UI.MenuSetDragType.Drink);
    }
}
