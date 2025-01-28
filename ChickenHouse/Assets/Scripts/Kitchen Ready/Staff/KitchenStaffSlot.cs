using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KitchenStaffSlot : Mgr
{
    [SerializeField] private Image              face;
    [SerializeField] private TextMeshProUGUI    staffName;

    [SerializeField] private RectTransform      selectRect;
    [SerializeField] private RectTransform      partyRect;

    private NoParaDel selectStaffFun;

    public void SetUI(EWorker pWorker,bool pSelect,bool pIsParty, NoParaDel pSelectFun)
    {
        WorkerData workerData = workerMgr.GetWorkerData(pWorker);
        if(workerData == null)
        {
            gameObject.SetActive(false);
            return;
        }

        selectStaffFun = pSelectFun;
        if (ColorUtility.TryParseHtmlString(pSelect ? "#FFFFFF" : "#B27256", out Color color))
            staffName.color = color;

        gameObject.SetActive(true);
        partyRect.gameObject.SetActive(pIsParty);
        selectRect.gameObject.SetActive(pSelect);
        face.sprite = workerData.face;
        LanguageMgr.SetString(staffName, workerData.nameKey);
    }

    public void SelectStaff() => selectStaffFun?.Invoke();
}
