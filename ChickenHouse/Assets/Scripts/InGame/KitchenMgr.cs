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
    [System.NonSerialized] public TableSideMenuSlot  pickleSlot;
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

    /** �ֹ� �˹ٻ� **/
    [SerializeField] private Worker_Kitchen     workerKitchen;
    /** Ƣ��� �˹ٻ� **/
    [SerializeField] private Worker_OilZone     workerOilZone;
    /** Ƣ��� �˹ٻ� **/
    [SerializeField] private Worker_Counter     workerCounter;

    /** ��� **/
    [SerializeField] private List<ChickenSpicyObj>  spicys      = new List<ChickenSpicyObj>();
    /** �帵ũ **/
    [SerializeField] private List<DrinkObj>         drinks      = new List<DrinkObj>();
    /** ��Ŭ **/
    [SerializeField] private List<SideMenuObj>      sideMenus   = new List<SideMenuObj>();

    [SerializeField] private List<Oil_Zone>         oilMachines         = new List<Oil_Zone>();
    [SerializeField] private List<GameObject>       chickenPackslots    = new List<GameObject>();

    public bool runWorker { get; private set; }

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

        int subMenuActCnt = 0;

        /////////////////////////////////////////////////////////////////////////////////
        //���� & ���̵�޴� ����
        drinks.ForEach((x) => x.gameObject.SetActive(false));
        List<Drink> actDrink = new List<Drink>();
        for (int i = 0; i < (int)MenuSetPos.DrinkMAX; i++)
        {
            if (gameMgr.playData == null)
                continue;
            Drink isDrink = (Drink)gameMgr.playData.drink[i];
            if (isDrink == Drink.None)
                continue;
            actDrink.Add(isDrink);
        }

        int sideMenuCnt = 0;
        sideMenus.ForEach((x) => x.gameObject.SetActive(false));
        for (int i = 0; i < sideMenus.Count; i++)
        {
            if (gameMgr.playData == null)
                continue;

            for (int j = 0; j < gameMgr.playData.sideMenu.Length; j++)
            {
                SideMenu sideMenu = (SideMenu)gameMgr.playData.sideMenu[j];
                if (sideMenu == SideMenu.None || sideMenus[i].SideMenu != sideMenu)
                    continue;
                sideMenus[i].gameObject.SetActive(true);
                sideMenuCnt++;
                break;
            }
        }

        if(sideMenuCnt >= 3 || actDrink.Count >= 3)
        {
            for (int i = 0; i < actDrink.Count; i++)
            {
                drinks[i].SetObj(actDrink[i]);
                drinks[i].gameObject.SetActive(true);
            }
        }
        else if(actDrink.Count > 0)
        {
            drinks[0].SetObj(actDrink[0]);
            drinks[0].gameObject.SetActive(true);
            if (actDrink.Count > 1)
            {
                drinks[2].SetObj(actDrink[1]);
                drinks[2].gameObject.SetActive(true);
            }
        }


        /////////////////////////////////////////////////////////////////////////////////
        //Ƣ���&ġŲ���� �غ�
        foreach (Oil_Zone oilZone in oilMachines)
        {
            oilZone.Init();
        }

        if (gameMgr.playData != null && gameMgr.playData.hasItem[(int)ShopItem.NEW_OIL_ZONE_1])
        {
            chickenPackslots[1].gameObject.SetActive(true);
            oilMachines[1].gameObject.SetActive(true);
        }
        else
        {
            oilMachines[1].gameObject.SetActive(false);
            chickenPackslots[1].gameObject.SetActive(false);
        }

        if (gameMgr.playData != null && gameMgr.playData.hasItem[(int)ShopItem.NEW_OIL_ZONE_2])
        {
            chickenPackslots[2].gameObject.SetActive(true);
            oilMachines[2].gameObject.SetActive(true);
        }
        else
        {
            oilMachines[2].gameObject.SetActive(false);
            chickenPackslots[2].gameObject.SetActive(false);
        }

        if (gameMgr.playData != null && gameMgr.playData.hasItem[(int)ShopItem.NEW_OIL_ZONE_3])
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

    private float KitchenWidth()
    {
        return kitchenRect.content.sizeDelta.x;
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

    public void SetkitchenSetPos(Vector2 movePos)
    {
        float width = KitchenWidth();
        kitchenRect.content.offsetMin = new Vector2(Mathf.Clamp(movePos.x, -width, 0), kitchenRect.content.offsetMin.y);
        kitchenRect.content.offsetMax = new Vector2(kitchenRect.content.offsetMin.x + width, kitchenRect.content.offsetMax.y);
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
}
