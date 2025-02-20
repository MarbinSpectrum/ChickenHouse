using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformUI : Mgr
{
    [SerializeField] private bool windowUI;


    private void Awake()
    {
        gameObject.SetActive(CheckMode.IsWindow() == windowUI);
    }
}
