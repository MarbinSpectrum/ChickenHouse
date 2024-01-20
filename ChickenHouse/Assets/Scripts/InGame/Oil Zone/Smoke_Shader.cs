using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Smoke_Shader : Mgr
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    private float Yvalue    = -1;
    private float Pow       = -1;

    [SerializeField, Range(0, 1)] private float yValue;
    [SerializeField] private float pow;

    private MaterialPropertyBlock mpb;

    private void Update()
    {
        if (Yvalue == yValue && Pow == pow)
            return;
        Yvalue = yValue;
        Pow = pow;

        mpb ??= new MaterialPropertyBlock();

        mpb.SetFloat("_Yvalue", Yvalue);
        mpb.SetFloat("_Pow", Pow);

        spriteRenderer.SetPropertyBlock(mpb);
    }
}
