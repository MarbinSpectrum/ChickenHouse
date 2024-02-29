using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MgrObj : Mgr
{
    public static bool Init = false;
    private void Start()
    {
        if(Init)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            Init = true;
        }
    }
}
