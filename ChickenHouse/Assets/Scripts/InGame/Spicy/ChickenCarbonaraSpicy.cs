using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenCarbonaraSpicy : Mgr
{
    [SerializeField] private GameObject obj;

    public void OnMouseDrag()
    {
        if (tutoMgr.tutoComplete == false)
        {
            //Ʃ�丮���� ���� �Ϸ�ȵȵ� ���Ƴ���
            return;
        }

        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if (kitchenMgr.dragState != DragState.None)
        {
            //����� ���¿��� �巡���ؾߵ�
            return;
        }
        if (kitchenMgr.cameraObj.lookArea != LookArea.Kitchen)
        {
            //�ֹ��� �����ִ� ���¿����� ��ȣ �ۿ� ����
            return;
        }
        kitchenMgr.dragState = DragState.Carbonara_Spicy;

        obj.gameObject.SetActive(false);
    }

    public void OnMouseUp()
    {
        if (tutoMgr.tutoComplete == false)
        {
            //Ʃ�丮���� ���� �Ϸ�ȵȵ� ���Ƴ���
            return;
        }

        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if (kitchenMgr.dragState != DragState.Carbonara_Spicy)
        {
            //�ش� ������Ʈ�� �巡�����̶�� �ǴܵǾ������� ����
            return;
        }

        //�������� ġŲ�ҽ��� ������
        kitchenMgr.dragState = DragState.None;

        if (kitchenMgr.mouseArea == DragArea.Chicken_Pack)
        {
            //ġŲ ��� �ֱ�
            if (kitchenMgr.chickenPack.AddChickenSource(ChickenSpicy.Carbonara))
            {
                kitchenMgr.chickenPack.UpdatePack();
            }
        }

        obj.gameObject.SetActive(true);
    }
}