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
        //�̱��� ����
        if (Instance == null)
        {
            Instance = gameObject.GetComponent<T>();
            //if (gameObject.scene.name == "DontDestroyOnLoad")
            //{
            //    //�̹� DontDestroyOnLoad ������Ʈ
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
