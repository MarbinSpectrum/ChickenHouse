using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DiaryUI_FileSaveMenu : Mgr
{
    [SerializeField] private RectTransform rect;

    [SerializeField] private RectTransform saveBtn;
    [SerializeField] private RectTransform loadBtn;

    [SerializeField] private RectTransform saveCheck;

    private int slotNum;
    private PlayData playData;
    private DiaryUI_FileSaveSlot saveSlot;

    public void SetUI(int pSlotNum,PlayData pPlayData, DiaryUI_FileSaveSlot pSaveSlot)
    {
        slotNum = pSlotNum;
        playData = pPlayData;
        saveSlot = pSaveSlot;
        if (playData == null)
            loadBtn.gameObject.SetActive(false);
        else
            loadBtn.gameObject.SetActive(true);

        gameObject.SetActive(true);

        rect.transform.position = pSaveSlot.transform.position;
    }

    public void CloseUI()
    {
        //인스펙터로 끌어서 사용하는 함수
        gameObject.SetActive(false);
    }

    public void SaveData()
    {
        //인스펙터로 끌어서 사용하는 함수
        if (playData == null)
        {
            //빈 슬롯
            gameObject.SetActive(false);

            //저장
            gameMgr.selectSaveSlot = slotNum;
            gameMgr.SaveData();
            saveSlot.SetUI(gameMgr.playData, slotNum);
            return;
        }

        saveCheck.gameObject.SetActive(true);
    }

    public void LoadData()
    {
        if (playData == null)
            return;

        gameMgr.selectSaveSlot = slotNum;
        gameMgr.LoadData();

        if (gameMgr.playData.day == 1)
        {
            //1일차는 프롤로그부터 시작
            sceneMgr.SceneLoad(Scene.PROLOGUE, false, false, SceneChangeAni.FADE);
        }
        else
        {
            //나머지 경우는 타운에서 시작
            sceneMgr.SceneLoad(Scene.TOWN, false, false, SceneChangeAni.FADE);
        }
    }

    public void SaveCheckYes()
    {
        //인스펙터로 끌어서 사용하는 함수
        saveCheck.gameObject.SetActive(false);

        //덮어쓰기
        gameMgr.selectSaveSlot = slotNum;
        gameObject.SetActive(false);

        //저장
        gameMgr.SaveData();
        saveSlot.SetUI(gameMgr.playData, slotNum);
    }

    public void SaveCheckNo()
    {
        //인스펙터로 끌어서 사용하는 함수
        saveCheck.gameObject.SetActive(false);
    }
}
