using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KitchenStaffDuties : Mgr
{
    [SerializeField] private RectTransform empty;

    [SerializeField] private RectTransform faceRect;
    [SerializeField] private Image face;
    [SerializeField] private TextMeshProUGUI staffName;
    private NoParaDel selectDutiesFun;

    public void SetUI(EWorker pWorker,NoParaDel pSelectDuties)
    {
        selectDutiesFun = pSelectDuties;

        WorkerData workerData = workerMgr.GetWorkerData(pWorker);
        if (workerData == null)
        {
            face.sprite = null;
            faceRect.gameObject.SetActive(false);
            empty.gameObject.SetActive(true);
            LanguageMgr.SetString(staffName, "KITCHEN_DUTIES_EMPTY");
            return;
        }
        faceRect.gameObject.SetActive(true);
        empty.gameObject.SetActive(false);
        face.sprite = workerData.face;
        LanguageMgr.SetString(staffName, workerData.nameKey);
    }

    public void SelectDuties() => selectDutiesFun?.Invoke();
}
