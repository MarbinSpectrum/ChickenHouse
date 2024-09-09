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
    [SerializeField] private Chicken_Shader[] chickenObj;


    public WorkerHandState handState { private set; get; }

    public void SetState(WorkerHandState pWorkerHandState)
    {
        handState = pWorkerHandState;
        handAni.SetInteger("HandState", (int)pWorkerHandState);
    }

    public void SetStrainter(int pChickenCnt, bool mode, float lerpValue)
    {
        for (int i = 0; i < chickenObj.Length; i++)
        {
            //치킨 갯수만큼만 치킨을 보여주자.
            bool actChicken = (i < pChickenCnt);
            chickenObj[i].gameObject.SetActive(actChicken);
            chickenObj[i].Set_Shader(mode, lerpValue);
        }
    }

    public void SetWorkerImg(EWorker pWorker)
    {
        WorkerData workerData = workerMgr.GetWorkerData(pWorker);
        handImg0.sprite = workerData.handSprite.hand0;
        handImg1.sprite = workerData.handSprite.hand1;
        handImg2.sprite = workerData.handSprite.hand2;
    }
}
