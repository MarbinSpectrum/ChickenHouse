using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KitchenMgr : Mgr
{
    public static KitchenMgr Instance;

    /** 드래그중인 오브젝트 **/
    [System.NonSerialized] public DragState        dragState;
    /** 치킨 통 **/
    [System.NonSerialized] public ChickenBox       chickenBox;
    /** 계란물 통 **/
    [System.NonSerialized] public TrayEgg          trayEgg;
    /** 계란물 그릇 **/
    [System.NonSerialized] public BowlEgg          bowlEgg;
    /** 밀가루 통 **/
    [System.NonSerialized] public TrayFlour        trayFlour;
    /** 밀가루 통2 **/
    [System.NonSerialized] public TrayFlour2        trayFlour2;
    /** 치킨 건지 **/
    [System.NonSerialized] public ChickenStrainter chickenStrainter;
    /** 기름 **/
    [System.NonSerialized] public Oil_Zone         oilZone;
    /** 치킨 포장 박스 **/
    [System.NonSerialized] public ChickenPack      chickenPack;


    /** 치킨 슬롯 **/
    [System.NonSerialized] public TableChickenSlot chickenSlot;
    /** 치킨 무 슬롯 **/
    [System.NonSerialized] public TablePickleSlot  pickleSlot;
    /** 음료 슬롯 **/
    [System.NonSerialized] public TableDrinkSlot   drinkSlot;

    /** 마우스 포인터의 위치 **/
    public DragArea mouseArea;

    /** 카메라 오브젝트 **/
    public InGameCamera     cameraObj;
    /** 오브젝트 드래그 **/
    public DragObj          dragObj;
    /** 주방 Rect **/
    public ScrollRect       kitchenRect;
    /** 알바생 **/
    [SerializeField] private Worker_Kitchen   workerKitchen;
    /** 양념 **/
    [SerializeField] private List<ChickenSpicyObj>  spicys      = new List<ChickenSpicyObj>();
    /** 드링크 **/
    [SerializeField] private List<DrinkObj>         drinks      = new List<DrinkObj>();
    /** 피클 **/
    [SerializeField] private List<SideMenuObj>      sideMenus   = new List<SideMenuObj>();

    [SerializeField] private List<Oil_Zone>         oilMachines         = new List<Oil_Zone>();
    [SerializeField] private List<GameObject>       chickenPackslots    = new List<GameObject>();

    [System.Serializable]
    public struct UI
    {
        //주방 관련 UI

        /** 쓰레기 버리기 버튼 **/
        public TakeOut_UI   takeOut;
        /** 카운터로 이동하기 버튼 **/
        public GoCounter_UI goCounter;
        /** 메모 **/
        public Memo_UI      memo;
        /** 알바생 UI **/
        public Worker_UI    workerUI;
    }
    public UI ui;

    protected void Awake()
    {
        SetSingleton();
    }

    protected void SetSingleton()
    {
        //싱글톤 선언
        if (Instance == null)
        {
            Instance = gameObject.GetComponent<KitchenMgr>();
        }
    }

    public void Init()
    {
        /////////////////////////////////////////////////////////////////////////////////
        //양념통 세팅
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
        //음료 세팅
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
        //사이드메뉴 세팅
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
        //튀김기&치킨상자 준비
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
        //알바생 업무 시작
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
