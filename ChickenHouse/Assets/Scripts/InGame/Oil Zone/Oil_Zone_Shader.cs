using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class Oil_Zone_Shader : Mgr
{
    [SerializeField] private Image          img;
    [SerializeField] private Material       mat;
    [SerializeField] private Sprite         readyChicken;       //조리중인 치킨
    [SerializeField] private Sprite         goodChicken;        //잘만든 치킨
    [SerializeField] private Sprite         badChicken;         //태운 치킨

    public float    LerpValue   { get; private set; } = -1;
    public bool     Mode        { get; private set; } = false;

    [SerializeField] private bool mode; //true면 치킨을 조리 중
                                        //false면 치킨을 태우는 중

    [SerializeField, Range(0, 1)] private float lerpValue;

    private Material nowMat = null;

    private void Update()
    {
        if (LerpValue == lerpValue && Mode == mode)
            return;

        LerpValue   = lerpValue;
        Mode        = mode;

        Set_Shader(Mode, LerpValue);
    }

    public void Set_Shader(bool pMode,float v)
    {
        if (img == null || mat == null)
            return;
        if (nowMat == null)
        {
            nowMat = Instantiate(mat);
            img.material = nowMat;
        }

        lerpValue = v;
        mode      = pMode;
        LerpValue   = lerpValue;
        Mode        = mode;

        //쉐이더 값 설정
        if (mode)
        {
            img.sprite = readyChicken;
            img.material.SetTexture("_MainTex", readyChicken.texture);
            img.material.SetTexture("_SubTex", goodChicken.texture);
        }
        else
        {
            img.sprite = goodChicken;
            img.material.SetTexture("_MainTex", goodChicken.texture);
            img.material.SetTexture("_SubTex", badChicken.texture);
        }

        img.material.SetFloat("_LerpValue", lerpValue);
    }
}
