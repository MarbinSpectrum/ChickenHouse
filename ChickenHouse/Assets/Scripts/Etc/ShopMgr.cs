using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMgr : AwakeSingleton<ShopMgr>
{
    private Dictionary<ShopItem, ShopData> shopData = new();
    private static bool init = false;

    protected override void Awake()
    {
        base.Awake();

        if (init)
            return;
        init = true;
        for (ShopItem guest = ShopItem.OIL_Zone_1; guest < ShopItem.MAX; guest++)
        {
            ShopData sData = Resources.Load<ShopData>($"ShopData/{guest.ToString()}");
            if (sData == null)
                continue;
            shopData.Add(guest, sData);
        }
    }

    public ShopData GetShopData(ShopItem pShopItem)
    {
        //상점 정보 얻기
        if (shopData.ContainsKey(pShopItem))
            return shopData[pShopItem];
        return null;
    }
}
