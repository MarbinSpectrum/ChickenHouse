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

    private DiaryMenu nowMenu;

    public void Set_UI()
    {
        //if (tutoMgr.NowRunTuto())
        //    return;

        soundMgr.PlaySE(Sound.Page_SE);

        gameObject.SetActive(true);

        //닫기 버튼 처리
        closeBtn.onClick.RemoveAllListeners();
        closeBtn.onClick.AddListener(() =>
        {
            gameMgr.OptionStopGame(false);
            gameObject.SetActive(false);
        });

        Set_UI(DiaryMenu.Quest);
    }

    public void Set_UI(int menuIdx)
    {
        //인스펙터로 끌어다 쓰는 함수임
        Set_UI((DiaryMenu)menuIdx);
        soundMgr.PlaySE(Sound.Paper_SE);
    }

    public void Set_UI(DiaryMenu pMenu)
    {
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
}
