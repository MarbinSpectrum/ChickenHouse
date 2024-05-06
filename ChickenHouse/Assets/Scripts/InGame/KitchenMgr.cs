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
    /** �а��� �� **/
    [System.NonSerialized] public TrayFlour        trayFlour;
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
    public Worker worker;

    [System.Serializable]
    public struct Spicy
    {
        //ġŲ �ҽ�

        /** ��� ġŲ ��� **/
        public GameObject hotSpicy;
        /** ���� ��� **/
        public GameObject soySpicy;
        /** �Ӵ� ��� **/
        public GameObject hellSpicy;
        /** ����Ŭ ��� **/
        public GameObject prinkleSpicy;
        /** ������� ��� **/
        public GameObject carbonaraSpicy;
        /** �ٺ�ť ��� **/
        public GameObject bbqSpicy;
    }
    public Spicy spicy;

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

        spicy.soySpicy.gameObject.SetActive(false);
        spicy.hellSpicy.gameObject.SetActive(false);
        spicy.prinkleSpicy.gameObject.SetActive(false);
        spicy.carbonaraSpicy.gameObject.SetActive(false);
        spicy.bbqSpicy.gameObject.SetActive(false);
        if (gameMgr.playData.hasItem[(int)ShopItem.Recipe_1])
        {
            spicy.soySpicy.gameObject.SetActive(true);
        }
        if (gameMgr.playData.hasItem[(int)ShopItem.Recipe_2])
        {
            spicy.hellSpicy.gameObject.SetActive(true);
        }
        if (gameMgr.playData.hasItem[(int)ShopItem.Recipe_3])
        {
            spicy.prinkleSpicy.gameObject.SetActive(true);
        }
        if (gameMgr.playData.hasItem[(int)ShopItem.Recipe_4])
        {
            spicy.carbonaraSpicy.gameObject.SetActive(true);
        }
        if (gameMgr.playData.hasItem[(int)ShopItem.Recipe_5])
        {
            spicy.bbqSpicy.gameObject.SetActive(true);
        }

        /////////////////////////////////////////////////////////////////////////////////
        //�˹ٻ� ���� ����
        worker.UpdateHandMoveArea();
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
        kitchenRect.content.offsetMax = new Vector2(movePos.x + width, kitchenRect.content.offsetMax.y);
    }
}
