using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UpgradeSlot_UI : Mgr, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Animator           animator;
    [SerializeField] private Image              upgradeIcon;
    [SerializeField] private TextMeshProUGUI    upgradeName;
    [SerializeField] private TextMeshProUGUI    upgradeInfo;
    private Upgrade     upgrade;
    private Upgrade_UI  upgradeUI;

    public void OnPointerClick(PointerEventData eventData)
    {
        animator.SetTrigger("Normal");
        SelectMenu();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        animator.SetTrigger("Pressed");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        animator.SetTrigger("Highlighted");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        animator.SetTrigger("Normal");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        animator.SetTrigger("Normal");
    }

    public void SetData(Upgrade_UI pUpgradeUI, Upgrade pUpgrade)
    {
        //정보 설정
        upgradeUI   = pUpgradeUI;
        upgrade     = pUpgrade;

        UpgradeData upgradeData = upgradeMgr.GetUpgradeData(pUpgrade);
        if (upgradeData == null)
        {
            gameObject.SetActive(false);
            return;
        }
        gameObject.SetActive(true);

        //아이콘 설정
        if (upgradeIcon != null)
        {
            upgradeIcon.sprite = upgradeData.icon;
            upgradeIcon.gameObject.SetActive(true);
        }

        //이름 설정
        if (upgradeName != null)
        {
            upgradeName.text = LanguageMgr.GetText(upgradeData.nameKey);
            upgradeName.gameObject.SetActive(true);
        }

        //설명 설정
        if (upgradeInfo != null)
        {
            upgradeInfo.text = LanguageMgr.GetText(upgradeData.infoKey);
            upgradeInfo.gameObject.SetActive(true);
        }
    }

    private void SelectMenu()
    {
        //메뉴 선택
        upgradeUI.SelectUpgrade(upgrade);
    }
}
