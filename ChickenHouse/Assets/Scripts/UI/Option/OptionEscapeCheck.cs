using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionEscapeCheck : Mgr
{
    [SerializeField] private Option_UI optionUI;

    private void Update()
    {
        PlayData playData = gameMgr?.playData;
        if (playData == null)
            return;

        if(Input.GetKeyUp(KeyCode.Escape))
        {
            if (optionUI.gameObject.activeSelf == false)
                optionUI.Set_UI();
            else
                optionUI.Close_UI();
        }
    }
}
