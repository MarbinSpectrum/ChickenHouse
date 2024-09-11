using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestaurantOpenCheck : Mgr
{
    public void SetUI()
    {
        gameObject.SetActive(true);
    }

    public void OpenYes()
    {
        //인스펙터로 끌어서 사용하는 함수
        soundMgr.PlaySE(Sound.Btn_SE);
        gameObject.SetActive(false);
        sceneMgr.SceneLoad(Scene.INGAME,false, true, SceneChangeAni.FADE);
    }

    public void OepnNo()
    {
        //인스펙터로 끌어서 사용하는 함수
        soundMgr.PlaySE(Sound.Btn_SE);
        gameObject.SetActive(false);

    }
}
