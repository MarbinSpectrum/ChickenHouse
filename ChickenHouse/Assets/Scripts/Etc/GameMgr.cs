using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : AwakeSingleton<GameMgr>
{
    public int selectSaveSlot;
    public PlayData playData = null;
    public bool stopGame { private set; get; }

    /** 오늘 수입 **/
    public int dayMoney;
    /** 판매한 치킨 수 **/
    public int sellChickenCnt;
    /** 판매한 드링크 갯수 **/
    public Dictionary<Drink, int>       sellDrinkCnt        = new Dictionary<Drink, int>();
    /** 판매한 사이드메뉴 갯수 **/
    public Dictionary<SideMenu, int>    sellSideMenuCnt     = new Dictionary<SideMenu, int>();

    //-------------------------------------------------------------------------------------------------------
    protected override void Awake()
    {
        base.Awake();

        Application.targetFrameRate = 120;
        Time.timeScale = 1;
        SoundMgr soundMgr = SoundMgr.Instance;
        soundMgr.MuteSE(false);
    }

    //-------------------------------------------------------------------------------------------------------
    public void LoadData()
    {
        dayMoney = 0;
        sellChickenCnt = 0;
        sellDrinkCnt.Clear();
        sellSideMenuCnt.Clear();

        playData = LoadData(selectSaveSlot);
        playData ??= new PlayData();
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
}
