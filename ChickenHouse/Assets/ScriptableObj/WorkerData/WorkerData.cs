using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ResumeData", menuName = "ScriptableObjects/Worker", order = 3)]
public class WorkerData : ScriptableObject
{
    [Header("�������")]
    public Sprite face;

    [Header("�̸�")]
    public string nameKey;

    [Header("����")]
    public int age;

    [Header("����")]
    public int deposit;

    [Header("������")]
    public string residenceKey;

    [Header("�޿�")]
    public int salary;

    [Header("���/�ɷ�")]
    public List<WorkerSkill> skill = new List<WorkerSkill>();

    [System.Serializable]
    public struct SPRITE
    {
        /** ���� **/
        public Sprite hand0;
        /** ������ �� **/
        public Sprite hand1;
        /** ���� ���� �� **/
        public Sprite hand2;
    }
    [Header("�� �̹���")]
    public SPRITE handSprite;
}
