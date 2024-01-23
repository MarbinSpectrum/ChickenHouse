using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObj : Mgr
{
    /** 치킨 **/
    [System.Serializable]
    public struct CHICKEN_SPRITE
    {
        public Sprite normal;
        public Sprite egg;
        public Sprite flour;
    }
    [SerializeField] private CHICKEN_SPRITE chickenSprite;
    [SerializeField] private SpriteRenderer chickenImg;

    /** 치킨 소스 **/
    [SerializeField] private GameObject     chickenSource;

    /** 치킨 무 **/
    [SerializeField] private GameObject     chickenRadish;

    /** 치킨 건지 **/
    [SerializeField] private GameObject         strainterObj;
    [SerializeField] private Chicken_Shader[]   chickenObj;



    private void Update()
    {
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        switch (kitchenMgr.dragState)
        {
            case DragState.Normal:
                {
                    //치킨을 드래그한 상태
                    chickenImg.sprite = chickenSprite.normal;
                    chickenImg.gameObject.SetActive(true);
                    strainterObj.gameObject.SetActive(false);
                    chickenSource.gameObject.SetActive(false);
                    chickenRadish.gameObject.SetActive(false);

                    MoveMousePos();
                }
                return;
            case DragState.Egg:
                {
                    //계란물 묻힌 치킨을 드래그한 상태
                    chickenImg.sprite = chickenSprite.egg;
                    chickenImg.gameObject.SetActive(true);
                    strainterObj.gameObject.SetActive(false);
                    chickenSource.gameObject.SetActive(false);
                    chickenRadish.gameObject.SetActive(false);

                    MoveMousePos();
                }
                return;
            case DragState.Flour:
                {
                    //밀라구를 묻힌 치킨을 드래그한 상태
                    chickenImg.sprite = chickenSprite.flour;
                    chickenImg.gameObject.SetActive(true);
                    strainterObj.gameObject.SetActive(false);
                    chickenSource.gameObject.SetActive(false);
                    chickenRadish.gameObject.SetActive(false);

                    MoveMousePos();
                }
                return;
            case DragState.Fry_Chicken:
            case DragState.Chicken_Strainter:
                {
                    //치킨 건지를 드래그한 상태
                    chickenImg.gameObject.SetActive(false);
                    strainterObj.gameObject.SetActive(true);
                    chickenSource.gameObject.SetActive(false);
                    chickenRadish.gameObject.SetActive(false);

                    MoveMousePos();
                }
                return;
            case DragState.Chicken_Source:
                {
                    //치킨 양념을 드래그한 상태
                    chickenImg.gameObject.SetActive(false);
                    strainterObj.gameObject.SetActive(false);
                    chickenSource.gameObject.SetActive(true);
                    chickenRadish.gameObject.SetActive(false);

                    MoveMousePos();
                }
                return;
            case DragState.Chicken_Radish:
                {
                    //치킨 무를 드래그한 상태
                    chickenImg.gameObject.SetActive(false);
                    strainterObj.gameObject.SetActive(false);
                    chickenSource.gameObject.SetActive(false);
                    chickenRadish.gameObject.SetActive(true);

                    MoveMousePos();
                }
                return;
        }

        chickenImg.gameObject.SetActive(false);
        strainterObj.gameObject.SetActive(false);
        chickenSource.gameObject.SetActive(false);
        chickenRadish.gameObject.SetActive(false);
    }

    private void MoveMousePos()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(pos.x, pos.y, 0);
    }

    public void DragStrainter(int pChickenCnt, DragState pDragState)
    {
        DragStrainter(pChickenCnt, pDragState, true, 0);
    }

    public void DragStrainter(int pChickenCnt, DragState pDragState, bool mode, float lerpValue)
    {
        for (int i = 0; i < chickenObj.Length; i++)
        {
            //치킨 갯수만큼만 치킨을 보여주자.
            bool actChicken = (i < pChickenCnt);
            chickenObj[i].gameObject.SetActive(actChicken);
            chickenObj[i].Set_Shader(mode, lerpValue);
        }

        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        kitchenMgr.dragState = pDragState;
    }
}
