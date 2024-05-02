using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ResumeData", menuName = "ScriptableObjects/Resume", order = 3)]
public class ResumeData : ScriptableObject
{
    [Header("증명사진")]
    public Sprite face;

    [Header("이름")]
    public string nameKey;

    [Header("나이")]
    public int age;

    [Header("거주지")]
    public string residenceKey;

    [Header("급여")]
    public int salary;

    [Header("기술/능력")]
    public List<WorkerSkill> skill = new List<WorkerSkill>();
}
