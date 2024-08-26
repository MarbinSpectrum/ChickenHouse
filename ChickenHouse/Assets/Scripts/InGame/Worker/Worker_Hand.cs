using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Worker_Hand : Mgr
{
    [SerializeField] private Animator handAni;
    [SerializeField] private Image handImg0;
    [SerializeField] private Image handImg1;
    [SerializeField] private Image handImg2;


    public WorkerHandState handState { private set; get; }

    public void SetState(WorkerHandState pWorkerHandState)
    {
        handState = pWorkerHandState;
        handAni.SetInteger("HandState", (int)pWorkerHandState);
    }

    public void SetWorkerImg(EWorker pWorker)
    {
        WorkerData workerData = workerMgr.GetWorkerData(pWorker);
        handImg0.sprite = workerData.handSprite.hand0;
        handImg1.sprite = workerData.handSprite.hand1;
        handImg2.sprite = workerData.handSprite.hand2;
    }
}
