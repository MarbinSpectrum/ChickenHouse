using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ResumeData", menuName = "ScriptableObjects/Worker", order = 3)]
public class WorkerData : ScriptableObject
{
    [Header("증명사진")]
    public Sprite face;

    [Header("이름")]
    public string nameKey;

    [Header("나이")]
    public int age;

    [Header("계약금")]
    public int deposit;

    [Header("거주지")]
    public string residenceKey;

    [Header("급여")]
    public int salary;

    [Header("기술/능력")]
    public List<WorkerSkill> skill = new List<WorkerSkill>();

    [System.Serializable]
    public struct SPRITE
    {
        /** 엄지 **/
        public Sprite hand0;
        /** 나머지 손 **/
        public Sprite hand1;
        /** 보통 상태 손 **/
        public Sprite hand2;
    }
    [Header("손 이미지")]
    public SPRITE handSprite;
}
