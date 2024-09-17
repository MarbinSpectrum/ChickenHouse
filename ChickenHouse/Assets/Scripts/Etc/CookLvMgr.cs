using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookLvMgr : AwakeSingleton<CookLvMgr>
{
    [SerializeField]
    public struct LvData
    {
        public CookLvStat   cookLvStat;
        public int          value;
    }

    //idx = 0이면 2레벨에 도달시 얻는 보상임
    [SerializeField] private List<LvData> lvData = new List<LvData>();
    public int MAX_LV => lvData.Count+1;

    public int RequireExp(int pLv)
    {
        return 800 + (pLv - 2) * 1200;
    }

    public int GetLvSumValue(CookLvStat statType, int pLV)
    {
        int sum = 0;
        for(int lv = 0; lv <= pLV-2; lv++)
        {
            if (lvData.Count <= lv)
                break;
            if (lvData[lv].cookLvStat != statType)
                continue;
            sum += lvData[lv].value;
        }

        return sum;
    }

    public int GetLvValue(int pLV)
    {
        int tempLv = pLV - 2;
        if (tempLv < 0)
            return 0;
        if (lvData.Count <= tempLv)
            return 0;
        return lvData[tempLv].value;
    }
}
