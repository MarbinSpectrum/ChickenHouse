using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KichenStaffDrag : Mgr
{
    [SerializeField] private Image face;

    public EWorker worker { private set; get; }

    public void SetUI(EWorker pEWorker)
    {
        worker = pEWorker;
        WorkerData workerData = workerMgr.GetWorkerData(pEWorker);
        if (workerData == null)
            return;
        face.sprite = workerData.face;
    }

    private void Update()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(pos.x, pos.y, 0);
    }
}
