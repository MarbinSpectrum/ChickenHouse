using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenPackList : Mgr
{
    [SerializeField] private List<ChickenPack> chickenPacks = new List<ChickenPack>();

    public bool AddChickenPack(int pChickenCnt, ChickenState pChickenState, ChickenSpicy spicy0, ChickenSpicy spicy1
        ,bool pMode, float pLerpValue)
    {
        //남는 공간에 치킨을 넣는다.

        foreach(ChickenPack pack in chickenPacks)
        {
            if (pack.PackCkicken(pChickenCnt, pChickenState, spicy0, spicy1))
            {
                pack.Set_ChickenShader(pMode, pLerpValue);
                pack.UpdatePack();
                return true;
            }
        }
        return false;
    }
}
