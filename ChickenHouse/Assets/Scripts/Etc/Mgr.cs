using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Mgr : SerializedMonoBehaviour
{
    public static LanguageMgr   lanMgr      { get => LanguageMgr.Instance; }
    public static GameMgr       gameMgr     { get => GameMgr.Instance; }
    public static SoundMgr      soundMgr    { get => SoundMgr.Instance; }
    public static TutoMgr       tutoMgr     { get => TutoMgr.Instance; }
    public static SceneMgr      sceneMgr    { get => SceneMgr.Instance; }
    public static ShopMgr       shopMgr     { get => ShopMgr.Instance; }
    public static QuestMgr      questMgr    { get => QuestMgr.Instance; }
    public static BookMgr       bookMgr     { get => BookMgr.Instance; }
    public static WorkerMgr     workerMgr   { get => WorkerMgr.Instance; }
    public static SpicyMgr      spicyMgr    { get => SpicyMgr.Instance; }
    public static GuestMgr      guestMgr    { get => GuestMgr.Instance; }
    public static SubMenuMgr    subMenuMgr  { get => SubMenuMgr.Instance; }
}
