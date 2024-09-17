using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpicyMgr : AwakeSingleton<SpicyMgr>
{
    [SerializeField] private Dictionary<ChickenSpicy, SpicyData> spicyData = new Dictionary<ChickenSpicy, SpicyData>();

    public SpicyData GetSpicyData(ChickenSpicy pSpicy)
    {
        //양념 정보 얻기
        if (spicyData.ContainsKey(pSpicy))
            return spicyData[pSpicy];
        return null;
    }

    public int GetSpicyPrice(ChickenSpicy pSpicy)
    {
        //양념장 가격
        SpicyData spicyData = GetSpicyData(pSpicy);
        if (spicyData == null)
            return 0;
        return spicyData.price;
    }

    public static DragState GetSpicyDragState(ChickenSpicy pChickenSpicy)
    {
        switch (pChickenSpicy)
        {
            case ChickenSpicy.Hot:
                return DragState.Hot_Spicy;
            case ChickenSpicy.Soy:
                return DragState.Soy_Spicy;
            case ChickenSpicy.Hell:
                return DragState.Hell_Spicy;
            case ChickenSpicy.Prinkle:
                return DragState.Prinkle_Spicy;
            case ChickenSpicy.Carbonara:
                return DragState.Carbonara_Spicy;
            case ChickenSpicy.BBQ:
                return DragState.BBQ_Spicy;
        }
        return DragState.None;
    }

    public static ChickenSpicy GetDragStateSpicy(DragState pDragState)
    {
        switch (pDragState)
        {
            case DragState.Hot_Spicy:
                return ChickenSpicy.Hot;
            case DragState.Soy_Spicy:
                return ChickenSpicy.Soy;
            case DragState.Hell_Spicy:
                return ChickenSpicy.Hell;
            case DragState.Prinkle_Spicy:
                return ChickenSpicy.Prinkle;
            case DragState.Carbonara_Spicy:
                return ChickenSpicy.Carbonara;
            case DragState.BBQ_Spicy:
                return ChickenSpicy.BBQ;
        }
        return ChickenSpicy.None;
    }

    public static ChickenSpicy RecipeGetSpicy(ShopItem shopItem)
    {
        //레시피에 해당하는 양념 반환
        switch (shopItem)
        {
            case ShopItem.Recipe_0:
                return ChickenSpicy.Hot;
            case ShopItem.Recipe_1:
                return ChickenSpicy.Soy;
            case ShopItem.Recipe_2:
                return ChickenSpicy.Hell;
            case ShopItem.Recipe_3:
                return ChickenSpicy.Prinkle;
            case ShopItem.Recipe_4:
                return ChickenSpicy.Carbonara;
            case ShopItem.Recipe_5:
                return ChickenSpicy.BBQ;
        }
        return ChickenSpicy.Not;
    }

    public static ShopItem SpicyGetRecipe(ChickenSpicy pSpicy)
    {
        //양념에 해당하는 레시피 반환
        switch (pSpicy)
        {
            case ChickenSpicy.None:
                return ShopItem.None;
            case ChickenSpicy.Hot:
                return ShopItem.Recipe_0;
            case ChickenSpicy.Soy:
                return ShopItem.Recipe_1;
            case ChickenSpicy.Hell:
                return ShopItem.Recipe_2;
            case ChickenSpicy.Prinkle:
                return ShopItem.Recipe_3;
            case ChickenSpicy.Carbonara:
                return ShopItem.Recipe_4;
            case ChickenSpicy.BBQ:
                return ShopItem.Recipe_5;
        }
        return ShopItem.None;
    }
}
