using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayData
{
    /** 일차 **/
    public int day;
    /** 보유 자금 **/
    public long money;
    /** 업그레이드 상태 **/
    public bool[] upgradeState = new bool[(int)Upgrade.MAX];
    /** 튜토리얼 진행 여부 **/
    public bool showTuto;

}
