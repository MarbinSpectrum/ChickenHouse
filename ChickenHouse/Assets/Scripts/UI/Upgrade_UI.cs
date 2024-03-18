using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade_UI : Mgr
{
    [SerializeField] private UpgradeSlot_UI upgradeSlot;
    [SerializeField] private RectTransform  body;
    [SerializeField] private GameObject     dontTouch;

    private List<UpgradeSlot_UI>    slots       = new List<UpgradeSlot_UI>();
    private const int               MAX_UPGRADE = 3;
    private bool selectMenu = false;

    private void Start()
    {
        gameMgr.LoadData();

        SetUpgrade();
    }

    private void SetUpgrade()
    {
        bool HasUpgrade(Upgrade upgrade)
        {
            if (gameMgr.playData.upgradeState[(int)upgrade])
            {
                //�ش� ���׷��̵尡 �Ϸ�Ǿ�����
                return true;
            }

            //���׷��̵尡 �Ϸ� �ȵǾ�����
            return false;
        }

        List<Upgrade> randomUpgradeList = new List<Upgrade>();

        //-----------------------------------------------------------------
        //�⸧�� ���׷��̵�
        if(HasUpgrade(Upgrade.OIL_Zone_6))
        {
            //�⸧�� ���׷��̵尡 �ִ����
            //��������Ʈ�� ������� �ʴ´�.
        }
        else if (HasUpgrade(Upgrade.OIL_Zone_5))
            randomUpgradeList.Add(Upgrade.OIL_Zone_6);
        else if (HasUpgrade(Upgrade.OIL_Zone_4))
            randomUpgradeList.Add(Upgrade.OIL_Zone_5);
        else if (HasUpgrade(Upgrade.OIL_Zone_3))
            randomUpgradeList.Add(Upgrade.OIL_Zone_4);
        else if (HasUpgrade(Upgrade.OIL_Zone_2))
            randomUpgradeList.Add(Upgrade.OIL_Zone_3);
        else if (HasUpgrade(Upgrade.OIL_Zone_2))
            randomUpgradeList.Add(Upgrade.OIL_Zone_3);
        else if (HasUpgrade(Upgrade.OIL_Zone_1))
            randomUpgradeList.Add(Upgrade.OIL_Zone_2);
        else
            randomUpgradeList.Add(Upgrade.OIL_Zone_1);

        //-----------------------------------------------------------------
        //������ ���׷��̵�
        if (HasUpgrade(Upgrade.Recipe_4))
        {
            //������ ���׷��̵尡 �ִ����
            //��������Ʈ�� ������� �ʴ´�.
        }
        else if (HasUpgrade(Upgrade.Recipe_3))
            randomUpgradeList.Add(Upgrade.Recipe_4);
        else if (HasUpgrade(Upgrade.Recipe_2))
            randomUpgradeList.Add(Upgrade.Recipe_3);
        else if (HasUpgrade(Upgrade.Recipe_1))
            randomUpgradeList.Add(Upgrade.Recipe_2);
        else
            randomUpgradeList.Add(Upgrade.Recipe_1);

        //���� ����Ʈ�� �̿��ؼ� ���� ����Ʈ�� �����Ѵ�.
        List<Upgrade>   resultList  = new List<Upgrade>();

        //listCnt��ŭ ���� idx����
        int listCnt = Mathf.Min(randomUpgradeList.Count, MAX_UPGRADE);
        List<int> randomCheck = MyLib.Algorithm.CreateRandomList(randomUpgradeList.Count, listCnt);

        //�տ��� MAX_UPGRADE��ŭ�� ì���ֱ�
        for (int i = 0; i < MAX_UPGRADE; i++)
        {
            if (randomCheck.Count <= i)
                break;
            int idx = randomCheck[i];
            resultList.Add(randomUpgradeList[idx - 1]);
        }

        SetUpgradeSlot(resultList);
    }

    private void SetUpgradeSlot(List<Upgrade> upgrades)
    {
        slots.ForEach((x) => x.gameObject.SetActive(false));
        for (int i = 0; i < upgrades.Count; i++)
        {
            if(slots.Count <= upgrades.Count)
            {
                //������ �����ؼ� �߰�
                UpgradeSlot_UI newSlot = Instantiate(upgradeSlot, body);
                slots.Add(newSlot);
            }

            //���׷��̵� ��������
            slots[i].gameObject.SetActive(true);
            slots[i].SetData(this, upgrades[i]);
        } 
    }

    public void SelectUpgrade(Upgrade pUpgrade)
    {
        if (selectMenu)
            return;
        dontTouch.gameObject.SetActive(true);
        selectMenu = true;
        gameMgr.playData.upgradeState[(int)pUpgrade] = true;
        gameMgr.playData.day += 1;
        gameMgr.SaveData();
        sceneMgr.SceneLoad(Scene.INGAME, SceneChangeAni.CIRCLE);
    }
}
