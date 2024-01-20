using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenMgr : MonoBehaviour
{
    public static KitchenMgr Instance;

    /** �巡������ ������Ʈ **/
    [System.NonSerialized] public DragState        dragState;
    /** �����ִ� ���� **/
    [System.NonSerialized] public LookArea         lookArea;

    /** ġŲ �巡�� **/
    public DragChicken          dragChicken;
    /** ġŲ ���� �巡�� **/
    public DragChickenStrainter dragChickenStrainter;

    /** ����� �� **/
    [System.NonSerialized] public TrayEgg          trayEgg;
    /** �а��� �� **/
    [System.NonSerialized] public TrayFlour        trayFlour;
    /** ġŲ ���� **/
    [System.NonSerialized] public ChickenStrainter chickenStrainter;
    /** �⸧ **/
    [System.NonSerialized] public Oil_Zone         oilZone;

    /** ���콺 �������� ��ġ **/
    public  DragArea        mouseArea { get; private set; }
    /** ���콺 ������ ��ġ �Ǵ� �� **/
    private RaycastHit2D[]  raycastHit2D = new RaycastHit2D[20];

    protected void Awake()
    {
        SetSingleton();
    }

    protected void Update()
    {
        UpdateCheckMouseArea();
    }

    private void UpdateCheckMouseArea()
    {
        //���� ���콺 ��ġ�� ����ִ��� �˻�

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
                }
            }
            mouseArea = DragArea.None;
            return;
        }
    }

    protected void SetSingleton()
    {
        //�̱��� ����
        if (Instance == null)
        {
            Instance = gameObject.GetComponent<KitchenMgr>();
        }
    }
}
