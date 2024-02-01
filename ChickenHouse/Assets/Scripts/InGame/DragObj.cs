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

    /** ġŲ �ڽ� */
    [SerializeField] private GameObject         chickenBox;

    /** �ݶ� **/
    [SerializeField] private GameObject         cola;

    private void Update()
    {
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        All_Disable();

        switch (kitchenMgr.dragState)
        {
            case DragState.Normal:
                {
                    //ġŲ�� �巡���� ����
                    chickenImg.sprite = chickenSprite.normal;
                    chickenImg.gameObject.SetActive(true);

                    MoveMousePos();
                }
                return;
            case DragState.Egg:
                {
                    //����� ���� ġŲ�� �巡���� ����
                    chickenImg.sprite = chickenSprite.egg;
                    chickenImg.gameObject.SetActive(true);

                    MoveMousePos();
                }
                return;
            case DragState.Flour:
                {
                    //�ж󱸸� ���� ġŲ�� �巡���� ����
                    chickenImg.sprite = chickenSprite.flour;
                    chickenImg.gameObject.SetActive(true);

                    MoveMousePos();
                }
                return;
            case DragState.Fry_Chicken:
            case DragState.Chicken_Strainter:
                {
                    //ġŲ ������ �巡���� ����
                    strainterObj.gameObject.SetActive(true);

                    MoveMousePos();
                }
                return;
            case DragState.Hot_Spicy:
                {
                    //ġŲ ����� �巡���� ����
                    chickenSource.gameObject.SetActive(true);

                    MoveMousePos();
                }
                return;
            case DragState.Chicken_Pickle:
                {
                    //ġŲ ���� �巡���� ����
                    chickenRadish.gameObject.SetActive(true);

                    MoveMousePos();
                }
                return;
            case DragState.Chicken_Pack:
                {
                    //ġŲ �ڽ��� �巡���� ����
                    chickenBox.gameObject.SetActive(true);

                    MoveMousePos();
                }
                return;
            case DragState.Cola:
                {
                    //���Ḧ �巡���� ����
                    cola.gameObject.SetActive(true);

                    MoveMousePos();
                }
                return;
        }
    }

    private void All_Disable()
    {
        chickenImg.gameObject.SetActive(false);
        strainterObj.gameObject.SetActive(false);
        chickenSource.gameObject.SetActive(false);
        chickenRadish.gameObject.SetActive(false);
        chickenBox.gameObject.SetActive(false);
        cola.gameObject.SetActive(false);
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
