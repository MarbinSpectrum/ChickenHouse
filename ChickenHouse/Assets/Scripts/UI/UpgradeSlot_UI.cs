using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UpgradeSlot_UI : Mgr, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
    ,IDragHandler , IBeginDragHandler , IEndDragHandler
{
    [SerializeField] private Animator                   animator;
    [SerializeField] private Image                      upgradeIcon;
    [SerializeField] private TextMeshProUGUI            upgradeName;
    [SerializeField] private TextMeshProUGUI            upgradeInfo;
    [SerializeField] private TextMeshProUGUI            upgradeMoney;
    [SerializeField] private Upgrade_UI                 upgradeUI;
    [SerializeField] private LoopHorizontalScrollRect   scrollRect;
    [SerializeField] private Button                     upgradeBtn;

    private Upgrade     upgrade;
    private UpgradeData upgradeData = null;

    public void OnPointerClick(PointerEventData eventData)
    {
        animator.SetTrigger("Normal");
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

    public void OnDrag(PointerEventData eventData)
    {
        scrollRect.OnDrag(eventData);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        animator.SetTrigger("Normal");
        scrollRect.OnBeginDrag(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        scrollRect.OnEndDrag(eventData);
    }

    public void SetData(Upgrade pUpgrade)
    {
        //정보 설정
        upgrade     = SetUpgrade(pUpgrade);

        upgradeData = upgradeMgr.GetUpgradeData(upgrade);
        if (upgradeData == null)
        {
            gameObject.SetActive(false);
            return;
        }
        gameObject.SetActive(true);

        bool isMaxUpgrade = IsMaxUpgrade(upgrade);

        //아이콘 설정
        if (upgradeIcon != null)
        {
            upgradeIcon.sprite = upgradeData.icon;
            upgradeIcon.gameObject.SetActive(true);
        }

        //이름 설정
        if (upgradeName != null)
        {
            LanguageMgr.SetString(upgradeName, upgradeData.nameKey);
            upgradeName.gameObject.SetActive(true);
        }

        //설명 설정
        if (upgradeInfo != null)
        {
            LanguageMgr.SetString(upgradeInfo, upgradeData.infoKey);
            upgradeInfo.gameObject.SetActive(true);
        }

        //설명 설정
        if (upgradeMoney != null)
        {
            string strNum = string.Format("{0:N0} $", upgradeData.money);
            LanguageMgr.SetText(upgradeMoney, strNum);
            upgradeInfo.gameObject.SetActive(true);
        }

        //버튼 설정
        if(upgradeBtn != null)
        {
            if (isMaxUpgrade)
            {
                upgradeBtn.gameObject.SetActive(false);
            }
            else
            {
                upgradeBtn.gameObject.SetActive(true);
                upgradeBtn.onClick.RemoveAllListeners();
                upgradeBtn.onClick.AddListener(() => UpgradeBtn());
            }
        }
    }

    private bool HasUpgrade(Upgrade upgrade)
    {
        if (gameMgr.playData.upgradeState[(int)upgrade])
        {
            //해당 업그레이드가 완료되어있음
            return true;
        }

        //업그레이드가 완료 안되어있음
        return false;
    }

    private bool IsMaxUpgrade(Upgrade pUpgrade)
    {
        //최대 업글여부
        switch (pUpgrade)
        {
            case Upgrade.OIL_Zone_MAX:
            case Upgrade.Recipe_MAX:
                return true;
        }

        return false;
    }

    private Upgrade SetUpgrade(Upgrade pUpgrade)
    {
        switch(pUpgrade)
        {
            //-----------------------------------------------------------------
            //기름통 업그레이드
            case Upgrade.OIL_Zone_1:
                if (HasUpgrade(pUpgrade))
                    return SetUpgrade(Upgrade.OIL_Zone_2);
                else
                    return pUpgrade;
            case Upgrade.OIL_Zone_2:
                if (HasUpgrade(pUpgrade))
                    return SetUpgrade(Upgrade.OIL_Zone_3);
                else
                    return pUpgrade;
            case Upgrade.OIL_Zone_3:
                if (HasUpgrade(pUpgrade))
                    return SetUpgrade(Upgrade.OIL_Zone_4);
                else
                    return pUpgrade;
            case Upgrade.OIL_Zone_4:
                if (HasUpgrade(pUpgrade))
                    return SetUpgrade(Upgrade.OIL_Zone_5);
                else
                    return pUpgrade;
            case Upgrade.OIL_Zone_5:
                if (HasUpgrade(pUpgrade))
                    return SetUpgrade(Upgrade.OIL_Zone_6);
                else
                    return pUpgrade;
            case Upgrade.OIL_Zone_6:
                if (HasUpgrade(pUpgrade))
                    return SetUpgrade(Upgrade.OIL_Zone_MAX);
                else
                    return pUpgrade;
            case Upgrade.OIL_Zone_MAX:
                    //기름통 업그레이드가 최대상태
                return pUpgrade;

            //-----------------------------------------------------------------
            //레시피 업그레이드
            case Upgrade.Recipe_1:
                if (HasUpgrade(pUpgrade))
                    return SetUpgrade(Upgrade.Recipe_2);
                else
                    return pUpgrade;
            case Upgrade.Recipe_2:
                if (HasUpgrade(pUpgrade))
                    return SetUpgrade(Upgrade.Recipe_3);
                else
                    return pUpgrade;
            case Upgrade.Recipe_3:
                if (HasUpgrade(pUpgrade))
                    return SetUpgrade(Upgrade.Recipe_4);
                else
                    return pUpgrade;
            case Upgrade.Recipe_4:
                if (HasUpgrade(pUpgrade))
                    return SetUpgrade(Upgrade.Recipe_MAX);
                else
                    return pUpgrade;
            case Upgrade.Recipe_MAX:
                //레시피 업그레이드가 최대상태
                return pUpgrade;
        }

        return Upgrade.None;
    }

    private void UpgradeBtn()
    {
        //업그레이드 버튼

        if (upgradeData == null)
        {
            //업그레이드 정보가 없다.
            return;
        }

        if(gameMgr.playData.money < upgradeData.money)
        {
            //비용이 비싸서 구입못함
            return;
        }

        bool isMaxUpgrade = IsMaxUpgrade(upgrade);
        if (isMaxUpgrade)
        {
            //최대 업글임
            return;
        }

        gameMgr.playData.money -= upgradeData.money;
        gameMgr.playData.upgradeState[(int)upgrade] = true;

        upgradeUI.SetMoney();
        SetData(upgrade);
    }
}
