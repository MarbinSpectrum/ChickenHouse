using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrayFlour : Mgr
{
    private const int MAX_CHICKEN_SLOT = 4;

    /**닭 갯수 **/
    private int chickenCnt;

    [System.Serializable]
    public struct SPITE_IMG
    {
        //오브젝트 스프라이트 이미지
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
                //치킨을 드래그해서 가져왔으며
                //슬롯이 비어져있다.
                sprite.spriteImg.sprite = sprite.canUseSprite;
            }
            else
            {
                //나머지 상태면 이미지가 보이지 않는다.
                sprite.spriteImg.sprite = sprite.normalSprite;
            }
        }
        else
        {
            //마우스를 밖으로 내보내면 이펙트 비활성화
            sprite.spriteImg.sprite = sprite.normalSprite;
        }
    }

    public bool AddChicken()
    {
        if (chickenCnt >= MAX_CHICKEN_SLOT)
            return false;

        //트레이에 올려져있는 닭 증가
        chickenCnt++;
        soundMgr.PlaySE(Sound.Put_SE);

        foreach (FlourSlot flourSlot in flourSlots)
        {
            if (flourSlot.isEmpty)
            {
                //빈슬롯에 해당하는 곳에 닭을 넣는다.
                flourSlot.isEmpty = false;
                flourSlot.SpawnChicken();
                break;
            }
        }

        return true;
    }

    public bool RemoveChicken()
    {
        //트레이에 올려져있는 닭 감소
        if (chickenCnt <= 0)
            return false;
        chickenCnt--;
        return true;
    }
}