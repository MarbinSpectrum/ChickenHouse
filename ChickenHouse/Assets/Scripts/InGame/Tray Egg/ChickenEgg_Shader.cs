using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ChickenEgg_Shader : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Texture chickenNormal;
    [SerializeField] private Texture chickenEgg;

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
        mpb.SetTexture("_SubTex", chickenEgg);
        mpb.SetFloat("_LerpValue", lerpValue);

        spriteRenderer.SetPropertyBlock(mpb);
    }
}
