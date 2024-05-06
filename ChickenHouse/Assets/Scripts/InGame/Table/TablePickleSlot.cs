using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TablePickleSlot : Mgr
{
    /** 피클이 올려졌는지 여부 **/
    public bool hasPickle { get; private set; }

    [SerializeField] private TableDrinkSlot drinkSlot;
    [SerializeField] private Image          pickleImg;
    [SerializeField] private GameObject     slotUI;
    [SerializeField] private TutoObj        tutoObj;
    [SerializeField] private ScrollObj      scrollObj;

    public void OnMouseEnter()
    {
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        kitchenMgr.mouseArea = DragArea.Pickle_Slot;
        kitchenMgr.pickleSlot = this;

        if (hasPickle)
        {
            //음료가 이미 놓여있음
            return;
        }
        if (kitchenMgr.dragState == DragState.Chicken_Pickle)
        {
            //음료를 놓을수있는 상태이긴하다.
            pickleImg.color = new Color(1, 1, 1, 0.5f);
            slotUI.gameObject.SetActive(true);
        }
    }

    public void OnMouseExit()
    {
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        kitchenMgr.mouseArea = DragArea.None;
        kitchenMgr.pickleSlot = null;

        if (hasPickle)
        {
            //음료가 이미 놓여있음
            return;
        }
        pickleImg.color = new Color(0, 0, 0, 0);
        slotUI.gameObject.SetActive(true);
    }

    public void OnMouseDown()
    {
        if (tutoMgr.tutoComplete == false)
        {
            //튜토리얼에서는 제거가 되지않음
            return;
        }

        //올려져있는 피클을 제거할때 사용
        if (hasPickle == false)
            return;

        pickleImg.color = new Color(0, 0, 0, 0);
        slotUI.gameObject.SetActive(true);

        hasPickle = false;

        scrollObj.isRun = true;
    }

    public bool Put_Pickle()
    {
        if (hasPickle)
        {
            //이미 치킨무가 놓임
            return false;
        }

        soundMgr.PlaySE(Sound.Put_SE);

        if (tutoMgr.tutoComplete == false && tutoMgr.nowTuto == Tutorial.Tuto_6 && drinkSlot.hasDrink)
        {
            tutoObj.PlayTuto();

            KitchenMgr kitchenMgr = KitchenMgr.Instance;
            //kitchenMgr.ui.goCounter.OpenBtn();
        }

        pickleImg.color = new Color(1, 1, 1, 1);
        slotUI.gameObject.SetActive(false);

        hasPickle = true;

        scrollObj.isRun = false;

        return true;
    }

    public void Init()
    {
        pickleImg.color = new Color(0, 0, 0, 0);
        slotUI.gameObject.SetActive(true);

        hasPickle = false;

        scrollObj.isRun = true;
    }
}
