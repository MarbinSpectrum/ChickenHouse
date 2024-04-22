using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrayEgg : Mgr
{
    private const int MAX_CHICKEN_SLOT = 4;
    private const float DEFAULT_SLOT_WIDTH = 9.5f;
    private const float DEFAULT_SLOT_HEIGHT = 3.5f;

    /**닭 갯수 **/
    private int chickenCnt;

    [System.Serializable]
    public struct SPITE_IMG
    {
        //오브젝트 스프라이트 이미지
        public Sprite           normalSprite;
        public Sprite           canUseSprite;
    }
    [SerializeField] private SPITE_IMG  sprite;
    [SerializeField] private Image      image;
    [SerializeField] private EggSlot[]  eggSlots;
    [SerializeField] private TutoObj    tutoObj;

    public void OnMouseEnter()
    {
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if (kitchenMgr.dragState == DragState.Normal && chickenCnt < MAX_CHICKEN_SLOT)
        {
            //치킨을 드래그해서 가져왔으며
            //슬롯이 비어져있다.
            image.sprite = sprite.canUseSprite;
        }
        else
        {
            //나머지 상태면 이미지가 보이지 않는다.
            image.sprite = sprite.normalSprite;
        }
        kitchenMgr.trayEgg = this;
        kitchenMgr.mouseArea = DragArea.Tray_Egg;
    }

    public void OnMouseExit()
    {
        image.sprite = sprite.normalSprite;

        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        kitchenMgr.trayEgg = null;
        kitchenMgr.mouseArea = DragArea.None;
    }

    public bool AddChicken()
    {
        if (IsMax())
            return false;

        //트레이에 올려져있는 닭 증가
        chickenCnt++;
        soundMgr.PlaySE(Sound.Put_SE);

        image.sprite = sprite.normalSprite;

        foreach (EggSlot eggSlot in eggSlots)
        {
            if(eggSlot.isEmpty)
            {
                //빈슬롯에 해당하는 곳에 닭을 넣는다.
                eggSlot.gameObject.SetActive(true);
                eggSlot.isEmpty = false;
                eggSlot.SpawnChicken();
                RefreshSlotCollider();

                if (IsMax())
                {
                    if (tutoMgr.tutoComplete == false)
                    {
                        //튜토리얼을 진행안한듯?
                        //튜토리얼로 진입
                        tutoObj.PlayTuto();
                    }
                }

                KitchenMgr kitchenMgr = KitchenMgr.Instance;
                kitchenMgr.worker.UpdateHandMoveArea();

                return true;
            }
        }

        return false;
    }

    public bool IsMax() => (chickenCnt == MAX_CHICKEN_SLOT);

    public bool HasChicken()
    {
        int cnt = 0;
        foreach (EggSlot eggSlot in eggSlots)
        {
            if (eggSlot.isEmpty)
                continue;
            if (eggSlot.isDrag)
                continue;
            cnt++;
        }
        return cnt > 0;
    }

    public bool RemoveChicken()
    {
        //트레이에 올려져있는 닭 감소
        if (chickenCnt <= 0)
            return false;
        chickenCnt--;

        RefreshSlotCollider();

        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        kitchenMgr.worker.UpdateHandMoveArea();

        return true;
    }

    public void RemoveEggSlotbyWorker()
    {
        foreach (EggSlot eggSlot in eggSlots)
        {
            if (eggSlot.isEmpty)
                continue;
            if (eggSlot.isDrag)
                continue;
            eggSlot.RemoveChicken();
            return;
        }
    }

    public void RefreshSlotCollider()
    {
        //슬롯 콜라이더 수정하는 부분임
        //트레이 전체에서 드래그 가능할수있도록 수정함

        bool frontCheck = false;
        for (int i = 0; i < 4; i++)
        {
            if (eggSlots[i].isEmpty)
                continue;
            float headValue = 0;
            float tailValue = 0;
            if (frontCheck == false)
            {
                frontCheck = true;

                for (int j = 0; j < i - 1; j++)
                {
                    if (eggSlots[j].isEmpty == false)
                        break;
                    headValue++;
                }
            }

            for (int j = i + 1; j < MAX_CHICKEN_SLOT; j++)
            {
                if (eggSlots[j].isEmpty == false)
                    break;
                tailValue++;
            }

            Vector2 newPos = new Vector2(0, (headValue * (headValue + 1) / 2 - tailValue * (tailValue + 1) / 2) / (headValue + 1 + tailValue)) * DEFAULT_SLOT_HEIGHT;
            Vector2 newSize = new Vector2(DEFAULT_SLOT_WIDTH, (headValue + 1 + tailValue) * DEFAULT_SLOT_HEIGHT);
            eggSlots[i].SetRect(newPos, newSize);
        }
    }
}
