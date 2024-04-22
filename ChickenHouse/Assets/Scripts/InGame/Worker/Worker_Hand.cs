using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker_Hand : Mgr
{
    [SerializeField] private Animator handAni;

    public WorkerHandState handState { private set; get; }

    public void SetState(WorkerHandState pWorkerHandState)
    {
        handState = pWorkerHandState;
        handAni.SetInteger("HandState", (int)pWorkerHandState);
    }
}
