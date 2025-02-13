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
        //�ν����ͷ� ��� ����ϴ� �Լ�
        soundMgr.PlaySE(Sound.Btn_SE);
        gameObject.SetActive(false);
        sceneMgr.SceneLoad(Scene.INGAME,false, false, SceneChangeAni.FADE);
    }

    public void OpenNo()
    {
        //�ν����ͷ� ��� ����ϴ� �Լ�
        soundMgr.PlaySE(Sound.Btn_SE);
        gameObject.SetActive(false);

    }
}
