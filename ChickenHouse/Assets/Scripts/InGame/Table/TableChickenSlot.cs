using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableChickenSlot : Mgr
{
    /** ����ִ� ġŲ ���� **/
    private int chickenCnt;
    /** ġŲ ���� **/
    private ChickenState chickenState;

    /** �ҽ�0 **/
    private ChickenSpicy source0;
    /** �ҽ�1 **/
    private ChickenSpicy source1;

    [SerializeField] private SpriteRenderer boxImg;
    [SerializeField] private GameObject     slotUI;

    private void Update()
    {
        UpdateChickenSlot();
    }

    private void UpdateChickenSlot()
    {
        if (chickenCnt > 0)
        {
            //ġŲ�� �̹� ��������
            boxImg.color = new Color(1, 1, 1, 1);
            slotUI.gameObject.SetActive(false);
            return;
        }
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if (kitchenMgr.dragState == DragState.Chicken_Pack)
        {
            //ġŲ�� �������ִ� �����̱��ϴ�.
            boxImg.color = new Color(1, 1, 1, 0.5f);
            slotUI.gameObject.SetActive(true);
        }
        else
        {
            //���콺�� ������ �������� ����Ʈ ��Ȱ��ȭ
            boxImg.color = new Color(0, 0, 0, 0);
            slotUI.gameObject.SetActive(true);
        }
    }

    public bool Put_ChickenPack(int pChickenCnt, ChickenState pChickenState, ChickenSpicy pSource0, ChickenSpicy pSource1)
    {
        if (chickenCnt > 0)
        {
            //�̹� ġŲ�� ����
            return false;
        }

        chickenCnt = pChickenCnt;
        chickenState = pChickenState;
        source0 = pSource0;
        source1 = pSource1;

        return true;
    }
}
