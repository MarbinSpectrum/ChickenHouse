using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EggSlot : Mgr
{
    [SerializeField] private Image          img;
    [SerializeField] private Animation      animation;
    [SerializeField] private TrayEgg        trayEgg;
    [SerializeField] private RectTransform  eventBox;

    public bool isEmpty;

    public void SpawnChicken()
    {
        animation.Play("AddChicken");
        isEmpty = false;
        img.enabled = true;
    }

    public void RemoveChicken()
    {
        gameObject.SetActive(false);
        isEmpty = true;
        img.enabled = false;
    }

    public void OnMouseDrag()
    {
        if (tutoMgr.tutoComplete == false && tutoMgr.nowTuto != Tutorial.Tuto_2)
        {
            //튜토리얼이 아직 완료안된듯
            //혹시모르니 튜토리얼 타이밍때만 작동하도록 막아놓자
            return;
        }

        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if (kitchenMgr.dragState != DragState.None)
        {
            //빈손인 상태에서 드래그해야됨
            return;
        }
        if (kitchenMgr.cameraObj.lookArea != LookArea.Kitchen)
        {
            //주방을 보고있는 상태에서만 상호 작용 가능
            return;
        }
        kitchenMgr.dragState = DragState.Egg;
        img.enabled = false;
    }

    public void OnMouseUp()
    {
        if (tutoMgr.tutoComplete == false && tutoMgr.nowTuto != Tutorial.Tuto_2)
        {
            //튜토리얼이 아직 완료안된듯
            //혹시모르니 튜토리얼 타이밍때만 작동하도록 막아놓자
            return;
        }

        //손을때면 치킨이 떨어짐
        KitchenMgr kitchenMgr = KitchenMgr.Instance;

        if (kitchenMgr.mouseArea == DragArea.Tray_Flour &&
            kitchenMgr.dragState == DragState.Egg)
        {
            //밀가루 쪽으로 치킨을 드래그함
            if (kitchenMgr.trayFlour.AddChicken())
            {
                RemoveChicken();
                trayEgg.RemoveChicken();
                kitchenMgr.dragState = DragState.None;
                return;
            }
        }

        //손을때면 치킨이 떨어짐
        kitchenMgr.dragState = DragState.None;
        img.enabled = true;
    }

    public void SetRect(Vector2 pos ,Vector2 size)
    {
        eventBox.anchoredPosition = pos;
        eventBox.sizeDelta = size;
    }
}
