using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Title_UI : Mgr
{
    [SerializeField] private Button         loadGameBtn;
    [SerializeField] private Button         newGameBtn;
    [SerializeField] private SaveSlot_UI    saveSlotUI;

    [SerializeField] private Button         optionBtn;
    [SerializeField] private Option_UI      optionUI;

    private List<PlayData> playDatas = new List<PlayData>();
    private void Awake()
    {
        for (int i = 0; i < SaveSlot_UI.MAX_SLOT; i++)
        {
            PlayData data = gameMgr.LoadData(i);
            playDatas.Add(data);
        }

        loadGameBtn.gameObject.SetActive(playDatas[0] != null);
        loadGameBtn.onClick.RemoveAllListeners();
        loadGameBtn.onClick.AddListener(() => LoadGame());

        newGameBtn.onClick.RemoveAllListeners();
        newGameBtn.onClick.AddListener(() => NewGame());

        optionBtn.onClick.RemoveAllListeners();
        optionBtn.onClick.AddListener(() => optionUI.Set_UI());

        soundMgr.PlayBGM(Sound.Title_BG);

    }

    private void NewGame()
    {
        gameMgr.DeleteData(0);
        gameMgr.selectSaveSlot = 0;
        sceneMgr.SceneLoad(Scene.PROLOGUE, SceneChangeAni.FADE);
    }

    private void LoadGame()
    {
        gameMgr.selectSaveSlot = 0;
        sceneMgr.SceneLoad(Scene.INGAME, SceneChangeAni.FADE);

        return;

        saveSlotUI.SetSlot_UI(playDatas);
    }
}
