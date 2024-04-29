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
        //정보 설정
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

        //아이콘 설정
        if (upgradeIcon != null)
        {
            upgradeIcon.sprite = shopData.icon;
            upgradeIcon.gameObject.SetActive(true);
        }

        //이름 설정
        if (upgradeName != null)
        {
            LanguageMgr.SetString(upgradeName, shopData.nameKey);
            upgradeName.gameObject.SetActive(true);
        }

        //설명 설정
        if (upgradeInfo != null)
        {
            LanguageMgr.SetString(upgradeInfo, shopData.infoKey);
            upgradeInfo.gameObject.SetActive(true);
        }

        //설명 설정
        if (upgradeMoney != null)
        {
            string strNum = string.Format("{0:N0} $", shopData.money);
            LanguageMgr.SetText(upgradeMoney, strNum);
            upgradeInfo.gameObject.SetActive(true);
        }

        //버튼 설정
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
            //해당 업그레이드가 완료되어있음
            return true;
        }

        //업그레이드가 완료 안되어있음
        return false;
    }

    private void BuyShopItem()
    {
        //업그레이드 버튼

        if (shopData == null)
        {
            //업그레이드 정보가 없다.
            return;
        }

        if(gameMgr.playData.money < shopData.money)
        {
            //비용이 비싸서 구입못함
            return;
        }

        gameMgr.playData.money -= shopData.money;
        gameMgr.playData.hasItem[(int)shopItem] = true;

        upgradeUI.SetMoney();
        SetData(shopItem);
    }
}
