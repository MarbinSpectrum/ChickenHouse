using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwakeSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance;
    protected virtual void Awake()
    {
        SetSingleton();
    }
    protected void SetSingleton()
    {
        //½Ì±ÛÅæ ¼±¾ð
        if (Instance == null)
        {
            Instance = gameObject.GetComponent<T>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
