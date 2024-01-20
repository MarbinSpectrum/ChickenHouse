using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlourSlot : Mgr
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animation      animation;
    [SerializeField] private Collider2D     collider;

    public bool isEmpty;

    public void SpawnChicken()
    {
        spriteRenderer.enabled = true;

        animation.Play("ToFlour");
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
        kitchenMgr.dragState = DragState.Flour;
        spriteRenderer.enabled = false;
    }

    private void OnMouseUp()
    {
        //손을때면 치킨이 떨어짐
        KitchenMgr kitchenMgr = KitchenMgr.Instance;

        if (kitchenMgr.mouseArea == DragArea.Chicken_Strainter &&
            kitchenMgr.dragState == DragState.Flour)
        {
            //치킨 건지 쪽으로 치킨을 드래그함
            if (kitchenMgr.chickenStrainter.AddChicken())
            {
                RemoveChicken();
                kitchenMgr.trayFlour.RemoveChicken();
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
