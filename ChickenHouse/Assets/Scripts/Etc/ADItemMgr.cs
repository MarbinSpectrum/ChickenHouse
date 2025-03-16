using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADItemMgr : AwakeSingleton<ADItemMgr>
{
    private Dictionary<ADItem, ADData> ADData = new();

    private static bool init = false;

    protected override void Awake()
    {
        base.Awake();

        if (init)
            return;

        init = true;

        for (ADItem adItem = ADItem.Advertisement_1; adItem < ADItem.MAX; adItem++)
        {
            ADData iData = Resources.Load<ADData>($"ADData/{adItem.ToString()}");
            if (iData == null)
                continue;
            ADData.Add(adItem, iData);
        }
    }

    public ADData GetADData(ADItem pADItem)
    {
        //인테리어 소품 정보 얻기
        if (ADData.ContainsKey(pADItem))
            return ADData[pADItem];
        return null;
    }
}
