using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TableDrinkSlot : Mgr
{
    /** ���ᰡ �÷������� ���� **/
    public bool hasDrink { get; private set; }

    [SerializeField] private TablePickleSlot    pickleSlot;
    [SerializeField] private Image              drinkImg;
    [SerializeField] private GameObject         slotUI;
    [SerializeField] private TutoObj            tutoObj;
    [SerializeField] private ScrollObj          scrollObj;

    public void OnMouseEnter()
    {
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        kitchenMgr.mouseArea = DragArea.SideMenu_Slot;
        kitchenMgr.drinkSlot = this;

        if (hasDrink)
        {
            //���ᰡ �̹� ��������
            return;
        }


        if (kitchenMgr.dragState == DragState.Cola)
        {
            //���Ḧ �������ִ� �����̱��ϴ�.
            DrinkData drinkData = subMenuMgr.GetDrinkData(Drink.Cola);
            drinkImg.sprite = drinkData.img;
            drinkImg.color = new Color(1, 1, 1, 0.5f);
            slotUI.gameObject.SetActive(true);
        }
        else if (kitchenMgr.dragState == DragState.Beer)
        {
            //���Ḧ �������ִ� �����̱��ϴ�.
            DrinkData drinkData = subMenuMgr.GetDrinkData(Drink.Beer);
            drinkImg.sprite = drinkData.img;
            drinkImg.color = new Color(1, 1, 1, 0.5f);
            slotUI.gameObject.SetActive(true);
        }
    }

    public void OnMouseExit()
    {
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        kitchenMgr.mouseArea = DragArea.None;
        kitchenMgr.drinkSlot = null;

        if (hasDrink)
        {
            //���ᰡ �̹� ��������
            return;
        }
        drinkImg.color = new Color(0, 0, 0, 0);
        slotUI.gameObject.SetActive(true);
    }

    public void OnMouseDown()
    {
        if (tutoMgr.tutoComplete1 == false)
        {
            //Ʃ�丮�󿡼��� ���Ű� ��������
            return;
        }

        //�÷����ִ� ���Ḧ �����Ҷ� ���
        if (hasDrink == false)
            return;

        drinkImg.color = new Color(0, 0, 0, 0);
        slotUI.gameObject.SetActive(true);

        hasDrink = false;

        scrollObj.isRun = true;
    }

    public bool Put_Drink()
    {
        if (hasDrink)
        {
            //�̹� ���ᰡ ����
            return false;
        }

        soundMgr.PlaySE(Sound.Put_SE);

        if (tutoMgr.tutoComplete1 == false && tutoMgr.nowTuto == Tutorial.Tuto_10 && pickleSlot.hasPickle)
        {
            tutoObj.PlayTuto();

            KitchenMgr kitchenMgr = KitchenMgr.Instance;
            //kitchenMgr.ui.goCounter.OpenBtn();
        }

        drinkImg.color = new Color(1, 1, 1, 1);
        slotUI.gameObject.SetActive(false);

        hasDrink = true;

        scrollObj.isRun = false;

        return true;
    }

    public void Init()
    {
        drinkImg.color = new Color(0, 0, 0, 0);
        slotUI.gameObject.SetActive(true);

        hasDrink = false;

        scrollObj.isRun = true;
    }
}
