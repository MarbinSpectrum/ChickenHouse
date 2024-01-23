using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenSource : MonoBehaviour
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
        kitchenMgr.dragState = DragState.Chicken_Source;
        obj.gameObject.SetActive(false);
    }

    private void OnMouseUp()
    {
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if (kitchenMgr.dragState != DragState.Chicken_Source)
        {
            //�ش� ������Ʈ�� �巡�����̶�� �ǴܵǾ������� ����
            return;
        }

        //�������� ġŲ�ҽ��� ������
        kitchenMgr.dragState = DragState.None;
        if (kitchenMgr.mouseArea == DragArea.Chicken_Pack)
        {
            //ġŲ ��� �ֱ�
            if (kitchenMgr.chickenPack.AddChickenSource())
            {
                kitchenMgr.chickenPack.Show_Chicken(true);
            }
        }

        obj.gameObject.SetActive(true);
    }
}
