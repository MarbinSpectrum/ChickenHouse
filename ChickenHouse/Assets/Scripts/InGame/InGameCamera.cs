using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameCamera : Mgr
{
    [SerializeField] private ChangeLook changeLook;
    [SerializeField] private DragCamera dragCamera;
    [SerializeField] private LookArea   startArea;

    /** �����ִ� ���� **/
    [System.NonSerialized] public LookArea lookArea;

    private void Awake()
    {
        lookArea = startArea;
        changeLook.ChangeCamera(lookArea, 0);
    }

    private void LateUpdate()
    {
        //������Ʈ �巡���߿��� ȭ���̵��� ���� �ʵ������� 
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
