using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : AwakeSingleton<GameMgr>
{
    protected override void Awake()
    {
        base.Awake();

        Application.targetFrameRate = 120;
    }
}
