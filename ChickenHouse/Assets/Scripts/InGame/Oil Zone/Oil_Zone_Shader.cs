using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Oil_Zone_Shader : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Texture        readyChicken;       //�������� ġŲ
    [SerializeField] private Texture        goodChicken;        //�߸��� ġŲ
    [SerializeField] private Texture        badChicken;         //�¿� ġŲ

    private float   LerpValue = -1;
    private bool    Mode = false;
    [SerializeField] private bool mode; //true�� ġŲ�� ���� ��
                                        //false�� ġŲ�� �¿�� ��

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
