using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragChickenStrainter : Mgr
{
    [System.Serializable]
    public struct SPITE_IMG
    {
        public Sprite badChicken_0; //�� ���� ġŲ
        public Sprite badChicken_1; //���� �¿� ġŲ
        public Sprite badChicken_2; //�¿� ġŲ
        public Sprite goodChicken;  //�� Ƣ�� ġŲ
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
                    //�ش� ���¿����� ġŲ�� �巡�׵�

                    //�̹��� Ȱ��ȭ
                    strainterObj.gameObject.SetActive(true);

                    //ġŲ�̹����� �巡�� ����Ʈ�� �̵�
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
                //�������ϰų� ������ ���
                chickenSprite = spriteImg.badChicken_0;
                break;
            case ChickenState.BadChicken_1:
                //���� �¿� ġŲ
                chickenSprite = spriteImg.badChicken_1;
                break;
            case ChickenState.BadChicken_2:
                //�¿� ġŲ
                chickenSprite = spriteImg.badChicken_2;
                break;
            case ChickenState.GoodChicken:
                //��Ƣ�� ġŲ
                chickenSprite = spriteImg.goodChicken;
                break;
        }
        for (int i = 0; i < chickenObj.Length; i++)
        {
            //ġŲ ������ŭ�� ġŲ�� ��������.
            bool actChicken = (i < pChickenCnt);
            chickenObj[i].gameObject.SetActive(actChicken);
            chickenObj[i].sprite = chickenSprite;
        }


        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        kitchenMgr.dragState = pDragState;
    }
}
