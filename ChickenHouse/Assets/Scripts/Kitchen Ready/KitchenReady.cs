using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenReady : Mgr
{
    public struct StaffList
    {
        public RectTransform staffListContents;
        public KitchenStaffSlot staffSlot;
        [System.NonSerialized] public List<KitchenStaffSlot> slotList;
    }    
    [SerializeField] private StaffList staffList;

    public struct StaffDuties
    {
        public Dictionary<KitchenSetWorkerPos, KitchenStaffDuties> staffDuties;
    }
    [SerializeField] private StaffDuties staffDuties;

    [SerializeField] private KitchenStaffInfo staffInfo;

    [SerializeField] private StartGame startGame;

    private EWorker selectWorker = EWorker.None;



    private void Start()
    {
        gameMgr.InitData();

        PlayData playData = gameMgr.playData;
        if (playData == null)
        {
            gameObject.SetActive(false);
            startGame.Run();
            return;
        }

        for (EWorker eWorker = EWorker.Worker_1; eWorker < EWorker.MAX; eWorker++)
        {
            if (playData.hasWorker[(int)eWorker])
            {
                //�����������̸� ����Ʈ ����
                staffInfo.SetUI(selectWorker);
                UpdateStaffList();
                UpdateStaffDuties();
                return;
            }    
        }
        gameObject.SetActive(false);
        startGame.Run();
    }

    private void UpdateStaffList()
    {
        PlayData playData = gameMgr.playData;
        if (playData == null)
            return;

        List<EWorker> staffs = new List<EWorker>();
        for (EWorker eWorker = EWorker.Worker_1; eWorker < EWorker.MAX; eWorker++)
        {
            if (playData.hasWorker[(int)eWorker] == false)
            {
                //�������� ���� ����
                continue;
            }
            bool setWorkerFlag = false;
            for (KitchenSetWorkerPos workerPos = KitchenSetWorkerPos.CounterWorker;
                workerPos < KitchenSetWorkerPos.MAX; workerPos++)
            {
                EWorker worker = (EWorker)playData.workerPos[(int)workerPos];
                if (eWorker == worker)
                {
                    //�ش� ������ �̹� ��ġ�Ǿ� �ִ�.
                    setWorkerFlag = true;
                    break;
                }
            }

            if (setWorkerFlag)
                continue;

            staffs.Add(eWorker);
        }

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
            staffList.slotList[i].SetUI(eWorker, isSelect, () =>
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

        for (KitchenSetWorkerPos workerPos = KitchenSetWorkerPos.CounterWorker;
                workerPos < KitchenSetWorkerPos.MAX; workerPos++)
        {
            KitchenSetWorkerPos tempWorkerPos = workerPos;
            EWorker eWorker = (EWorker)playData.workerPos[(int)workerPos];
            staffDuties.staffDuties[workerPos].SetUI(eWorker, () =>
            {
                if (eWorker == EWorker.None)
                {
                    if (selectWorker == EWorker.None)
                        return;

                    playData.workerPos[(int)tempWorkerPos] = (int)selectWorker;
                    selectWorker = EWorker.None;
                    UpdateStaffList();
                    UpdateStaffDuties();
                }
                else
                {
                    playData.workerPos[(int)tempWorkerPos] = (int)EWorker.None;
                    selectWorker = EWorker.None;
                    UpdateStaffList();
                    UpdateStaffDuties();
                }
                staffInfo.SetUI(selectWorker);
            });
        }
    }

    public void SelectWorkerCancelBtn()
    {
        selectWorker = EWorker.None;
        UpdateStaffList();
        UpdateStaffDuties();
        staffInfo.SetUI(selectWorker);
    }
}
