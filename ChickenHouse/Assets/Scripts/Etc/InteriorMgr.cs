using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteriorMgr : AwakeSingleton<InteriorMgr>
{
    private Dictionary<InteriorItem, InteriorData> interiorData = new();

    private static bool init = false;

    protected override void Awake()
    {
        base.Awake();

        if (init)
            return;

        init = true;

        for (InteriorItem interior = InteriorItem.Interior_Wall_0; interior < InteriorItem.MAX; interior++)
        {
            InteriorData iData = Resources.Load<InteriorData>($"InteriorData/{interior.ToString()}");
            if (iData == null)
                continue;
            interiorData.Add(interior, iData);
        }
    }

    public InteriorData GetInteriorData(InteriorItem pInteriorItem)
    {
        //인테리어 소품 정보 얻기
        if (interiorData.ContainsKey(pInteriorItem))
            return interiorData[pInteriorItem];
        return null;
    }

    public int GetInteriorItemPrice(InteriorItem pInteriorItem)
    {
        //인테리어 소품 가격
        InteriorData interiorData = GetInteriorData(pInteriorItem);
        if (interiorData == null)
            return 0;
        return interiorData.price;
    }

    public static InteriorItem GetShopItemToInteriorItem(ShopItem shopItem)
    {
        //ShopItem -> InteriorItem
        switch (shopItem)
        {
            case ShopItem.Interior_Wall_0:
                return InteriorItem.Interior_Wall_0;
            case ShopItem.Interior_Wall_1:
                return InteriorItem.Interior_Wall_1;
            case ShopItem.Interior_Wall_2:
                return InteriorItem.Interior_Wall_2;
            case ShopItem.Interior_Wall_3:
                return InteriorItem.Interior_Wall_3;
            case ShopItem.Interior_Wall_4:
                return InteriorItem.Interior_Wall_4;

            case ShopItem.Interior_Desk_0:
                return InteriorItem.Interior_Desk_0;
            case ShopItem.Interior_Desk_1:
                return InteriorItem.Interior_Desk_1;
            case ShopItem.Interior_Desk_2:
                return InteriorItem.Interior_Desk_2;
            case ShopItem.Interior_Desk_3:
                return InteriorItem.Interior_Desk_3;
            case ShopItem.Interior_Desk_4:
                return InteriorItem.Interior_Desk_4;

            case ShopItem.Interior_Floor_0:
                return InteriorItem.Interior_Floor_0;
            case ShopItem.Interior_Floor_1:
                return InteriorItem.Interior_Floor_1;
            case ShopItem.Interior_Floor_2:
                return InteriorItem.Interior_Floor_2;
            case ShopItem.Interior_Floor_3:
                return InteriorItem.Interior_Floor_3;
            case ShopItem.Interior_Floor_4:
                return InteriorItem.Interior_Floor_4;

            case ShopItem.Interior_Table_0:
                return InteriorItem.Interior_Table_0;
            case ShopItem.Interior_Table_1:
                return InteriorItem.Interior_Table_1;
            case ShopItem.Interior_Table_2:
                return InteriorItem.Interior_Table_2;
            case ShopItem.Interior_Table_3:
                return InteriorItem.Interior_Table_3;
            case ShopItem.Interior_Table_4:
                return InteriorItem.Interior_Table_4;
        }
        return InteriorItem.None;
    }

    public static ShopItem GetInteriorItemToShopItem(InteriorItem pInteriorItem)
    {
        //InteriorItem -> ShopItem
        switch (pInteriorItem)
        {
            case InteriorItem.Interior_Wall_0:
                return ShopItem.Interior_Wall_0;
            case InteriorItem.Interior_Wall_1:
                return ShopItem.Interior_Wall_1;
            case InteriorItem.Interior_Wall_2:
                return ShopItem.Interior_Wall_2;
            case InteriorItem.Interior_Wall_3:
                return ShopItem.Interior_Wall_3;
            case InteriorItem.Interior_Wall_4:
                return ShopItem.Interior_Wall_4;

            case InteriorItem.Interior_Desk_0:
                return ShopItem.Interior_Desk_0;
            case InteriorItem.Interior_Desk_1:
                return ShopItem.Interior_Desk_1;
            case InteriorItem.Interior_Desk_2:
                return ShopItem.Interior_Desk_2;
            case InteriorItem.Interior_Desk_3:
                return ShopItem.Interior_Desk_3;
            case InteriorItem.Interior_Desk_4:
                return ShopItem.Interior_Desk_4;

            case InteriorItem.Interior_Floor_0:
                return ShopItem.Interior_Floor_0;
            case InteriorItem.Interior_Floor_1:
                return ShopItem.Interior_Floor_1;
            case InteriorItem.Interior_Floor_2:
                return ShopItem.Interior_Floor_2;
            case InteriorItem.Interior_Floor_3:
                return ShopItem.Interior_Floor_3;
            case InteriorItem.Interior_Floor_4:
                return ShopItem.Interior_Floor_4;

            case InteriorItem.Interior_Table_0:
                return ShopItem.Interior_Table_0;
            case InteriorItem.Interior_Table_1:
                return ShopItem.Interior_Table_1;
            case InteriorItem.Interior_Table_2:
                return ShopItem.Interior_Table_2;
            case InteriorItem.Interior_Table_3:
                return ShopItem.Interior_Table_3;
            case InteriorItem.Interior_Table_4:
                return ShopItem.Interior_Table_4;
        }
        return ShopItem.None;
    }
}
