using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class RewardItem : Mgr
{
    [SerializeField] private Image              rewardImg;
    [SerializeField] private TextMeshProUGUI    rewardText;
    [SerializeField] private Button             btn;
    private const float DEFAULT_WIDTH = 422.35f;

    public void SetUI(ShopData pShopData, NoParaDel pFun)
    {
        soundMgr.PlaySE(Sound.GetSpicy_SE);
        rewardImg.sprite = pShopData.icon;
        float newRate = (float)pShopData.icon.rect.height / (float)pShopData.icon.rect.width;
        rewardImg.GetComponent<RectTransform>().sizeDelta = new Vector2(DEFAULT_WIDTH, DEFAULT_WIDTH * newRate);

        string rewardName = LanguageMgr.GetText(pShopData.nameKey);
        rewardText.text = string.Format(LanguageMgr.GetText("GET_ITEM"), rewardName);

        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(() => pFun?.Invoke());
    }
}
