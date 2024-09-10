using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlourSlot : Mgr
{
    [SerializeField] private Image          img;
    [SerializeField] private Animation      animation;
    [SerializeField] private TrayFlour      trayFlour;
    [SerializeField] private RectTransform  eventBox;

    public bool isEmpty;
    public bool isDrag;
    public void SpawnChicken()
    {
        animation.Play("AddChicken");
        isEmpty = false;
        img.enabled = true;
        isDrag = false;
    }

    public void RemoveChicken()
    {
        gameObject.SetActive(false);
        isEmpty = true;
        img.enabled = false;
        isDrag = false;
    }

    public void OnMouseDrag()
    {
        if (tutoMgr.tutoComplete == false && tutoMgr.nowTuto != Tutorial.Tuto_6)
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
        kitchenMgr.dragState = DragState.Flour;
        img.enabled = false;
        isDrag = true;
    }

    public void OnMouseUp()
    {
        if (tutoMgr.tutoComplete == false && tutoMgr.nowTuto != Tutorial.Tuto_6)
        {
            //Ʃ�丮���� ���� �Ϸ�ȵȵ�
            //Ȥ�ø𸣴� Ʃ�丮�� Ÿ�ֶ̹��� �۵��ϵ��� ���Ƴ���
            return;
        }

        //�������� ġŲ�� ������
        KitchenMgr kitchenMgr = KitchenMgr.Instance;

        if (kitchenMgr.mouseArea == DragArea.Chicken_Strainter &&
            kitchenMgr.dragState == DragState.Flour)
        {
            //ġŲ ���� ������ ġŲ�� �巡����
            if (kitchenMgr.chickenStrainter.AddChicken())
            {
                RemoveChicken();
                trayFlour.RemoveChicken();
                kitchenMgr.dragState = DragState.None;
                return;
            }
        }

        //�������� ġŲ�� ������
        kitchenMgr.dragState = DragState.None;
        img.enabled = true;
        isDrag = false;
    }

    public void SetRect(Vector2 pos, Vector2 size)
    {
        eventBox.anchoredPosition = pos;
        eventBox.sizeDelta = size;
    }
}
