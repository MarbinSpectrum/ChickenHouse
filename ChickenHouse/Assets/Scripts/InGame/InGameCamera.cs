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
        SetCamera();
    }

    private void Start()
    {
        defaultPos = Camera.main.transform.position;
    }

#if UNITY_EDITOR
    private void Update()
    {
        SetCamera();
    }
#endif

    private void LateUpdate()
    {
        //�ֹ濡���� ȭ�� �巡�װ��ǵ��� ����
        if (lookArea == LookArea.Kitchen)
        {
            dragCamera.ViewMoving();
        }
    }

    private void SetCamera()
    {
        if ((float)Screen.height / (float)Screen.width > SafeArea.SCREEN_HEIGHT / SafeArea.SCREEN_WIDTH)
        {
            float scaleRate = Mathf.Min(1, (float)Screen.height / (float)Screen.width);
            float newValue = Mathf.Lerp(15, 25, scaleRate);
            Camera.main.orthographicSize = newValue;
        }
        else
        {
            Camera.main.orthographicSize = 15;
        }
    }

    public void ChangeLook(LookArea pLookArea, NoParaDel fun = null)
    {
        if (pLookArea == LookArea.Kitchen)
        {
            Camera.main.transform.position = defaultPos;
        }

        changeLook.ChangeCamera(pLookArea, fun);
    }
}
