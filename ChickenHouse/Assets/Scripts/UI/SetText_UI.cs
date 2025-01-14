using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetText_UI : Mgr
{
    [SerializeField] private TextMeshProUGUI textUI;
    [SerializeField] private string stringKey;
    [SerializeField] private bool isOutLine;

    private void Awake()
    {
        LanguageMgr.SetString(textUI, stringKey, isOutLine);
    }

    public void SetText(string pStrKey)
    {
        stringKey = pStrKey;
        LanguageMgr.SetString(textUI, stringKey, isOutLine);
    }
}
