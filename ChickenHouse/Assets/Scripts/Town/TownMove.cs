using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownMove : Mgr
{
    //Ÿ�� �� �̵��� ���� ��ũ��Ʈ

    [SerializeField] private TownMap    townMap;
    [SerializeField] private Town       town;

    public void MoveTown() => town.TownMapLoad(townMap);
}
