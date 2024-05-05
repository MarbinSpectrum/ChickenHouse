using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ShopSlot_UI : Mgr, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
    ,IDragHandler , IBeginDragHandler , IEndDragHandler
{
    [SerializeField] private Shop_UI                    shopUI;
    [SerializeField] private Animator                   animator;
    [SerializeField] private LoopScrollRect             scrollRect;

    [SerializeField] private Image                      itemIcon;
    [SerializeField] private TextMeshProUGUI            itemName;
    [SerializeField] private TextMeshProUGUI            itemInfo;
    [SerializeField] private Button                     infoBtn;

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
        shopData = shopMgr.GetShopData(pShopItem);

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
            itemInfo.gameObject.SetActive(true);
            if(IsWorker(shopItem))
            {
                string checkResume = LanguageMgr.GetText("CHECK_RESUME");
                string resumeString = string.Format("<color=#9999FF>{0}</color>", checkResume);
                LanguageMgr.SetText(itemInfo, resumeString);

                infoBtn.gameObject.SetActive(true);
                infoBtn.onClick.RemoveAllListeners();
                infoBtn.onClick.AddListener(()=> shopUI.SetResume(shopItem));
                itemInfo.fontStyle = FontStyles.Underline;
            }
            else
            {
                LanguageMgr.SetString(itemInfo, shopData.infoKey);

                infoBtn.gameObject.SetActive(false);
                infoBtn.onClick.RemoveAllListeners();
                itemInfo.fontStyle = FontStyles.Normal;
            }
        }

        //��ư ����
        if(buyBtn != null)
        {
            buyBtn.gameObject.SetActive(true);
            if(IsWorker(pShopItem))
            {
                if (hasItem && useItem)
                {
                    //�����̰� �ش� ������ �����
                    LanguageMgr.SetString(btnText, "USE_ITEM");
                    buyBtn.image.raycastTarget = true;
                    buyBtn.image.color = new Color(72 / 255f, 241 / 255f, 129 / 255f);
                    buyBtn.onClick.RemoveAllListeners();
                    buyBtn.onClick.AddListener(() => {
                        UnActShopItem(
                        ShopItem.Worker_1, ShopItem.Worker_2,
                        ShopItem.Worker_3, ShopItem.Worker_4,
                        ShopItem.Worker_5, ShopItem.Worker_6);
                        scrollRect.RefreshCells();

                    });
                }
                else if (hasItem && useItem == false)
                {
                    //�����̰� �ش� ������ ���Ϸ���
                    LanguageMgr.SetString(btnText, "EMPLOY_COMPLETE");
                    buyBtn.image.raycastTarget = true;
                    buyBtn.image.color = new Color(125 / 255f, 152 / 255f, 248 / 255f);
                    buyBtn.onClick.RemoveAllListeners();
                    buyBtn.onClick.AddListener(() => UseShopItem());
                }
                else if (hasItem == false)
                {
                    //�ش� ������ ������� ����
                    string strNum = string.Empty;
                    if (gameMgr.playData.money >= shopData.money)
                        strNum = string.Format("{0:N0} $", shopData.money);
                    else
                        strNum = string.Format("<color=#FF4444>{0:N0} $</color>", shopData.money);
                    LanguageMgr.SetText(btnText, strNum);

                    buyBtn.image.raycastTarget = true;
                    buyBtn.image.color = new Color(125 / 255f, 152 / 255f, 248 / 255f);
                    buyBtn.onClick.RemoveAllListeners();
                    buyBtn.onClick.AddListener(() => BuyShopItem());
                    buyBtn.onClick.AddListener(() => UseShopItem());
                }
            }
            else if(IsCookItem(shopItem))
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
                    string strNum = string.Empty;
                    if (gameMgr.playData.money >= shopData.money)
                        strNum = string.Format("{0:N0} $", shopData.money);
                    else
                        strNum = string.Format("<color=#FF4444>{0:N0} $</color>", shopData.money);
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
                    string strNum = string.Empty;
                    if (gameMgr.playData.money >= shopData.money)
                        strNum = string.Format("{0:N0} $", shopData.money);
                    else
                        strNum = string.Format("<color=#FF4444>{0:N0} $</color>", shopData.money);
                    LanguageMgr.SetText(btnText, strNum);

                    buyBtn.image.raycastTarget = true;
                    buyBtn.image.color = new Color(125 / 255f, 152 / 255f, 248 / 255f);
                    buyBtn.onClick.RemoveAllListeners();
                    buyBtn.onClick.AddListener(() => BuyShopItem());
                }
            }
        }
    }

    private bool IsWorker(ShopItem pShopItem)
    {
        switch (pShopItem)
        {
            case ShopItem.Worker_1:
            case ShopItem.Worker_2:
            case ShopItem.Worker_3:
            case ShopItem.Worker_4:
            case ShopItem.Worker_5:
            case ShopItem.Worker_6:
                return true;
            default:
                return false;
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

        soundMgr.PlaySE(Sound.GetMoney_SE);
        gameMgr.playData.money -= shopData.money;
        gameMgr.playData.hasItem[(int)shopItem] = true;

        shopUI.SetMoney();
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
            case ShopItem.Worker_1:
            case ShopItem.Worker_2:
            case ShopItem.Worker_3:
            case ShopItem.Worker_4:
            case ShopItem.Worker_5:
            case ShopItem.Worker_6:
                UseShopItem(
                    ShopItem.Worker_1, ShopItem.Worker_2, 
                    ShopItem.Worker_3, ShopItem.Worker_4, 
                    ShopItem.Worker_5, ShopItem.Worker_6);
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

        UnActShopItem(unActiveItem);

        gameMgr.playData.useItem[(int)shopItem] = true;


        scrollRect.RefreshCells();
    }

    private void UnActShopItem(params ShopItem[] unActiveItem)
    {
        foreach (ShopItem item in unActiveItem)
            gameMgr.playData.useItem[(int)item] = false;
    }
}
