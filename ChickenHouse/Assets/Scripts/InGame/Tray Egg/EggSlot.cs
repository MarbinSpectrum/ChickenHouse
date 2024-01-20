using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggSlot : Mgr
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animation      animation;
    [SerializeField] private Collider2D     collider;

    public bool isEmpty;

    public void SpawnChicken()
    {
        spriteRenderer.enabled = true;

        animation.Play("ToEgg");
        isEmpty = false;
    }

    public void RemoveChicken()
    {
        spriteRenderer.enabled = false;
        collider.enabled = false;
        isEmpty = true;
    }

    private void OnMouseDrag()
    {
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if (kitchenMgr.dragState != DragState.None)
        {
            //빈손인 상태에서 드래그해야됨
            return;
        }
        kitchenMgr.dragState = DragState.Egg;
        spriteRenderer.enabled = false;
    }

    private void OnMouseUp()
    {
        //손을때면 치킨이 떨어짐
        KitchenMgr kitchenMgr = KitchenMgr.Instance;

        if (kitchenMgr.mouseArea == DragArea.Tray_Flour &&
            kitchenMgr.dragState == DragState.Egg)
        {
            //밀가루 쪽으로 치킨을 드래그함
            if (kitchenMgr.trayFlour.AddChicken())
            {
                RemoveChicken();
                kitchenMgr.trayEgg.RemoveChicken();
                kitchenMgr.dragState = DragState.None;
                return;
            }
        }

        //손을때면 치킨이 떨어짐
        kitchenMgr.dragState = DragState.None;
        spriteRenderer.enabled = true;
        collider.enabled = true;
    }
}
