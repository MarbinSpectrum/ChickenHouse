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
        //�ν����ͷ� ��� ����ϴ� �Լ�
        gameObject.SetActive(false);
    }

    public void SaveData()
    {
        //�ν����ͷ� ��� ����ϴ� �Լ�
        if (playData == null)
        {
            //�� ����
            gameObject.SetActive(false);

            //����
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
            //1������ ���ѷα׺��� ����
            sceneMgr.SceneLoad(Scene.PROLOGUE, false, false, SceneChangeAni.FADE);
        }
        else
        {
            //������ ���� Ÿ��� ����
            sceneMgr.SceneLoad(Scene.TOWN, false, false, SceneChangeAni.FADE);
        }
    }

    public void SaveCheckYes()
    {
        //�ν����ͷ� ��� ����ϴ� �Լ�
        saveCheck.gameObject.SetActive(false);

        //�����
        gameMgr.selectSaveSlot = slotNum;
        gameObject.SetActive(false);

        //����
        gameMgr.SaveData();
        saveSlot.SetUI(gameMgr.playData, slotNum);
    }

    public void SaveCheckNo()
    {
        //�ν����ͷ� ��� ����ϴ� �Լ�
        saveCheck.gameObject.SetActive(false);
    }
}
