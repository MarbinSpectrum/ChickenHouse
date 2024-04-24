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
        //��� �ؽ�Ʈ ����
        switch(pLanguage)
        {
            case Language.Korea:
                LanguageMgr.SetString(languageText, "KOREAN", pLanguage);
                break;
            case Language.English:
                LanguageMgr.SetString(languageText, "ENGLISH", pLanguage);
                break;
        }

        //��ư �̹��� ����
        if (pLanguage == pSelectLanguage)
            btn.image.material = null;
        else
            btn.image.material = mat;

        //��ư ����
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(() =>
        {
            //��� ����
            callback?.Invoke(pLanguage);
        });
    }
}
