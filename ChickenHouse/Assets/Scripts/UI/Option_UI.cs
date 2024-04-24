using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Text;
using System;

public class Option_UI : Mgr
{
    private enum OptionMenu
    {
        Main,
        Language,
        Sound,
        Restart,
    }
    
    [SerializeField] private Button closeBtn;

    [SerializeField] private RectTransform  crossIcon;
    [SerializeField] private RectTransform  backIcon;
    [SerializeField] private Button         windowCloseBtn;


    [System.Serializable]
    public struct MainUI
    {
        public RectTransform    baseObj;
        public Button           languageBtn;
        public Button           soundBtn;
    }

    [System.Serializable]
    public struct LanguageUI
    {
        public RectTransform                                    baseObj;
        public ScrollRect                                       scrollRect;
        public Button                                           okBtn;
        public OptionLanguageBtn                                btnPrefab;
        [System.NonSerialized] public List<OptionLanguageBtn>   languageBtns;
    }

    [System.Serializable]
    public struct SoundUI
    {
        public RectTransform baseObj;
    }

    [System.Serializable]
    public struct ReStartUI
    {
        public RectTransform    baseObj;
        public Button           restartBtn;
    }

    [SerializeField] private MainUI         mainUI;
    [SerializeField] private LanguageUI     languageUI;
    [SerializeField] private SoundUI        soundUI;
    [SerializeField] private ReStartUI      restartUI;

    private OptionMenu  nowMenu;
    private Language    selectLan;
    public void Set_UI()
    {
        gameObject.SetActive(true);
        SetButtonEvent();
        OpenMenu(OptionMenu.Main);
    }

    private void SetButtonEvent()
    {
        //버튼 이벤트 등록

        //닫기 버튼 처리
        closeBtn.onClick.RemoveAllListeners();
        closeBtn.onClick.AddListener(() => gameObject.SetActive(false));
        windowCloseBtn.onClick.RemoveAllListeners();
        windowCloseBtn.onClick.AddListener(() =>
        {
            if(nowMenu == OptionMenu.Main)
                gameObject.SetActive(false);
            else
                OpenMenu(OptionMenu.Main);
        });

        //메인 버튼
        mainUI.languageBtn.onClick.RemoveAllListeners();
        mainUI.languageBtn.onClick.AddListener(() =>
        {
            OpenMenu(OptionMenu.Language);
        });
        mainUI.soundBtn.onClick.RemoveAllListeners();
        mainUI.soundBtn.onClick.AddListener(() =>
        {
            OpenMenu(OptionMenu.Sound);
        });

        //언어 버튼
        languageUI.okBtn.onClick.RemoveAllListeners();
        languageUI.okBtn.onClick.AddListener(() =>
        {
            OpenMenu(OptionMenu.Restart);
        });

        //재시작 버튼
        restartUI.restartBtn.onClick.RemoveAllListeners();
        restartUI.restartBtn.onClick.AddListener(() =>
        {
            if (lanMgr.nowLanguage == selectLan)
                return;
            lanMgr.ChangeLanguage(selectLan);
            sceneMgr.SceneLoad(Scene.LOGO);
        });
    }

    private void OpenMenu(OptionMenu optionMenu)
    {
        nowMenu = optionMenu;
        switch (optionMenu)
        {
            case OptionMenu.Main:
                {
                    mainUI.baseObj.gameObject.SetActive(true);
                    languageUI.baseObj.gameObject.SetActive(false);
                    soundUI.baseObj.gameObject.SetActive(false);
                    restartUI.baseObj.gameObject.SetActive(false);

                    crossIcon.gameObject.SetActive(true);
                    backIcon.gameObject.SetActive(false);
                }
                break;
            case OptionMenu.Language:
                {
                    selectLan = lanMgr.nowLanguage;

                    mainUI.baseObj.gameObject.SetActive(false);
                    languageUI.baseObj.gameObject.SetActive(true);
                    soundUI.baseObj.gameObject.SetActive(false);
                    restartUI.baseObj.gameObject.SetActive(false);

                    crossIcon.gameObject.SetActive(false);
                    backIcon.gameObject.SetActive(true);

                    languageUI.languageBtns ??= new List<OptionLanguageBtn>();
                    languageUI.languageBtns.ForEach((x) => x.gameObject.SetActive(false));

                    //언어 버튼 설정
                    UpdateLanuageBtns();

                    languageUI.scrollRect.verticalNormalizedPosition = 0;
                }
                break;
            case OptionMenu.Sound:
                {
                    mainUI.baseObj.gameObject.SetActive(false);
                    languageUI.baseObj.gameObject.SetActive(false);
                    soundUI.baseObj.gameObject.SetActive(true);
                    restartUI.baseObj.gameObject.SetActive(false);

                    crossIcon.gameObject.SetActive(false);
                    backIcon.gameObject.SetActive(true);
                }
                break;
            case OptionMenu.Restart:
                {
                    mainUI.baseObj.gameObject.SetActive(false);
                    languageUI.baseObj.gameObject.SetActive(false);
                    soundUI.baseObj.gameObject.SetActive(false);
                    restartUI.baseObj.gameObject.SetActive(true);
                }
                break;
        }
    }

    private void UpdateLanuageBtns()
    {
        int idx = 0;
        foreach (Language lan in Enum.GetValues(typeof(Language)))
        {
            if (lan == Language.NONE)
                continue;

            if (idx >= languageUI.languageBtns.Count)
            {
                languageUI.languageBtns.Add(Instantiate(languageUI.btnPrefab, languageUI.scrollRect.content));
            }
            OptionLanguageBtn languageBtn = languageUI.languageBtns[idx];
            languageBtn.gameObject.SetActive(true);
            languageBtn.Set_UI(lan, selectLan, (newLan) =>
            {
                selectLan = (Language)newLan;
                UpdateLanuageBtns();
            });
            idx++;
        }
    }
}
