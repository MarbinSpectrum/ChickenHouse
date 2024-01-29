using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenMgr : Mgr
{
    public static KitchenMgr Instance;

    /** 드래그중인 오브젝트 **/
    [System.NonSerialized] public DragState        dragState;
    /** 보고있는 지역 **/
    [System.NonSerialized] public LookArea         lookArea;
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
    /** 치킨 소스 **/
    [System.NonSerialized] public ChickenHotSpicy  hotSpicy;


    /** 마우스 포인터의 위치 **/
    public  DragArea        mouseArea { get; private set; }
    /** 마우스 포인터 위치 판단 용 **/
    private RaycastHit2D[]  raycastHit2D = new RaycastHit2D[20];

    /** 오브젝트 드래그 **/
    public DragObj          dragObj;

    [System.Serializable]
    public struct UI
    {
        //주방 관련 UI

        /** 쓰레기 버리기 버튼 **/
        public TakeOut_UI takeOut;
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

    protected void Update()
    {
        UpdateCheckMouseArea();
    }

    private void UpdateCheckMouseArea()
    {
        //현재 마우스 위치가 어디있는지 검사

        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        int cnt = Physics2D.RaycastNonAlloc(pos, Vector2.zero, raycastHit2D);
        if (cnt == 0)
        {
            mouseArea = DragArea.None;
        }
        else
        {
            foreach (RaycastHit2D hid2D in raycastHit2D)
            {
                if (hid2D.transform == null)
                    continue;

                switch (hid2D.transform.tag)    
                {
                    case "Chicken_Box":
                        mouseArea = DragArea.Chicken_Box;
                        return;
                    case "Tray_Egg":
                        {
                            trayEgg = hid2D.transform.GetComponent<TrayEgg>();
                            mouseArea = DragArea.Tray_Egg;
                        }
                        return;
                    case "Tray_Flour":
                        {
                            trayFlour = hid2D.transform.GetComponent<TrayFlour>();
                            mouseArea = DragArea.Tray_Flour;
                        }
                        return;
                    case "Chicken_Strainter":
                        {
                            chickenStrainter = hid2D.transform.GetComponent<ChickenStrainter>();
                            if (chickenStrainter.isRun)
                            {
                                mouseArea = DragArea.Chicken_Strainter;
                                return;
                            }
                        }
                        break;
                    case "Oil_Zone":
                        {
                            oilZone = hid2D.transform.GetComponent<Oil_Zone>();
                            mouseArea = DragArea.Oil_Zone;
                        }
                        return;
                    case "Trash_Btn":
                        {
                            mouseArea = DragArea.Trash_Btn;
                        }
                        return;
                    case "Chicken_Pack":
                        {
                            chickenPack = hid2D.transform.GetComponent<ChickenPack>();
                            mouseArea = DragArea.Chicken_Pack;
                        }
                        return;
                    case "Hot_Spicy":
                        {
                            hotSpicy = hid2D.transform.GetComponent<ChickenHotSpicy>();
                            mouseArea = DragArea.Hot_Spicy;
                        }
                        break;
                }
            }
            mouseArea = DragArea.None;
            return;
        }
    }
}
