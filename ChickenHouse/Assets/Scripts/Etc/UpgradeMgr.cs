using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeMgr : AwakeSingleton<UpgradeMgr>
{
    [SerializeField] private Dictionary<Upgrade, UpgradeData> upgrade = new Dictionary<Upgrade, UpgradeData>();

    public UpgradeData GetUpgradeData(Upgrade pUpgrade)
    {
        //업그레이드 정보 얻기
        if (upgrade.ContainsKey(pUpgrade))
            return upgrade[pUpgrade];
        return null;
    }
}
