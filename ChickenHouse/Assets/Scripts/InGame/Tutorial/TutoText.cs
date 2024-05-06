using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutoText : Mgr
{
    [SerializeField] private TextMeshProUGUI    text;
    [SerializeField] private GameObject         tutoObj;
    private void Awake()
    {
        tutoMgr.RegistTutoText(this);
    }

    public void ShowText(Tutorial tutotype)
    {
        switch(tutotype)
        {
            case Tutorial.Tuto_1:
                LanguageMgr.SetString(text, "TUTO_1");
                break;
            case Tutorial.Tuto_2:
                LanguageMgr.SetString(text, "TUTO_2");
                break;
            case Tutorial.Tuto_3:
                LanguageMgr.SetString(text, "TUTO_3");
                break;
            case Tutorial.Tuto_4:
                LanguageMgr.SetString(text, "TUTO_4");
                break;
            case Tutorial.Tuto_5_0:
                LanguageMgr.SetString(text, "TUTO_5_0");
                break;
            case Tutorial.Tuto_5_1:
                LanguageMgr.SetString(text, "TUTO_5_1");
                break;
            case Tutorial.Tuto_5_2:
                LanguageMgr.SetString(text, "TUTO_5_2");
                break;
            case Tutorial.Tuto_6:
                LanguageMgr.SetString(text, "TUTO_6");
                break;
            case Tutorial.Tuto_7:
                LanguageMgr.SetString(text, "TUTO_7");
                break;
            case Tutorial.Tuto_8:
                LanguageMgr.SetString(text, "TUTO_8");
                break;
        }
        tutoObj.gameObject.SetActive(true);
    }

    public void CloseText()
    {
        tutoObj.gameObject.SetActive(false);
    }
}
