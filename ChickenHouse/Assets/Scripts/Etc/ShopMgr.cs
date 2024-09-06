using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMgr : AwakeSingleton<ShopMgr>
{
    [SerializeField] private Dictionary<ShopItem, ShopData> shopData = new Dictionary<ShopItem, ShopData>();

    public ShopData GetShopData(ShopItem pShopItem)
    {
        //���� ���� ���
        if (shopData.ContainsKey(pShopItem))
            return shopData[pShopItem];
        return null;
    }
}
