using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DiaryUI_PostIt : Mgr
{
    //일기장 포스트잇

    [System.Serializable]
    public struct POSTIT
    {
        public TextMeshProUGUI label;
        public Image[] imgs;
    }

    [SerializeField] private POSTIT[] postIt;

    private const string QUEST_TAB_KEY  = "QUEST";
    private const string BOOK_TAB_KEY   = "BOOK";
    private const string FILE_TAB_KEY   = "FILE";

    public void OnPostIt(DiaryMenu pMenu)
    {
        int idx = (int)pMenu;

        for (int i = 0; i < postIt.Length; i++)
        {
            //idx에 해당하는 포스트잇만 할성화
            bool act = (idx == i);
            postIt[i].imgs[0].enabled = act;
            postIt[i].imgs[1].enabled = !act;

            //포스트 글자 세팅
            string tabStr = string.Empty;
            switch((DiaryMenu)i)
            {
                case DiaryMenu.Quest:
                    tabStr = LanguageMgr.GetText(QUEST_TAB_KEY);
                    break;
                case DiaryMenu.Book:
                    tabStr = LanguageMgr.GetText(BOOK_TAB_KEY);
                    break;
                case DiaryMenu.File:
                    tabStr = LanguageMgr.GetText(FILE_TAB_KEY);
                    break;
            }

            //탭 글씨
            string tab = string.Empty;
            for(int j = 0; j < tabStr.Length; j++)
            {
                tab += tabStr[j];
                if(j + 1 < tabStr.Length)
                    tab += "<br>";
            }
            postIt[i].label.text = tab;
        }
    }

}
