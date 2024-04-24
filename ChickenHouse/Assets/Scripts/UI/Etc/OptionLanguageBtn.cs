using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionLanguageBtn : Mgr
{
    [SerializeField] private TextMeshProUGUI    languageText;
    [SerializeField] private Button             btn;
    [SerializeField] private Material           mat;

    public void Set_UI(Language pLanguage, Language pSelectLanguage, OneParaDel callback)
    {
        //언어 텍스트 설정
        switch(pLanguage)
        {
            case Language.Korea:
                LanguageMgr.SetString(languageText, "KOREAN", pLanguage);
                break;
            case Language.English:
                LanguageMgr.SetString(languageText, "ENGLISH", pLanguage);
                break;
        }

        //버튼 이미지 설정
        if (pLanguage == pSelectLanguage)
            btn.image.material = null;
        else
            btn.image.material = mat;

        //버튼 설정
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(() =>
        {
            //언어 변경
            callback?.Invoke(pLanguage);
        });
    }
}
