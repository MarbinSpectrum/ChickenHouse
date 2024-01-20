using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleAni : Mgr
{
    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.Update(Random.Range(0, 30));
    }
}
