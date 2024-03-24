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
    /** 밀가루 통 **/
    [System.NonSerialized] public TrayFlour        trayFlour;
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

    [System.Serializable]
    public struct Spicy
    {
        //치킨 소스

        /** 양념 치킨 양념 **/
        public GameObject hotSpicy;
        /** 간장 양념 **/
        public GameObject soySpicy;
        /** 붉닭 양념 **/
        public GameObject hellSpicy;
        /** 프링클 양념 **/
        public GameObject prinkleSpicy;
        /** 바베큐 양념 **/
        public GameObject bbqSpicy;
    }
    public Spicy spicy;

    [System.Serializable]
    public struct Table
    {
        //테이블

        /** 표준 테이블 **/
        public GameObject table0;
        /** 양념 3개 이상일때 사용되는 테이블 **/
        public GameObject table1;
    }
    public Table table;

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
        int spicyCnt = 0;
        spicy.soySpicy.gameObject.SetActive(false);
        spicy.hellSpicy.gameObject.SetActive(false);
        spicy.prinkleSpicy.gameObject.SetActive(false);
        spicy.bbqSpicy.gameObject.SetActive(false);
        if (gameMgr.playData.upgradeState[(int)Upgrade.Recipe_1])
        {
            spicy.soySpicy.gameObject.SetActive(true);
            spicyCnt++;
        }
        if (gameMgr.playData.upgradeState[(int)Upgrade.Recipe_2])
        {
            spicy.hellSpicy.gameObject.SetActive(true);
            spicyCnt++;
        }
        if (gameMgr.playData.upgradeState[(int)Upgrade.Recipe_3])
        {
            spicy.prinkleSpicy.gameObject.SetActive(true);
            spicyCnt++;
        }
        if (gameMgr.playData.upgradeState[(int)Upgrade.Recipe_4])
        {
            spicy.bbqSpicy.gameObject.SetActive(true);
            spicyCnt++;
        }

        Vector2 sizeValue = kitchenRect.content.sizeDelta;
        table.table0.gameObject.SetActive(false);
        table.table1.gameObject.SetActive(false);
        if (spicyCnt < 2)
        {
            sizeValue = new Vector2(KitchenWidth(), sizeValue.y);
            table.table0.gameObject.SetActive(true);
        }
        else
        {
            sizeValue = new Vector2(KitchenWidth(), sizeValue.y);
            table.table1.gameObject.SetActive(true);
        }
        kitchenRect.content.sizeDelta = sizeValue;
    }

    private float KitchenWidth()
    {
        int spicyCnt = 0;
        if (gameMgr.playData.upgradeState[(int)Upgrade.Recipe_1])
        {
            spicyCnt++;
        }
        if (gameMgr.playData.upgradeState[(int)Upgrade.Recipe_2])
        {
            spicyCnt++;
        }
        if (gameMgr.playData.upgradeState[(int)Upgrade.Recipe_3])
        {
            spicyCnt++;
        }
        if (gameMgr.playData.upgradeState[(int)Upgrade.Recipe_4])
        {
            spicyCnt++;
        }


        if (spicyCnt < 2)
        {
            return 36;
        }
        else
        {
            return 43;
        }
    }

    public void SetkitchenSetPos(Vector2 movePos)
    {
        float width = KitchenWidth();
        kitchenRect.content.transform.Translate(movePos);
        kitchenRect.content.offsetMin = new Vector2(Mathf.Clamp(movePos.x, -width, 0), kitchenRect.content.offsetMin.y);
        kitchenRect.content.offsetMax = new Vector2(movePos.x + width, kitchenRect.content.offsetMax.y);
    }
}
