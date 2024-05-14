using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMgr : AwakeSingleton<SceneMgr>
{
    [SerializeField] private Dictionary<SceneChangeAni,Animator> changeList = new Dictionary<SceneChangeAni, Animator>();
    [SerializeField] private RectTransform saveObj;

    public void SceneLoad(Scene scene,bool save, SceneChangeAni changeAni = SceneChangeAni.NOT)
    {
        StartCoroutine(RunSceneLoad(scene, save, changeAni));
    }

    private IEnumerator RunSceneLoad(Scene scene, bool save, SceneChangeAni changeAni)
    {
        //씬이동 코루틴
        if(changeList.ContainsKey(changeAni))
            changeList[changeAni].Play("On");

        yield return new WaitForSeconds(1f);

        if(save)
        {
            saveObj.gameObject.SetActive(true);

            GameMgr gameMgr = GameMgr.Instance;
            gameMgr.SaveData();

            yield return new WaitForSeconds(1.5f);
        }

        saveObj.gameObject.SetActive(false);

        SoundMgr soundMgr = SoundMgr.Instance;
        soundMgr.StopSE();
        soundMgr.StopBGM();

        SceneManager.LoadScene((int)scene);

        if (changeList.ContainsKey(changeAni))
            changeList[changeAni].Play("Off");
    }
}
