using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameCamera : Mgr
{
    [SerializeField] private ChangeLook changeLook;
    [SerializeField] private DragCamera dragCamera;
    [SerializeField] private LookArea   startArea;

    /** 보고있는 지역 **/
    [System.NonSerialized] public LookArea lookArea;

    private void Awake()
    {
        lookArea = startArea;
        changeLook.ChangeCamera(lookArea, 0);
    }

    private void LateUpdate()
    {
        //오브젝트 드래그중에는 화면이동이 되지 않도록하자 
        if (lookArea == LookArea.Kitchen)
        {
            dragCamera.ViewMoving();
        }
    }

    public void ChangeLook(LookArea pLookArea)
    {
        lookArea = pLookArea;
        changeLook.ChangeCamera(lookArea);
    }
}
