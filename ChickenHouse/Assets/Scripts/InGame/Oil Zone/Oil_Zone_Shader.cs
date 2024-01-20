using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Oil_Zone_Shader : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Texture        readyChicken;       //조리중인 치킨
    [SerializeField] private Texture        goodChicken;        //잘만든 치킨
    [SerializeField] private Texture        badChicken;         //태운 치킨

    private float   LerpValue = -1;
    private bool    Mode = false;
    [SerializeField] private bool mode; //true면 치킨을 조리 중
                                        //false면 치킨을 태우는 중

    [SerializeField, Range(0, 1)] private float lerpValue;

    private MaterialPropertyBlock mpb;

    private void Update()
    {
        if (LerpValue == lerpValue && Mode == mode)
            return;
        LerpValue   = lerpValue;
        Mode        = mode;
        mpb ??= new MaterialPropertyBlock();

        if(mode)
        {
            mpb.SetTexture("_MainTex",  readyChicken);
            mpb.SetTexture("_SubTex",   goodChicken);
        }
        else
        {
            mpb.SetTexture("_MainTex",  goodChicken);
            mpb.SetTexture("_SubTex",   badChicken);
        }

        mpb.SetFloat("_LerpValue", lerpValue);

        spriteRenderer.SetPropertyBlock(mpb);
    }
}
