using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayData
{
    /** ���� **/
    public int day;
    /** ���� �ڱ� **/
    public long money;
    /** ���׷��̵� ���� **/
    public bool[] upgradeState = new bool[(int)Upgrade.MAX];
    /** Ʃ�丮�� ���� ���� **/
    public bool showTuto;

}
