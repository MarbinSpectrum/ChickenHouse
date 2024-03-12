using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : AwakeSingleton<GameMgr>
{
    public int selectSaveSlot;
    public PlayData playData = null;

    /** 오늘 수입 **/
    public int dayMoney;
    /** 판매한 치킨 수 **/
    public int sellChickenCnt;

    //-------------------------------------------------------------------------------------------------------
    protected override void Awake()
    {
        base.Awake();

        Application.targetFrameRate = 120;
    }

    //-------------------------------------------------------------------------------------------------------
    public void LoadData()
    {
        dayMoney = 0;
        sellChickenCnt = 0;

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
        string jsonData = MyLib.Json.ObjectToJson(playData);
        MyLib.Json.CreateJsonFile(string.Format("PlayData{0}",selectSaveSlot), jsonData);
    }
}
