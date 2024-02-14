using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Title_UI : Mgr
{
    [SerializeField] private Button         saveSlotBtn;
    [SerializeField] private SaveSlot_UI    saveSlotUI;

    private void Awake()
    {
        saveSlotBtn.onClick.RemoveAllListeners();
        saveSlotBtn.onClick.AddListener(() => LoadSaveSlot());
    }

    public void LoadSaveSlot()
    {
        List<PlayData> playDatas = new List<PlayData>();
        for(int i = 0; i < SaveSlot_UI.MAX_SLOT; i++)
        {
            PlayData data = gameMgr.LoadData(i);
            playDatas.Add(data);
        }
        saveSlotUI.SetSlot_UI(playDatas);
    }
}
