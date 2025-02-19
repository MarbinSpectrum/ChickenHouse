using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerContractCheck : Mgr
{
    private NoParaDel fun;

    public void SetUI(NoParaDel pFun)
    {
        fun = pFun;
        gameObject.SetActive(true);
    }

    public void OpenYes()
    {
        //인스펙터로 끌어서 사용하는 함수
        if (gameMgr.playData.tutoComplete4 == false)
            return;

        soundMgr.PlaySE(Sound.Btn_SE);
        gameObject.SetActive(false);
        fun?.Invoke();
    }

    public void OpenNo()
    {
        //인스펙터로 끌어서 사용하는 함수
        soundMgr.PlaySE(Sound.Btn_SE);
        gameObject.SetActive(false);

    }
}
