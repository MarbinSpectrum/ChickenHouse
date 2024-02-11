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
        public SpriteRenderer   spriteImg;
        public Sprite           normalSprite;
        public Sprite           canUseSprite;
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
                break;
            }
        }

        return true;
    }

    public bool RemoveChicken()
    {
        //Ʈ���̿� �÷����ִ� �� ����
        if (chickenCnt <= 0)
            return false;
        chickenCnt--;
        return true;
    }
}