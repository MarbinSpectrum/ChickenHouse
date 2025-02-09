using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TableDrinkSlot : Mgr
{
    /** ���ᰡ �÷������� ���� **/
    public Drink drink { get; private set; }

    [SerializeField] private TableSideMenuSlot    pickleSlot;
    [SerializeField] private Image              drinkImg;
    [SerializeField] private GameObject         slotUI;
    [SerializeField] private TutoObj            tutoObj;
    [SerializeField] private ScrollObj          scrollObj;

    public void OnMouseEnter()
    {
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        kitchenMgr.mouseArea = DragArea.SideMenu_Slot;
        kitchenMgr.drinkSlot = this;

        if (drink != Drink.None)
        {
            //���ᰡ �̹� ��������
            return;
        }

        Drink dragDrink = subMenuMgr.GetDragStateToDrink(kitchenMgr.dragState);

        //���Ḧ �������ִ� �����̱��ϴ�.
        DrinkData drinkData = subMenuMgr.GetDrinkData(dragDrink);
        if (drinkData == null)
            return;

        drinkImg.sprite = drinkData.img;
        drinkImg.color = new Color(1, 1, 1, 0.5f);
        slotUI.gameObject.SetActive(true);
    }

    public void OnMouseExit()
    {
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        kitchenMgr.mouseArea = DragArea.None;
        kitchenMgr.drinkSlot = null;

        if (drink != Drink.None)
        {
            //���ᰡ �̹� ��������
            return;
        }
        drinkImg.color = new Color(0, 0, 0, 0);
        slotUI.gameObject.SetActive(true);
    }

    public void OnMouseDown()
    {
        if (gameMgr.playData.tutoComplete1 == false)
        {
            //Ʃ�丮�󿡼��� ���Ű� ��������
            return;
        }

        //�÷����ִ� ���Ḧ �����Ҷ� ���
        if (drink == Drink.None)
            return;

        drinkImg.color = new Color(0, 0, 0, 0);
        slotUI.gameObject.SetActive(true);

        drink = Drink.None;

        scrollObj.isRun = true;
    }

    public bool Put_Drink(Drink eDrink)
    {
        if (drink != Drink.None)
        {
            //�̹� ���ᰡ ����
            return false;
        }

        soundMgr.PlaySE(Sound.Put_SE);

        if (gameMgr.playData.tutoComplete1 == false && tutoMgr.nowTuto == Tutorial.Tuto_10 && pickleSlot.pickle != SideMenu.None)
        {
            tutoObj.PlayTuto();

            KitchenMgr kitchenMgr = KitchenMgr.Instance;
            //kitchenMgr.ui.goCounter.OpenBtn();
        }

        drinkImg.color = new Color(1, 1, 1, 1);
        slotUI.gameObject.SetActive(false);

        drink = eDrink;

        scrollObj.isRun = false;

        return true;
    }

    public void Init()
    {
        drinkImg.color = new Color(0, 0, 0, 0);
        slotUI.gameObject.SetActive(true);

        drink = Drink.None;

        scrollObj.isRun = true;
    }
}
