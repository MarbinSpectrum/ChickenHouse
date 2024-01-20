using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragChickenStrainter : Mgr
{
    [System.Serializable]
    public struct SPITE_IMG
    {
        public Sprite badChicken_0; //덜 익힌 치킨
        public Sprite badChicken_1; //조금 태운 치킨
        public Sprite badChicken_2; //태운 치킨
        public Sprite goodChicken;  //잘 튀긴 치킨
    }
    [SerializeField] private SPITE_IMG spriteImg;

    [SerializeField] private GameObject         strainterObj;
    [SerializeField] private SpriteRenderer[]   chickenObj;

    private void Update()
    {
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        switch (kitchenMgr.dragState)
        {
            case DragState.FryChicken:
            case DragState.ChickenStrainter:
                {
                    //해당 상태에서만 치킨이 드래그됨

                    //이미지 활성화
                    strainterObj.gameObject.SetActive(true);

                    //치킨이미지를 드래그 포인트로 이동
                    Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    transform.position = new Vector3(pos.x, pos.y, 0);
                }
                return;
            default:
                {
                    strainterObj.gameObject.SetActive(false);
                }
                break;
        }
    }

    public void DragStart(int pChickenCnt, ChickenState pChickenState, DragState pDragState)
    {
        Sprite chickenSprite = null;
        switch (pChickenState)
        {
            case ChickenState.NotCook:
            case ChickenState.BadChicken_0:
                //조리안하거나 덜익은 경우
                chickenSprite = spriteImg.badChicken_0;
                break;
            case ChickenState.BadChicken_1:
                //조금 태운 치킨
                chickenSprite = spriteImg.badChicken_1;
                break;
            case ChickenState.BadChicken_2:
                //태운 치킨
                chickenSprite = spriteImg.badChicken_2;
                break;
            case ChickenState.GoodChicken:
                //잘튀긴 치킨
                chickenSprite = spriteImg.goodChicken;
                break;
        }
        for (int i = 0; i < chickenObj.Length; i++)
        {
            //치킨 갯수만큼만 치킨을 보여주자.
            bool actChicken = (i < pChickenCnt);
            chickenObj[i].gameObject.SetActive(actChicken);
            chickenObj[i].sprite = chickenSprite;
        }


        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        kitchenMgr.dragState = pDragState;
    }
}
