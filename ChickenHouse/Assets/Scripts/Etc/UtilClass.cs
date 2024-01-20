using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilClass
{
    public static GameObject LoadPrefab(string strPath)
    {
        return Resources.Load<GameObject>(strPath);
    }
}
