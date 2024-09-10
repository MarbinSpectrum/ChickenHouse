using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Worker_Counter : Mgr
{
    [SerializeField] private Sprite goodBox;
    [SerializeField] private Sprite normalBox;
    [SerializeField] private Sprite badBox;
    [SerializeField] private Image boxImg;
    [SerializeField] private RectTransform veryGoodIcon;
    [SerializeField] private RectTransform goodIcon;
    [SerializeField] private RectTransform badIcon;
    [SerializeField] private Animation animation;

    private WorkerData resumeData
    {
        get
        {
            EWorker eWorker = (EWorker)gameMgr.playData.workerPos[(int)KitchenSet_UI.KitchenSetWorkerPos.CounterWorker];
            return workerMgr.GetWorkerData(eWorker);
        }
    }

    public void RunTalkBox(WorkerCounterTalkBox pWorkerCounterTalkBox)
    {
        if (resumeData == null)
            return;

        animation.Rewind();
        animation.Play();
        switch (pWorkerCounterTalkBox)
        {
            case WorkerCounterTalkBox.Bad:
                {
                    boxImg.sprite = badBox;
                    veryGoodIcon.gameObject.SetActive(false);
                    goodIcon.gameObject.SetActive(false);
                    badIcon.gameObject.SetActive(true);
                }
                break;
            case WorkerCounterTalkBox.Good:
                {
                    boxImg.sprite = goodBox;
                    veryGoodIcon.gameObject.SetActive(true);
                    goodIcon.gameObject.SetActive(false);
                    badIcon.gameObject.SetActive(false);
                }
                break;
            case WorkerCounterTalkBox.Normal:
                {
                    boxImg.sprite = normalBox;
                    veryGoodIcon.gameObject.SetActive(false);
                    goodIcon.gameObject.SetActive(true);
                    badIcon.gameObject.SetActive(false);
                }
                break;
        }
    }
}
