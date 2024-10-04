using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BadEndStory : Mgr
{
    [SerializeField] private Animator       creditorAni;
    [SerializeField] private TalkBox_UI     talkBox;
    [SerializeField] private BlurFast       blurEffect;
    [SerializeField] private GameObject  nightCounter;
    [SerializeField] private GameObject morningCounter;

    private bool skipTuto = false;
    private bool goInGame = false;

    private IEnumerator talkCor;

    private void Start()
    {
        soundMgr.MuteSE(false);
        soundMgr.PlayBGM(Sound.Prologue_BG);

        Prologue1();
    }

    private void Prologue1()
    {
        nightCounter.SetActive(true);
        morningCounter.SetActive(false);

        IEnumerator RunCor()
        {
            yield return new WaitForSeconds(1f);

            creditorAni.SetTrigger("Show");
            yield return new WaitForSeconds(2f);

            creditorAni.SetBool("Talk", true);
            string talkStr = LanguageMgr.GetText("BAD_END_1");

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
            string talkStr = LanguageMgr.GetText("BAD_END_2");

            soundMgr.PlayLoopSE(Sound.Voice7_SE);
            talkBox.ShowText(talkStr, TalkBoxType.Normal, () =>
            {
                if (skipTuto)
                    return;

                creditorAni.SetBool("Talk", false);
                soundMgr.StopLoopSE(Sound.Voice7_SE);
                BlurEffect();
            });
        }
        talkCor = RunCor();
        StartCoroutine(talkCor);
    }

    private void BlurEffect()
    {
        IEnumerator RunCor()
        {
            yield return new WaitForSeconds(1f);
            talkBox.CloseTalkBox();
            creditorAni.SetBool("Talk", true);
            blurEffect.gameObject.SetActive(true);

            float seValue = soundMgr.seValue;
            for(int i = 0; i < 3; i++)
            {
                soundMgr.PlaySE(Sound.Laugh0_SE, seValue);
                yield return new WaitForSeconds(1f);
            }
            for (int i = 0; i < 5; i++)
            {
                seValue *= 0.5f;
                soundMgr.PlaySE(Sound.Laugh0_SE, seValue);
                yield return new WaitForSeconds(1f);
            }
            yield return new WaitForSeconds(2f);
            soundMgr.PlayBGM(Sound.Morning_BG);
            nightCounter.SetActive(false);
            morningCounter.SetActive(true);
            GoTown();
        }
        talkCor = RunCor();
        StartCoroutine(talkCor);
    }

    private void GoTown()
    {
        if (goInGame)
            return;
        goInGame = true;
        gameMgr.playData.day -= 3;
        WakeupMsg.wakeUpFlag = true;
        IEnumerator RunCor()
        {
            yield return new WaitForSeconds(10f);

            sceneMgr.SceneLoad(Scene.TOWN, false, false, SceneChangeAni.CIRCLE);
        }
        StartCoroutine(RunCor());
    }
}
