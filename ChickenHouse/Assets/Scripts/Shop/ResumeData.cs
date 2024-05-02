using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ResumeData", menuName = "ScriptableObjects/Resume", order = 3)]
public class ResumeData : ScriptableObject
{
    [Header("�������")]
    public Sprite face;

    [Header("�̸�")]
    public string nameKey;

    [Header("����")]
    public int age;

    [Header("������")]
    public string residenceKey;

    [Header("�޿�")]
    public int salary;

    [Header("���/�ɷ�")]
    public List<WorkerSkill> skill = new List<WorkerSkill>();
}
