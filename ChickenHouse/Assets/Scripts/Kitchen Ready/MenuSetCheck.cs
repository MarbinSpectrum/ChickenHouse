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
        //인스펙터로 끌어서 사용하는 함수
        gameObject.SetActive(false);
    }
}
