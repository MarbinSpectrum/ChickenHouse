using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ChickenFlour_Shader : Mgr
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Texture        chickenNormal;
    [SerializeField] private Texture        chickenFlour;

    private float LerpValue = -1;
    [SerializeField, Range(0, 1)] private float lerpValue;

    private MaterialPropertyBlock mpb;

    private void Update()
    {
        if (LerpValue == lerpValue)
            return;
        LerpValue = lerpValue;
        mpb ??= new MaterialPropertyBlock();
        mpb.SetTexture("_MainTex", chickenNormal);
        mpb.SetTexture("_SubTex", chickenFlour);
        mpb.SetFloat("_LerpValue", lerpValue);

        spriteRenderer.SetPropertyBlock(mpb);
    }
}
