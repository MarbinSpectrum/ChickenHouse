using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerMgr : AwakeSingleton<WorkerMgr>
{
    [SerializeField] private Dictionary<EWorker, WorkerData> workerData = new Dictionary<EWorker, WorkerData>();

    public WorkerData GetWorkerData(EWorker pWorker)
    {
        //���� ���� ���
        if (workerData.ContainsKey(pWorker))
            return workerData[pWorker];
        return null;
    }
}