using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeScroll_Init : LoopScrollInit
{
    [SerializeField] private Upgrade_UI upgradeUI;

    public override void ProvideData(Transform transform, int idx)
    {
        UpgradeSlot_UI upgradeSlot = transform.GetComponent<UpgradeSlot_UI>();
        if (upgradeSlot == null)
            return;

        upgradeSlot.SetData(upgradeUI.upgradeList[idx]);
    }
}
