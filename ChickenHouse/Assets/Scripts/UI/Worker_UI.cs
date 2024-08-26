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
        WorkerData resumeData = null;

        foreach(RectTransform rect in workerObjs)
            rect.gameObject.SetActive(false);

        //초상화 설정
        if(resumeData != null)
        {
            workerObjs[0].gameObject.SetActive(true);
            icon[0].sprite = resumeData.face;
        }

        OffBox();
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
