using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuSetCheck : Mgr
{
    [SerializeField] private TextMeshProUGUI msg;

    public void SetUI(string pStrKey)
    {
        gameObject.SetActive(true);
        LanguageMgr.SetString(msg, pStrKey);
    }

    public void OpenOK()
    {
        //�ν����ͷ� ��� ����ϴ� �Լ�
        gameObject.SetActive(false);
    }
}
