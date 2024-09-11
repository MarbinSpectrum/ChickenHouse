using UnityEditor;
using UnityEngine;

public class RemovePlayerPrefs : Mgr
{
    [MenuItem("PlayData/RemovePlayerPrefs")]
    static void RemovePlayerPrefsData()
    {
        PlayerPrefs.DeleteAll();
    }
}
