using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMgr : AwakeSingleton<SceneMgr>
{
    [SerializeField] private Animator animator;

    public void SceneLoad(Scene scene)
    {
        StartCoroutine(RunSceneLoad(scene));
    }

    private IEnumerator RunSceneLoad(Scene scene)
    {
        //씬이동 코루틴
        animator.Play("On");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene((int)scene);

        animator.Play("Off");
    }
}
