using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenPack : MonoBehaviour
{
    private int     chickenCnt;
    private bool    isRun;

    [System.Serializable]
    public struct SPITE_IMG
    {
        //������Ʈ ��������Ʈ �̹���
        public SpriteRenderer   spriteImg;
        public Sprite           normalSprite;
        public Sprite           canUseSprite;
    }
    [SerializeField] private SPITE_IMG sprite;

    private void Update()
    {
        UpdateChickenPack();
    }

    private void UpdateChickenPack()
    {
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if (kitchenMgr.mouseArea == DragArea.Chicken_Pack &&
            (kitchenMgr.chickenPack != null && kitchenMgr.chickenPack == this))
        {
            if (kitchenMgr.dragState == DragState.FryChicken && isRun)
            {
                //ġŲ ����ڽ��� ��� ���̴�.
                sprite.spriteImg.sprite = sprite.normalSprite;
            }
            else
            {
                //�ش� ��⸦ ��� �����ϴ�.
                sprite.spriteImg.sprite = sprite.canUseSprite;
            }
        }
        else
        {
            //���콺�� ������ �������� ����Ʈ ��Ȱ��ȭ
            sprite.spriteImg.sprite = sprite.normalSprite;
        }
    }
}
