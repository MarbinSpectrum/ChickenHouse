using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StaffReady : Mgr
{
    public struct StaffList
    {
        public RectTransform    staffListLock;
        public RectTransform    staffListContents;
        public KitchenStaffSlot staffSlot;
        [System.NonSerialized] public List<KitchenStaffSlot> slotList;
    }    
    [SerializeField] private StaffList staffList;

    public struct StaffDuties
    {
        public Dictionary<KitchenSetWorkerPos, KitchenStaffDuties> staffDuties;
    }
    [SerializeField] private StaffDuties staffDuties;

    [SerializeField] private TextMeshProUGUI  staffCnt;
    [SerializeField] private KitchenStaffInfo staffInfo;

    private EWorker selectWorker = EWorker.None;

    public void SetUI()
    {
        PlayData playData = gameMgr.playData;
        if (playData == null)
            return;

        for (EWorker eWorker = EWorker.Worker_1; eWorker < EWorker.MAX; eWorker++)
        {
            if (playData.hasWorker[(int)eWorker])
            {
                //직원보유중이면 리스트 생성
                staffInfo.SetUI(selectWorker);
                UpdateStaffList();
                UpdateStaffDuties();
                return;
            }    
        }
        gameObject.SetActive(false);
    }

    private void UpdateStaffList()
    {
        PlayData playData = gameMgr.playData;
        if (playData == null)
            return;

        int setWorkerCnt = 0;
        List<EWorker> staffs = new List<EWorker>();
        HashSet<EWorker> workStaff = new HashSet<EWorker>();
        for (EWorker eWorker = EWorker.Worker_1; eWorker < EWorker.MAX; eWorker++)
        {
            if (playData.hasWorker[(int)eWorker] == false)
            {
                //보유하지 않은 직원
                continue;
            }

            for (KitchenSetWorkerPos workerPos = KitchenSetWorkerPos.CounterWorker;
                workerPos < KitchenSetWorkerPos.MAX; workerPos++)
            {
                EWorker worker = (EWorker)playData.workerPos[(int)workerPos];
                if (eWorker == worker)
                {
                    //해당 직원은 이미 배치되어 있다.
                    workStaff.Add(eWorker);
                    setWorkerCnt++;
                    break;
                }
            }

            staffs.Add(eWorker);
        }

        staffList.staffListLock.gameObject.SetActive(setWorkerCnt == PlayData.MAX_WORKER);

        staffList.slotList ??= new List<KitchenStaffSlot>();
        foreach (KitchenStaffSlot slot in staffList.slotList)
            slot.gameObject.SetActive(false);
        for (int i = 0; i < staffs.Count; i++)
        {
            while (staffList.slotList.Count <= i)
            {
                KitchenStaffSlot staffSlot =
                    Instantiate(staffList.staffSlot, staffList.staffListContents);
                staffList.slotList.Add(staffSlot);
            }
            EWorker eWorker = staffs[i];
            bool isSelect = (eWorker == selectWorker);
            bool isParty = workStaff.Contains(eWorker);
            staffList.slotList[i].SetUI(eWorker, isSelect, isParty,() =>
            {
                if (eWorker == EWorker.None)
                    return;
                if (eWorker == selectWorker)
                    selectWorker = EWorker.None;
                else
                    selectWorker = eWorker;
                staffInfo.SetUI(selectWorker);
                UpdateStaffList();
            });
        }
    }

    private void UpdateStaffDuties()
    {
        PlayData playData = gameMgr.playData;
        if (playData == null)
            return;

        int setWorkerCnt = 0;
        for (KitchenSetWorkerPos workerPos = KitchenSetWorkerPos.CounterWorker;
                workerPos < KitchenSetWorkerPos.MAX; workerPos++)
        {
            KitchenSetWorkerPos tempWorkerPos = workerPos;
            EWorker eWorker = (EWorker)playData.workerPos[(int)workerPos];
            staffDuties.staffDuties[workerPos].SetUI(eWorker, () =>
            {
                if (playData.workerPos[(int)tempWorkerPos] != (int)EWorker.None)
                {
                    selectWorker = EWorker.None;
                    UpdateStaffList();
                    UpdateStaffDuties();
                    staffInfo.SetUI((EWorker)playData.workerPos[(int)tempWorkerPos]);
                    return;
                }

                if (selectWorker == EWorker.None)
                    return;

                playData.workerPos[(int)tempWorkerPos] = (int)selectWorker;
                selectWorker = EWorker.None;
                UpdateStaffList();
                UpdateStaffDuties();
                staffInfo.SetUI(selectWorker);
            }, () =>
            {
                playData.workerPos[(int)tempWorkerPos] = (int)EWorker.None;
                selectWorker = EWorker.None;
                UpdateStaffList();
                UpdateStaffDuties();
                staffInfo.SetUI(selectWorker);
            });
            if (eWorker != EWorker.None)
                setWorkerCnt++;

        }
        staffCnt.text = string.Format("({0}/{1})", setWorkerCnt, PlayData.MAX_WORKER);
    }

    public void SelectWorkerCancelBtn()
    {
        selectWorker = EWorker.None;
        UpdateStaffList();
        UpdateStaffDuties();
        staffInfo.SetUI(selectWorker);
    }
}
