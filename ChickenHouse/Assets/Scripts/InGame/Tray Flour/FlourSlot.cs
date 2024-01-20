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
            //����� ���¿��� �巡���ؾߵ�
            return;
        }
        kitchenMgr.dragState = DragState.Flour;
        spriteRenderer.enabled = false;
    }

    private void OnMouseUp()
    {
        //�������� ġŲ�� ������
        KitchenMgr kitchenMgr = KitchenMgr.Instance;

        if (kitchenMgr.mouseArea == DragArea.Chicken_Strainter &&
            kitchenMgr.dragState == DragState.Flour)
        {
            //ġŲ ���� ������ ġŲ�� �巡����
            if (kitchenMgr.chickenStrainter.AddChicken())
            {
                RemoveChicken();
                kitchenMgr.trayFlour.RemoveChicken();
                kitchenMgr.dragState = DragState.None;
                return;
            }
        }

        //�������� ġŲ�� ������
        kitchenMgr.dragState = DragState.None;
        spriteRenderer.enabled = true;
        collider.enabled = true;
    }
}
