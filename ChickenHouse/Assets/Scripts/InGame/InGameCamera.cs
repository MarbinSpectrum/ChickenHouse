using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameCamera : MonoBehaviour
{
    [SerializeField] private ChangeLook changeLook;
    [SerializeField] private DragCamera dragCamera;
    [SerializeField] private LookArea   startArea;

    private void Awake()
    {
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        kitchenMgr.lookArea = startArea;
        changeLook.ChangeCamera(kitchenMgr.lookArea, 0);
    }

#if UNITY_EDITOR
    private void Update()
    {
        //에디터에서 테스트용
        //카메라 전환

        KitchenMgr kitchenMgr = KitchenMgr.Instance;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            switch (kitchenMgr.lookArea)
            {
                case LookArea.Counter:
                    {
                        kitchenMgr.lookArea = LookArea.Kitchen;
                    }
                    break;
                case LookArea.Kitchen:
                    {
                        kitchenMgr.lookArea = LookArea.Counter;
                    }
                    break;
            }
            changeLook.ChangeCamera(kitchenMgr.lookArea);
        }
    }
#endif

    private void LateUpdate()
    {
        //오브젝트 드래그중에는 화면이동이 되지 않도록하자 

        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if (kitchenMgr.lookArea == LookArea.Kitchen)
        {
            dragCamera.ViewMoving();
        }
    }
}
