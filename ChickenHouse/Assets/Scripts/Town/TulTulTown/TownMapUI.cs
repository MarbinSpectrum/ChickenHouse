using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownMapUI : Mgr
{
    [SerializeField] private Animator animator;

    public void TownMenuButton()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("TownMapStart"))
            animator.SetTrigger("On");
        else if (stateInfo.IsName("TownMapOff"))
            animator.SetTrigger("On");
        else if (stateInfo.IsName("TownMapOn"))
            animator.SetTrigger("Off");
    }
}
