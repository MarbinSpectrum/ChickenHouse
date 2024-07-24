using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeasoningFace : Mgr
{
    [SerializeField] private Dictionary<ChickenSpicy, Image> spicyImgs = new Dictionary<ChickenSpicy, Image>();

    private ChickenSpicy spicy;

    public Image GetNowImg()
    {
        //현재 이미지 가져오기
        if (spicyImgs.ContainsKey(spicy) == false)
            return null;

        return spicyImgs[spicy];
    }

    public void SetUI(ChickenSpicy pSpicy)
    {
        spicy = pSpicy;

        foreach (var pair in spicyImgs)
        {
            Image img = pair.Value;
            img.gameObject.SetActive(false);
        }

        Image nowImg = GetNowImg();
        if (nowImg == null)
            return;

        nowImg.gameObject.SetActive(true);
    }
}
