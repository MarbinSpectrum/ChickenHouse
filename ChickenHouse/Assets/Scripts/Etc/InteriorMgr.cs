using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteriorMgr : AwakeSingleton<InteriorMgr>
{
    private Dictionary<InteriorItem, InteriorData> interiorData = new();

    private static bool init = false;

    protected override void Awake()
    {
        base.Awake();

        if (init)
            return;

        init = true;

        for (InteriorItem interior = InteriorItem.Interior_Wall_0; interior < InteriorItem.MAX; interior++)
        {
            InteriorData iData = Resources.Load<InteriorData>($"InteriorData/{interior.ToString()}");
            if (iData == null)
                continue;
            interiorData.Add(interior, iData);
        }
    }

    public InteriorData GetInteriorData(InteriorItem pInteriorItem)
    {
        //인테리어 소품 정보 얻기
        if (interiorData.ContainsKey(pInteriorItem))
            return interiorData[pInteriorItem];
        return null;
    }

    public static InteriorTab GetInteriorTab(InteriorItem pInteriorItem)
    {
        switch (pInteriorItem)
        {
            case InteriorItem.Interior_Wall_0:
            case InteriorItem.Interior_Wall_1:
            case InteriorItem.Interior_Wall_2:
            case InteriorItem.Interior_Wall_3:
            case InteriorItem.Interior_Wall_4:
                return InteriorTab.Wall;
            case InteriorItem.Interior_Table_0:
            case InteriorItem.Interior_Table_1:
            case InteriorItem.Interior_Table_2:
            case InteriorItem.Interior_Table_3:
            case InteriorItem.Interior_Table_4:
                return InteriorTab.Table;
            case InteriorItem.Interior_Floor_0:
            case InteriorItem.Interior_Floor_1:
            case InteriorItem.Interior_Floor_2:
            case InteriorItem.Interior_Floor_3:
            case InteriorItem.Interior_Floor_4:
                return InteriorTab.Floor;
            case InteriorItem.Interior_Desk_0:
            case InteriorItem.Interior_Desk_1:
            case InteriorItem.Interior_Desk_2:
            case InteriorItem.Interior_Desk_3:
            case InteriorItem.Interior_Desk_4:
                return InteriorTab.Desk;
        }
        return InteriorTab.None;
    }
}
