using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObj : Mgr
{
    /** ġŲ **/
    [System.Serializable]
    public struct CHICKEN_SPRITE
    {
        public Sprite normal;
        public Sprite egg;
        public Sprite flour;
    }
    [SerializeField] private CHICKEN_SPRITE chickenSprite;
    [SerializeField] private SpriteRenderer chickenImg;

    /** ġŲ �ҽ� **/
    [SerializeField] private GameObject     chickenSource;

    /** ġŲ �� **/
    [SerializeField] private GameObject     chickenRadish;

    /** ġŲ ���� **/
    [SerializeField] private GameObject         strainterObj;
    [SerializeField] private Chicken_Shader[]   chickenObj;



    private void Update()
    {
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        switch (kitchenMgr.dragState)
        {
            case DragState.Normal:
                {
                    //ġŲ�� �巡���� ����
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
                    //����� ���� ġŲ�� �巡���� ����
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
                    //�ж󱸸� ���� ġŲ�� �巡���� ����
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
                    //ġŲ ������ �巡���� ����
                    chickenImg.gameObject.SetActive(false);
                    strainterObj.gameObject.SetActive(true);
                    chickenSource.gameObject.SetActive(false);
                    chickenRadish.gameObject.SetActive(false);

                    MoveMousePos();
                }
                return;
            case DragState.Chicken_Source:
                {
                    //ġŲ ����� �巡���� ����
                    chickenImg.gameObject.SetActive(false);
                    strainterObj.gameObject.SetActive(false);
                    chickenSource.gameObject.SetActive(true);
                    chickenRadish.gameObject.SetActive(false);

                    MoveMousePos();
                }
                return;
            case DragState.Chicken_Radish:
                {
                    //ġŲ ���� �巡���� ����
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
            //ġŲ ������ŭ�� ġŲ�� ��������.
            bool actChicken = (i < pChickenCnt);
            chickenObj[i].gameObject.SetActive(actChicken);
            chickenObj[i].Set_Shader(mode, lerpValue);
        }

        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        kitchenMgr.dragState = pDragState;
    }
}
