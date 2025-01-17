using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiaryBtn_RedDot : Mgr
{
    [SerializeField] private Image redDot;

    private void OnEnable()
    {
        UpdateRedDot();
    }

    public void UpdateRedDot()
    {
        //Äù½ºÆ® ·¹µå´å
        bool state = ActRedDot();
        redDot.gameObject.SetActive(state);
    }

    private bool ActRedDot()
    {
        if (gameMgr.playData == null)
            return false;

        for(int i = 0; i < (int)Quest.MAX; i++)
        {
            if (gameMgr.playData.quest[i] != 1)
                continue;
            if (gameMgr.playData.questCheck[i])
                continue;
            return true;
        }
        return false;
    }
}
