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
        //���� ����
        upgrade     = SetUpgrade(pUpgrade);

        upgradeData = upgradeMgr.GetUpgradeData(upgrade);
        if (upgradeData == null)
        {
            gameObject.SetActive(false);
            return;
        }
        gameObject.SetActive(true);

        bool isMaxUpgrade = IsMaxUpgrade(upgrade);

        //������ ����
        if (upgradeIcon != null)
        {
            upgradeIcon.sprite = upgradeData.icon;
            upgradeIcon.gameObject.SetActive(true);
        }

        //�̸� ����
        if (upgradeName != null)
        {
            LanguageMgr.SetString(upgradeName, upgradeData.nameKey);
            upgradeName.gameObject.SetActive(true);
        }

        //���� ����
        if (upgradeInfo != null)
        {
            LanguageMgr.SetString(upgradeInfo, upgradeData.infoKey);
            upgradeInfo.gameObject.SetActive(true);
        }

        //���� ����
        if (upgradeMoney != null)
        {
            string strNum = string.Format("{0:N0} $", upgradeData.money);
            LanguageMgr.SetText(upgradeMoney, strNum);
            upgradeInfo.gameObject.SetActive(true);
        }

        //��ư ����
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
            //�ش� ���׷��̵尡 �Ϸ�Ǿ�����
            return true;
        }

        //���׷��̵尡 �Ϸ� �ȵǾ�����
        return false;
    }

    private bool IsMaxUpgrade(Upgrade pUpgrade)
    {
        //�ִ� ���ۿ���
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
            //�⸧�� ���׷��̵�
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
                    //�⸧�� ���׷��̵尡 �ִ����
                return pUpgrade;

            //-----------------------------------------------------------------
            //������ ���׷��̵�
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
                //������ ���׷��̵尡 �ִ����
                return pUpgrade;
        }

        return Upgrade.None;
    }

    private void UpgradeBtn()
    {
        //���׷��̵� ��ư

        if (upgradeData == null)
        {
            //���׷��̵� ������ ����.
            return;
        }

        if(gameMgr.playData.money < upgradeData.money)
        {
            //����� ��μ� ���Ը���
            return;
        }

        bool isMaxUpgrade = IsMaxUpgrade(upgrade);
        if (isMaxUpgrade)
        {
            //�ִ� ������
            return;
        }

        gameMgr.playData.money -= upgradeData.money;
        gameMgr.playData.upgradeState[(int)upgrade] = true;

        upgradeUI.SetMoney();
        SetData(upgrade);
    }
}
