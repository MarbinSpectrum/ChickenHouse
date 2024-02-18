using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrayFlour : Mgr
{
    private const int MAX_CHICKEN_SLOT = 4;

    /**�� ���� **/
    private int chickenCnt;

    [System.Serializable]
    public struct SPITE_IMG
    {
        //������Ʈ ��������Ʈ �̹���
        public SpriteRenderer spriteImg;
        public Sprite normalSprite;
        public Sprite canUseSprite;
    }
    [SerializeField] private SPITE_IMG sprite;

    [SerializeField] private FlourSlot[] flourSlots;

    private void Update()
    {
        UpdateChickenTray();
    }

    private void UpdateChickenTray()
    {
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if (kitchenMgr.mouseArea == DragArea.Tray_Flour &&
            (kitchenMgr.trayFlour != null && kitchenMgr.trayFlour == this))
        {
            if (kitchenMgr.dragState == DragState.Egg &&
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
        if (chickenCnt >= MAX_CHICKEN_SLOT)
            return false;

        //Ʈ���̿� �÷����ִ� �� ����
        chickenCnt++;
        soundMgr.PlaySE(Sound.Put_SE);

        foreach (FlourSlot flourSlot in flourSlots)
        {
            if (flourSlot.isEmpty)
            {
                //�󽽷Կ� �ش��ϴ� ���� ���� �ִ´�.
                flourSlot.isEmpty = false;
                flourSlot.SpawnChicken();
                RefreshSlotCollider();
                return true;
            }
        }

        return false;
    }

    public bool RemoveChicken()
    {
        //Ʈ���̿� �÷����ִ� �� ����
        if (chickenCnt <= 0)
            return false;
        RefreshSlotCollider();
        chickenCnt--;
        return true;
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

            Vector2 newOffset = new Vector2(0,
                (headValue * (headValue + 1) / 2 - tailValue * (tailValue + 1) / 2) / (headValue + 1 + tailValue)) * 0.35f;
            Vector2 newSize = new Vector2(1, (headValue + 1 + tailValue) * 0.37f);
            flourSlots[i].collider.offset = newOffset;
            flourSlots[i].collider.size = newSize;
        }
    }
}