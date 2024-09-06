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

    //���̺곪 �ε� �Ϸ�� �۵��Ǵ� �Լ�
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
        //�ν����ͷ� ��� ����ϴ� �Լ�
        saveRect.gameObject.SetActive(false);
    }

    public void CloseSaveCoverUI()
    {
        //�ν����ͷ� ��� ����ϴ� �Լ�
        saveCoverCheck.gameObject.SetActive(false);
    }

    public void SaveData()
    {
        //�ν����ͷ� ��� ����ϴ� �Լ�
        if (playData == null)
        {
            //�� ����
            saveCoverCheck.gameObject.SetActive(false);

            //����
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
        //�ν����ͷ� ��� ����ϴ� �Լ�
        if (playData == null)
            return;

        gameMgr.selectSaveSlot = slotNum;
        gameMgr.LoadData();

        sceneMgr.SceneLoad(Scene.TOWN, false, SceneChangeAni.FADE);

        fun?.Invoke(slotNum);
    }

    public void SaveCoverCheckYes()
    {
        //�ν����ͷ� ��� ����ϴ� �Լ�
        saveCoverCheck.gameObject.SetActive(false);

        //�����
        gameMgr.selectSaveSlot = slotNum;
        saveCoverCheck.gameObject.SetActive(false);

        //����
        gameMgr.SaveData();
        saveSlot.SetUI(canSave, canLoad, gameMgr.playData);

        fun?.Invoke(slotNum);
    }

    public void SaveCoverCheckNo()
    {
        //�ν����ͷ� ��� ����ϴ� �Լ�
        saveCoverCheck.gameObject.SetActive(false);
        saveRect.gameObject.SetActive(false);
    }
}
