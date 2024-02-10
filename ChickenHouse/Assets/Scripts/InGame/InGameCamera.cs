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

    private Vector3 defaultPos;

    private void Awake()
    {
        lookArea = startArea;
        changeLook.ChangeCamera(lookArea, 0);
    }

    private void Start()
    {
        defaultPos = Camera.main.transform.position;
    }

    private void LateUpdate()
    {
        //�ֹ濡���� ȭ�� �巡�װ��ǵ��� ����
        if (lookArea == LookArea.Kitchen)
        {
            dragCamera.ViewMoving();
        }
    }

    public void ChangeLook(LookArea pLookArea, NoParaDel fun = null)
    {
        if(pLookArea == LookArea.Kitchen)
        {
            Camera.main.transform.position = defaultPos;
        }

        lookArea = pLookArea;
        changeLook.ChangeCamera(lookArea, fun);
    }
}
