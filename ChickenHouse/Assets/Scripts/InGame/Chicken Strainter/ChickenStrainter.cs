using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChickenStrainter : Mgr
{
    private const int MAX_CHICKEN_SLOT = 4;

    /**�� ���� **/
    private int chickenCnt;

    public bool isRun { get; private set; } = true;

    [System.Serializable]
    public struct SPITE_IMG
    {
        //������Ʈ ��������Ʈ �̹���
        public Sprite           normalSprite;
        public Sprite           canUseSprite;
    }
    [SerializeField] private SPITE_IMG      sprite;
    [SerializeField] private Image          image;
    [SerializeField] private Animation[]    chickenAni;
    [SerializeField] private ScrollObj[]    scrollObj;
    [SerializeField] private GameObject     obj;
    [SerializeField] private TutoObj        tutoObj;

    public void OnMouseEnter()
    {
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if (kitchenMgr.dragState == DragState.Flour && chickenCnt < MAX_CHICKEN_SLOT)
        {
            //ġŲ�� �巡���ؼ� ����������
            //������ ������ִ�.
            image.sprite = sprite.canUseSprite;
        }
        else
        {
            //������ ���¸� �̹����� ������ �ʴ´�.
            image.sprite = sprite.normalSprite;
        }
        kitchenMgr.chickenStrainter = this;
        kitchenMgr.mouseArea = DragArea.Chicken_Strainter;
    }

    public void OnMouseExit()
    {
        image.sprite = sprite.normalSprite;

        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        kitchenMgr.chickenStrainter = null;
        kitchenMgr.mouseArea = DragArea.None;
    }

    public void OnMouseDrag()
    {
        if (tutoMgr.tutoComplete == false && tutoMgr.nowTuto != Tutorial.Tuto_4)
        {
            //Ʃ�丮���� ���� �Ϸ�ȵȵ�
            //Ȥ�ø𸣴� Ʃ�丮�� Ÿ�ֶ̹��� �۵��ϵ��� ���Ƴ���
            return;
        }

        if (isRun == false)
            return;

        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if (kitchenMgr.cameraObj.lookArea != LookArea.Kitchen)
        {
            //�ֹ��� �����ִ� ���¿����� ��ȣ �ۿ� ����
            return;
        }
        if (chickenCnt >= MAX_CHICKEN_SLOT)
        {
            obj.gameObject.SetActive(false);
            isRun = false;
            kitchenMgr.dragObj.DragStrainter(chickenCnt, DragState.Chicken_Strainter);
        }
    }

    public void OnMouseUp()
    {
        if (tutoMgr.tutoComplete == false && tutoMgr.nowTuto != Tutorial.Tuto_4)
        {
            //Ʃ�丮���� ���� �Ϸ�ȵȵ�
            //Ȥ�ø𸣴� Ʃ�丮�� Ÿ�ֶ̹��� �۵��ϵ��� ���Ƴ���
            return;
        }

        KitchenMgr kitchenMgr = KitchenMgr.Instance;

        if (kitchenMgr.dragState != DragState.Chicken_Strainter)
        {
            //�ش� ������Ʈ�� �巡�����̶�� �ǴܵǾ������� ����
            return;
        }

        //�������� ġŲ���� ������
        kitchenMgr.dragState = DragState.None;
        if (kitchenMgr.mouseArea == DragArea.Oil_Zone)
        {
            //ġŲ Ƣ��� ����
            if (kitchenMgr.oilZone.Cook_Start(chickenCnt,this))
            {
                kitchenMgr.ui.takeOut.ChickenStrainter_SetData(kitchenMgr.oilZone, this);

                for (int i = 0; i < chickenCnt; i++)
                {
                    RemoveChicken();
                }
                return;
            }
        }

        isRun = true;
        obj.gameObject.SetActive(true);
    }

    public bool AddChicken()
    {
        if (isRun == false)
            return false;

        if (chickenCnt >= MAX_CHICKEN_SLOT)
            return false;

        //�⸧ ������ �÷����ִ� �� ����
        chickenCnt++;
        image.sprite = sprite.normalSprite;

        for (int i = 0; i < chickenAni.Length; i++)
        {
            if(i < chickenCnt)
                chickenAni[i].gameObject.SetActive(true);
            else
                chickenAni[i].gameObject.SetActive(false);
        }

        if (chickenAni.Length > chickenCnt - 1)
        {
            //�߿ø��� �ִϸ��̼� ����
            chickenAni[chickenCnt - 1].Play("ToChickenStrainter");
        }

        if (chickenCnt == MAX_CHICKEN_SLOT)
        {
            foreach (ScrollObj sObj in scrollObj)
            {
                sObj.isRun = false;
            }

            if (tutoMgr.tutoComplete == false)
            {
                //Ʃ�丮���� ������ѵ�?
                //Ʃ�丮��� ����
                tutoObj.PlayTuto();
            }
        }

        return true;
    }

    public bool RemoveChicken()
    {
        //Ʈ���̿� �÷����ִ� �� ����
        if (chickenCnt <= 0)
            return false;

        foreach(ScrollObj sObj in scrollObj)
        {
            sObj.isRun = true;
        }

        chickenCnt--;
        chickenAni[chickenCnt].gameObject.SetActive(false);
        return true;
    }

    public void Init()
    {
        //�ʱ�ȭ ��
        chickenCnt = 0;
        Array.ForEach(chickenAni, x => x.gameObject.SetActive(false));
        isRun = true;
        obj.gameObject.SetActive(true);

        foreach (ScrollObj sObj in scrollObj)
        {
            sObj.isRun = true;
        }
    }
}
