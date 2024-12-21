using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Money_UI : Mgr
{
    [SerializeField] private TextMeshProUGUI textMesh;

    public void SetMoney(long v)
    {
        string moneyStr = LanguageMgr.GetMoneyStr(textMesh.fontSize, v);
        LanguageMgr.SetText(textMesh, moneyStr);
    }
}
