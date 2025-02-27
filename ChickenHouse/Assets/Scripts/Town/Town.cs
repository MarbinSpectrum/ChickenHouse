using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Town : Mgr
{
    [SerializeField] private Dictionary<TownMap, RectTransform> townMap = new Dictionary<TownMap, RectTransform>();
    [SerializeField] private Animator                           moveAni;
    [SerializeField] private RectTransform                      diaryBtn;
    [SerializeField] private WarningText                        warningText;
    [SerializeField] private WakeupMsg                          wakeUpMsg;
    [SerializeField] private List<TownTalkObj>                  npcList;
    [SerializeField] private RectTransform                      tutoDontTouch;

    [SerializeField] private TutoObj                            tutoObj1;
    [SerializeField] private TutoObj                            tutoObj2;
    [SerializeField] private TutoObj                            tutoObj4;
    [SerializeField] private TutoObj                            tutoObj5;
    [SerializeField] private TutoObj                            tutoObj6;

    public bool nowSceneChange { get; private set; }
    /** 현재 지역 **/
    private TownMap nowArea = TownMap.TulTulTown;
    /** 중복 이동 방지용 **/
    private bool isMove = false;

    private bool warningFlag = false;

    private IEnumerator townMapLoadCor;

    private void Start()
    {
        ActTownMove(TownMap.None,nowArea);
        isMove = false;
        nowSceneChange = false;
        if (gameMgr.playData != null && gameMgr.playData.tutoComplete4 == false)
        {
            tutoDontTouch.gameObject.SetActive(true);
            tutoObj1.PlayTuto();
        }
        else
        {
            tutoDontTouch.gameObject.SetActive(false);
        }

        TownNpcInit();

        if (WakeupMsg.wakeUpFlag)
        {
            wakeUpMsg.SetUI();
        }
        else if (warningFlag == false)
        {
            warningFlag = true;
            if (gameMgr?.playData == null)
                return;

            if ((QuestState)gameMgr.playData.quest[(int)Quest.Event_0_Quest] == QuestState.Run)
            {
                //Event 0 진행중
                string str = LanguageMgr.GetText("EVENT0_WARNING_2");
                warningText.SetText(str);
            }
            else if ((QuestState)gameMgr.playData.quest[(int)Quest.MainQuest_1] == QuestState.Run)
            {
                //메인퀘스트1 진행중
                if (QuestMgr.MAIN_QUEST_1_LIMIT_DAY - gameMgr.playData.day <= 0)
                {
                    //퀘스트 오늘안에 완료해야함
                    string str = LanguageMgr.GetText("QUEST_WARNING");
                    warningText.SetText(str);
                }
                else if (QuestMgr.MAIN_QUEST_1_LIMIT_DAY - gameMgr.playData.day == 1)
                {
                    //퀘스트 이틀안에 완료 해야함
                    string str = LanguageMgr.GetText("QUEST_WARNING_MSG");
                    warningText.SetWarningMsgText(str);
                }
            }
        }
    }

    private void TownNpcInit()
    {
        if (gameMgr?.playData == null)
            return;

        foreach (TownTalkObj obj in npcList)
            obj.gameObject.SetActive(false);

        //해달 아줌마
        if ((QuestState)gameMgr.playData.quest[(int)Quest.SeaOtterQuest] == QuestState.Complete)
        {
            //퀘스트 완료함
            npcList[0].gameObject.SetActive(false);
        }
        else if ((QuestState)gameMgr.playData.quest[(int)Quest.SeaOtterQuest] == QuestState.Run)
        {
            //퀘스트 진행중임
            npcList[0].gameObject.SetActive(true);
            npcList[0].Init();
        }
        else if (2 <= gameMgr.playData.day && gameMgr.playData.day <= 5)
        {
            //등장날짜 2~5일
            npcList[0].gameObject.SetActive(true);
            npcList[0].Init();
        }
    }

    public void ActTownMove(TownMap pPrevMap, TownMap pTownMap)
    {
        foreach (var pair in townMap)
            pair.Value.gameObject.SetActive(false);
        townMap[pTownMap].gameObject.SetActive(true);
        switch(pTownMap)
        {
            case TownMap.TulTulTown:
                {
                    TulTulTown tultulTown = townMap[pTownMap].GetComponent<TulTulTown>();
                    if(pPrevMap == TownMap.None)
                        tultulTown.SetInit(TulTulTown.Zone.ChickenHeaven);
                    else if (pPrevMap == TownMap.NekoJobBank)
                        tultulTown.SetInit(TulTulTown.Zone.JobBank);
                    diaryBtn.gameObject.SetActive(true);
                    soundMgr.PlayBGM(Sound.Town_BG);
                }
                break;
            case TownMap.NekoJobBank:
                {
                    NekoJobBank nekoJobBank = townMap[pTownMap].GetComponent<NekoJobBank>();
                    nekoJobBank.SetInit();
                    diaryBtn.gameObject.SetActive(false);
                    soundMgr.PlayBGM(Sound.Shop_BG);
                }
                break;
            case TownMap.ChefPauxsCookingUtensils:
                {
                    ChefPauxsCookingUtensils chefPauxsCookingUtensils = townMap[pTownMap].GetComponent<ChefPauxsCookingUtensils>();
                    chefPauxsCookingUtensils.SetInit();
                    diaryBtn.gameObject.SetActive(false);
                    soundMgr.PlayBGM(Sound.Shop_BG);
                }
                break;
            case TownMap.LongNoseCompany:
                {
                    LongNose longNose = townMap[pTownMap].GetComponent<LongNose>();
                    longNose.SetInit();
                    diaryBtn.gameObject.SetActive(false);
                    soundMgr.PlayBGM(Sound.Shop_BG);
                }
                break;
        }
    }

    public void TownMapLoad(TownMap pTownMap)
    {
        if (nowArea == pTownMap)
            return;
        if (isMove)
            return;
        isMove = true;
        nowSceneChange = true;


        IEnumerator Run()
        {
            //맵 이동 코루틴
            switch(nowArea)
            {
                case TownMap.TulTulTown:
                case TownMap.NekoJobBank:
                case TownMap.ChefPauxsCookingUtensils:
                case TownMap.LongNoseCompany:
                    moveAni.Play("FadeOn");
                    break;

            }

            yield return new WaitForSeconds(1f);

            switch (pTownMap)
            {
                case TownMap.TulTulTown:
                    moveAni.Play("FadeOff");
                    break;
                case TownMap.NekoJobBank:
                case TownMap.ChefPauxsCookingUtensils:
                case TownMap.LongNoseCompany:
                    moveAni.Play("CircleOff");
                    break;
            }

            ActTownMove(nowArea, pTownMap);

            nowSceneChange = true;
            nowArea = pTownMap;
            isMove = false;
            yield return new WaitForSeconds(1f);
            nowSceneChange = false;

            if (gameMgr.playData.tutoComplete4 == false && tutoMgr.nowTuto == Tutorial.Town_Tuto_3_5)
            {
                tutoDontTouch.gameObject.SetActive(true);
                yield return new WaitForSeconds(1f);
                TutoEvent();
            }
        }
        if(townMapLoadCor != null)
        {
            StopCoroutine(townMapLoadCor);
            townMapLoadCor = null;
        }
        townMapLoadCor = Run();
        StartCoroutine(Run());
    }

    public void TutoEvent()
    {
        if (gameMgr.playData.tutoComplete4)
            return;
        switch(tutoMgr.nowTuto)
        {
            case Tutorial.Town_Tuto_1:
                {
                    TulTulTown tultulTown = townMap[TownMap.TulTulTown].GetComponent<TulTulTown>();
                    tultulTown.MoveZone(TulTulTown.Zone.JobBank, () => tutoObj2.PlayTuto());
                }
                break;
            case Tutorial.Town_Tuto_2:
                {
                    tutoDontTouch.gameObject.SetActive(false);
                }
                break;
            case Tutorial.Town_Tuto_3_5:
                {
                    TulTulTown tultulTown = townMap[TownMap.TulTulTown].GetComponent<TulTulTown>();
                    tultulTown.MoveZone(TulTulTown.Zone.CookingUtensils, () => tutoObj4.PlayTuto());
                }
                break;
            case Tutorial.Town_Tuto_4:
                {
                    TulTulTown tultulTown = townMap[TownMap.TulTulTown].GetComponent<TulTulTown>();
                    tultulTown.MoveZone(TulTulTown.Zone.LongNose, () => tutoObj5.PlayTuto());
                }
                break;
            case Tutorial.Town_Tuto_5:
                {
                    TulTulTown tultulTown = townMap[TownMap.TulTulTown].GetComponent<TulTulTown>();
                    tultulTown.MoveZone(TulTulTown.Zone.ChickenHeaven, () => tutoObj6.PlayTuto());
                }
                break;
        }
    }
}
