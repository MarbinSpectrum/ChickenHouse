using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ChickenLerp_Shader : Mgr
{
    [SerializeField] private Image          img;
    [SerializeField] private Material       mat;
    [SerializeField] private Texture        chicken0;
    [SerializeField] private Texture        chicken1;

    private Material nowMat = null;
    private float LerpValue = -1;
    [SerializeField, Range(0, 1)] private float lerpValue;

    private void Update()
    {
        if (img == null || mat == null)
            return;
        if (LerpValue == lerpValue)
            return;
        if(nowMat == null)
        {
            nowMat = Instantiate(mat);
            img.material = nowMat;
        }
        LerpValue = lerpValue;
        img.material.SetTexture("_MainTex", chicken0);
        img.material.SetTexture("_SubTex",  chicken1);
        img.material.SetFloat("_LerpValue", lerpValue);
    }
}
