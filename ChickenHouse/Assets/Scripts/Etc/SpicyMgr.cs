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

    public DragState GetSpicyDragState(ChickenSpicy pChickenSpicy)
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

    public ChickenSpicy GetDragStateSpicy(DragState pDragState)
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
}
