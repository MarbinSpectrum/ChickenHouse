using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Counter_BG : Mgr
{
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
        float baseRate = 16.0f / 9.0f;
        if (baseRate > (float)Screen.width / (float)Screen.height)
        {
            float rate = ((float)Screen.width / (float)Screen.height) * (9.0f / 16.0f);
            transform.localScale = Vector3.one * rate;
        }
        else
        {
            transform.localScale = Vector3.one;
        }
    }
}
