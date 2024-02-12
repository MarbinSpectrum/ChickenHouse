using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class InGameRect : Mgr
{
    private RectTransform rectTrans = null;

    private void Awake()
    {
        SetScale();
    }

#if UNITY_EDITOR
    private void Update()
    {
        SetScale();
    }
#endif

    private void SetScale()
    {
        //ȭ�� ������ ����ؼ�
        //canvas�� ũ�⸦ �����Ѵ�.
        rectTrans ??= GetComponent<RectTransform>();
        SafeArea.SetSafeArea(rectTrans);
    }
}
