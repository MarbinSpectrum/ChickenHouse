using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameRecord_UI : Mgr
{
    [SerializeField] private RectTransform saveRect;
    [SerializeField] private RectTransform saveCoverCheck;
    [SerializeField] private RectTransform saveMenu;
    [SerializeField] private RectTransform saveBtn;
    [SerializeField] private RectTransform loadBtn;
    [SerializeField] private ScrollRect srollRect;
    private const int MAX_SLOT = 18;
    private const int LINE_CNT = MAX_SLOT / 2 - 3;

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

        PlayData[] playDatas = new PlayData[saveSlots.Count];
        for (int i = 0; i < saveSlots.Count; i++)
        {
            PlayData data = gameMgr.LoadData(i);
            playDatas[i] = data;
        }
        int lineCnt = saveSlots.Count / 2 - 3;

        for (int i = 0; i < playDatas.Length; i++)
            saveSlots[i].SetUI(i,canSave, canLoad, playDatas[i]);

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
        soundMgr.PlaySE(Sound.Btn_SE);
        saveRect.gameObject.SetActive(false);
    }

    public void CloseSaveCoverUI()
    {
        //�ν����ͷ� ��� ����ϴ� �Լ�
        soundMgr.PlaySE(Sound.Btn_SE);
        saveCoverCheck.gameObject.SetActive(false);
    }

    public void SaveData()
    {
        //�ν����ͷ� ��� ����ϴ� �Լ�
        if (playData == null)
        {
            soundMgr.PlaySE(Sound.Btn_SE);

            //�� ����
            saveCoverCheck.gameObject.SetActive(false);

            //����
            gameMgr.selectSaveSlot = slotNum;
            gameMgr.SaveData();
            saveSlot.SetUI(slotNum,canSave, canLoad, gameMgr.playData);

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

        soundMgr.PlaySE(Sound.Btn_SE);

        gameMgr.selectSaveSlot = slotNum;
        gameMgr.LoadData();

        sceneMgr.SceneLoad(Scene.TOWN, false, false, SceneChangeAni.FADE);

        fun?.Invoke(slotNum);
    }

    public void SaveCoverCheckYes()
    {
        //�ν����ͷ� ��� ����ϴ� �Լ�
        soundMgr.PlaySE(Sound.Btn_SE);
        
        saveCoverCheck.gameObject.SetActive(false);

        //�����
        gameMgr.selectSaveSlot = slotNum;
        saveCoverCheck.gameObject.SetActive(false);

        //����
        gameMgr.SaveData();
        saveSlot.SetUI(slotNum,canSave, canLoad, gameMgr.playData);

        fun?.Invoke(slotNum);
    }

    public void SaveCoverCheckNo()
    {
        //�ν����ͷ� ��� ����ϴ� �Լ�
        soundMgr.PlaySE(Sound.Btn_SE);

        saveCoverCheck.gameObject.SetActive(false);
        saveRect.gameObject.SetActive(false);
    }
}
