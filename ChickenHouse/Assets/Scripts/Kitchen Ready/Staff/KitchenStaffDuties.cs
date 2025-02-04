using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KitchenStaffDuties : Mgr
{
    [SerializeField] private RectTransform      empty;

    [SerializeField] private RectTransform      closeBtn;
    [SerializeField] private RectTransform      faceRect;
    [SerializeField] private Image              face;
    [SerializeField] private TextMeshProUGUI    staffName;
    [SerializeField] private Image              enterEffect;
    [SerializeField] private KichenStaffDrag    dragObj;

    private NoParaDel selectDutiesFun;
    private NoParaDel removeDutiesFun;
    private EWorker eWorker;

    public void SetUI(EWorker pWorker,NoParaDel pSelectDuties, NoParaDel pRemoveDuties)
    {
        eWorker = pWorker;
        selectDutiesFun = pSelectDuties;
        removeDutiesFun = pRemoveDuties;

        WorkerData workerData = workerMgr.GetWorkerData(pWorker);
        if (workerData == null)
        {
            face.sprite = null;
            faceRect.gameObject.SetActive(false);
            empty.gameObject.SetActive(true);
            LanguageMgr.SetString(staffName, "KITCHEN_DUTIES_EMPTY");
            closeBtn.gameObject.SetActive(false);
            return;
        }
        faceRect.gameObject.SetActive(true);
        empty.gameObject.SetActive(false);
        face.sprite = workerData.face;
        LanguageMgr.SetString(staffName, workerData.nameKey);
        closeBtn.gameObject.SetActive(true);
        enterEffect.gameObject.SetActive(false);

    }

    public void SelectDuties() => selectDutiesFun?.Invoke();

    public void RemoveDuties() => removeDutiesFun?.Invoke();

    public void EnterEvent()
    {
        if (eWorker != EWorker.None)
            return;
        if (dragObj.worker == EWorker.None)
            return;

        if (gameMgr.playData != null)
        {
            for (KitchenSetWorkerPos setCheck = KitchenSetWorkerPos.CounterWorker;
                setCheck < KitchenSetWorkerPos.MAX; setCheck++)
            {
                EWorker worker = (EWorker)gameMgr.playData.workerPos[(int)setCheck];
                if (dragObj.worker == worker)
                {
                    //해당 직원은 이미 배치되어 있다.
                    return;
                }
            }
        }

        enterEffect.gameObject.SetActive(true);
    }

    public void ExitEvent()
    {
        enterEffect.gameObject.SetActive(false);
    }

    public void AddEvent()
    {
        if (dragObj.worker == EWorker.None)
            return;
        selectDutiesFun?.Invoke();
    }
}
