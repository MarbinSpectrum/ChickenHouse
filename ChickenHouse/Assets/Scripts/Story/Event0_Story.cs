using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class Event0_Story : Mgr
{
    [SerializeField] private Animator morningCompetitorsAni;
    [SerializeField] private TalkBox_UI morningTalkBox;
    [SerializeField] private Animator nightCompetitorsAni;
    [SerializeField] private TalkBox_UI nightTalkBox;
    [SerializeField] private RectTransform selectMenu;
    [SerializeField] private GameObject morning;
    [SerializeField] private GameObject night;

    [SerializeField] private RectTransform      stealMsgBox;
    [SerializeField] private TextMeshProUGUI    stealText;

    private bool skipTuto = false;
    private bool goInGame = false;

    private IEnumerator talkCor;

    private void Start()
    {
        if ((QuestState)gameMgr.playData.quest[(int)Quest.Event_0_Quest] == QuestState.Not)
        {
            morning.SetActive(true);
            night.SetActive(false);
            soundMgr.MuteSE(false);
            soundMgr.PlayBGM(Sound.Morning_BG);

            Event1();
        }
        else if ((QuestState)gameMgr.playData.quest[(int)Quest.Event_0_Quest] == QuestState.Run)
        {
            morning.SetActive(false);
            night.SetActive(true);
            soundMgr.MuteSE(false);
            soundMgr.PlayBGM(Sound.Prologue_BG);

            if (gameMgr.playData.questCnt[(int)Quest.Event_0_Quest] >= QuestMgr.EVENT0_CNT)
                WinEvent0();
            else
                LoseEvent0();

        }
    }

    private void Event1()
    {
        IEnumerator RunCor()
        {
            yield return new WaitForSeconds(1f);

            morningCompetitorsAni.SetTrigger("Show");
            yield return new WaitForSeconds(2f);

            morningCompetitorsAni.SetInteger("Talk", 1);
            string talkStr = LanguageMgr.GetText("EVENT0_1");

            soundMgr.PlayLoopSE(Sound.Voice28_SE);
            morningTalkBox.ShowText(talkStr, TalkBoxType.Normal, () =>
            {
                if (skipTuto)
                    return;

                morningCompetitorsAni.SetInteger("Talk", 0);
                soundMgr.StopLoopSE(Sound.Voice28_SE);
                Event2();
            });
        }
        talkCor = RunCor();
        StartCoroutine(talkCor);
    }

    private void Event2()
    {
        IEnumerator RunCor()
        {
            yield return new WaitForSeconds(1f);

            morningCompetitorsAni.SetInteger("Talk", 1);
            string talkStr = LanguageMgr.GetText("EVENT0_2");

            soundMgr.PlayLoopSE(Sound.Voice28_SE);
            morningTalkBox.ShowText(talkStr, TalkBoxType.Normal, () =>
            {
                if (skipTuto)
                    return;

                morningCompetitorsAni.SetInteger("Talk", 0);
                soundMgr.StopLoopSE(Sound.Voice28_SE);
                Event3();
            });
        }
        talkCor = RunCor();
        StartCoroutine(talkCor);
    }

    private void Event3()
    {
        IEnumerator RunCor()
        {
            yield return new WaitForSeconds(1f);

            morningCompetitorsAni.SetInteger("Talk", 1);
            string talkStr = LanguageMgr.GetText("EVENT0_3");

            soundMgr.PlayLoopSE(Sound.Voice28_SE);
            morningTalkBox.ShowText(talkStr, TalkBoxType.Normal, () =>
            {
                if (skipTuto)
                    return;

                morningCompetitorsAni.SetInteger("Talk", 0);
                soundMgr.StopLoopSE(Sound.Voice28_SE);
                Event4();
            });
        }
        talkCor = RunCor();
        StartCoroutine(talkCor);
    }

    private void Event4()
    {
        IEnumerator RunCor()
        {
            yield return new WaitForSeconds(1f);

            morningCompetitorsAni.SetInteger("Talk", 1);
            string talkStr = LanguageMgr.GetText("EVENT0_4");

            soundMgr.PlayLoopSE(Sound.Voice28_SE);
            morningTalkBox.ShowText(talkStr, TalkBoxType.Normal, () =>
            {
                if (skipTuto)
                    return;

                morningCompetitorsAni.SetInteger("Talk", 0);
                soundMgr.StopLoopSE(Sound.Voice28_SE);
                Event5();
            });
        }
        talkCor = RunCor();
        StartCoroutine(talkCor);
    }

    private void Event5()
    {
        IEnumerator RunCor()
        {
            yield return new WaitForSeconds(1f);

            morningCompetitorsAni.SetInteger("Talk", 1);
            string talkStr = LanguageMgr.GetText("EVENT0_5");

            soundMgr.PlayLoopSE(Sound.Voice28_SE);
            morningTalkBox.ShowText(talkStr, TalkBoxType.Normal, () =>
            {
                if (skipTuto)
                    return;

                morningCompetitorsAni.SetInteger("Talk", 0);
                soundMgr.StopLoopSE(Sound.Voice28_SE);
                Event6();
            });
        }
        talkCor = RunCor();
        StartCoroutine(talkCor);
    }

    private void Event6()
    {
        IEnumerator RunCor()
        {
            yield return new WaitForSeconds(1f);

            morningCompetitorsAni.SetInteger("Talk", 1);
            string talkStr = LanguageMgr.GetText("EVENT0_6");

            soundMgr.PlayLoopSE(Sound.Voice28_SE);
            morningTalkBox.ShowText(talkStr, TalkBoxType.Normal, () =>
            {
                if (skipTuto)
                    return;

                morningCompetitorsAni.SetInteger("Talk", 0);
                soundMgr.StopLoopSE(Sound.Voice28_SE);
                Event7();
            });
        }
        talkCor = RunCor();
        StartCoroutine(talkCor);
    }

    private void Event7()
    {
        IEnumerator RunCor()
        {
            yield return new WaitForSeconds(1f);

            morningCompetitorsAni.SetInteger("Talk", 1);
            string talkStr = LanguageMgr.GetText("EVENT0_7");

            soundMgr.PlayLoopSE(Sound.Voice28_SE);
            morningTalkBox.ShowText(talkStr, TalkBoxType.Normal, () =>
            {
                if (skipTuto)
                    return;

                morningCompetitorsAni.SetInteger("Talk", 0);
                soundMgr.StopLoopSE(Sound.Voice28_SE);
                Event8();
            });
        }
        talkCor = RunCor();
        StartCoroutine(talkCor);
    }

    private void Event8()
    {
        IEnumerator RunCor()
        {
            yield return new WaitForSeconds(1f);

            morningCompetitorsAni.SetInteger("Talk", 1);
            string talkStr = LanguageMgr.GetText("EVENT0_8");

            soundMgr.PlayLoopSE(Sound.Voice28_SE);
            morningTalkBox.ShowText(talkStr, TalkBoxType.Normal, () =>
            {
                if (skipTuto)
                    return;

                morningCompetitorsAni.SetInteger("Talk", 0);
                soundMgr.StopLoopSE(Sound.Voice28_SE);
                Event9();
            });
        }
        talkCor = RunCor();
        StartCoroutine(talkCor);
    }

    private void Event9()
    {
        IEnumerator RunCor()
        {
            yield return new WaitForSeconds(1f);

            morningCompetitorsAni.SetInteger("Talk", 1);
            string talkStr = LanguageMgr.GetText("EVENT0_9");

            soundMgr.PlayLoopSE(Sound.Voice28_SE);
            morningTalkBox.ShowText(talkStr, TalkBoxType.Normal, () =>
            {
                if (skipTuto)
                    return;

                morningCompetitorsAni.SetInteger("Talk", 0);
                soundMgr.StopLoopSE(Sound.Voice28_SE);
                selectMenu.gameObject.SetActive(true);
            });
        }
        talkCor = RunCor();
        StartCoroutine(talkCor);
    }

    public void SelectBattle(bool goBattle)
    {
        if (selectMenu.gameObject.activeSelf == false)
            return;
        selectMenu.gameObject.SetActive(false);
        if (goBattle)
        {
            gameMgr.playData.quest[(int)Quest.Event_0_Quest] = (int)QuestState.Run;
            IEnumerator RunCor()
            {
                yield return new WaitForSeconds(1f);

                morningCompetitorsAni.SetInteger("Talk", 1);
                string talkStr = LanguageMgr.GetText("EVENT0_10_0");

                soundMgr.PlayLoopSE(Sound.Voice28_SE);
                morningTalkBox.ShowText(talkStr, TalkBoxType.Normal, () =>
                {
                    if (skipTuto)
                        return;

                    morningCompetitorsAni.SetInteger("Talk", 0);
                    soundMgr.StopLoopSE(Sound.Voice28_SE);
                    GoTown(false);
                });
            }
            talkCor = RunCor();
            StartCoroutine(talkCor);
        }
        else
        {
            IEnumerator RunCor()
            {
                yield return new WaitForSeconds(1f);

                morningCompetitorsAni.SetInteger("Talk", 1);
                string talkStr = LanguageMgr.GetText("EVENT0_10_1");

                soundMgr.PlayLoopSE(Sound.Voice28_SE);
                morningTalkBox.ShowText(talkStr, TalkBoxType.Normal, () =>
                {
                    if (skipTuto)
                        return;

                    morningCompetitorsAni.SetInteger("Talk", 0);
                    soundMgr.StopLoopSE(Sound.Voice28_SE);
                    GoTown(false);
                });
            }
            talkCor = RunCor();
            StartCoroutine(talkCor);
        }
    }

    private void WinEvent0()
    {
        IEnumerator RunCor()
        {
            yield return new WaitForSeconds(1f);

            nightCompetitorsAni.SetTrigger("Show");
            yield return new WaitForSeconds(2f);

            nightCompetitorsAni.SetInteger("Talk", 3);
            string talkStr = LanguageMgr.GetText("EVENT0_11_0");

            soundMgr.PlayLoopSE(Sound.Voice28_SE);
            nightTalkBox.ShowText(talkStr, TalkBoxType.Normal, () =>
            {
                if (skipTuto)
                    return;

                soundMgr.StopLoopSE(Sound.Voice28_SE);
                WinEvent1();
            });
        }
        talkCor = RunCor();
        StartCoroutine(talkCor);
    }

    private void WinEvent1()
    {
        IEnumerator RunCor()
        {
            yield return new WaitForSeconds(1f);

            nightCompetitorsAni.SetInteger("Talk", 3);
            string talkStr = LanguageMgr.GetText("EVENT0_11_1");

            soundMgr.PlayLoopSE(Sound.Voice28_SE);
            nightTalkBox.ShowText(talkStr, TalkBoxType.Normal, () =>
            {
                if (skipTuto)
                    return;

                soundMgr.StopLoopSE(Sound.Voice28_SE);
                GoTown(true);
            });
        }
        talkCor = RunCor();
        StartCoroutine(talkCor);
    }

    private void LoseEvent0()
    {
        gameMgr.playData.quest[(int)Quest.Event_0_Quest] = (int)QuestState.Complete;
        IEnumerator RunCor()
        {
            yield return new WaitForSeconds(1f);

            nightCompetitorsAni.SetTrigger("Show");
            yield return new WaitForSeconds(2f);

            nightCompetitorsAni.SetInteger("Talk", 2);
            string talkStr = LanguageMgr.GetText("EVENT0_12_0");

            soundMgr.PlayLoopSE(Sound.Voice28_SE);
            nightTalkBox.ShowText(talkStr, TalkBoxType.Normal, () =>
            {
                if (skipTuto)
                    return;

                soundMgr.StopLoopSE(Sound.Voice28_SE);
                LoseEvent1();
            });
        }
        talkCor = RunCor();
        StartCoroutine(talkCor);
    }

    private void LoseEvent1()
    {
        IEnumerator RunCor()
        {
            yield return new WaitForSeconds(1f);

            nightCompetitorsAni.SetInteger("Talk", 2);
            string talkStr = LanguageMgr.GetText("EVENT0_12_1");

            soundMgr.PlayLoopSE(Sound.Voice28_SE);
            nightTalkBox.ShowText(talkStr, TalkBoxType.Normal, () =>
            {
                if (skipTuto)
                    return;

                soundMgr.StopLoopSE(Sound.Voice28_SE);
                RemoveChickenRecipe();
            });
        }
        talkCor = RunCor();
        StartCoroutine(talkCor);
    }

    private void RemoveChickenRecipe()
    {
        List<ShopItem> randomList = new List<ShopItem>();
        for (ChickenSpicy spicy = ChickenSpicy.Hot; spicy <= ChickenSpicy.MAX; spicy++)
        {
            ShopItem shopItem = SpicyMgr.SpicyGetRecipe(spicy);
            if (gameMgr.playData.hasItem[(int)shopItem])
                randomList.Add(shopItem);
        }
        if (randomList.Count > 0)
        {
            int r = Random.Range(0, randomList.Count);
            gameMgr.playData.hasItem[(int)randomList[r]] = false;
            ChickenSpicy chickenSpicy = SpicyMgr.RecipeGetSpicy(randomList[r]);
            for (int i = 0; i < (int)MenuSetPos.SpicyMAX; i++)
            {
                if (gameMgr.playData.spicyState[i] == (int)chickenSpicy)
                    gameMgr.playData.spicyState[i] = (int)ChickenSpicy.None;
            }

            SpicyData spicyData = spicyMgr.GetSpicyData(chickenSpicy);
            string spicyName = LanguageMgr.GetText(spicyData.nameKey);
            string strFormat = LanguageMgr.GetText("STEAL_RECIPE");
            string str = string.Format(strFormat, spicyName);
            LanguageMgr.SetText(stealText, str);
            stealMsgBox.gameObject.SetActive(true);
        }
        else
        {
            GoTown(false);
        }

    }

    public void StealMsgBoxOK()
    {
        stealMsgBox.gameObject.SetActive(false);
        GoTown(false);
    }

    private void GoTown(bool checkQuest)
    {
        if (goInGame)
            return;
        goInGame = true;
        soundMgr.StopLoopSE(Sound.Voice28_SE);
        IEnumerator RunCor()
        {
            yield return new WaitForSeconds(1f);

            sceneMgr.SceneLoad(Scene.TOWN, checkQuest, true, SceneChangeAni.CIRCLE);
        }
        StartCoroutine(RunCor());
    }
}
