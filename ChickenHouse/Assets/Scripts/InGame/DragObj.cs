using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    [SerializeField] private Image chickenImg;

    /** ��� **/
    [System.Serializable]
    public struct SPICY_OBJ
    {
        public RectTransform    spicyRect;
        public Image            spicyImg;
        public TextMeshProUGUI  spicyText;
    }
    [SerializeField] private SPICY_OBJ spicyObj;

    /** ġŲ ���� **/
    [SerializeField] private GameObject         strainterObj;
    [SerializeField] private Chicken_Shader[]   chickenObj;

    /** ġŲ �ڽ� */
    [SerializeField] private GameObject         chickenBox;

    /** ���� **/
    [System.Serializable]
    public struct DRINK_OBJ
    {
        public RectTransform    drinkRect;
        public Image            drinkImg;
        public TextMeshProUGUI  drinkText;
    }
    [SerializeField] private DRINK_OBJ drinkObj;

    /** ���̵� �޴� **/
    [System.Serializable]
    public struct SIDEMENU_OBJ
    {
        public RectTransform    sideMenuRect;
        public Image            sideMenuImg;
        public TextMeshProUGUI  sideMenuText;
    }
    [SerializeField] private SIDEMENU_OBJ sideMenuObj;

    /** ġŲ **/
    [System.Serializable]
    public struct HOLD_OBJ
    {
        public List<ChickenStrainter> chickenStrainters;
        public List<Oil_Zone> oilZones;
    }
    [SerializeField] private HOLD_OBJ holdObj;
    public GameObject holdGameObj { get; private set; }

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
            case DragState.Soy_Spicy:
            case DragState.Hell_Spicy:
            case DragState.Prinkle_Spicy:
            case DragState.Carbonara_Spicy:
            case DragState.BBQ_Spicy:
                {
                    //����� �巡���� ����
                    spicyObj.spicyRect.gameObject.SetActive(true);

                    ChickenSpicy    chickenSpicy    = SpicyMgr.GetDragStateSpicy(kitchenMgr.dragState);
                    SpicyData       spicyData       = spicyMgr.GetSpicyData(chickenSpicy);
                    spicyObj.spicyImg.sprite        = spicyData.img;
                    LanguageMgr.SetString(spicyObj.spicyText, spicyData.nameKey);

                    MoveMousePos();
                }
                return;
            case DragState.Chicken_Pack:
            case DragState.Chicken_Pack_Holl:
                {
                    //ġŲ �ڽ��� �巡���� ����
                    chickenBox.gameObject.SetActive(true);

                    MoveMousePos();
                }
                return;
            case DragState.Beer:
            case DragState.Cola:
            case DragState.SuperPower:
            case DragState.LoveMelon:
            case DragState.SodaSoda:
                {
                    Drink       eDrink      = subMenuMgr.GetDragStateToDrink(kitchenMgr.dragState);
                    DrinkData   drinkData   = subMenuMgr.GetDrinkData(eDrink);

                    //���Ḧ �巡���� ����
                    drinkObj.drinkImg.sprite = drinkData.img;
                    drinkObj.drinkRect.gameObject.SetActive(true);
                    LanguageMgr.SetString(drinkObj.drinkText, drinkData.nameKey);

                    MoveMousePos();
                }
                return;
            case DragState.Chicken_Radish:
            case DragState.Pickle:
            case DragState.Coleslaw:
            case DragState.CornSalad:
            case DragState.FrenchFries:
            case DragState.ChickenNugget:
                {
                    SideMenu        eSideMenu       = SubMenuMgr.GetDragStateToSideMenu(kitchenMgr.dragState);
                    SideMenuData    sideMenuData    = subMenuMgr.GetSideMenuData(eSideMenu);

                    //���̵� ������Ʈ�� �巡���� ����
                    sideMenuObj.sideMenuImg.sprite = sideMenuData.img;
                    sideMenuObj.sideMenuRect.gameObject.SetActive(true);
                    LanguageMgr.SetString(sideMenuObj.sideMenuText, sideMenuData.nameKey);

                    MoveMousePos();
                }
                return;
        }
    }

    private void All_Disable()
    {
        chickenImg.gameObject.SetActive(false);
        strainterObj.gameObject.SetActive(false);
        chickenBox.gameObject.SetActive(false);
        spicyObj.spicyRect.gameObject.SetActive(false);
        drinkObj.drinkRect.gameObject.SetActive(false);
        sideMenuObj.sideMenuRect.gameObject.SetActive(false);
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

    public void HoldObj(GameObject hold)
    {
        holdGameObj = hold;
    }    

    public void HoldOut()
    {
        foreach (ChickenStrainter chickenStrainter in holdObj.chickenStrainters)
            chickenStrainter.HoldStrainter(false);
        foreach (Oil_Zone oil in holdObj.oilZones)
            oil.HoldStrainter(false);
    }
}
