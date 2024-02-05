using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TablePickleSlot : Mgr
{
    /** ��Ŭ�� �÷������� ���� **/
    private bool hasPickle;

    [SerializeField] private SpriteRenderer pickleImg;
    [SerializeField] private GameObject     slotUI;

    private void OnMouseDown()
    {
        //�÷����ִ� ��Ŭ�� �����Ҷ� ���
        if (hasPickle == false)
            return;
        hasPickle = false;
    }

    private void Update()
    {
        UpdatePickleSlot();
    }

    private void UpdatePickleSlot()
    {
        if (hasPickle)
        {
            //ġŲ���� �̹� ��������
            pickleImg.color = new Color(1, 1, 1, 1);
            slotUI.gameObject.SetActive(false);
            return;
        }
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if (kitchenMgr.dragState == DragState.Chicken_Pickle)
        {
            //ġŲ���� �������ִ� �����̱��ϴ�.
            pickleImg.color = new Color(1, 1, 1, 0.5f);
            slotUI.gameObject.SetActive(true);
        }
        else
        {
            //ġŲ���� ������ �������� ����Ʈ ��Ȱ��ȭ
            pickleImg.color = new Color(0, 0, 0, 0);
            slotUI.gameObject.SetActive(true);
        }
    }

    public bool Put_Pickle()
    {
        if (hasPickle)
        {
            //�̹� ġŲ���� ����
            return false;
        }
        hasPickle = true;
        return true;
    }
}
