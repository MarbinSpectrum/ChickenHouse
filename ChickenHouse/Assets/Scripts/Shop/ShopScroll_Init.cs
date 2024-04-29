using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScroll_Init : LoopScrollInit
{
    [SerializeField] private Shop_UI shopUI;

    public override void ProvideData(Transform transform, int idx)
    {
        ShopSlot_UI upgradeSlot = transform.GetComponent<ShopSlot_UI>();
        if (upgradeSlot == null)
            return;

        upgradeSlot.SetData(shopUI.shopItemList[idx]);
    }
}
