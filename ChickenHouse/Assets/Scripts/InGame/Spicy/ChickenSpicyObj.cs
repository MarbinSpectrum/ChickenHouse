using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChickenSpicyObj : Mgr
{
    [SerializeField] private Image      spicyImg;
    private ChickenSpicy    eChickenSpicy;
    private DragState       eDragState;

    public void SetObj(ChickenSpicy pChickenSpicy)
    {
        eChickenSpicy = pChickenSpicy;
        eDragState = SpicyMgr.GetSpicyDragState(eChickenSpicy);

        SpicyData spicyData = spicyMgr.GetSpicyData(eChickenSpicy);
        spicyImg.sprite = spicyData.img;
    }

    public void OnMouseDrag()
    {
        if (gameMgr.playData.tutoComplete1 == false)
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
        kitchenMgr.dragState = eDragState;

        spicyImg.color = new Color(0, 0, 0, 0);
    }

    public void OnMouseUp()
    {
        if (gameMgr.playData.tutoComplete1 == false)
        {
            //Ʃ�丮���� ���� �Ϸ�ȵȵ� ���Ƴ���
            return;
        }

        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if (kitchenMgr.dragState != eDragState)
        {
            //�ش� ������Ʈ�� �巡�����̶�� �ǴܵǾ������� ����
            return;
        }

        //�������� ġŲ�ҽ��� ������
        kitchenMgr.dragState = DragState.None;

        if (kitchenMgr.mouseArea == DragArea.Chicken_Pack)
        {
            //ġŲ ��� �ֱ�
            if (kitchenMgr.chickenPack.AddChickenSource(eChickenSpicy))
            {
                kitchenMgr.chickenPack.UpdatePack();
            }
        }

        spicyImg.color = new Color(1, 1, 1, 1);
    }
}
