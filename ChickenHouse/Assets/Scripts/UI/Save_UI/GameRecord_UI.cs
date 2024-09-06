using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRecord_UI : Mgr
{
    [SerializeField] private RectTransform saveRect;
    [SerializeField] private RectTransform saveCoverCheck;
    [SerializeField] private RectTransform saveMenu;
    [SerializeField] private RectTransform saveBtn;
    [SerializeField] private RectTransform loadBtn;

    private int slotNum;
    private PlayData playData;
    private bool canSave;
    private bool canLoad;
    private SaveSlot_UI saveSlot;

    //세이브나 로드 완료시 작동되는 함수
    private OneParaDel fun;

    [SerializeField]  private List<SaveSlot_UI> saveSlots;

    public void SetUI(bool pCanSave, bool pCanLoad, OneParaDel pFun = null)
    {
        canSave = pCanSave;
        canLoad = pCanLoad;
        fun = pFun;

        saveCoverCheck.gameObject.SetActive(false);
        saveRect.gameObject.SetActive(false);

        saveBtn.gameObject.SetActive(canSave);
        loadBtn.gameObject.SetActive(canLoad);

        PlayData[] playDatas = new PlayData[6];
        for (int i = 0; i < 6; i++)
        {
            PlayData data = gameMgr.LoadData(i);
            playDatas[i] = data;
        }

        for (int i = 0; i < playDatas.Length; i++)
            saveSlots[i].SetUI(canSave, canLoad, playDatas[i]);
    }

    public void SetUI(int pSlotNum, PlayData pPlayData, SaveSlot_UI pSaveSlot)
    {
        slotNum = pSlotNum;
        playData = pPlayData;
        saveSlot = pSaveSlot;

        saveRect.gameObject.SetActive(true);
        saveCoverCheck.gameObject.SetActive(false);
        saveMenu.transform.position = pSaveSlot.transform.position;
    }

    public void CloseSaveRect()
    {
        //인스펙터로 끌어서 사용하는 함수
        saveRect.gameObject.SetActive(false);
    }

    public void CloseSaveCoverUI()
    {
        //인스펙터로 끌어서 사용하는 함수
        saveCoverCheck.gameObject.SetActive(false);
    }

    public void SaveData()
    {
        //인스펙터로 끌어서 사용하는 함수
        if (playData == null)
        {
            //빈 슬롯
            saveCoverCheck.gameObject.SetActive(false);

            //저장
            gameMgr.selectSaveSlot = slotNum;
            gameMgr.SaveData();
            saveSlot.SetUI(canSave, canLoad, gameMgr.playData);

            fun?.Invoke(slotNum);
            return;
        }

        saveCoverCheck.gameObject.SetActive(true);
    }

    public void LoadData()
    {
        //인스펙터로 끌어서 사용하는 함수
        if (playData == null)
            return;

        gameMgr.selectSaveSlot = slotNum;
        gameMgr.LoadData();

        sceneMgr.SceneLoad(Scene.TOWN, false, SceneChangeAni.FADE);

        fun?.Invoke(slotNum);
    }

    public void SaveCoverCheckYes()
    {
        //인스펙터로 끌어서 사용하는 함수
        saveCoverCheck.gameObject.SetActive(false);

        //덮어쓰기
        gameMgr.selectSaveSlot = slotNum;
        saveCoverCheck.gameObject.SetActive(false);

        //저장
        gameMgr.SaveData();
        saveSlot.SetUI(canSave, canLoad, gameMgr.playData);

        fun?.Invoke(slotNum);
    }

    public void SaveCoverCheckNo()
    {
        //인스펙터로 끌어서 사용하는 함수
        saveCoverCheck.gameObject.SetActive(false);
        saveRect.gameObject.SetActive(false);
    }
}
