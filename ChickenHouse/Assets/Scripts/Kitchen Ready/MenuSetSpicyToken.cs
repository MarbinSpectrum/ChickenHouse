using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSetSpicyToken : Mgr
{
    [SerializeField] private SeasoningFace spicyFace;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private MenuSet_UI menuSetUI;

    private ChickenSpicy spicy = ChickenSpicy.None;

    public void SetUI(ChickenSpicy pSpicy, float pAlpha = 1)
    {
        if (pSpicy == ChickenSpicy.None)
        {
            canvasGroup.alpha = 0;
            return;
        }

        spicy = pSpicy;

        spicyFace.SetUI(pSpicy);

        canvasGroup.alpha = pAlpha;
    }

    public void DragToken()
    {
        //�ν����Ϳ��� ��� ����ϴ� �Լ���
        menuSetUI.DragToken((int)spicy, MenuSet_UI.MenuSetDragType.Spicy);
    }
}
