using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Title_UI : Mgr
{
    [SerializeField] private Button         loadGameBtn;
    [SerializeField] private Button         newGameBtn;
    [SerializeField] private Button         quitGameBtn;

    [SerializeField] private Button         optionBtn;
    [SerializeField] private Option_UI      optionUI;

    private void Awake()
    {
        bool hasData = false;
        for (int i = 0; i < 6; i++)
        {
            PlayData data = gameMgr.LoadData(i);
            if (data == null)
                continue;
            hasData = true;
        }
        loadGameBtn.gameObject.SetActive(hasData);

        loadGameBtn.onClick.RemoveAllListeners();
        loadGameBtn.onClick.AddListener(() => LoadGame());

        newGameBtn.onClick.RemoveAllListeners();
        newGameBtn.onClick.AddListener(() => NewGame());

        quitGameBtn.onClick.RemoveAllListeners();
        quitGameBtn.onClick.AddListener(() =>
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        });

        optionBtn.onClick.RemoveAllListeners();
        optionBtn.onClick.AddListener(() => optionUI.Set_UI());

        soundMgr.MuteSE(false);
        soundMgr.PlayBGM(Sound.Title_BG);

    }

    private void NewGame()
    {
        gameMgr.OpenRecordUI(true, false, (saveSlot) =>
        {
            int slotNum = (int)saveSlot;
            gameMgr.selectSaveSlot = slotNum;
            sceneMgr.SceneLoad(Scene.PROLOGUE, false, SceneChangeAni.FADE);
            gameMgr.CloseRecordUI();
        });
    }

    private void LoadGame()
    {
        gameMgr.OpenRecordUI(false, true, (saveSlot) =>
        {
            int slotNum = (int)saveSlot;
            gameMgr.selectSaveSlot = slotNum;
            if(gameMgr.playData.day == 1)
            {
                //1일차는 프롤로그부터 시작
                sceneMgr.SceneLoad(Scene.PROLOGUE, false, SceneChangeAni.FADE);
            }
            else
            {
                //나머지 경우는 타운에서 시작
                sceneMgr.SceneLoad(Scene.TOWN, false, SceneChangeAni.FADE);
            }
            gameMgr.CloseRecordUI();
        });
    }
}
