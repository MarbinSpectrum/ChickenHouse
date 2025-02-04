using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KitchenSpicySetSlot : Mgr
{
    [SerializeField] private RectTransform      spicyRect;
    [SerializeField] private SeasoningFace      spicyFace;
    [SerializeField] private Image              enterEffect;
    [SerializeField] private KitchenSpicyDrag   dragObj;
    private NoParaDel selectFun;
    private ChickenSpicy eChickenSpicy;
    public void SetUI(ChickenSpicy pChickenSpicy, NoParaDel pSelect)
    {
        eChickenSpicy = pChickenSpicy;
        selectFun = pSelect;

        bool isAct = (pChickenSpicy != ChickenSpicy.None);
        spicyRect.gameObject.SetActive(isAct);
        spicyFace.SetUI(pChickenSpicy);
        enterEffect.gameObject.SetActive(false);
    }

    public void Select() => selectFun?.Invoke();

    public void EnterEvent()
    {
        if (eChickenSpicy != ChickenSpicy.None)
            return;
        if (dragObj.spicy == ChickenSpicy.None)
            return;

        if (gameMgr.playData != null)
        {
            for (MenuSetPos setCheck = MenuSetPos.Spicy0;
                    setCheck < MenuSetPos.SpicyMAX; setCheck++)
            {
                ChickenSpicy spicy = (ChickenSpicy)gameMgr.playData.spicy[(int)setCheck];
                if (dragObj.spicy == spicy)
                {
                    //해당 양념은 이미 배치되어 있다.
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
        if (dragObj.spicy == ChickenSpicy.None)
            return;
        selectFun?.Invoke();
    }
}
