using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KitchenStaffSlot : Mgr
{
    [SerializeField] private Image              face;
    [SerializeField] private TextMeshProUGUI    staffName;

    [SerializeField] private CanvasGroup        canvasGroup;
    [SerializeField] private RectTransform      selectRect;
    private NoParaDel selectStaffFun;

    public void SetUI(EWorker pWorker,bool pSelect, NoParaDel pSelectFun)
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
        selectRect.gameObject.SetActive(pSelect);
        VisibleUI(true);
        face.sprite = workerData.face;
        LanguageMgr.SetString(staffName, workerData.nameKey);
    }

    public void VisibleUI(bool pState) => canvasGroup.alpha = pState ? 1 : 0;

    public void SelectStaff() => selectStaffFun?.Invoke();
}
