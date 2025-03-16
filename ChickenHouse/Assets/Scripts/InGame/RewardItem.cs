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

    public void SetUI(QuestData.QuestRewardData pRewardItem, NoParaDel pFun)
    {
        soundMgr.PlaySE(Sound.GetSpicy_SE);
        Sprite icon = pRewardItem.GetRewardIcon();
        if (icon == null)
            return;
        rewardImg.sprite = icon;
        float newRate = (float)icon.rect.height / (float)icon.rect.width;
        rewardImg.GetComponent<RectTransform>().sizeDelta = new Vector2(DEFAULT_WIDTH, DEFAULT_WIDTH * newRate);

        string rewardName = LanguageMgr.GetText(pRewardItem.GetRewardNameKey());
        rewardText.text = string.Format(LanguageMgr.GetText("GET_ITEM"), rewardName);

        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(() => pFun?.Invoke());
    }
}
