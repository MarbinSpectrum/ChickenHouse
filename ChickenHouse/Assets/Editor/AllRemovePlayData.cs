using UnityEditor;

public class AllRemovePlayData : Mgr
{
    [MenuItem("PlayData/RemoveAllData")]
    static void RemoveAllData()
    {
        for (int i = 0; i <= 12; i++)
            MyLib.Json.DeleteJsonFile(string.Format("PlayData{0}", i));
    }
}
