using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowKeyBoardKey : Mgr
{
    [SerializeField] private RectTransform AD_Key;
    [SerializeField] private RectTransform Space_Key;

    public void ActKeyBoard(int pKey)
    {
        AD_Key.gameObject.SetActive(false);
        Space_Key.gameObject.SetActive(false);
        switch (pKey)
        {
            case 1:
                AD_Key.gameObject.SetActive(true);
                break;
            case 2:
                Space_Key.gameObject.SetActive(true);
                break;
        }

    }
}
