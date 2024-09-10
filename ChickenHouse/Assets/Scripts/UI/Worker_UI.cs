using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Worker_UI : Mgr
{
    [SerializeField] private Image[] icon;
    [SerializeField] private RectTransform[] workerObjs;
    [SerializeField] private CanvasGroup canvasGroup;

    public void Init()
    {
        foreach (RectTransform rect in workerObjs)
            rect.gameObject.SetActive(false);

        for (KitchenSet_UI.KitchenSetWorkerPos pos = KitchenSet_UI.KitchenSetWorkerPos.KitchenWorker0; pos < KitchenSet_UI.KitchenSetWorkerPos.MAX; pos++)
        {
            int idx = (int)pos;
            EWorker eWorker = (EWorker)gameMgr.playData.workerPos[idx];
            WorkerData resumeData = workerMgr.GetWorkerData(eWorker);
            if (resumeData == null)
                continue;


            //초상화 설정
            if (resumeData != null)
            {
                workerObjs[idx].gameObject.SetActive(true);
                icon[idx].sprite = resumeData.face;
            }
        }
    }

    public void OffBox()
    {
        canvasGroup.alpha = 0;
    }

    public void OnBox()
    {
        canvasGroup.alpha = 1;
    }
}
