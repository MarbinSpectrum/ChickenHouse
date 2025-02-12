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
    [System.NonSerialized] public TableSideMenuSlot  pickleSlot;
    /** 음료 슬롯 **/
    [System.NonSerialized] public TableDrinkSlot   drinkSlot;

    /** 마우스 포인터의 위치 **/
    public DragArea mouseArea;

    /** 카메라 오브젝트 **/
    public InGameCamera     cameraObj;
    /** 오브젝트 드래그 **/
    public DragObj          dragObj;
    /** 주방 Rect **/
    [SerializeField] private ScrollRect       kitchenRect;

    /** 주방 알바생 **/
    [SerializeField] private Worker_Kitchen     workerKitchen;
    /** 튀김기 알바생 **/
    [SerializeField] private Worker_OilZone     workerOilZone;
    /** 튀김기 알바생 **/
    [SerializeField] private Worker_Counter     workerCounter;

    /** 양념 **/
    [SerializeField] private List<ChickenSpicyObj>  spicys      = new List<ChickenSpicyObj>();

    [SerializeField] private List<Oil_Zone>         oilMachines         = new List<Oil_Zone>();

    public bool runWorker { get; private set; }

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
        /** 사이드 메뉴 UI **/
        public KitchenSideMenuUI sideMenuUI;
        /** 테이블 UI **/
        public KitchenTableMenuRect tableUI;
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
        for(int i = 0; i < (int)MenuSetPos.SpicyMAX; i++)
        {
            if(gameMgr.playData == null || (ChickenSpicy)gameMgr.playData.spicy[i] == ChickenSpicy.None)
            {
                spicys[i].gameObject.SetActive(false);
            }
            else
            {
                spicys[i].SetObj((ChickenSpicy)gameMgr.playData.spicy[i]);
                spicys[i].gameObject.SetActive(true);
            }
        }

        ui.sideMenuUI.UpdateSlot();

        /////////////////////////////////////////////////////////////////////////////////
        //튀김기&치킨상자 준비
        foreach (Oil_Zone oilZone in oilMachines)
        {
            oilZone.Init();
        }

        if (gameMgr.playData != null && gameMgr.playData.hasItem[(int)ShopItem.NEW_OIL_ZONE_1])
            oilMachines[1].gameObject.SetActive(true);
        else
            oilMachines[1].gameObject.SetActive(false);

        if (gameMgr.playData != null && gameMgr.playData.hasItem[(int)ShopItem.NEW_OIL_ZONE_2])
            oilMachines[2].gameObject.SetActive(true);
        else
            oilMachines[2].gameObject.SetActive(false);

        if (gameMgr.playData != null && gameMgr.playData.hasItem[(int)ShopItem.NEW_OIL_ZONE_3])
            oilMachines[3].gameObject.SetActive(true);
        else
            oilMachines[3].gameObject.SetActive(false);

        ui.tableUI.UpdateTable();


        /////////////////////////////////////////////////////////////////////////////////
        //알바생 업무 시작
        WorkerAct();
        ui.workerUI.Init();

        if (gameMgr.playData == null || (EWorker)gameMgr.playData.workerPos[(int)KitchenSetWorkerPos.CounterWorker] == EWorker.None)
        {
            cameraObj.ChangeLook(LookArea.Counter);
            ui.memo.CloseTriggerBox();
            ui.workerUI.OffBox();
        }
        else
        {
            cameraObj.ChangeLook(LookArea.Kitchen);
            ui.memo.OpenTriggerBox();
            ui.workerUI.OnBox();

        }
    }

    private void WorkerAct()
    {
        workerKitchen.WorkerAct();
        workerOilZone.WorkerAct();
    }

    public void RunWorker(bool state)
    {
        runWorker = state;
    }

    public RectTransform KitchenContent()
    {
        return kitchenRect.content;
    }

    public void UpdateOilZoneLoopSE()
    {
        bool isRun = false;
        foreach (Oil_Zone oilZone in oilMachines)
        {
            if (oilZone.gameObject.activeSelf == false)
                continue;
            if (oilZone.IsRun() == false)
                continue;
            if (oilZone.isHold)
                continue;
            isRun = true;
            break;
        }

        if(isRun)
            soundMgr.PlayLoopSE(Sound.Oil_SE);
        else
            soundMgr.StopLoopSE(Sound.Oil_SE);
    }

    public int GetActiveOilZoneCnt()
    {
        //활성화중인 튀김기 갯수
        int res = 0;
        foreach (Oil_Zone oilZone in oilMachines)
        {
            if (oilZone.gameObject.activeSelf == false)
                continue;
            res++;
        }

        return res;
    }

    public void SetkitchenSetPos(Vector2 movePos)
    {
        float width = KitchenContent().sizeDelta.x;
        kitchenRect.content.offsetMin = new Vector2(Mathf.Clamp(movePos.x, -width, 0), kitchenRect.content.offsetMin.y);
        kitchenRect.content.offsetMax = new Vector2(kitchenRect.content.offsetMin.x + width, kitchenRect.content.offsetMax.y);
        kitchenRect.velocity = Vector2.zero;
    }

    public void AddkitchenSetPos(float v, DragCamera.DRAG_OUTLINE outline)
    {
        float dis = Mathf.Abs(kitchenRect.content.offsetMin.x - outline.head.offsetMin.x);
        float dis2 = Mathf.Abs(kitchenRect.content.offsetMin.x + v - outline.head.offsetMin.x);
        if (kitchenRect.content.offsetMin.x + v < outline.head.offsetMin.x && dis <= dis2)
        {
            return;
        }
        else if (kitchenRect.content.offsetMin.x + v > outline.tail.offsetMin.x && dis <= dis2)
        {
            return;
        }

        SetkitchenSetPos(new Vector2(kitchenRect.content.offsetMin.x + v, 0));
    }

    public void RunWorkerTalkBox(WorkerCounterTalkBox pWorkerCounterTalkBox) => workerCounter.RunTalkBox(pWorkerCounterTalkBox);

    public void ActKitchenRect(bool pState) => kitchenRect.enabled = pState;
}
