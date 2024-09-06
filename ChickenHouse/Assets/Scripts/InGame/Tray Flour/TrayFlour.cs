using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrayFlour : Mgr
{
    private const int MAX_CHICKEN_SLOT = 4;
    private const float DEFAULT_SLOT_WIDTH = 9.5f;
    private const float DEFAULT_SLOT_HEIGHT = 3.5f;

    /**�� ���� **/
    private int chickenCnt;

    [System.Serializable]
    public struct SPITE_IMG
    {
        //������Ʈ ��������Ʈ �̹���
        public Sprite normalSprite;
        public Sprite canUseSprite;
    }
    [SerializeField] private SPITE_IMG sprite;
    [SerializeField] private Image          image;
    [SerializeField] private FlourSlot[]    flourSlots;
    [SerializeField] private TutoObj        tutoObj;

    public void OnMouseEnter()
    {
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if (kitchenMgr.dragState == DragState.Egg && chickenCnt < MAX_CHICKEN_SLOT)
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
        kitchenMgr.trayFlour = this;
        kitchenMgr.mouseArea = DragArea.Tray_Flour;
    }

    public void OnMouseExit()
    {
        image.sprite = sprite.normalSprite;

        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        kitchenMgr.trayFlour = null;
        kitchenMgr.mouseArea = DragArea.None;
    }

    public bool AddChicken()
    {
        if (IsMax())
            return false;

        //Ʈ���̿� �÷����ִ� �� ����
        chickenCnt++;
        soundMgr.PlaySE(Sound.Put_SE);

        image.sprite = sprite.normalSprite;

        foreach (FlourSlot flourSlot in flourSlots)
        {
            if (flourSlot.isEmpty)
            {
                //�󽽷Կ� �ش��ϴ� ���� ���� �ִ´�.
                flourSlot.gameObject.SetActive(true);
                flourSlot.isEmpty = false;
                flourSlot.SpawnChicken();
                RefreshSlotCollider();

                if (IsMax())
                {
                    if (tutoMgr.tutoComplete == false)
                    {
                        //Ʃ�丮���� ������ѵ�?
                        //Ʃ�丮��� ����
                        tutoObj.PlayTuto();
                    }
                }

                KitchenMgr kitchenMgr = KitchenMgr.Instance;
                kitchenMgr.UpdateWorkerAct();

                return true;
            }
        }

        return false;
    }

    public bool IsMax() => (chickenCnt == MAX_CHICKEN_SLOT);

    public bool HasChicken()
    {
        int cnt = 0;
        foreach (FlourSlot flourSlot in flourSlots)
        {
            if (flourSlot.isEmpty)
                continue;
            if (flourSlot.isDrag)
                continue;
            cnt++;
        }
        return cnt > 0;
    }

        public bool RemoveChicken()
    {
        //Ʈ���̿� �÷����ִ� �� ����
        if (chickenCnt <= 0)
            return false;
        chickenCnt--;

        RefreshSlotCollider();

        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        kitchenMgr.UpdateWorkerAct();

        return true;
    }

    public void RemoveFlourSlotbyWorker()
    {
        foreach (FlourSlot flourSlot in flourSlots)
        {
            if (flourSlot.isEmpty)
                continue;
            if (flourSlot.isDrag)
                continue;
            flourSlot.RemoveChicken();
            return;
        }
    }

    public void RefreshSlotCollider()
    {
        //���� �ݶ��̴� �����ϴ� �κ���
        //Ʈ���� ��ü���� �巡�� �����Ҽ��ֵ��� ������

        bool frontCheck = false;
        for (int i = 0; i < 4; i++)
        {
            if (flourSlots[i].isEmpty)
                continue;
            float headValue = 0;
            float tailValue = 0;
            if (frontCheck == false)
            {
                frontCheck = true;

                for (int j = 0; j < i - 1; j++)
                {
                    if (flourSlots[j].isEmpty == false)
                        break;
                    headValue++;
                }
            }

            for (int j = i + 1; j < MAX_CHICKEN_SLOT; j++)
            {
                if (flourSlots[j].isEmpty == false)
                    break;
                tailValue++;
            }

            Vector2 newPos = new Vector2(0, (headValue * (headValue + 1) / 2 - tailValue * (tailValue + 1) / 2) / (headValue + 1 + tailValue)) * DEFAULT_SLOT_HEIGHT;
            Vector2 newSize = new Vector2(DEFAULT_SLOT_WIDTH, (headValue + 1 + tailValue) * DEFAULT_SLOT_HEIGHT);
            flourSlots[i].SetRect(newPos, newSize);
        }
    }
}