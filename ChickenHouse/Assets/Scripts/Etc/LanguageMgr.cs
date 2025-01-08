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
            //�̹� �ε尡 �Ϸ�Ǿ��ִ�.
            yield break;
        }

        //,�� �������� ����� csv������ �д´�.
        TextAsset textAsset = Resources.Load<TextAsset>(filePath);
        if (textAsset == null)
            yield break;

        //���� ������.
        string[] rows = textAsset.text.Split('\n');
        List<string> rowList = new List<string>();
        for (int i = 0; i < rows.Length; i++)
        {
            if (string.IsNullOrEmpty(rows[i]))
            {
                //�ƹ��͵� ���� ��ü
                continue;
            }
            string row = rows[i].Replace("\r", string.Empty);
            row = row.Trim();
            rowList.Add(rows[i]);
        }

        //������
        string[] subjects = rowList[0].Split("\t");

        for (int r = 1; r < rowList.Count; r++)
        {
            //�ش� �ٺ��� �����ʹ�.
            string[] values = rowList[r].Split("\t");

            //Ű���� ��´�.
            string keyValue = values[0].Replace('\r', ' ').Trim();

            for (int c = 1; c < values.Length; c++)
            {
                //�ش�ĭ�� �� �����´�.
                subjects[c] = subjects[c].Replace('\r', ' ').Trim();
                Language language = (Language)Enum.Parse(typeof(Language), subjects[c]);
                if (languageData.ContainsKey(language) == false)
                    languageData.Add(language, new Dictionary<string, string>());

                //������ȯ�Ѵ�.
                values[c] = values[c].Replace('\r', ' ').Trim();

                //�����͸� �߰��Ѵ�.
                languageData[language].Add(keyValue, values[c]);
            }
        }

        isLoad = true;
    }

    public static string GetText(string pKey)
    {
        //Key�� �ش��ϴ� ��� �ؽ�Ʈ�� �ҷ��´�.

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
        //textUI�� �ش��ϴ� TextMeshUI�� ���ڸ� �����ش�.
        //pKey�� �ش��ϴ� ���ڸ� �־���
        string str = GetText(pKey);
        SetText(textUI, str, pLanguagee);
    }

    public static void SetText(TextMeshProUGUI textUI, string pStr)
    {
        SetText(textUI, pStr, Instance.nowLanguage);
    }

    public static void SetText(TextMeshProUGUI textUI, string pStr, Language pLanguagee)
    {
        //textUI�� �ش��ϴ� TextMeshUI�� ���ڸ� �����ش�.
        //pStr�� �״�� �־���
        textUI.text = pStr;

        //�� �´� ��Ʈ�� �������ش�.
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
