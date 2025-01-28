using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenSpicySetSlot : Mgr
{
    [SerializeField] private RectTransform spicyRect;
    [SerializeField] private SeasoningFace spicyFace;
    private NoParaDel selectFun;

    public void SetUI(ChickenSpicy pChickenSpicy, NoParaDel pSelect)
    {
        selectFun = pSelect;

        bool isAct = (pChickenSpicy != ChickenSpicy.None);
        spicyRect.gameObject.SetActive(isAct);
        spicyFace.SetUI(pChickenSpicy);
    }

    public void Select() => selectFun?.Invoke();
}
