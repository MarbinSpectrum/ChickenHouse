using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class AwakeSingleton<T> : SerializedMonoBehaviour where T : SerializedMonoBehaviour
{
    public static T Instance;
    protected virtual void Awake()
    {
        SetSingleton();
    }
    protected void SetSingleton()
    {
        //싱글톤 선언
        if (Instance == null)
        {
            Instance = gameObject.GetComponent<T>();
            //if (gameObject.scene.name == "DontDestroyOnLoad")
            //{
            //    //이미 DontDestroyOnLoad 오브젝트
            //    return;
            //}
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
