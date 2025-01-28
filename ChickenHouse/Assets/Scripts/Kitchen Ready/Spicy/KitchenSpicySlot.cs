using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KitchenSpicySlot : Mgr
{
    [SerializeField] private TextMeshProUGUI    spicyName;
    [SerializeField] private SeasoningFace      seasoningFace;

    [SerializeField] private RectTransform      selectRect;
    [SerializeField] private RectTransform      partyRect;

    private NoParaDel selectSpicyFun;

    public void SetUI(ChickenSpicy pSpicy, bool pSelect, bool pIsParty, NoParaDel pSelectFun)
    {
        SpicyData spicyData = spicyMgr.GetSpicyData(pSpicy);
        if (spicyData == null)
        {
            gameObject.SetActive(false);
            return;
        }

        selectSpicyFun = pSelectFun;

        gameObject.SetActive(true);
        partyRect.gameObject.SetActive(pIsParty);
        selectRect.gameObject.SetActive(pSelect);
        seasoningFace.SetUI(pSpicy);
        LanguageMgr.SetString(spicyName, spicyData.nameKey);
    }

    public void SelectSpicy() => selectSpicyFun?.Invoke();
}
