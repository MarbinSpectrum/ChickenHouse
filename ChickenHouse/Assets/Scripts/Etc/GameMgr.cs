using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : AwakeSingleton<GameMgr>
{
    [NonSerialized] public int selectSaveSlot;
    [NonSerialized] public PlayData playData = null;
    public bool stopGame { private set; get; }

    /** ���� ���� **/
    [NonSerialized] public int dayMoney;
    /** �Ǹ��� �޴� **/
    [NonSerialized] public List<GuestMenu> sellMenu = new List<GuestMenu>();

    [SerializeField] private GameRecord_UI gameRecord;

    //-------------------------------------------------------------------------------------------------------
    protected override void Awake()
    {
        base.Awake();

        Application.targetFrameRate = 120;
        Time.timeScale = 1;
        SoundMgr soundMgr = SoundMgr.Instance;
        if(soundMgr != null)
            soundMgr.MuteSE(false);
    }

    private void Update()
    {
        if (Input.acceleration.y > 0.5f)
        {
            //ȭ�� ���⿡���� �ڵ����� ȸ���ϵ��� ����
            if (Screen.orientation == ScreenOrientation.LandscapeRight)
                Screen.orientation = ScreenOrientation.LandscapeLeft;
            else
                Screen.orientation = ScreenOrientation.LandscapeRight;
        }
    }

    //-------------------------------------------------------------------------------------------------------
    public void LoadData()
    {
        playData = LoadData(selectSaveSlot);
        playData ??= new PlayData();
    }

    public void InitData()
    {
        dayMoney = 0;
        sellMenu.Clear();
    }

    public PlayData LoadData(int select)
    {
        return MyLib.Json.LoadJsonFile<PlayData>(string.Format("PlayData{0}", select));
    }

    public void DeleteData(int select)
    {
        MyLib.Json.DeleteJsonFile(string.Format("PlayData{0}", select));
    }

    public void SaveData()
    {
        playData ??= new PlayData();

        DateTime dateTime = DateTime.Now;
        int saveYear = dateTime.Year;
        int saveMonth = dateTime.Month;
        int saveDay = dateTime.Day;
        int saveHour = dateTime.Hour;
        int saveMin = dateTime.Minute;
        playData.saveYear = saveYear;
        playData.saveMonth = saveMonth;
        playData.saveDay = saveDay;
        playData.saveHour = saveHour;
        playData.saveMin = saveMin;


        string jsonData = MyLib.Json.ObjectToJson(playData);
        MyLib.Json.CreateJsonFile(string.Format("PlayData{0}",selectSaveSlot), jsonData);
    }

    public void StopGame(bool state)
    {
        stopGame = state;
        Time.timeScale = state ? 0 : 1;
        SoundMgr soundMgr = SoundMgr.Instance;
        soundMgr.MuteSE(state);
    }

    public void OptionStopGame(bool state)
    {
        if(stopGame && state == false)
        {
            //�ΰ��ӿ��� ������¶�� �ɼǿ��� Ǯ� ���������
            Time.timeScale = 0;
            return;
        }

        Time.timeScale = state ? 0 : 1;
        SoundMgr soundMgr = SoundMgr.Instance;
        soundMgr.MuteSE(state);
    }

    public void OpenRecordUI(bool canSave, bool canLoad, OneParaDel pFun)
    {
        gameRecord.SetUI(canSave, canLoad, pFun);
        gameRecord.gameObject.SetActive(true);
    }

    public void CloseRecordUI()
    {
        gameRecord.gameObject.SetActive(false);
    }

    public void DayEndEvent()
    {
        //��¥�� �ٲ� ó���Ǵ� �̺�Ʈ
        QuestMgr questMgr = QuestMgr.Instance;

        //����Ʈ ���൵ ����
        questMgr.QuestApply();

        //���ο� ����Ʈ ���
        switch (playData.day)
        {
            case 2:
                {
                    questMgr.AddQuest(Quest.SpicyQuest_1);
                    questMgr.AddQuest(Quest.DrinkQuest_1);
                }
                break;
        }
    }
}
