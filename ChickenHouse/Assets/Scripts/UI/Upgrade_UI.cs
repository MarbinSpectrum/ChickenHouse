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
                //해당 업그레이드가 완료되어있음
                return true;
            }

            //업그레이드가 완료 안되어있음
            return false;
        }

        List<Upgrade> randomUpgradeList = new List<Upgrade>();

        //-----------------------------------------------------------------
        //기름통 업그레이드
        if(HasUpgrade(Upgrade.OIL_Zone_6))
        {
            //기름통 업그레이드가 최대상태
            //랜덤리스트에 등록하지 않는다.
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
        //레시피 업그레이드
        if (HasUpgrade(Upgrade.Recipe_4))
        {
            //레시피 업그레이드가 최대상태
            //랜덤리스트에 등록하지 않는다.
        }
        else if (HasUpgrade(Upgrade.Recipe_3))
            randomUpgradeList.Add(Upgrade.Recipe_4);
        else if (HasUpgrade(Upgrade.Recipe_2))
            randomUpgradeList.Add(Upgrade.Recipe_3);
        else if (HasUpgrade(Upgrade.Recipe_1))
            randomUpgradeList.Add(Upgrade.Recipe_2);
        else
            randomUpgradeList.Add(Upgrade.Recipe_1);

        //랜덤 리스트를 이용해서 최종 리스트를 생성한다.
        List<Upgrade>   resultList  = new List<Upgrade>();

        //listCnt만큼 랜덤 idx생성
        int listCnt = Mathf.Min(randomUpgradeList.Count, MAX_UPGRADE);
        List<int> randomCheck = MyLib.Algorithm.CreateRandomList(randomUpgradeList.Count, listCnt);

        //앞에서 MAX_UPGRADE만큼만 챙겨주기
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
                //슬롯이 부족해서 추가
                UpgradeSlot_UI newSlot = Instantiate(upgradeSlot, body);
                slots.Add(newSlot);
            }

            //업그레이드 정보설정
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
