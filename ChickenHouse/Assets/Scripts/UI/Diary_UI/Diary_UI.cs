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

    private DiaryMenu nowMenu;

    public void Set_UI()
    {
        gameObject.SetActive(true);

        //�ݱ� ��ư ó��
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
        //�ν����ͷ� ����� ���� �Լ���
        Set_UI((DiaryMenu)menuIdx);
    }

    public void Set_UI(DiaryMenu pMenu)
    {
        nowMenu = pMenu;
        postIt.OnPostIt(nowMenu);

        quest.SetState(false);
        book.SetState(false);

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
                break;
        }
    }
}
