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
    /** Ʃ�丮�� ���� ���� **/
    public bool showTuto;

    public float GetDefaultPoint()
    {
        //���� ������ ���� ������ ġŲ�� �⺻ ����
        return 2.0f;
    }
}
