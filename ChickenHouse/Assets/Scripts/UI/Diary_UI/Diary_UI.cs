using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Diary_UI : Mgr
{
    [SerializeField] private Button closeBtn;
    [SerializeField] private DiaryUI_PostIt postIt;
    [SerializeField] private DiaryUI_Book   book;
    [SerializeField] private DiaryUI_Quest  quest;
    [SerializeField] private DiaryUI_File   file;
    [SerializeField] private RectTransform dontClick;
    private DiaryMenu nowMenu;

    public bool isOpen { private set; get; }
    public void Set_UI()
    {
        nowMenu = (DiaryMenu)( -1);
        if (tutoMgr.NowRunTuto())
            return;

        soundMgr.PlaySE(Sound.Page_SE);

        gameObject.SetActive(true);

        //닫기 버튼 처리
        closeBtn.onClick.RemoveAllListeners();
        closeBtn.onClick.AddListener(() =>
        {
            CloseUI(true);
        });

        isOpen = true;

        Set_UI(DiaryMenu.Quest);
    }

    public void CloseUI(bool pAll)
    {
        if (pAll)
        {
            file.CloseUI();
            gameMgr.OptionStopGame(false);
            gameObject.SetActive(false);
            isOpen = false;
            return;
        }

        if (file.saveMenuAct)
            file.CloseUI();
        else
        {
            gameMgr.OptionStopGame(false);
            gameObject.SetActive(false);
            isOpen = false;
        }
    }

    public void Set_UI(int menuIdx)
    {
        //인스펙터로 끌어다 쓰는 함수임
        if (nowMenu == (DiaryMenu)menuIdx)
            return;
        Set_UI((DiaryMenu)menuIdx);
        soundMgr.PlaySE(Sound.Paper_SE);
    }

    public void Set_UI(DiaryMenu pMenu)
    {
        if (nowMenu == pMenu)
            return;
        nowMenu = pMenu;
        postIt.OnPostIt(nowMenu);

        quest.SetState(false);
        book.SetState(false);
        file.SetState(false);

        switch (nowMenu)
        {
            case DiaryMenu.Quest:
                quest.SetUI();
                quest.SetState(true);
                break;
            case DiaryMenu.Book:
                book.SetUI();
                book.SetState(true);
                break;
            case DiaryMenu.File:
                file.SetUI();
                file.SetState(true);
                break;
        }
    }

    public void TutoEventRun1()
    {
        //인스펙터로 끌어서 사용하는 함수
        ActDonClick(true);
        gameObject.SetActive(true);
        Set_UI(DiaryMenu.File);
    }

    public void TutoEventRun2()
    {
        //인스펙터로 끌어서 사용하는 함수
        ActDonClick(true);
        gameObject.SetActive(true);
        Set_UI(DiaryMenu.Quest);
    }

    public void TutoEventRun3()
    {
        //인스펙터로 끌어서 사용하는 함수
        ActDonClick(false);
        gameObject.SetActive(false);
        if(gameMgr.playData.tutoComplete4 == false)
        {
            //튜토리얼 완료
            gameMgr.playData.tutoComplete4 = true;
        }
    }

    public void ActDonClick(bool state) => dontClick.gameObject.SetActive(state);
}
