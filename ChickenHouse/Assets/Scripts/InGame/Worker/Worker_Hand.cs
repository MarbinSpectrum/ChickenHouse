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

    [System.Serializable]
    public struct SPRITE
    {
        /** 엄지 **/
        public Sprite hand0;
        /** 나머지 손 **/
        public Sprite hand1;
        /** 보통 상태 손 **/
        public Sprite hand2;
    }
    [SerializeField] private Dictionary<ShopItem, SPRITE> handSprite;

    public WorkerHandState handState { private set; get; }

    public void SetState(WorkerHandState pWorkerHandState)
    {
        handState = pWorkerHandState;
        handAni.SetInteger("HandState", (int)pWorkerHandState);
    }

    public void SetWorkerImg(ShopItem pWorker)
    {
        handImg0.sprite = handSprite[pWorker].hand0;
        handImg1.sprite = handSprite[pWorker].hand1;
        handImg2.sprite = handSprite[pWorker].hand2;
    }
}
