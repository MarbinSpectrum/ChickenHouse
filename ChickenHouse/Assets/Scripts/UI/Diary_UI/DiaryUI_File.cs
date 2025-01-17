using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DiaryUI_File : Mgr
{
    [SerializeField] private RectTransform[]            fileRect;

    [SerializeField] private List<DiaryUI_FileSaveSlot> saveSlot = new List<DiaryUI_FileSaveSlot>();
    [SerializeField] private DiaryUI_FilePlayerInfo     filePlayerInfo;
    [SerializeField] private ScrollRect                 srollRect;

    public void SetUI()
    {
        PlayData[] playDatas = new PlayData[saveSlot.Count];
        for (int i = 0; i < saveSlot.Count; i++)
        {
            PlayData data = gameMgr.LoadData(i);
            playDatas[i] = data;
        }
        int lineCnt = saveSlot.Count / 2 - 3;

        for (int i = 0; i < playDatas.Length; i++)
            saveSlot[i].SetUI(playDatas[i], i);

        filePlayerInfo.SetUI(gameMgr.playData);

        srollRect.onValueChanged.RemoveAllListeners();
        srollRect.onValueChanged.AddListener((x) =>
        {
            for (int i = 0; i <= lineCnt; i++)
            {
                float s = Mathf.Max(((lineCnt - i) * 2 - 1) / (float)lineCnt / 2f, 0);
                float e = Mathf.Min(((lineCnt - i) * 2 + 1) / (float)lineCnt / 2f, 1);
                if (s < x.y && x.y <= e)
                {
                    float newYPos = 160 * i;
                    srollRect.verticalScrollbar.value = (lineCnt - i) / (float)lineCnt;
                    srollRect.content.anchoredPosition = new Vector2(srollRect.content.anchoredPosition.x, newYPos);
                    break;
                }
            }
        });

        srollRect.verticalScrollbar.value = 1;
    }

    public void SetState(bool state)
    {
        for (int i = 0; i < fileRect.Length; i++)
            fileRect[i].gameObject.SetActive(state);
    }
}
