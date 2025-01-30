using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeKeyCheck : Mgr
{
    [SerializeField] private Diary_UI diaryUI;
    [SerializeField] private Memo_UI memoUI;
    [SerializeField] private Town town;
    [SerializeField] private Option_UI option;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
            Run();
    }

    private void Run()
    {
        if(gameMgr != null)
        {
            if (gameMgr.gameRecordOpen)
            {
                gameMgr.CloseRecordUI(false);
                return;
            }
        }

        if(sceneMgr != null)
        {
            if (sceneMgr.nowSceneChange)
                return;
        }

        if (town != null)
        {
            if (town.nowSceneChange)
                return;
        }

        if (diaryUI != null)
        {
            if (diaryUI.isOpen)
            {
                diaryUI.CloseUI(false);
                return;
            }
        }

        if (memoUI != null)
        {
            if (memoUI.isOpen)
            {
                memoUI.CloseMemo();
                return;
            }
        }

        if (option != null)
        {
            if(option.isOpen)
                option.Close_UI();
            else
                option.Set_UI();
            return;
        }
    }
}
