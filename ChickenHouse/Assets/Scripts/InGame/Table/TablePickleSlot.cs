using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TablePickleSlot : Mgr
{
    /** ��Ŭ�� �÷������� ���� **/
    public bool hasPickle { get; private set; }

    [SerializeField] private TableDrinkSlot drinkSlot;
    [SerializeField] private Image          pickleImg;
    [SerializeField] private GameObject     slotUI;
    [SerializeField] private TutoObj        tutoObj;
    [SerializeField] private ScrollObj      scrollObj;

    public void OnMouseEnter()
    {
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        kitchenMgr.mouseArea = DragArea.Pickle_Slot;
        kitchenMgr.pickleSlot = this;

        if (hasPickle)
        {
            //���ᰡ �̹� ��������
            return;
        }
        if (kitchenMgr.dragState == DragState.Chicken_Pickle)
        {
            //���Ḧ �������ִ� �����̱��ϴ�.
            pickleImg.color = new Color(1, 1, 1, 0.5f);
            slotUI.gameObject.SetActive(true);
        }
    }

    public void OnMouseExit()
    {
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        kitchenMgr.mouseArea = DragArea.None;
        kitchenMgr.pickleSlot = null;

        if (hasPickle)
        {
            //���ᰡ �̹� ��������
            return;
        }
        pickleImg.color = new Color(0, 0, 0, 0);
        slotUI.gameObject.SetActive(true);
    }

    public void OnMouseDown()
    {
        if (tutoMgr.tutoComplete == false)
        {
            //Ʃ�丮�󿡼��� ���Ű� ��������
            return;
        }

        //�÷����ִ� ��Ŭ�� �����Ҷ� ���
        if (hasPickle == false)
            return;

        pickleImg.color = new Color(0, 0, 0, 0);
        slotUI.gameObject.SetActive(true);

        hasPickle = false;

        scrollObj.isRun = true;
    }

    public bool Put_Pickle()
    {
        if (hasPickle)
        {
            //�̹� ġŲ���� ����
            return false;
        }

        soundMgr.PlaySE(Sound.Put_SE);

        if (tutoMgr.tutoComplete == false && tutoMgr.nowTuto == Tutorial.Tuto_6 && drinkSlot.hasDrink)
        {
            tutoObj.PlayTuto();

            KitchenMgr kitchenMgr = KitchenMgr.Instance;
            //kitchenMgr.ui.goCounter.OpenBtn();
        }

        pickleImg.color = new Color(1, 1, 1, 1);
        slotUI.gameObject.SetActive(false);

        hasPickle = true;

        scrollObj.isRun = false;

        return true;
    }

    public void Init()
    {
        pickleImg.color = new Color(0, 0, 0, 0);
        slotUI.gameObject.SetActive(true);

        hasPickle = false;

        scrollObj.isRun = true;
    }
}
