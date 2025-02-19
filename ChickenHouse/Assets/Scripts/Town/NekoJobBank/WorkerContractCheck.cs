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
        //�ν����ͷ� ��� ����ϴ� �Լ�
        if (gameMgr.playData.tutoComplete4 == false)
            return;

        soundMgr.PlaySE(Sound.Btn_SE);
        gameObject.SetActive(false);
        fun?.Invoke();
    }

    public void OpenNo()
    {
        //�ν����ͷ� ��� ����ϴ� �Լ�
        soundMgr.PlaySE(Sound.Btn_SE);
        gameObject.SetActive(false);

    }
}
