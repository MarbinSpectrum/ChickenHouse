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
        gameObject.SetActive(false);
        sceneMgr.SceneLoad(Scene.INGAME, false, SceneChangeAni.FADE);
    }

    public void OepnNo()
    {
        //�ν����ͷ� ��� ����ϴ� �Լ�
       gameObject.SetActive(false);

    }
}
