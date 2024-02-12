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
        //화면 비율을 계산해서
        //canvas의 크기를 조정한다.
        rectTrans ??= GetComponent<RectTransform>();
        SafeArea.SetSafeArea(rectTrans);
    }
}
