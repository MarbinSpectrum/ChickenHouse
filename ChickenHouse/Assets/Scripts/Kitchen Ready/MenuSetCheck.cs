using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuSetCheck : Mgr
{
    [SerializeField] private TextMeshProUGUI msg;

    public void SetUI(string pStrKey)
    {
        soundMgr.PlaySE(Sound.Btn_SE);
        gameObject.SetActive(true);
        LanguageMgr.SetString(msg, pStrKey);
    }

    public void OpenOK()
    {
        //�ν����ͷ� ��� ����ϴ� �Լ�
        soundMgr.PlaySE(Sound.Btn_SE);
        gameObject.SetActive(false);
    }
}
