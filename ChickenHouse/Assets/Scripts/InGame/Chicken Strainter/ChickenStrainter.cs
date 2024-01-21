using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenStrainter : Mgr
{
    private const int MAX_CHICKEN_SLOT = 6;

    /**�� ���� **/
    private int chickenCnt;

    public bool isRun { get; private set; } = true;

    [System.Serializable]
    public struct SPITE_IMG
    {
        //������Ʈ ��������Ʈ �̹���
        public SpriteRenderer   spriteImg;
        public Sprite           normalSprite;
        public Sprite           canUseSprite;
    }
    [SerializeField] private SPITE_IMG sprite;

    [SerializeField] private Animation[]    chickenAni;
    [SerializeField] private GameObject     obj;

    private void OnMouseDrag()
    {
        if (isRun == false)
            return;

        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if(chickenCnt >= 4)
        {
            obj.gameObject.SetActive(false);
            isRun = false;
            kitchenMgr.dragChickenStrainter.DragStart(chickenCnt, DragState.ChickenStrainter);
        }
    }

    private void OnMouseUp()
    {
        KitchenMgr kitchenMgr = KitchenMgr.Instance;

        if (kitchenMgr.dragState != DragState.ChickenStrainter)
        {
            //�ش� ������Ʈ�� �巡�����̶�� �ǴܵǾ������� ����
            return;
        }

        //�������� ġŲ���� ������
        kitchenMgr.dragState = DragState.None;
        if (kitchenMgr.mouseArea == DragArea.Oil_Zone)
        {
            //ġŲ Ƣ��� ����
            if (kitchenMgr.oilZone.Cook_Start(chickenCnt))
            {
                kitchenMgr.ui.takeOut.SetData(kitchenMgr.oilZone, this);

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

    private void Update()
    {
        UpdateChickenTray();
    }

    private void UpdateChickenTray()
    {
        if (isRun == false)
            return;

        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if (kitchenMgr.mouseArea == DragArea.Chicken_Strainter &&
            (kitchenMgr.chickenStrainter != null && kitchenMgr.chickenStrainter == this))
        {
            if (kitchenMgr.dragState == DragState.Flour &&
                    chickenCnt < MAX_CHICKEN_SLOT)
            {
                //ġŲ�� �巡���ؼ� ����������
                //������ ������ִ�.
                sprite.spriteImg.sprite = sprite.canUseSprite;
            }
            else
            {
                //������ ���¸� �̹����� ������ �ʴ´�.
                sprite.spriteImg.sprite = sprite.normalSprite;
            }
        }
        else
        {
            //���콺�� ������ �������� ����Ʈ ��Ȱ��ȭ
            sprite.spriteImg.sprite = sprite.normalSprite;
        }
    }

    public bool AddChicken()
    {
        if (isRun == false)
            return false;

        if (chickenCnt >= MAX_CHICKEN_SLOT)
            return false;

        //�⸧ ������ �÷����ִ� �� ����
        chickenCnt++;

        for(int i = 0; i < chickenAni.Length; i++)
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

        return true;
    }

    public bool RemoveChicken()
    {
        //Ʈ���̿� �÷����ִ� �� ����
        if (chickenCnt <= 0)
            return false;
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
    }
}
