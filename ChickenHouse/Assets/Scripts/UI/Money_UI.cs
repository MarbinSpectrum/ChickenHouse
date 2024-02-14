using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Money_UI : Mgr
{
    [SerializeField] private TextMeshProUGUI textMesh;

    public void SetMoney(long v)
    {
        string strNum = string.Format("{0:N0} $", v);
        LanguageMgr.SetText(textMesh, strNum);
    }
}
