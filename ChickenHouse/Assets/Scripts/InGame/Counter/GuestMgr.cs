using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestMgr : Mgr
{
    public static GuestMgr Instance;

    [SerializeField] private Fox fox;


    protected void Awake()
    {
        SetSingleton();
    }

    protected void SetSingleton()
    {
        //½Ì±ÛÅæ ¼±¾ð
        if (Instance == null)
        {
            Instance = gameObject.GetComponent<GuestMgr>();
        }
    }

    public void ShowGuest(Guest guest)
    {

    }
}
