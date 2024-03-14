using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeMgr : AwakeSingleton<UpgradeMgr>
{
    [SerializeField] private Dictionary<Upgrade, UpgradeData> upgrade = new Dictionary<Upgrade, UpgradeData>();
}
