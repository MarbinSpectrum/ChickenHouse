using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMgr : AwakeSingleton<ShopMgr>
{
    [SerializeField] private Dictionary<ShopItem, ShopData> shopData = new Dictionary<ShopItem, ShopData>();
    [SerializeField] private Dictionary<ShopItem, WorkerData> resumeData = new Dictionary<ShopItem, WorkerData>();

    public ShopData GetShopData(ShopItem pShopItem)
    {
        //���� ���� ���
        if (shopData.ContainsKey(pShopItem))
            return shopData[pShopItem];
        return null;
    }

    public WorkerData GetResumeData(ShopItem pShopItem)
    {
        //�̷¼� ���� ���
        if (resumeData.ContainsKey(pShopItem))
            return resumeData[pShopItem];
        return null;
    }
}
