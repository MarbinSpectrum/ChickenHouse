using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]

public class BlurFast : MonoBehaviour
{
    [SerializeField] private Image img;
    [SerializeField] private Material mat;

    private Material nowMat = null;
    private float OpacityValue = -1;
    private float SizeValue = -1;
    [SerializeField] private float opacityValue;
    [SerializeField] private float sizeValue;

    private void Update()
    {
        if (img == null || mat == null)
            return;
        if (OpacityValue == opacityValue && SizeValue == sizeValue)
            return;

        if (nowMat == null)
        {
            nowMat = Instantiate(mat);
            img.material = nowMat;
        }
        OpacityValue = opacityValue;
        SizeValue = sizeValue;
        img.material.SetFloat("_Opacity", opacityValue);
        img.material.SetFloat("_Size", sizeValue);
    }
}
