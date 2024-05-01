using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ShopSlot_UI : Mgr, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
    ,IDragHandler , IBeginDragHandler , IEndDragHandler
{
    [SerializeField] private Shop_UI                    upgradeUI;
    [SerializeField] private Animator                   animator;
    [SerializeField] private LoopScrollRect             scrollRect;

    [SerializeField] private Image                      itemIcon;
    [SerializeField] private TextMeshProUGUI            itemName;
    [SerializeField] private TextMeshProUGUI            itemInfo;

    [SerializeField] private Button                     buyBtn;
    [SerializeField] private TextMeshProUGUI            btnText;

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
        bool useItem     = UseItem(pShopItem);

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
        if (itemIcon != null)
        {
            itemIcon.sprite = shopData.icon;
            itemIcon.gameObject.SetActive(true);
        }

        //�̸� ����
        if (itemName != null)
        {
            LanguageMgr.SetString(itemName, shopData.nameKey);
            itemName.gameObject.SetActive(true);
        }

        //���� ����
        if (itemInfo != null)
        {
            LanguageMgr.SetString(itemInfo, shopData.infoKey);
            itemInfo.gameObject.SetActive(true);
        }

        //��ư ����
        if(buyBtn != null)
        {
            buyBtn.gameObject.SetActive(true);
            if(IsCookItem(shopItem))
            {
                if(hasItem && useItem)
                {
                    //���������̰� �������� �����
                    LanguageMgr.SetString(btnText, "USE_ITEM");
                    buyBtn.image.raycastTarget = false;
                    buyBtn.image.color = new Color(72 / 255f, 241 / 255f, 129 / 255f);
                    buyBtn.onClick.RemoveAllListeners();
                }
                else if (hasItem && useItem == false)
                {
                    //���������̰� �������� ������� �ƴ�
                    LanguageMgr.SetString(btnText, "CANUSE_ITEM");
                    buyBtn.image.raycastTarget = true;
                    buyBtn.image.color = new Color(125 / 255f, 152 / 255f, 248 / 255f);
                    buyBtn.onClick.RemoveAllListeners();
                    buyBtn.onClick.AddListener(() => UseShopItem());
                }
                else if (hasItem == false)
                {
                    //�������� ���ž���
                    string strNum = string.Format("{0:N0} $", shopData.money);
                    LanguageMgr.SetText(btnText, strNum);

                    buyBtn.image.raycastTarget = true;
                    buyBtn.image.color = new Color(125 / 255f, 152 / 255f, 248 / 255f);
                    buyBtn.onClick.RemoveAllListeners();
                    buyBtn.onClick.AddListener(() => BuyShopItem());
                    buyBtn.onClick.AddListener(() => UseShopItem());
                }
            }
            else
            {
                if (hasItem)
                {
                    //���������� �ƴϰ�
                    //�������� ��������
                    LanguageMgr.SetString(btnText, "BUY_COMPLETE");

                    buyBtn.image.raycastTarget = false;
                    buyBtn.image.color = new Color(72 / 255f, 241 / 255f, 129 / 255f);
                    buyBtn.onClick.RemoveAllListeners();
                }
                else
                {
                    //�������� ���ž���
                    string strNum = string.Format("{0:N0} $", shopData.money);
                    LanguageMgr.SetText(btnText, strNum);

                    buyBtn.image.raycastTarget = true;
                    buyBtn.image.color = new Color(125 / 255f, 152 / 255f, 248 / 255f);
                    buyBtn.onClick.RemoveAllListeners();
                    buyBtn.onClick.AddListener(() => BuyShopItem());
                }
            }
        }
    }

    private bool IsCookItem(ShopItem pShopItem)
    {
        switch(pShopItem)
        {
            case ShopItem.OIL_Zone_1:
            case ShopItem.OIL_Zone_2:
            case ShopItem.OIL_Zone_3:
            case ShopItem.OIL_Zone_4:
                return true;
            default:
                return false;
        }
    }

    private bool HasItem(ShopItem pShopItem)
    {
        if (gameMgr.playData.hasItem[(int)pShopItem])
        {
            //�ش� �������� ��������
            return true;
        }

        //�ش� �������� ���� �ȵǾ�����
        return false;
    }

    private bool UseItem(ShopItem pShopItem)
    {
        if (gameMgr.playData.useItem[(int)pShopItem])
        {
            //�ش� �������� �������
            return true;
        }

        //�ش� ������ ������� �ƴ�
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

    private void UseShopItem()
    {
        //���׷��̵� ��ư

        switch(shopItem)
        {
            case ShopItem.OIL_Zone_1:
            case ShopItem.OIL_Zone_2:
            case ShopItem.OIL_Zone_3:
            case ShopItem.OIL_Zone_4:
                UseShopItem(ShopItem.OIL_Zone_1, ShopItem.OIL_Zone_2, ShopItem.OIL_Zone_3, ShopItem.OIL_Zone_4);
                break;
        }
    }

    private void UseShopItem(params ShopItem[] unActiveItem)
    {
        //���׷��̵� ��ư

        if (shopData == null)
        {
            //���׷��̵� ������ ����.
            return;
        }

        if (gameMgr.playData.useItem[(int)shopItem])
        {
            //�̹� ������ΰŰ���?
            return;
        }

        foreach(ShopItem item in unActiveItem)
            gameMgr.playData.useItem[(int)item] = false;

        gameMgr.playData.useItem[(int)shopItem] = true;

        SetData(shopItem);
        scrollRect.RefreshCells();
    }
}
