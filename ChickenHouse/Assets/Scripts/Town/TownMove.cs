using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownMove : Mgr
{
    //타운 맵 이동에 쓰는 스크립트

    [SerializeField] private TownMap    townMap;
    [SerializeField] private Town       town;

    public void MoveTown() => town.TownMapLoad(townMap);
}
