using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayData
{
    /** ���� **/
    public int day = 1;
    /** ���� �ڱ� **/
    public long money;
    /** ���׷��̵� ���� **/
    public bool[] upgradeState = new bool[(int)Upgrade.MAX];

    public float GetDefaultPoint()
    {
        //���� ������ ���� ������ ġŲ�� �⺻ ����
        return 2.0f;
    }

    public int GetMenuValue()
    {
        //�޴� ����
        int defaultValue = 300;

        int percent = 100;
        if (upgradeState[(int)Upgrade.Recipe_1])
            percent += 20;
        if (upgradeState[(int)Upgrade.Recipe_2])
            percent += 20;
        if (upgradeState[(int)Upgrade.Recipe_3])
            percent += 20;
        if (upgradeState[(int)Upgrade.Recipe_4])
            percent += 20;

        int resultValue = (defaultValue * percent) / 100;

        return resultValue;
    }
}
