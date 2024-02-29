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
            //Ʃ�丮���� ���� �Ϸ�ȵȵ�
            //Ȥ�ø𸣴� Ʃ�丮�� Ÿ�ֶ̹��� �۵��ϵ��� ���Ƴ���
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
        kitchenMgr.dragState = DragState.Egg;
        img.enabled = false;
    }

    public void OnMouseUp()
    {
        if (tutoMgr.tutoComplete == false && tutoMgr.nowTuto != Tutorial.Tuto_2)
        {
            //Ʃ�丮���� ���� �Ϸ�ȵȵ�
            //Ȥ�ø𸣴� Ʃ�丮�� Ÿ�ֶ̹��� �۵��ϵ��� ���Ƴ���
            return;
        }

        //�������� ġŲ�� ������
        KitchenMgr kitchenMgr = KitchenMgr.Instance;

        if (kitchenMgr.mouseArea == DragArea.Tray_Flour &&
            kitchenMgr.dragState == DragState.Egg)
        {
            //�а��� ������ ġŲ�� �巡����
            if (kitchenMgr.trayFlour.AddChicken())
            {
                RemoveChicken();
                trayEgg.RemoveChicken();
                kitchenMgr.dragState = DragState.None;
                return;
            }
        }

        //�������� ġŲ�� ������
        kitchenMgr.dragState = DragState.None;
        img.enabled = true;
    }

    public void SetRect(Vector2 pos ,Vector2 size)
    {
        eventBox.anchoredPosition = pos;
        eventBox.sizeDelta = size;
    }
}
