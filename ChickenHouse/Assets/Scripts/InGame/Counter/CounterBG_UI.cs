using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CounterBG_UI : CounterBG
{
    protected override void SetColor(GameObject pImg, Color pColor)
    {
        Image image = pImg.GetComponent<Image>();
        image.color = pColor;
    }

    protected override void SetSprite(GameObject pImg, Sprite pSprite)
    {
        Image image = pImg.GetComponent<Image>();
        image.sprite = pSprite;
    }
}
