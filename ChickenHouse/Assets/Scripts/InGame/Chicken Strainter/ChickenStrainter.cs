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

    //public bool isRun { get; private set; } = true;

    public bool isHold { get; private set; } = false;

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
        kitchenMgr.mouseArea = DragArea.None;

        if(isHold == false)
            kitchenMgr.chickenStrainter = null;
    }

    public void HoldStrainter()
    {
        if (CheckMode.IsDropMode() == false)
            return;



        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if (kitchenMgr.dragObj.holdGameObj == null)
        {
            HoldStrainter(true);
        }
        else if (kitchenMgr.dragObj.holdGameObj == gameObject)
        {
            HoldStrainter(false);
        }
        else if (kitchenMgr.dragObj.holdGameObj != gameObject)
        {
            kitchenMgr.dragState = DragState.None;
            kitchenMgr.dragObj.HoldObj(null);
            kitchenMgr.dragObj.HoldOut();
        }
    }

    public void HoldStrainter(bool state)
    {
        //�ν����Ϳ��� ��� ����ϴ� �Լ���
        if (CheckMode.IsDropMode() == false)
            return;

        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if (state)
        {
            if (tutoMgr.tutoComplete == false && tutoMgr.nowTuto != Tutorial.Tuto_4)
            {
                //Ʃ�丮���� ���� �Ϸ�ȵȵ�
                //Ȥ�ø𸣴� Ʃ�丮�� Ÿ�ֶ̹��� �۵��ϵ��� ���Ƴ���
                return;
            }

            if (kitchenMgr.cameraObj.lookArea != LookArea.Kitchen)
            {
                //�ֹ��� �����ִ� ���¿����� ��ȣ �ۿ� ����
                return;
            }

            if (isHold)
            {
                //�̹� ����ִµ� �������������?
                return;
            }

            if (IsMax())
            {
                isHold = true;
                obj.gameObject.SetActive(false);
                kitchenMgr.chickenStrainter = this;
                kitchenMgr.dragObj.DragStrainter(chickenCnt, DragState.Chicken_Strainter);
                kitchenMgr.dragObj.HoldObj(gameObject);
            }
        }
        else
        {
            if (isHold == false)
            {
                return;
            }

            isHold = false;
            obj.gameObject.SetActive(true);
            kitchenMgr.chickenStrainter = null;
            kitchenMgr.dragState = DragState.None;
            kitchenMgr.dragObj.HoldObj(null);
        }
    }

    public void PutDown(Oil_Zone pOilZone)
    {
        if (tutoMgr.tutoComplete == false && tutoMgr.nowTuto != Tutorial.Tuto_4)
        {
            //Ʃ�丮���� ���� �Ϸ�ȵȵ�
            //Ȥ�ø𸣴� Ʃ�丮�� Ÿ�ֶ̹��� �۵��ϵ��� ���Ƴ���
            return;
        }

        if (IsMax() == false)
        {
            //������ ���·� �������ߵ�
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

        //ġŲ Ƣ��� ����
        if (pOilZone.Cook_Start(chickenCnt))
        {
            int removeCnt = chickenCnt;
            for (int i = 0; i < removeCnt; i++)
            {
                RemoveChicken();
            }
        }

        isHold = false;
        obj.gameObject.SetActive(true);
        kitchenMgr.dragObj.HoldObj(null);
    }

    public void OnMouseDrag()
    {
        if (CheckMode.IsDropMode())
            return;

        if (tutoMgr.tutoComplete == false && tutoMgr.nowTuto != Tutorial.Tuto_4)
        {
            return;
        }

        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if (kitchenMgr.cameraObj.lookArea != LookArea.Kitchen)
        {
            return;
        }
        if (IsMax())
        {
            isHold = true;
            obj.gameObject.SetActive(false);
            kitchenMgr.dragObj.DragStrainter(chickenCnt, DragState.Chicken_Strainter);
        }
    }

    public void OnMouseUp()
    {
        if (CheckMode.IsDropMode())
            return;

        if (tutoMgr.tutoComplete == false && tutoMgr.nowTuto != Tutorial.Tuto_4)
        {
            return;
        }

        KitchenMgr kitchenMgr = KitchenMgr.Instance;

        if (kitchenMgr.dragState != DragState.Chicken_Strainter)
        {
            return;
        }

        kitchenMgr.dragState = DragState.None;
        if (kitchenMgr.mouseArea == DragArea.Oil_Zone)
        {
            if (kitchenMgr.oilZone.Cook_Start(chickenCnt))
            {
                int removeCnt = chickenCnt;
                for (int i = 0; i < removeCnt; i++)
                {
                    RemoveChicken();
                }
            }
        }

        isHold = false;
        obj.gameObject.SetActive(true);
    }

    public bool AddChicken()
    {
        if (IsMax())
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

        if (IsMax())
        {
            if (CheckMode.IsDropMode() == false)
            {
                foreach (ScrollObj sObj in scrollObj)
                {
                    sObj.isRun = false;
                }
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

    public bool IsMax() => (chickenCnt == MAX_CHICKEN_SLOT);

    public bool RemoveChicken()
    {
        //Ʈ���̿� �÷����ִ� �� ����
        if (chickenCnt <= 0)
            return false;
        if (CheckMode.IsDropMode() == false)
        {
            foreach (ScrollObj sObj in scrollObj)
            {
                sObj.isRun = true;
            }
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
        obj.gameObject.SetActive(true);

        if (CheckMode.IsDropMode() == false)
        {
            foreach (ScrollObj sObj in scrollObj)
            {
                sObj.isRun = true;
            }
        }
    }
}
