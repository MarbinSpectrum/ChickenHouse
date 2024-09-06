using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMgr : AwakeSingleton<SceneMgr>
{
    [SerializeField] private Dictionary<SceneChangeAni,Animator> changeList = new Dictionary<SceneChangeAni, Animator>();
    [SerializeField] private RectTransform saveAni;
    [SerializeField] private RectTransform saveCheckUI;

    private bool saveCheckFlag = false;
    private bool saveDataFlag = false;

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
            saveCheckFlag = false;
            saveDataFlag = false;

            saveCheckUI.gameObject.SetActive(true);

            yield return new WaitUntil(() => saveCheckFlag);

            saveCheckUI.gameObject.SetActive(false);

        }

        if(saveDataFlag)
        {
            //저장확인용 플래그
            saveAni.gameObject.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            saveAni.gameObject.SetActive(false);
        }

        SoundMgr soundMgr = SoundMgr.Instance;
        soundMgr.StopSE();
        soundMgr.StopBGM();

        SceneManager.LoadScene((int)scene);

        if (changeList.ContainsKey(changeAni))
            changeList[changeAni].Play("Off");
    }

    public void ShowAnimation(SceneChangeAni changeAni,bool show)
    {
        if (changeList.ContainsKey(changeAni))
            changeList[changeAni].Play(show ? "On" : "Off");
    }

    public void SaveCheckYes()
    {
        GameMgr gameMgr = GameMgr.Instance;
        gameMgr.OpenRecordUI(true,false, (saveSlot) =>
        {
            int slotNum = (int)saveSlot;
            gameMgr.selectSaveSlot = slotNum;
            saveCheckFlag = true;
            saveDataFlag = true;
        });
    }

    public void SaveCheckNo()
    {
        saveCheckFlag = true;
    }
}
