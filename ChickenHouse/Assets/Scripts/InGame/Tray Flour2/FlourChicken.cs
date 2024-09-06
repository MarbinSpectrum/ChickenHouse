using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlourChicken : Mgr
{
    [SerializeField] private ChickenLerp_Shader lerpImg;
    [SerializeField] private Animation animation;
    [SerializeField] private TrayFlour2 trayFlour;
    [SerializeField] private RectTransform eventBox;

    public bool isEmpty;
    public bool isDrag;
    private int clickCnt = 0;
    private const int MAX_CLICK_CNT = 5;

    public void SpawnChicken()
    {
        animation.Play("AddChicken2");
        isEmpty = false;
        lerpImg.gameObject.SetActive(true);
        isDrag = false;
        clickCnt = 0;
        lerpImg.SetValue(0);
    }

    public void RemoveChicken()
    {
        gameObject.SetActive(false);
        isEmpty = true;
        lerpImg.gameObject.SetActive(false);
        isDrag = false;
        lerpImg.SetValue(0);
    }

    public void OnMouseDrag()
    {
        if (tutoMgr.tutoComplete == false && tutoMgr.nowTuto != Tutorial.Tuto_3)
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
        if(clickCnt != MAX_CLICK_CNT)
        {
            //�Ϸ��������
            return;
        }

        kitchenMgr.dragState = DragState.Flour;
        lerpImg.gameObject.SetActive(false);
        isDrag = true;
    }

    public void ClickChicken()
    {
        if (clickCnt == MAX_CLICK_CNT)
        {
            //�̹� �а��� �� ����
            return;
        }
        clickCnt++;
        lerpImg.SetValue((float)clickCnt / MAX_CLICK_CNT);
    }


    public void OnMouseUp()
    {
        if (tutoMgr.tutoComplete == false && tutoMgr.nowTuto != Tutorial.Tuto_3)
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
        lerpImg.gameObject.SetActive(true);
        isDrag = false;
    }

    public void SetRect(Vector2 pos, Vector2 size)
    {
        eventBox.anchoredPosition = pos;
        eventBox.sizeDelta = size;
    }
}
