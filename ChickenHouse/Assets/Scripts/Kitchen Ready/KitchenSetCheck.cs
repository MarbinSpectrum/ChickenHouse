using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenSetCheck : Mgr
{
    private NoParaDel fun;

    public void SetUI(NoParaDel pFun = null)
    {
        fun = pFun;
        gameObject.SetActive(true);
    }

    public void OpenYes()
    {
        //인스펙터로 끌어서 사용하는 함수
        gameObject.SetActive(false);
        fun?.Invoke();
    }

    public void OepnNo()
    {
        //인스펙터로 끌어서 사용하는 함수
        gameObject.SetActive(false);
    }
}
