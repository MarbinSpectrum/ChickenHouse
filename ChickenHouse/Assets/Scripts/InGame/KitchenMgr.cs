using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KitchenMgr : Mgr
{
    public static KitchenMgr Instance;

    /** �巡������ ������Ʈ **/
    [System.NonSerialized] public DragState        dragState;
    /** ġŲ �� **/
    [System.NonSerialized] public ChickenBox       chickenBox;
    /** ����� �� **/
    [System.NonSerialized] public TrayEgg          trayEgg;
    /** ����� �׸� **/
    [System.NonSerialized] public BowlEgg          bowlEgg;
    /** �а��� �� **/
    [System.NonSerialized] public TrayFlour        trayFlour;
    /** �а��� ��2 **/
    [System.NonSerialized] public TrayFlour2        trayFlour2;
    /** ġŲ ���� **/
    [System.NonSerialized] public ChickenStrainter chickenStrainter;
    /** �⸧ **/
    [System.NonSerialized] public Oil_Zone         oilZone;
    /** ġŲ ���� �ڽ� **/
    [System.NonSerialized] public ChickenPack      chickenPack;


    /** ġŲ ���� **/
    [System.NonSerialized] public TableChickenSlot chickenSlot;
    /** ġŲ �� ���� **/
    [System.NonSerialized] public TablePickleSlot  pickleSlot;
    /** ���� ���� **/
    [System.NonSerialized] public TableDrinkSlot   drinkSlot;

    /** ���콺 �������� ��ġ **/
    public DragArea mouseArea;

    /** ī�޶� ������Ʈ **/
    public InGameCamera     cameraObj;
    /** ������Ʈ �巡�� **/
    public DragObj          dragObj;
    /** �ֹ� Rect **/
    public ScrollRect       kitchenRect;
    /** �˹ٻ� **/
    [SerializeField] private Worker_Kitchen   workerKitchen;
    /** ��� **/
    [SerializeField] private List<ChickenSpicyObj>  spicys      = new List<ChickenSpicyObj>();
    /** �帵ũ **/
    [SerializeField] private List<DrinkObj>         drinks      = new List<DrinkObj>();
    /** ��Ŭ **/
    [SerializeField] private List<SideMenuObj>      sideMenus   = new List<SideMenuObj>();

    [SerializeField] private List<Oil_Zone>         oilMachines         = new List<Oil_Zone>();
    [SerializeField] private List<GameObject>       chickenPackslots    = new List<GameObject>();

    [System.Serializable]
    public struct UI
    {
        //�ֹ� ���� UI

        /** ������ ������ ��ư **/
        public TakeOut_UI   takeOut;
        /** ī���ͷ� �̵��ϱ� ��ư **/
        public GoCounter_UI goCounter;
        /** �޸� **/
        public Memo_UI      memo;
        /** �˹ٻ� UI **/
        public Worker_UI    workerUI;
    }
    public UI ui;

    protected void Awake()
    {
        SetSingleton();
    }

    protected void SetSingleton()
    {
        //�̱��� ����
        if (Instance == null)
        {
            Instance = gameObject.GetComponent<KitchenMgr>();
        }
    }

    public void Init()
    {
        /////////////////////////////////////////////////////////////////////////////////
        //����� ����
        spicys.ForEach((x) => x.gameObject.SetActive(false));
        for(int i = 0; i < gameMgr.playData.spicyState.Length; i++)
        {
            if((ChickenSpicy)gameMgr.playData.spicyState[i] == ChickenSpicy.None)
            {
                spicys[i].gameObject.SetActive(false);
            }
            else
            {
                spicys[i].SetObj((ChickenSpicy)gameMgr.playData.spicyState[i]);
                spicys[i].gameObject.SetActive(true);
            }
        }

        /////////////////////////////////////////////////////////////////////////////////
        //���� ����
        drinks.ForEach((x) => x.gameObject.SetActive(false));
        for (int i = 0; i < gameMgr.playData.drinkState.Length; i++)
        {
            if ((Drink)gameMgr.playData.drinkState[i] == Drink.None)
            {
                drinks[i].gameObject.SetActive(false);
            }
            else
            {
                drinks[i].SetObj((Drink)gameMgr.playData.drinkState[i]);
                drinks[i].gameObject.SetActive(true);
            }
        }

        /////////////////////////////////////////////////////////////////////////////////
        //���̵�޴� ����
        sideMenus.ForEach((x) => x.gameObject.SetActive(false));
        for (int i = 0; i < gameMgr.playData.sideMenuState.Length; i++)
        {
            if ((SideMenu)gameMgr.playData.sideMenuState[i] == SideMenu.None)
            {
                sideMenus[i].gameObject.SetActive(false);
            }
            else
            {
                sideMenus[i].SetObj((SideMenu)gameMgr.playData.sideMenuState[i]);
                sideMenus[i].gameObject.SetActive(true);
            }
        }

        /////////////////////////////////////////////////////////////////////////////////
        //Ƣ���&ġŲ���� �غ�
        foreach (Oil_Zone oilZone in oilMachines)
        {
            oilZone.Init();
        }

        if (gameMgr.playData.hasItem[(int)ShopItem.NEW_OIL_ZONE_1])
        {
            chickenPackslots[1].gameObject.SetActive(true);
            oilMachines[1].gameObject.SetActive(true);
        }
        else
        {
            oilMachines[1].gameObject.SetActive(false);
            chickenPackslots[1].gameObject.SetActive(false);
        }

        if (gameMgr.playData.hasItem[(int)ShopItem.NEW_OIL_ZONE_2])
        {
            chickenPackslots[2].gameObject.SetActive(true);
            oilMachines[2].gameObject.SetActive(true);
        }
        else
        {
            oilMachines[2].gameObject.SetActive(false);
            chickenPackslots[2].gameObject.SetActive(false);
        }

        if (gameMgr.playData.hasItem[(int)ShopItem.NEW_OIL_ZONE_3])
        {
            chickenPackslots[3].gameObject.SetActive(true);
            oilMachines[3].gameObject.SetActive(true);
        }
        else
        {
            oilMachines[3].gameObject.SetActive(false);
            chickenPackslots[3].gameObject.SetActive(false);
        }


        /////////////////////////////////////////////////////////////////////////////////
        //�˹ٻ� ���� ����
        UpdateWorkerAct();
        ui.workerUI.Init();
    }

    public void UpdateWorkerAct()
    {
        workerKitchen.UpdateWorkerAct();
    }

    private float KitchenWidth()
    {
        return kitchenRect.content.sizeDelta.x;
    }

    public void SetkitchenSetPos(Vector2 movePos)
    {
        float width = KitchenWidth();
        kitchenRect.content.transform.Translate(movePos);
        kitchenRect.content.offsetMin = new Vector2(Mathf.Clamp(movePos.x, -width, 0), kitchenRect.content.offsetMin.y);
        kitchenRect.content.offsetMax = new Vector2(kitchenRect.content.offsetMin.x + width, kitchenRect.content.offsetMax.y);
    }
}
