using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LanguageMgr : AwakeSingleton<LanguageMgr>
{
    [SerializeField] private Dictionary<Language, TMP_FontAsset> normalFont;
    [SerializeField] private Dictionary<Language, TMP_FontAsset> outLineFont;

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
            rowList.Add(row);
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

    public static void SetString(TextMeshProUGUI textUI, string pKey, bool isOutLine = false)
    {
        SetString(textUI, pKey, Instance.nowLanguage, isOutLine);
    }

    public static void SetString(TextMeshProUGUI textUI,string pKey, Language pLanguage, bool isOutLine = false)
    {
        //textUI에 해당하는 TextMeshUI의 글자를 정해준다.
        //pKey에 해당하는 문자를 넣어줌
        string str = GetText(pKey);
        SetText(textUI, str, pLanguage, isOutLine);
    }

    public static void SetText(TextMeshProUGUI textUI, string pStr, bool isOutLine = false)
    {
        SetText(textUI, pStr, Instance.nowLanguage, isOutLine);
    }

    public static void SetText(TextMeshProUGUI textUI, string pStr, Language pLanguage, bool isOutLine = false)
    {
        //textUI에 해당하는 TextMeshUI의 글자를 정해준다.
        //pStr를 그대로 넣어줌
        textUI.text = pStr;

        //언어에 맞는 폰트를 설정해준다.
        LanguageMgr languageMgr = Instance;
        TMP_FontAsset fontAsset = null;
        if (isOutLine && languageMgr.outLineFont.ContainsKey(pLanguage))
            fontAsset = languageMgr.outLineFont[pLanguage];
        else if (languageMgr.normalFont.ContainsKey(pLanguage))
            fontAsset = languageMgr.normalFont[pLanguage];

        if (fontAsset == null)
            return;
        textUI.font = fontAsset;
    }

    public static string GetMoneyStr(float fontSize, long money)
    {
        string priceStr = string.Format(MONEY_FORMAT, money, fontSize * 0.6f);
        return priceStr;
    }

    public static void SplitStringbyTag(string str, ref List<string> strList, ref List<bool> isTagText)
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

    public static string GetSmartText(string str, float remainWidth, TextMeshProUGUI textUI)
    {
        //마지막줄이 remainWidth이하만큼 남으면 윗줄이랑 Merge함

        List<string>    tempList    = new List<string>();
        List<bool>      isTagText   = new List<bool>();
        SplitStringbyTag(str, ref tempList, ref isTagText);
        string  tempString      = string.Empty;
        float   textRectWidth   = Mathf.Abs(textUI.GetComponent<RectTransform>().sizeDelta.x);

        string newTextUIStr = string.Empty;
        for (int i = 0; i < tempList.Count; i++)
        {
            tempString += tempList[i];
            if (isTagText[i])
                continue;

            textUI.text = tempString;
            textUI.ForceMeshUpdate();

            float nowTextWidth = 0;
            string nowTextString = string.Empty;
            List<string> arrayStr = new List<string>();
            List<float> arrayWidth = new List<float>();
            int charIdx = 0;
            for (int j = 0; j <= i; j++)
            {
                if (isTagText[j])
                {
                    nowTextString += tempList[j];
                    if (tempList[j] == "\n")
                    {
                        arrayStr.Add(nowTextString);
                        arrayWidth.Add(nowTextWidth);
                        nowTextWidth = 0;
                        nowTextString = string.Empty;
                    }
                }
                else
                {
                    TMP_CharacterInfo charInfo = textUI.textInfo.characterInfo[charIdx];

                    float charWidth = Mathf.Abs(charInfo.topRight.x - charInfo.topLeft.x);
                    if (nowTextWidth + charWidth > textRectWidth && tempList[j] == " ")
                    {
                        nowTextString += "\n";
                        arrayStr.Add(nowTextString);
                        arrayWidth.Add(nowTextWidth);
                        nowTextWidth = 0;
                        nowTextString = string.Empty;
                    }
                    else
                    {
                        nowTextWidth += charWidth;
                        nowTextString += tempList[j];
                    }

                    charIdx++;
                }
            }

            if (nowTextString != string.Empty)
            {
                nowTextString += "\n";
                arrayStr.Add(nowTextString);
                arrayWidth.Add(nowTextWidth);
                nowTextWidth = 0;
                nowTextString = string.Empty;
            }

            if (i == tempList.Count - 1)
            {
                for (int j = arrayWidth.Count - 1; j >= 1; j--)
                {
                    if (arrayWidth.Count >= 2 && arrayWidth[j] < remainWidth)
                    {
                        arrayWidth[j - 1] += arrayWidth[j];
                        //substring을 넣는이유는 뒤에 붙어 있는 \n을 넣지않기 위해서임
                        arrayStr[j - 1] = arrayStr[j - 1].Substring(0, arrayStr[j - 1].Length - 1);
                        arrayStr[j - 1] += arrayStr[j];
                        arrayWidth.RemoveAt(j);
                        arrayStr.RemoveAt(j);
                    }
                }

                float avgNewFontSize = 0;


                for (int j = 0; j < arrayWidth.Count; j++)
                {
                    float newFontSize = Mathf.Min(textUI.fontSize, textUI.fontSize * textRectWidth / arrayWidth[j]);
                    avgNewFontSize += newFontSize;
                }
                avgNewFontSize /= arrayWidth.Count;

                for (int j = 0; j < arrayWidth.Count; j++)
                {
                    float newFontSize = Mathf.Min(avgNewFontSize, textUI.fontSize * textRectWidth / arrayWidth[j]);
                    arrayStr[j] = string.Format("<size={0}>{1}</size>", newFontSize, arrayStr[j]);
                }
            }

            newTextUIStr = string.Empty;
            for (int j = 0; j < arrayStr.Count; j++)
                newTextUIStr += arrayStr[j];
        }

        return newTextUIStr;
    }
}
