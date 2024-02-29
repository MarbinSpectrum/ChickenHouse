using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayData
{
    /** 일차 **/
    public int day = 1;
    /** 보유 자금 **/
    public long money;
    /** 업그레이드 상태 **/
    public bool[] upgradeState = new bool[(int)Upgrade.MAX];

    public float GetDefaultPoint()
    {
        //유저 정보를 토대로 나오는 치킨의 기본 점수
        return 2.0f;
    }
}
