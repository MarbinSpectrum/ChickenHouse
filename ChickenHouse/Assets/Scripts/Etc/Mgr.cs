using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mgr : MonoBehaviour
{
    public static LanguageMgr   lanMgr  { get => LanguageMgr.Instance; }
    public static GameMgr       gameMgr { get => GameMgr.Instance; }
}
