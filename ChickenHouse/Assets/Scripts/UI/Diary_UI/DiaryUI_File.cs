using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DiaryUI_File : Mgr
{
    [SerializeField] private RectTransform[]            fileRect;

    [SerializeField] private List<DiaryUI_FileSaveSlot> saveSlot = new List<DiaryUI_FileSaveSlot>();
    [SerializeField] private DiaryUI_FilePlayerInfo     filePlayerInfo;

    private const int MAX_SLOT = 6;

    public void SetUI()
    {
        PlayData[] playDatas = new PlayData[MAX_SLOT];
        for (int i = 0; i < MAX_SLOT; i++)
        {
            PlayData data = gameMgr.LoadData(i);
            playDatas[i] = data;
        }

        for (int i = 0; i < playDatas.Length; i++)
            saveSlot[i].SetUI(playDatas[i]);

        filePlayerInfo.SetUI(gameMgr.playData);
    }

    public void SetState(bool state)
    {
        for (int i = 0; i < fileRect.Length; i++)
            fileRect[i].gameObject.SetActive(state);
    }
}
