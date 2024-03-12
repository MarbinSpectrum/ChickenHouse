using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Title_StartText : Mgr
{
    public Animation[] animation;
    public float dis;

    private void Start()
    {
        StartCoroutine(runAni());
    }

    private IEnumerator runAni()
    {
        foreach(Animation ani in animation)
        {
            ani.Play();

            yield return new WaitForSeconds(dis);
        }

        if(gameObject.activeSelf)
        {
            yield return new WaitForSeconds(2f);
            StartCoroutine(runAni());
        }
    }
}
