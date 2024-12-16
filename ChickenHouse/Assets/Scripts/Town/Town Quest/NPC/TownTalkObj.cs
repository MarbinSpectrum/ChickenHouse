using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownTalkObj : Mgr
{
    [SerializeField] protected TownTalk         townTalk;
    [SerializeField] protected TownQuestTalkBox questTalkBox;
    [SerializeField] protected RewardItem       rewardItem;
    [SerializeField] protected Sprite           npcSprites0;
    [SerializeField] protected Sprite           npcSprites1;

    public virtual void Init()
    {

    }

    public virtual void StartTalk()
    {

    }
}
