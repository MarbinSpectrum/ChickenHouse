using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableDrinkSlot : Mgr
{
    /** ���ᰡ �÷������� ���� **/
    private bool hasDrink;

    [SerializeField] private SpriteRenderer drinkImg;
    [SerializeField] private GameObject     slotUI;

    private void Update()
    {
        UpdateDrinkSlot();
    }

    private void UpdateDrinkSlot()
    {
        if (hasDrink)
        {
            //���ᰡ �̹� ��������
            drinkImg.color = new Color(1, 1, 1, 1);
            slotUI.gameObject.SetActive(false);
            return;
        }
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if (kitchenMgr.dragState == DragState.Cola)
        {
            //���Ḧ �������ִ� �����̱��ϴ�.
            drinkImg.color = new Color(1, 1, 1, 0.5f);
            slotUI.gameObject.SetActive(true);
        }
        else
        {
            //���Ḧ ������ �������� ����Ʈ ��Ȱ��ȭ
            drinkImg.color = new Color(0, 0, 0, 0);
            slotUI.gameObject.SetActive(true);
        }
    }

    public bool Put_Drinkt()
    {
        if (hasDrink)
        {
            //�̹� ���ᰡ ����
            return false;
        }
        hasDrink = true;
        return true;
    }
}
