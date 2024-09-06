using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : AwakeSingleton<GameMgr>
{
    [NonSerialized] public int selectSaveSlot;
    [NonSerialized] public PlayData playData = null;
    public bool stopGame { private set; get; }

    /** 오늘 수입 **/
    [NonSerialized] public int dayMoney;
    /** 판매한 치킨 수 **/
    [NonSerialized] public int sellChickenCnt;
    /** 판매한 드링크 갯수 **/
    [NonSerialized] public Dictionary<Drink, int>       sellDrinkCnt        = new Dictionary<Drink, int>();
    /** 판매한 사이드메뉴 갯수 **/
    [NonSerialized] public Dictionary<SideMenu, int>    sellSideMenuCnt     = new Dictionary<SideMenu, int>();

    [SerializeField] private GameRecord_UI gameRecord;

    public static int TARGET_MONEY_1 = 50000;


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
            //화면 방향에따라서 자동으로 회전하도록 설정
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
        sellChickenCnt = 0;
        sellDrinkCnt.Clear();
        sellSideMenuCnt.Clear();
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
        if (playData == null)
            return;

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
            //인게임에서 멈춤상태라면 옵션에서 풀어도 멈춤상태임
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
}
