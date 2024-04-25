using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "GuestData", menuName = "ScriptableObjects/Guest", order = 1)]
public class GuestData : ScriptableObject
{
    [Header("���� ��¥")]
    public int day;
    [Header("�ֹ� �޴�")]
    public List<GuestMenu> goodChicken;
}

[System.Serializable]
public class GuestMenu
{
    /** ���ϴ� ġŲ �� **/
    public ChickenSpicy spicy0;
    public ChickenSpicy spicy1;

    /** ������ ��Ű�� ����� **/
    public Drink        drink;
    /** ������ ��Ű�� ���̵� �޴� **/
    public SideMenu     sideMenu;
}