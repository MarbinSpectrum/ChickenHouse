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

        public LvData(CookLvStat pCookLvStat,int pValue)
        {
            cookLvStat = pCookLvStat;
            value = pValue;
        }
    }

    //idx = 0이면 2레벨에 도달시 얻는 보상임
    [SerializeField] private List<LvData> lvData = new List<LvData>();
    public int MAX_LV => lvData.Count+1;

    public int RequireExp(int pLv)
    {
        //pLV달서에 필요한 경험치
        return 800 + (pLv - 2) * 400;
    }

    public int GetLvSumValue(CookLvStat statType, int pLV)
    {
        //pLV까지의 보상
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

    public LvData GetLvData(int pLV)
    {
        //pLV달성시 보상
        int tempLv = pLV - 2;
        if (tempLv < 0)
            return new LvData(CookLvStat.None,0);
        if (lvData.Count <= tempLv)
            return new LvData(CookLvStat.None, 0);
        return lvData[tempLv];
    }
}
