using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BowlEgg : Mgr
{
    private const int MAX_CHICKEN_SLOT = 1;
    /**닭 갯수 **/
    private int chickenCnt;

    [System.Serializable]
    public struct SPITE_IMG
    {
        //오브젝트 스프라이트 이미지
        public Sprite normalSprite;
        public Sprite canUseSprite;
    }
    [SerializeField] private SPITE_IMG          sprite;
    [SerializeField] private Image              image;
    //Tuto_2
    [SerializeField] private TutoObj            tutoObj;
    [SerializeField] private BowlChicken        bowlChicken;

    public bool isDrag => bowlChicken.isDrag;

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
        kitchenMgr.bowlEgg = this;
        kitchenMgr.mouseArea = DragArea.Bowl_Egg;
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

        bowlChicken.Init();
        bowlChicken.gameObject.SetActive(true);

        if (IsMax())
        {
            if (tutoMgr.tutoComplete == false)
            {
                //튜토리얼을 진행안한듯?
                //튜토리얼로 진입
                tutoObj.PlayTuto();
            }
        }

        return true;
    }

    public void WorkerEggChickenPutAway()
    {
        bowlChicken.WorkerEggChickenPutAway();
    }

    public bool IsMax() => (chickenCnt == MAX_CHICKEN_SLOT);

    public bool RemoveChicken()
    {
        //트레이에 올려져있는 닭 감소
        if (chickenCnt <= 0)
            return false;
        chickenCnt--;

        bowlChicken.gameObject.SetActive(false);
        bowlChicken.Init();

        return true;
    }

    public bool CompleteEgg()
    {
        //계란물 묻히기 작업이 완료됬는지 여부를 반환
        return bowlChicken.CompleteEgg();
    }

    public void WorkerDragChicken(float v)
    {
        bowlChicken.WorkerDragChicken(v);
    }
}
