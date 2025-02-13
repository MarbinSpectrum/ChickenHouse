using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongNoseContractCheck : Mgr
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
