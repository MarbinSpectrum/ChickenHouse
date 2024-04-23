using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Option_UI : Mgr
{
    [SerializeField] private Button closeBtn;
    [SerializeField] private Button windowCloseBtn;


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
        public RectTransform baseObj;
    }

    [System.Serializable]
    public struct SoundUI
    {
        public RectTransform baseObj;
    }

    [SerializeField] private MainUI         mainUI;
    [SerializeField] private LanguageUI     languageUI;
    [SerializeField] private SoundUI        soundUI;
    public void Set_UI()
    {
        gameObject.SetActive(true);

        mainUI.baseObj.gameObject.SetActive(true);
        languageUI.baseObj.gameObject.SetActive(false);
        soundUI.baseObj.gameObject.SetActive(false);

        SetButtonEvent();
    }

    private void SetButtonEvent()
    {
        //버튼 이벤트 등록

        //닫기 버튼 처리
        closeBtn.onClick.RemoveAllListeners();
        closeBtn.onClick.AddListener(() => gameObject.SetActive(false));
        windowCloseBtn.onClick.RemoveAllListeners();
        windowCloseBtn.onClick.AddListener(() => gameObject.SetActive(false));

        //메인 버튼
        mainUI.languageBtn.onClick.RemoveAllListeners();
        mainUI.languageBtn.onClick.AddListener(() =>
        {
            mainUI.baseObj.gameObject.SetActive(false);
            languageUI.baseObj.gameObject.SetActive(true);
            soundUI.baseObj.gameObject.SetActive(false);
        });
        mainUI.soundBtn.onClick.RemoveAllListeners();
        mainUI.soundBtn.onClick.AddListener(() =>
        {
            mainUI.baseObj.gameObject.SetActive(false);
            languageUI.baseObj.gameObject.SetActive(false);
            soundUI.baseObj.gameObject.SetActive(true);
        });
    }
}
