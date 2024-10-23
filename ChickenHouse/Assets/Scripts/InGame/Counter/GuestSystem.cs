using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GuestSystem : Mgr
{
    public static GuestSystem Instance;

    private const float START_GUEST_WAIT    = 3f;
    public const float  GUEST_DELAY_TIME    = 30f;
    private const int   GUEST_MAX           = 6;

    [SerializeField] private SpriteRenderer[] guestPos = new SpriteRenderer[GUEST_MAX];
    [SerializeField] private Animator vinylAni;
    [SerializeField] private Button skipTalkBtn;
    [SerializeField] private Button gotoKitchenBtn;

    [System.Serializable]
    public struct UI
    {
        //카운터 관련 UI

        /** 주방으로 이동하기 버튼 **/
        public GoKitchen_UI goKitchen;
        /** 현재 금화 **/
        public Money_UI     nowMoney;
        /** 돈 획득 **/
        public GetMoney_UI  getMoney;
        /** 시간 및 날짜 표시 **/
        public Timer_UI     timer;
        /** 날짜 종료 **/
        public DayEnd_UI    dayEnd;
        /** 이벤트0 UI **/
        public Event0_UI    event0_UI;
    }
    public UI ui;

    /** 이미 방문해있는 손님 **/
    private HashSet<Guest>  visitedGuest = new HashSet<Guest>();


    private Dictionary<Guest, int> guestWeight = new Dictionary<Guest, int>();
    /** 현재 주문중인 여부 **/
    private bool            nowOrder     = false;
    /** 현재 손님 **/
    private GuestObj        guestObj => waitGuest[0];
    /** 대기 손님 **/
    private GuestObj[]      waitGuest = new GuestObj[GUEST_MAX];
    public int guestcnt { private set; get; }
    private bool moveGuest = false;

    private WorkerData counterWorker
    {
        get
        {
            EWorker eWorker = (EWorker)gameMgr.playData.workerPos[(int)KitchenSet_UI.KitchenSetWorkerPos.CounterWorker];
            return workerMgr.GetWorkerData(eWorker);
        }
    }


    private void Awake()
    {
        SetSingleton();
    }

    protected void SetSingleton()
    {
        //싱글톤 선언
        if (Instance == null)
        {
            Instance = gameObject.GetComponent<GuestSystem>();
        }
    }
    //-------------------------------------------------------------------------------------

    public void Init()
    {
        skipTalkBtn.onClick.RemoveAllListeners();
        skipTalkBtn.onClick.AddListener(() =>
        {
            if (guestObj == null)
                return;
            SkipTalk();
        });

        gotoKitchenBtn.onClick.RemoveAllListeners();
        gotoKitchenBtn.onClick.AddListener(() =>
        {
            ui.goKitchen.GoKitchen();
        });
        gotoKitchenBtn.gameObject.SetActive(false);

        ui.nowMoney.SetMoney(gameMgr.playData.money);

        guestMgr.RemoveAllGuest();

        StartCoroutine(RunGuestCycle());
        StartCoroutine(EndCheck());

        //ui.goKitchen.OpenBtn();
    }

    public IEnumerator RunGuestCycle()
    {
        for (Guest guest = Guest.Fox; guest < Guest.MAX; guest++)
            guestWeight[guest] = 0;

        yield return new WaitForSeconds(START_GUEST_WAIT);

        //인게임 코루틴
        while (true)
        {
            if (gameMgr.playData.tutoComplete1 == false)
            {
                //손님이 이동중일대는 대기
                yield return new WaitWhile(() => moveGuest);

                //튜토리얼용 손님 생성
                CreateGuest();

                //튜토리얼동안 대기
                yield return new WaitUntil(() => gameMgr.playData.tutoComplete1);

                //잠깐 대기
                yield return new WaitForSeconds(2f);
            }
            else
            {
                //손님이 이동중일대는 대기
                yield return new WaitWhile(() => moveGuest);

                if (ui.timer.IsEndTime() == false && guestcnt < GUEST_MAX)
                {
                    //영업 종료 시간이 아님 and 손님최대수에 걸리지 않음
                    //손님을 생성해준다.
                    CreateGuest();
                }

                //손님 딜레이
                float delayValue = GUEST_DELAY_TIME / (gameMgr.playData.GuestTotalDelayRate()/100f);
                if (counterWorker != null && counterWorker.skill.Contains(WorkerSkill.WorkerSkill_5))
                {
                    //카운터 업무 경력자(카운터에 배치시 손님이 방문률 +50%)
                    delayValue *= 0.5f;
                }

                while (delayValue > 0)
                {
                    delayValue -= Time.deltaTime;
                    if (guestcnt == 0)
                    {
                        //1초정도만 대기하고 손님을 바로 넣어준다.
                        delayValue = Mathf.Min(delayValue, 1f);
                        yield return new WaitForSeconds(delayValue);
                        break;
                    }

                    yield return null;
                }


            }

            if ((QuestState)gameMgr.playData.quest[(int)Quest.Event_0_Quest] == QuestState.Run 
                && ui.event0_UI.battleResult != Event_0_Battle_Result.None)
            {
                //배틀종료
                break;
            }
            else if (ui.timer.IsEndTime())
            {
                //종료시간이라면 탈출
                break;
            }
        }
    }


    public IEnumerator EndCheck()
    {
        if ((QuestState)gameMgr.playData.quest[(int)Quest.Event_0_Quest] == QuestState.Run)
        {
            yield return new WaitUntil(() => ui.event0_UI.battleResult != Event_0_Battle_Result.None);

            //종료
            ui.dayEnd.ShowResult();
        }
        else
        {
            //영업종료시간까지 대기
            yield return new WaitUntil(() => ui.timer.IsEndTime());

            //손님이 0명일때까지 대기
            yield return new WaitUntil(() => (guestcnt == 0));

            //종료
            ui.dayEnd.ShowResult();
        }
    }


    private void CreateGuest()
    {
        for (int i = 0; i < waitGuest.Length; i++)
        {
            if (waitGuest[i] == null)
            {
                //랜덤하게 손님을호출하되
                //오늘방문한 손님은 다시 오지않음
                List<Guest> guestList = new List<Guest>();
                for (Guest guest = Guest.Fox; guest < Guest.MAX; guest++)
                {
                    GuestObj guestObj = guestMgr.GetGuest(guest);
                    if (guestObj.GetShowDay() > gameMgr.playData.day)
                        continue;
                    if (guestWeight[guest] > 0)
                        continue;
                    //생성할 손님이 존재하긴한다.
                    if (visitedGuest.Contains(guest))
                    {
                        //이미 있는 손님은 다시 오지 않음
                        continue;
                    }
                    guestList.Add(guest);
                }

                if (guestList.Count == 0)
                {
                    //왠만하면 여기로 오지 않도록 손님풀을 늘리는 방향으로 가야될듯
                    //방문손님을 초기화
                    //나올수있는 손님 리스트를 갱신'

                    for (Guest guest = Guest.Fox; guest < Guest.MAX; guest++)
                        guestWeight[guest] = 0;

                    for (Guest guest = Guest.Fox; guest < Guest.MAX; guest++)
                    {
                        GuestObj guestObj = guestMgr.GetGuest(guest);
                        if (guestObj.GetShowDay() > gameMgr.playData.day)
                            continue;
                        if (guestWeight[guest] > 0)
                            continue;
                        //생성할 손님이 존재하긴한다.
                        if (visitedGuest.Contains(guest))
                        {
                            //이미 있는 손님은 다시 오지 않음
                            continue;
                        }
                        guestList.Add(guest);
                    }
                }

                if (guestList.Count == 0)
                    continue;

                //손님을 호출
                int guestRandom = Random.Range(0, guestList.Count);
                Guest nowGuest = guestList[guestRandom];
                if(gameMgr.playData.tutoComplete1 == false)
                {
                    //튜토리얼에서는 고정으로 여우가 나옴
                    nowGuest = Guest.Fox;
                }

                visitedGuest.Add(nowGuest);

                for (Guest guest = Guest.Fox; guest < Guest.MAX; guest++)
                    guestWeight[guest]--;
                guestWeight[nowGuest] = 3;

                //손님생성
                GuestObj newGuest = guestMgr.GetGuestObj(nowGuest);
                float orderTime = ui.timer.time;
                if (newGuest.CreateMenu(orderTime) == false)
                {
                    //레시피 못만듬 다른 손님 생성
                    guestMgr.RemoveGuest(newGuest);
                    i--;
                    continue;
                }

                //손님을 만듬     
                guestcnt++;

                //주방에있을때 미리 주문 내역을 적어줌

                newGuest.OrderGuest();
                KitchenMgr kitchenMgr = KitchenMgr.Instance;
                if(kitchenMgr.cameraObj.lookArea == LookArea.Kitchen)
                    soundMgr.PlaySE(Sound.NewOrder_SE);
                string talkStr = newGuest.GetTalkText();
                Sprite guestFace = newGuest.GetGuestFace();
                kitchenMgr.ui.memo.AddMemo(talkStr, guestFace, newGuest);

                newGuest.gameObject.SetActive(true);
                newGuest.CloseTalkBox();
                newGuest.ShowGuest();

                newGuest.transform.position = guestPos[i].transform.position;
                newGuest.transform.localScale = guestPos[i].transform.localScale;
                newGuest.SetOrderSprite(guestPos[i].sortingOrder);
                newGuest.SetColor(guestPos[i].color);

                waitGuest[i] = newGuest;

                if (guestcnt == 1)
                {
                    //첫손님 주문

                    if (kitchenMgr.cameraObj.lookArea == LookArea.Counter)
                    {
                        ui.goKitchen.OpenBtn();
                    }

                    StartCoroutine(GuestOrder());
                }

                break;
            }
        }
    }

    public void LeaveGuest(int idx)
    {
        //손님떠남처리
        if (waitGuest.Length <= idx)
            return;
        if (waitGuest[idx] == null)
            return;

        waitGuest[idx].LeaveGuest();
        guestcnt--;
        visitedGuest.Remove(waitGuest[idx].guest);
        guestMgr.RemoveGuest(waitGuest[idx]);

        waitGuest[idx] = null;
        for (int i = idx; i < waitGuest.Length - 1; i++)
        {
            waitGuest[i] = waitGuest[i + 1];
            waitGuest[i + 1] = null;
            if (waitGuest[i] == null)
                continue;
            waitGuest[i].SetOrderSprite(guestPos[i].sortingOrder);
        }

        StartCoroutine(MoveGuest());
    }

    public string GetTalkBoxStr() => guestObj.GetTalkText();

    public Sprite GetGuestFace() => guestObj.GetGuestFace();

    public void CloseTalkBox()
    {
        if (guestObj == null)
            return;

        guestObj.CloseResult();
        guestObj.CloseTalkBox();
    }

    public void SkipTalk()
    {
        if (guestObj == null)
            return;
        guestObj.SkipTalk();
    }

    public void SetSkipTalkBtnState(bool state)
    {
        skipTalkBtn.gameObject.SetActive(state);
    }

    public void SetGotoKitchenBtnState(bool state)
    {
        gotoKitchenBtn.gameObject.SetActive(state);
    }

    public void GiveChicken(ChickenSpicy spicy0, ChickenSpicy spicy1, ChickenState chickenState,
                            Drink pDrink, SideMenu pSideMenu,bool useWorker)
    {
        //useWorker : 직원 사용여부

        vinylAni.gameObject.SetActive(true);
        KitchenMgr kitchenMgr = KitchenMgr.Instance;

        StartCoroutine(RunCor());
        IEnumerator RunCor()
        {
            yield return new WaitForSeconds(0.5f);

            //손님의 평가 진행

            if (useWorker == false)
                skipTalkBtn.gameObject.SetActive(true);

            GuestReviews result = guestObj.ChickenPoint(spicy0, spicy1, chickenState, pDrink, pSideMenu);
            bool chickenStateResult = guestObj.CheckChickenState(chickenState);
            bool spicyResult = guestObj.CheckSpicy(spicy0, spicy1);
            bool drinkResult = guestObj.CheckDrink(pDrink);
            bool sideResult = guestObj.CheckSide(pSideMenu);

            //도감에 해동 손님 등록
            BookMgr.ActGuestData(guestObj.guest);

            switch (result)
            {
                case GuestReviews.Bad:
                    {
                        if (useWorker)
                            kitchenMgr.RunWorkerTalkBox(WorkerCounterTalkBox.Bad);
                        else
                            guestObj.AngryGuest(() => NextOrder());
                    }
                    break;
                case GuestReviews.Normal:
                    {
                        //돈지불
                        int getValue = gameMgr.playData.GetMenuValue(result, spicy0, spicy1, chickenState, pDrink, pSideMenu);
                        ui.getMoney.RunAni(getValue, 0);
                        gameMgr.dayMoney += getValue;
                        ui.nowMoney.SetMoney(gameMgr.playData.money + gameMgr.dayMoney);

                        if (useWorker)
                            kitchenMgr.RunWorkerTalkBox(WorkerCounterTalkBox.Normal);
                        else
                            guestObj.ThankGuest(() => NextOrder());
                    }
                    break;
                case GuestReviews.Happy:
                    {
                        //돈지불
                        int getValue = gameMgr.playData.GetMenuValue(result, spicy0, spicy1, chickenState, pDrink, pSideMenu);
                        float tipRate = gameMgr.playData.TipRate()/100f;
                        if(counterWorker != null && counterWorker.skill.Contains(WorkerSkill.WorkerSkill_4))
                        {
                            //잘생긴외모(팁 증가 +100%)
                            tipRate = tipRate * (100f + 100f) / 100f;
                        }

                        int tipValue = (int)(getValue * tipRate);
                        ui.getMoney.RunAni(getValue, tipValue);
                        gameMgr.dayMoney += (getValue + tipValue);
                        ui.nowMoney.SetMoney(gameMgr.playData.money + gameMgr.dayMoney);

                        if (useWorker)
                            kitchenMgr.RunWorkerTalkBox(WorkerCounterTalkBox.Good);
                        else
                            guestObj.HappyGuest(() => NextOrder());

                        if ((QuestState)gameMgr.playData.quest[(int)Quest.Event_0_Quest] == QuestState.Run)
                            ui.event0_UI.AddPlayerCnt();
                    }
                    break;
            }

            guestObj.ShowResult(spicyResult, chickenStateResult, drinkResult, sideResult);

            GuestMenu sellMenu = new GuestMenu();
            sellMenu.spicy0 = spicy0;
            sellMenu.spicy1 = spicy1;
            sellMenu.drink = pDrink;
            sellMenu.sideMenu = pSideMenu;
            gameMgr.sellMenu.Add(sellMenu);
            if (useWorker)
                NextOrder();
        }
    }

    private void NextOrder()
    {
        skipTalkBtn.gameObject.SetActive(false);

        StartCoroutine(RunCor());
        IEnumerator RunCor()
        {
            KitchenMgr kitchenMgr = KitchenMgr.Instance;
            GuestObj leaveGuest = guestObj;

            waitGuest[0] = null;
            for (int i = 0; i < waitGuest.Length - 1; i++)
            {
                waitGuest[i] = waitGuest[i + 1];
                waitGuest[i + 1] = null;
                if (waitGuest[i] == null)
                    continue;
            }

            if (kitchenMgr.cameraObj.lookArea == LookArea.Counter)
            {
                ui.goKitchen.OpenBtn();
            }

            yield return new WaitForSeconds(1f);

            for (int i = 0; i < waitGuest.Length; i++)
            {
                if (waitGuest[i] == null)
                    continue;
                waitGuest[i].SetOrderSprite(guestPos[i].sortingOrder);
            }

            //대화창 닫기
            leaveGuest.CloseTalkBox();

            if (gameMgr.playData.tutoComplete1 == false)
            {
                //튜토리얼 완료
                gameMgr.playData.tutoComplete1 = true;
            }

            //손님떠남처리
            StartCoroutine(LeaveGueset(leaveGuest));

            vinylAni.gameObject.SetActive(false);
            nowOrder = false;

            //대기 손님 이동 애니메이션
            if (guestcnt > 0)
            {
                StartCoroutine(MoveGuest());
                StartCoroutine(GuestOrder());
            }
        }
    }

    private IEnumerator LeaveGueset(GuestObj pGuestObj)
    {
        pGuestObj.LeaveGuest();
        guestcnt--;

        yield return new WaitForSeconds(2f);

        visitedGuest.Remove(pGuestObj.guest);
        guestMgr.RemoveGuest(pGuestObj);
    }

    private IEnumerator MoveGuest()
    {
        moveGuest = true;

        float guestSpeed = 2f;
        float lerpTime = 0;
        while (lerpTime < 1)
        {
            for (int i = 0; i < waitGuest.Length - 1; i++)
            {
                if (waitGuest[i] == null)
                    continue;

                waitGuest[i].SetColor(Color.Lerp(guestPos[i + 1].color, guestPos[i].color, lerpTime));
                waitGuest[i].transform.position = Vector3.Lerp(guestPos[i + 1].transform.position, guestPos[i].transform.position, lerpTime);
                waitGuest[i].transform.localScale = Vector3.Lerp(guestPos[i + 1].transform.localScale, guestPos[i].transform.localScale, lerpTime);
            }

            lerpTime += Time.deltaTime * guestSpeed;
            yield return null;
        }

        for (int i = 0; i < waitGuest.Length - 1; i++)
        {
            if (waitGuest[i] == null)
                continue;

            waitGuest[i].SetColor(guestPos[i].color);
            waitGuest[i].transform.position = guestPos[i].transform.position;
            waitGuest[i].transform.localScale = guestPos[i].transform.localScale;
        }

        moveGuest = false;
    }

    private IEnumerator GuestOrder()
    {
        KitchenMgr kitchenMgr = KitchenMgr.Instance;

        if (nowOrder)
            yield break;
        if (guestObj == null)
            yield break;

        guestObj.CloseResult();

        if (kitchenMgr.cameraObj.lookArea != LookArea.Counter)
            yield break;

        yield return new WaitForSeconds(1f);

        if (kitchenMgr.cameraObj.lookArea != LookArea.Counter)
            yield break;

        if (guestObj == null)
            yield break;

        nowOrder = true;
        TalkOrder();
    }

    public void TalkOrder()
    {
        if (guestObj == null)
            return;

        KitchenMgr kitchenMgr = KitchenMgr.Instance;

        gotoKitchenBtn.gameObject.SetActive(false);
        skipTalkBtn.gameObject.SetActive(false);
        if (kitchenMgr.cameraObj.lookArea == LookArea.Counter)
        {
            skipTalkBtn.gameObject.SetActive(true);
            guestObj.TalkOrder(() =>
            {
                if (kitchenMgr.cameraObj.lookArea == LookArea.Kitchen)
                {
                    gotoKitchenBtn.gameObject.SetActive(false);
                    skipTalkBtn.gameObject.SetActive(false);
                }
                else
                {
                    //gotoKitchenBtn.gameObject.SetActive(true);
                    skipTalkBtn.gameObject.SetActive(false);
                }
            });

        }
    }
}
