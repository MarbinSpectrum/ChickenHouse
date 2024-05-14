using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrologueStory : Mgr
{
    [SerializeField] private Animator creditorAni;
    [SerializeField] private TalkBox_UI talkBox;
    [SerializeField] private Button skipBtn;

    private bool skipTuto = false;
    private bool goInGame = false;

    private IEnumerator talkCor;

    private void Awake()
    {
        //버튼설정
        skipBtn.onClick.RemoveAllListeners();
        skipBtn.onClick.AddListener(() => SkipBtn());
    }

    private void Start()
    {
        gameMgr.LoadData();
        soundMgr.PlayBGM(Sound.Prologue_BG);

        //첫 프롤로그는 스킵이 안되게
        bool actBtn = PlayerPrefs.GetInt("PROLOGUE", 0) == 1;
        skipBtn.gameObject.SetActive(actBtn);

        PlayerPrefs.SetInt("PROLOGUE", 1);

        Prologue1();
    }

    private void Prologue1()
    {
        IEnumerator RunCor()
        {
            yield return new WaitForSeconds(1f);

            creditorAni.SetTrigger("Show");
            yield return new WaitForSeconds(2f);

            creditorAni.SetBool("Talk", true);
            string talkStr = LanguageMgr.GetText("PROLOGUE_1");

            soundMgr.PlayLoopSE(Sound.Voice7_SE);
            talkBox.ShowText(talkStr, TalkBoxType.Normal, () =>
            {
                if (skipTuto)
                    return;

                creditorAni.SetBool("Talk", false);
                soundMgr.StopLoopSE(Sound.Voice7_SE);
                Prologue2();
            });
        }
        talkCor = RunCor();
        StartCoroutine(talkCor);
    }

    private void Prologue2()
    {
        IEnumerator RunCor()
        {
            yield return new WaitForSeconds(1f);

            creditorAni.SetBool("Talk", true);
            string talkStr = LanguageMgr.GetText("PROLOGUE_2");

            soundMgr.PlayLoopSE(Sound.Voice7_SE);
            talkBox.ShowText(talkStr, TalkBoxType.Normal, () =>
            {
                if (skipTuto)
                    return;

                creditorAni.SetBool("Talk", false);
                soundMgr.StopLoopSE(Sound.Voice7_SE);
                Prologue3();
            });
        }
        talkCor = RunCor();
        StartCoroutine(talkCor);
    }

    private void Prologue3()
    {
        IEnumerator RunCor()
        {
            yield return new WaitForSeconds(1f);

            creditorAni.SetBool("Talk", true);
            string talkStr = LanguageMgr.GetText("PROLOGUE_3");

            soundMgr.PlayLoopSE(Sound.Voice7_SE);
            talkBox.ShowText(talkStr, TalkBoxType.Normal, () =>
            {
                if (skipTuto)
                    return;

                creditorAni.SetBool("Talk", false);
                soundMgr.StopLoopSE(Sound.Voice7_SE);
                Prologue4();
            });
        }
        talkCor = RunCor();
        StartCoroutine(talkCor);
    }

    private void Prologue4()
    {
        IEnumerator RunCor()
        {
            yield return new WaitForSeconds(1f);

            creditorAni.SetBool("Talk", true);
            string talkStr = LanguageMgr.GetText("PROLOGUE_4");

            soundMgr.PlayLoopSE(Sound.Voice7_SE);
            talkBox.ShowText(talkStr, TalkBoxType.Normal, () =>
            {
                if (skipTuto)
                    return;

                creditorAni.SetBool("Talk", false);
                soundMgr.StopLoopSE(Sound.Voice7_SE);
                Prologue5();
            });
        }
        talkCor = RunCor();
        StartCoroutine(talkCor);
    }

    private void Prologue5()
    {
        IEnumerator RunCor()
        {
            yield return new WaitForSeconds(1f);

            creditorAni.SetBool("Talk", true);
            string talkStr = LanguageMgr.GetText("PROLOGUE_5");

            soundMgr.PlayLoopSE(Sound.Voice7_SE);
            talkBox.ShowText(talkStr, TalkBoxType.Normal, () =>
            {
                if (skipTuto)
                    return;

                creditorAni.SetBool("Talk", false);
                soundMgr.StopLoopSE(Sound.Voice7_SE);
                Prologue6();
            });
        }
        talkCor = RunCor();
        StartCoroutine(talkCor);
    }

    private void Prologue6()
    {
        IEnumerator RunCor()
        {
            yield return new WaitForSeconds(1f);

            creditorAni.SetBool("Talk", true);
            string talkStr = LanguageMgr.GetText("PROLOGUE_6");

            soundMgr.PlayLoopSE(Sound.Voice7_SE);
            talkBox.ShowText(talkStr, TalkBoxType.Normal, () =>
            {
                if (skipTuto)
                    return;

                creditorAni.SetBool("Talk", false);
                soundMgr.StopLoopSE(Sound.Voice7_SE);
                GoInGame();
            });
        }
        talkCor = RunCor();
        StartCoroutine(talkCor);
    }

    private void SkipBtn()
    {
        if (skipTuto)
            return;
        skipTuto = true;

        if (talkCor != null)
        {
            StopCoroutine(talkCor);
            talkCor = null;
        }

        GoInGame();
    }

    private void GoInGame()
    {
        if (goInGame)
            return;
        goInGame = true;
        soundMgr.StopLoopSE(Sound.Voice7_SE);
        IEnumerator RunCor()
        {
            yield return new WaitForSeconds(1f);

            sceneMgr.SceneLoad(Scene.INGAME,true, SceneChangeAni.CIRCLE);
        }
        StartCoroutine(RunCor());
    }
}
