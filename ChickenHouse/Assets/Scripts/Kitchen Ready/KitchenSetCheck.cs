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
        //�ν����ͷ� ��� ����ϴ� �Լ�
        gameObject.SetActive(false);
        fun?.Invoke();
    }

    public void OepnNo()
    {
        //�ν����ͷ� ��� ����ϴ� �Լ�
        gameObject.SetActive(false);
    }
}
