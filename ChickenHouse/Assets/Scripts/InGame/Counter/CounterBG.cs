using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterBG : Mgr
{
    public enum CounterTime
    {
        Moring,
        Lunch,
        Night,
    }

    [SerializeField] private SpriteRenderer table;
    [SerializeField] private SpriteRenderer wall;
    [SerializeField] private SpriteRenderer floor;
    [SerializeField] private SpriteRenderer desk;
    [SerializeField] private SpriteRenderer nightDeep;

    private CounterTime     time;
    private InteriorItem    interiorWall   = InteriorItem.Interior_Wall_0;
    private InteriorItem    interiorTable  = InteriorItem.Interior_Table_0;
    private InteriorItem    interiorFloor  = InteriorItem.Interior_Floor_0;
    private InteriorItem    interiorDesk   = InteriorItem.Interior_Desk_0;

    public void SetInteriorPlayData(CounterTime pTime)
    {
        SetInterior((InteriorItem)gameMgr.playData.useInteriorWall, (InteriorItem)gameMgr.playData.useInteriorTable
            , (InteriorItem)gameMgr.playData.useInteriorFloor, (InteriorItem)gameMgr.playData.useInteriorDesk, pTime);
    }

    public void SetInterior(InteriorItem pWall, InteriorItem pTable, InteriorItem pFloor, InteriorItem pDesk, CounterTime pTime)
    {
        interiorWall    = pWall;
        interiorTable   = pTable;
        interiorFloor   = pFloor;
        interiorDesk    = pDesk;
        time            = pTime;

        UpdateInterior();
    }

    private void UpdateInterior()
    {
        /////////////////////////////////////////////////////////////////////
        //Table
        InteriorData tableData = interiorMgr.GetInteriorData(interiorTable);
        table.sprite = tableData.objImg1;
        Color tableColor = Color.white;
        if (time == CounterTime.Moring)
            ColorUtility.TryParseHtmlString("#BABBC8", out tableColor);
        else if (time == CounterTime.Night)
            ColorUtility.TryParseHtmlString("#0F0F11", out tableColor);
        table.color = tableColor;


        /////////////////////////////////////////////////////////////////////
        //Wall
        InteriorData wallData = interiorMgr.GetInteriorData(interiorWall);
        if (time == CounterTime.Lunch || time == CounterTime.Night)
            wall.sprite = wallData.objImg2;
        else
            wall.sprite = wallData.objImg1;
        Color bgColor = Color.white;
        if (time == CounterTime.Night)
            ColorUtility.TryParseHtmlString("#0F0F11", out bgColor);
        wall.color = bgColor;

        /////////////////////////////////////////////////////////////////////
        //Floor
        InteriorData floorData = interiorMgr.GetInteriorData(interiorFloor);
        floor.sprite = floorData.objImg1;
        Color floorColor = Color.white;
        if (time == CounterTime.Night)
            ColorUtility.TryParseHtmlString("#0F0F11", out floorColor);
        floor.color = floorColor;

        /////////////////////////////////////////////////////////////////////
        //Desk
        InteriorData deskData = interiorMgr.GetInteriorData(interiorDesk);
        desk.sprite = deskData.objImg1;
        Color deskColor = Color.white;
        if (time == CounterTime.Moring)
            ColorUtility.TryParseHtmlString("#BABBC8", out deskColor);
        else if (time == CounterTime.Night)
            ColorUtility.TryParseHtmlString("#5C769F", out deskColor);
        desk.color = deskColor;

        /////////////////////////////////////////////////////////////////////
        //Deep
        Color deepColor = Color.white;
         if(time == CounterTime.Night)
            ColorUtility.TryParseHtmlString("#242A7B", out deepColor);
        else if (time == CounterTime.Lunch)
            ColorUtility.TryParseHtmlString("#66A4AD", out deepColor);
        nightDeep.color = deepColor;
    }
}
