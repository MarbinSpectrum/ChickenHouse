using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableDrinkSlot : Mgr
{
    /** 음료가 올려졌는지 여부 **/
    public bool hasDrink { get; private set; }

    [SerializeField] private SpriteRenderer drinkImg;
    [SerializeField] private GameObject     slotUI;

    private void OnMouseDown()
    {
        //올려져있는 음료를 제거할때 사용
        if (hasDrink == false)
            return;
        hasDrink = false;
    }

    private void Update()
    {
        UpdateDrinkSlot();
    }

    private void UpdateDrinkSlot()
    {
        if (hasDrink)
        {
            //음료가 이미 놓여있음
            drinkImg.color = new Color(1, 1, 1, 1);
            slotUI.gameObject.SetActive(false);
            return;
        }
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if (kitchenMgr.dragState == DragState.Cola)
        {
            //음료를 놓을수있는 상태이긴하다.
            drinkImg.color = new Color(1, 1, 1, 0.5f);
            slotUI.gameObject.SetActive(true);
        }
        else
        {
            //음료를 밖으로 내보내면 이펙트 비활성화
            drinkImg.color = new Color(0, 0, 0, 0);
            slotUI.gameObject.SetActive(true);
        }
    }

    public bool Put_Drink()
    {
        if (hasDrink)
        {
            //이미 음료가 놓임
            return false;
        }

        soundMgr.PlaySE(Sound.Put_SE);
        hasDrink = true;
        return true;
    }

    public void Init()
    {
        hasDrink = false;
    }
}
