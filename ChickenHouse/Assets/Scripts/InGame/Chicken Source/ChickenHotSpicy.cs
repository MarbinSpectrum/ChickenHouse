using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenHotSpicy : Mgr
{
    [SerializeField] private GameObject obj;

    private void OnMouseDrag()
    {
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if (kitchenMgr.dragState != DragState.None)
        {
            //����� ���¿��� �巡���ؾߵ�
            return;
        }
        kitchenMgr.dragState = DragState.Hot_Spicy;
        obj.gameObject.SetActive(false);
    }

    private void OnMouseUp()
    {
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if (kitchenMgr.dragState != DragState.Hot_Spicy)
        {
            //�ش� ������Ʈ�� �巡�����̶�� �ǴܵǾ������� ����
            return;
        }

        //�������� ġŲ�ҽ��� ������
        kitchenMgr.dragState = DragState.None;
        if (kitchenMgr.mouseArea == DragArea.Chicken_Pack)
        {
            //ġŲ ��� �ֱ�
            if (kitchenMgr.chickenPack.AddChickenSource(ChickenSpicy.Hot))
            {
                kitchenMgr.chickenPack.UpdatePack();
            }
        }

        obj.gameObject.SetActive(true);
    }
}
