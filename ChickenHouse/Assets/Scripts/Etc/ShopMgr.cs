using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMgr : AwakeSingleton<ShopMgr>
{
    [SerializeField] private Dictionary<ShopItem, ShopData> shopData = new Dictionary<ShopItem, ShopData>();

    public ShopData GetUpgradeData(ShopItem pShopItem)
    {
        //업그레이드 정보 얻기
        if (shopData.ContainsKey(pShopItem))
            return shopData[pShopItem];
        return null;
    }
}
