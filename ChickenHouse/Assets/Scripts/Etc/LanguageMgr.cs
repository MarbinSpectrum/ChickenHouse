using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LanguageMgr : AwakeSingleton<LanguageMgr>
{
    [SerializeField] private TMP_FontAsset KoreaFont;
    [SerializeField] private TMP_FontAsset EnglishFont;

    private bool isLoad = false;

    public string   filePath;
    public Language nowLanguage;

    private Dictionary<Language, Dictionary<string, string>> languageData
        = new Dictionary<Language, Dictionary<string, string>>();

    private const string LANGUAGE_KEY = "LANGUAGE";
    private const string MONEY_FORMAT = "{0:N0}<size={1}><b>C</b></size>";

    protected override void Awake()
    {
        base.Awake();
        Language loadLan = (Language)PlayerPrefs.GetInt(LANGUAGE_KEY, 0);
        if(loadLan == Language.NONE)
            loadLan = Language.Korea;
        ChangeLanguage(loadLan);
        StartCoroutine(runLoadData());
    }

    public IEnumerator runLoadData()
    {
        if (isLoad)
        {
            //이미 로드가 완료되어있다.
            yield break;
        }

        //,자 형식으로 저장된 csv파일을 읽는다.
        TextAsset textAsset = Resources.Load<TextAsset>(filePath);
        if (textAsset == null)
            yield break;

        //줄을 나눈다.
        string[] rows = textAsset.text.Split('\n');
        List<string> rowList = new List<string>();
        for (int i = 0; i < rows.Length; i++)
        {
            if (string.IsNullOrEmpty(rows[i]))
            {
                //아무것도 없는 객체
                continue;
            }
            string row = rows[i].Replace("\r", string.Empty);
            row = row.Trim();
            rowList.Add(rows[i]);
        }

        //제목줄
        string[] subjects = rowList[0].Split("\t");

        for (int r = 1; r < rowList.Count; r++)
        {
            //해당 줄부터 데이터다.
            string[] values = rowList[r].Split("\t");

            //키값을 얻는다.
            string keyValue = values[0].Replace('\r', ' ').Trim();

            for (int c = 1; c < values.Length; c++)
            {
                //해당칸의 언어를 가져온다.
                subjects[c] = subjects[c].Replace('\r', ' ').Trim();
                Language language = (Language)Enum.Parse(typeof(Language), subjects[c]);
                if (languageData.ContainsKey(language) == false)
                    languageData.Add(language, new Dictionary<string, string>());

                //값을반환한다.
                values[c] = values[c].Replace('\r', ' ').Trim();

                //언어데이터를 추가한다.
                languageData[language].Add(keyValue, values[c]);
            }
        }

        isLoad = true;
    }

    public static string GetText(string pKey)
    {
        //Key에 해당하는 언어 텍스트를 불러온다.

        LanguageMgr languageMgr         = Instance;
        var         languageData        = languageMgr.languageData;
        Language    nowLanguage         = languageMgr.nowLanguage;

        if (languageData.ContainsKey(nowLanguage))
        {
            if (languageData[nowLanguage].ContainsKey(pKey))
            {
                string str = languageData[nowLanguage][pKey];
                str = str.Replace("\\n", "\n");
                return str;
            }
        }
        return string.Empty;
    }

    public void ChangeLanguage(Language pLanguage)
    {
        PlayerPrefs.SetInt(LANGUAGE_KEY, (int)pLanguage);
        nowLanguage = pLanguage;
    }

    public static void SetString(TextMeshProUGUI textUI, string pKey)
    {
        SetString(textUI, pKey, Instance.nowLanguage);
    }

    public static void SetString(TextMeshProUGUI textUI,string pKey, Language pLanguagee)
    {
        //textUI에 해당하는 TextMeshUI의 글자를 정해준다.
        //pKey에 해당하는 문자를 넣어줌
        string str = GetText(pKey);
        SetText(textUI, str, pLanguagee);
    }

    public static void SetText(TextMeshProUGUI textUI, string pStr)
    {
        SetText(textUI, pStr, Instance.nowLanguage);
    }

    public static void SetText(TextMeshProUGUI textUI, string pStr, Language pLanguagee)
    {
        //textUI에 해당하는 TextMeshUI의 글자를 정해준다.
        //pStr를 그대로 넣어줌
        textUI.text = pStr;

        //언어에 맞는 폰트를 설정해준다.
        LanguageMgr languageMgr = Instance;
        TMP_FontAsset fontAsset = null;

        switch (pLanguagee)
        {
            case Language.Korea:
                fontAsset = languageMgr.KoreaFont;
                break;
            case Language.English:
                fontAsset = languageMgr.EnglishFont;
                break;
        }

        textUI.font = fontAsset;
    }

    public static string GetMoneyStr(float fontSize, long money)
    {
        string priceStr = string.Format(MONEY_FORMAT, money, fontSize * 0.6f);
        return priceStr;
    }

    public static void SplitString(string str, ref List<string> strList, ref List<bool> isTagText)
    {
        strList.Clear();
        isTagText.Clear();

        int idx = 0;
        string tagString = string.Empty;
        bool tagMode = false;

        while (idx < str.Length)
        {
            if (tagMode)
            {
                tagString += str[idx];
                if (str[idx] == '>')
                {
                    strList.Add(tagString);
                    isTagText.Add(true);
                    tagString = string.Empty;
                    tagMode = false;
                }
                idx++;
            }
            else
            {
                if (str[idx] == '<')
                {
                    tagMode = true;
                }
                else if (str[idx] == '\n')
                {
                    strList.Add(str[idx].ToString());
                    isTagText.Add(true);
                    idx++;
                }
                else
                {
                    strList.Add(str[idx].ToString());
                    isTagText.Add(false);
                    idx++;
                }
            }
        }
    }
}
