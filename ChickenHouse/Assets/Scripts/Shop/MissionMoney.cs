using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MissionMoney : Mgr
{
    [SerializeField] private TextMeshProUGUI textMesh;

    public const long TARGET_MONEY_1 = 50000;
    public const long TARGET_MONEY_2 = 250000;
    public const long TARGET_MONEY_3 = 1000000;

    public void SetMoney(int day, long nowMoney)
    {
        long requireMoney = 0;
        if (day <= 10)
            requireMoney = (long)Mathf.Max(0,TARGET_MONEY_1 - nowMoney);
        else if (day <= 20)
            requireMoney = (long)Mathf.Max(0, TARGET_MONEY_2 - nowMoney);
        else if (day <= 30)
            requireMoney = (long)Mathf.Max(0, TARGET_MONEY_3 - nowMoney);
        string strLabel = LanguageMgr.GetText("MISSON_LABEL");
        string resultStr = string.Format("{0} : <color=#FF0000>{1:N0} $</color>", strLabel, requireMoney);

        LanguageMgr.SetText(textMesh, resultStr);
    }
}