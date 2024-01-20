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
            //����� ���¿��� �巡���ؾߵ�
            return;
        }
        kitchenMgr.dragState = DragState.Egg;
        spriteRenderer.enabled = false;
    }

    private void OnMouseUp()
    {
        //�������� ġŲ�� ������
        KitchenMgr kitchenMgr = KitchenMgr.Instance;

        if (kitchenMgr.mouseArea == DragArea.Tray_Flour &&
            kitchenMgr.dragState == DragState.Egg)
        {
            //�а��� ������ ġŲ�� �巡����
            if (kitchenMgr.trayFlour.AddChicken())
            {
                RemoveChicken();
                kitchenMgr.trayEgg.RemoveChicken();
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
