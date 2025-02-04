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
    private NoParaDel dragStart;
    private NoParaDel dragEnd;

    public void SetUI(EWorker pWorker,bool pSelect,bool pIsParty, NoParaDel pSelectFun
        , NoParaDel pDragStartFun, NoParaDel pDragEndFun)
    {
        WorkerData workerData = workerMgr.GetWorkerData(pWorker);
        if(workerData == null)
        {
            gameObject.SetActive(false);
            return;
        }

        selectStaffFun = pSelectFun;
        dragStart = pDragStartFun;
        dragEnd = pDragEndFun;

        gameObject.SetActive(true);
        partyRect.gameObject.SetActive(pIsParty);
        selectRect.gameObject.SetActive(pSelect);
        face.sprite = workerData.face;
        LanguageMgr.SetString(staffName, workerData.nameKey, true);
    }

    public void SelectStaff() => selectStaffFun?.Invoke();

    public void DragStart() => dragStart?.Invoke();

    public void DragEnd() => dragEnd?.Invoke();
}
