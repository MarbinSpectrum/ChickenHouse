using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : AwakeSingleton<GameMgr>
{
    public int selectSaveSlot;
    public PlayData playData = null;

    protected override void Awake()
    {
        base.Awake();

        Application.targetFrameRate = 120;
    }

    public void LoadData()
    {
        playData = LoadData(selectSaveSlot);
        playData ??= new PlayData();
    }

    public PlayData LoadData(int select)
    {
        return MyLib.Json.LoadJsonFile<PlayData>(string.Format("PlayData{0}", select));
    }

    public void OnApplicationQuit()
    {
        if (playData == null)
            return;
        string jsonData = MyLib.Json.ObjectToJson(playData);
        MyLib.Json.CreateJsonFile(string.Format("PlayData{0}",selectSaveSlot), jsonData);
    }
}
