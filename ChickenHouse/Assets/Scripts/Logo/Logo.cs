using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logo : Mgr
{
    private void Start()
    {
        sceneMgr.SceneLoad(Scene.TITLE,false, SceneChangeAni.CIRCLE);
    }
}
