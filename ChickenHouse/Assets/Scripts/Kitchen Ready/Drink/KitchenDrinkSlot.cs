using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class KitchenDrinkSlot : Mgr
{
    [SerializeField] private TextMeshProUGUI    drinkName;
    [SerializeField] private Image              drinkFace;

    [SerializeField] private RectTransform selectRect;
    [SerializeField] private RectTransform partyRect;

    private NoParaDel selectDrinkFun;
    private NoParaDel dragStart;
    private NoParaDel dragEnd;

    public void SetUI(Drink pDrink, bool pSelect, bool pIsParty, NoParaDel pSelectFun
        , NoParaDel pDragStartFun, NoParaDel pDragEndFun)
    {
        DrinkData drinkData = subMenuMgr.GetDrinkData(pDrink);
        if (drinkData == null)
        {
            gameObject.SetActive(false);
            return;
        }

        selectDrinkFun = pSelectFun;
        dragStart = pDragStartFun;
        dragEnd = pDragEndFun;

        gameObject.SetActive(true);
        partyRect.gameObject.SetActive(pIsParty);
        selectRect.gameObject.SetActive(pSelect);
        drinkFace.sprite = drinkData.img;
        LanguageMgr.SetString(drinkName, drinkData.nameKey, true);
    }

    public void SelectDrinkMenu() => selectDrinkFun?.Invoke();

    public void DragStart() => dragStart?.Invoke();

    public void DragEnd() => dragEnd?.Invoke();
}
