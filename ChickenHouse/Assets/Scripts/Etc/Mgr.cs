using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Mgr : SerializedMonoBehaviour
{
    public static LanguageMgr   lanMgr      { get => LanguageMgr.Instance; }
    public static GameMgr       gameMgr     { get => GameMgr.Instance; }
    public static SoundMgr      soundMgr    { get => SoundMgr.Instance; }
}
