using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ShopSlot_UI : Mgr, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
    ,IDragHandler , IBeginDragHandler , IEndDragHandler
{
    [SerializeField] private Animator                   animator;
    [SerializeField] private Image                      upgradeIcon;
    [SerializeField] private TextMeshProUGUI            upgradeName;
    [SerializeField] private TextMeshProUGUI            upgradeInfo;
    [SerializeField] private TextMeshProUGUI            upgradeMoney;
    [SerializeField] private Shop_UI                 upgradeUI;
    [SerializeField] private LoopHorizontalScrollRect   scrollRect;
    [SerializeField] private Button                     upgradeBtn;

    private ShopItem        shopItem;
    private ShopData        shopData = null;

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

    public void SetData(ShopItem pShopItem)
    {
        //���� ����
        bool hasItem     = HasItem(pShopItem);
        
        shopItem = pShopItem;
        shopData = upgradeMgr.GetUpgradeData(pShopItem);

        if (shopData == null)
        {
            gameObject.SetActive(false);
            return;
        }

        animator.SetTrigger("Normal");
        gameObject.SetActive(true);

        //������ ����
        if (upgradeIcon != null)
        {
            upgradeIcon.sprite = shopData.icon;
            upgradeIcon.gameObject.SetActive(true);
        }

        //�̸� ����
        if (upgradeName != null)
        {
            LanguageMgr.SetString(upgradeName, shopData.nameKey);
            upgradeName.gameObject.SetActive(true);
        }

        //���� ����
        if (upgradeInfo != null)
        {
            LanguageMgr.SetString(upgradeInfo, shopData.infoKey);
            upgradeInfo.gameObject.SetActive(true);
        }

        //���� ����
        if (upgradeMoney != null)
        {
            string strNum = string.Format("{0:N0} $", shopData.money);
            LanguageMgr.SetText(upgradeMoney, strNum);
            upgradeInfo.gameObject.SetActive(true);
        }

        //��ư ����
        if(upgradeBtn != null)
        {
            if (hasItem)
            {
                upgradeBtn.gameObject.SetActive(false);
            }
            else
            {
                upgradeBtn.gameObject.SetActive(true);
                upgradeBtn.onClick.RemoveAllListeners();
                upgradeBtn.onClick.AddListener(() => BuyShopItem());
            }
        }
    }

    private bool HasItem(ShopItem pShopItem)
    {
        if (gameMgr.playData.hasItem[(int)pShopItem])
        {
            //�ش� ���׷��̵尡 �Ϸ�Ǿ�����
            return true;
        }

        //���׷��̵尡 �Ϸ� �ȵǾ�����
        return false;
    }

    private void BuyShopItem()
    {
        //���׷��̵� ��ư

        if (shopData == null)
        {
            //���׷��̵� ������ ����.
            return;
        }

        if(gameMgr.playData.money < shopData.money)
        {
            //����� ��μ� ���Ը���
            return;
        }

        gameMgr.playData.money -= shopData.money;
        gameMgr.playData.hasItem[(int)shopItem] = true;

        upgradeUI.SetMoney();
        SetData(shopItem);
    }
}
