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

    [SerializeField] private GameObject oTable;
    [SerializeField] private GameObject oWall;
    [SerializeField] private GameObject oFloor;
    [SerializeField] private GameObject oDesk;
    [SerializeField] private GameObject oDeep;

    protected CounterTime     time;
    protected InteriorItem    interiorWall   = InteriorItem.Interior_Wall_0;
    protected InteriorItem    interiorTable  = InteriorItem.Interior_Table_0;
    protected InteriorItem    interiorFloor  = InteriorItem.Interior_Floor_0;
    protected InteriorItem    interiorDesk   = InteriorItem.Interior_Desk_0;

    public void SetInteriorPlayData(CounterTime pTime)
    {
        //플레이어 정보 기반으로 BG 생성
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

    public void SetInteriorBeaveriorUI(InteriorItem pAny, CounterTime pTime)
    {
        //Beaverior표시용
        InteriorTab interiorTab = InteriorMgr.GetInteriorTab(pAny);
        switch (interiorTab)
        {
            case InteriorTab.Wall:
                SetInterior(pAny, InteriorItem.None, InteriorItem.Interior_Floor_0, InteriorItem.None, pTime);
                break;
            case InteriorTab.Table:
                SetInterior(InteriorItem.Interior_Wall_0, pAny, InteriorItem.Interior_Floor_0, InteriorItem.Interior_Desk_0, pTime);
                break;
            case InteriorTab.Floor:
                SetInterior(InteriorItem.Interior_Wall_0, InteriorItem.None, pAny, InteriorItem.None, pTime);
                break;
            case InteriorTab.Desk:
                SetInterior(InteriorItem.Interior_Wall_0, InteriorItem.Interior_Table_0, InteriorItem.Interior_Floor_0, pAny, pTime);
                break;
        }
    }

    protected void UpdateInterior(GameObject pTable, GameObject pWall, GameObject pFloor, GameObject pDesk, GameObject pDeep)
    {
        /////////////////////////////////////////////////////////////////////
        //Table
        InteriorData tableData = interiorMgr.GetInteriorData(interiorTable);
        Color tableColor = Color.white;
        if (time == CounterTime.Moring)
            ColorUtility.TryParseHtmlString("#BABBC8", out tableColor);
        else if (time == CounterTime.Night)
            ColorUtility.TryParseHtmlString("#0F0F11", out tableColor);
        tableColor.a = (interiorTable == InteriorItem.None) ? 0 : 1;
        SetSprite(pTable, tableData == null ? null : tableData.objImg1);
        SetColor(pTable, tableColor);

        /////////////////////////////////////////////////////////////////////
        //Wall
        InteriorData wallData = interiorMgr.GetInteriorData(interiorWall);
        Color bgColor = Color.white;
        if (time == CounterTime.Night)
            ColorUtility.TryParseHtmlString("#0F0F11", out bgColor);
        bgColor.a = (interiorWall == InteriorItem.None) ? 0 : 1;
        SetSprite(pWall, (time == CounterTime.Lunch || time == CounterTime.Night)
            ? wallData.objImg2 : wallData.objImg1);
        SetColor(pWall, bgColor);

        /////////////////////////////////////////////////////////////////////
        //Floor
        InteriorData floorData = interiorMgr.GetInteriorData(interiorFloor);
        Color floorColor = Color.white;
        if (time == CounterTime.Night)
            ColorUtility.TryParseHtmlString("#0F0F11", out floorColor);
        floorColor.a = (interiorFloor == InteriorItem.None) ? 0 : 1;
        SetSprite(pFloor, floorData == null ? null : floorData.objImg1);
        SetColor(pFloor, floorColor);

        /////////////////////////////////////////////////////////////////////
        //Desk
        InteriorData deskData = interiorMgr.GetInteriorData(interiorDesk);
        Color deskColor = Color.white;
        if (time == CounterTime.Moring)
            ColorUtility.TryParseHtmlString("#BABBC8", out deskColor);
        else if (time == CounterTime.Night)
            ColorUtility.TryParseHtmlString("#5C769F", out deskColor);
        deskColor.a = (interiorDesk == InteriorItem.None) ? 0 : 1;
        SetSprite(pDesk, deskData == null ? null : deskData.objImg1);
        SetColor(pDesk, deskColor);

        /////////////////////////////////////////////////////////////////////
        //Deep
        Color deepColor = Color.white;
        if (time == CounterTime.Night)
            ColorUtility.TryParseHtmlString("#242A7B", out deepColor);
        else if (time == CounterTime.Lunch)
            ColorUtility.TryParseHtmlString("#66A4AD", out deepColor);
        SetColor(pDeep, deepColor);
    }

    protected void UpdateInterior()
    {
        UpdateInterior(oTable, oWall, oFloor, oDesk, oDeep);
    }

    protected virtual void SetColor(GameObject pImg, Color pColor)
    {
        SpriteRenderer spriteRenderer = pImg.GetComponent<SpriteRenderer>();
        spriteRenderer.color = pColor;
    }

    protected virtual void SetSprite(GameObject pImg, Sprite pSprite)
    {
        SpriteRenderer spriteRenderer = pImg.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = pSprite;
    }
}
