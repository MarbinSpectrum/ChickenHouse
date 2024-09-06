using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KitchenSetWorkerToken : Mgr
{
    [SerializeField] private Image          workerFace;
    [SerializeField] private CanvasGroup    canvasGroup;
    [SerializeField] private KitchenSet_UI  kitchenSetUI;

    private EWorker worker = EWorker.None;

    public void SetUI(EWorker pWorker,float pAlpha = 1)
    {
        if(pWorker == EWorker.None)
        {
            canvasGroup.alpha = 0;
            return;
        }

        worker = pWorker;

        WorkerData workerData = workerMgr.GetWorkerData(worker);
        workerFace.sprite = workerData.face;

        canvasGroup.alpha = pAlpha;
    }

    public void DragToken()
    {
        //�ν����Ϳ��� ��� ����ϴ� �Լ���
        kitchenSetUI.DragToken(worker);
    }
}
