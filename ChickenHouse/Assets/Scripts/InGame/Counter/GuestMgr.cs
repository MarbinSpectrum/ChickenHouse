using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestMgr : Mgr
{
    public static GuestMgr Instance;

    private const float START_GUEST_WAIT    = 3.5f;
    private const float GUEST_DELAY_TIME    = 20f;
    private const int   GUEST_MAX           = 6;

    [SerializeField] private SpriteRenderer[] guestPos = new SpriteRenderer[GUEST_MAX];
    [SerializeField] private Dictionary<Guest, GuestObj> guests;
    [SerializeField] private Animator vinylAni;

    [System.Serializable]
    public struct UI
    {
        //카운터 관련 UI

        /** 주방으로 이동하기 버튼 **/
        public GoKitchen_UI goKitchen;
        /** 현재 금화 **/
        public Money_UI     nowMoney;
        /** 시간 및 날짜 표시 **/
        public Timer_UI     timer;
        /** 날짜 종료 **/
        public DayEnd_UI    dayEnd;
    }
    public UI ui;

    /** 손님 객체 관리용 풀링 **/
    private Dictionary<Guest, Queue<GuestObj>> guestPool = new Dictionary<Guest, Queue<GuestObj>>();
    /** 오늘 방문한 손님 **/
    private HashSet<Guest>  visitedGuest = new HashSet<Guest>();
    /** 현재 주문중인 여부 **/
    private bool            nowOrder     = false;
    /** 현재 손님 **/
    private GuestObj        guestObj => waitGuest[0];
    /** 대기 손님 **/
    private GuestObj[]      waitGuest = new GuestObj[GUEST_MAX];
    private int guestcnt = 0;
    private bool moveGuest = false;

    private void Awake()
    {
        SetSingleton();
    }

    protected void SetSingleton()
    {
        //싱글톤 선언
        if (Instance == null)
        {
            Instance = gameObject.GetComponent<GuestMgr>();
        }
    }
    //-------------------------------------------------------------------------------------

    public void StartGuestCycle()
    {
        StartCoroutine(RunGuestCycle());
        StartCoroutine(EndCheck());
    }

    public IEnumerator RunGuestCycle()
    {
        yield return new WaitForSeconds(START_GUEST_WAIT);

        //인게임 코루틴
        while (true)
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
            float delayValue = GUEST_DELAY_TIME;
            if (gameMgr.playData.upgradeState[(int)Upgrade.Advertisement_5])
            {
                delayValue = GUEST_DELAY_TIME * 0.5f;
            }
            else if (gameMgr.playData.upgradeState[(int)Upgrade.Advertisement_4])
            {
                delayValue = GUEST_DELAY_TIME * 0.6f;
            }
            else if (gameMgr.playData.upgradeState[(int)Upgrade.Advertisement_3])
            {
                delayValue = GUEST_DELAY_TIME * 0.7f;
            }
            else if (gameMgr.playData.upgradeState[(int)Upgrade.Advertisement_2])
            {
                delayValue = GUEST_DELAY_TIME * 0.8f;
            }
            else if (gameMgr.playData.upgradeState[(int)Upgrade.Advertisement_1])
            {
                delayValue = GUEST_DELAY_TIME * 0.9f;
            }
            else
            {
                //기본 딜레이
                delayValue = GUEST_DELAY_TIME;
            }
            yield return new WaitForSeconds(delayValue);

            if (ui.timer.IsEndTime())
            {
                //종료시간이라면 탈출
                break;
            }
        }
    }


    public IEnumerator EndCheck()
    {
        //영업종료시간까지 대기
        yield return new WaitUntil(() => ui.timer.IsEndTime());

        //손님이 0명일때까지 대기
        yield return new WaitUntil(() => (guestcnt == 0));

        //종료
        ui.dayEnd.ShowResult();
    }


    private void CreateGuest()
    {
        for (int i = 0; i < waitGuest.Length; i++)
        {
            if (waitGuest[i] == null)
            {
                //손님을 만듬     
                guestcnt++;

                //랜덤하게 손님을호출하되
                //오늘방문한 손님은 다시 오지않음
                List<Guest> guests = new List<Guest>();
                for (Guest guest = Guest.Fox; guest < Guest.MAX; guest++)
                {
                    if (visitedGuest.Contains(guest))
                    {
                        //오늘 방문한 손님은 다시 오지 않음
                        continue;
                    }
                    guests.Add(guest);
                }

                if (guests.Count == 0)
                {
                    //왠만하면 여기로 오지 않도록 손님풀을 늘리는 방향으로 가야될듯
                    //방문손님을 초기화
                    //나올수있는 손님 리스트를 갱신
                    visitedGuest.Clear();
                    for (Guest guest = Guest.Fox; guest < Guest.MAX; guest++)
                    {
                        guests.Add(guest);
                    }
                }

                //손님을 호출
                int guestRandom = Random.Range(0, guests.Count);
                Guest nowGuest = guests[guestRandom];
                visitedGuest.Add(nowGuest);


                //손님생성
                GuestObj newGuest = GetGuestObj(nowGuest);

                nowOrder = true;
                newGuest.gameObject.SetActive(true);
                newGuest.ShowGuest();

                newGuest.transform.position = guestPos[i].transform.position;
                newGuest.transform.localScale = guestPos[i].transform.localScale;
                newGuest.SetOrderSprite(guestPos[i].sortingOrder);
                newGuest.SetColor(guestPos[i].color);

                waitGuest[i] = newGuest;

                if(guestcnt == 1)
                {
                    //첫손님 주문
                    StartCoroutine(GuestOrder());
                }

                break;
            }
        }
    }

    private GuestObj GetGuestObj(Guest pGuest)
    {
        //풀링에서 손님객체를 가져온다.

        Queue<GuestObj> queue = null;
        if(guestPool.ContainsKey(pGuest) == false)
        {
            //풀링이 없다. 생성
            guestPool[pGuest] = new Queue<GuestObj>();
        }
        queue = guestPool[pGuest];

        GuestObj guest = null;
        if(queue.Count > 0)
        {
            //풀링에 손님객체 존재
            guest = queue.Dequeue();
            guest.gameObject.SetActive(true);
            return guest;
        }

        //풀링에 객체가 없다 생성
        if (guests.ContainsKey(pGuest))
            guest = Instantiate(guests[pGuest],transform);
        return guest;
    }

    public string GetTalkBoxStr() => guestObj.GetTalkText();

    public void CloseTalkBox()
    {
        if (guestObj == null)
            return;

        guestObj.CloseTalkBox();
    }

    public void GiveChicken(int chickenCnt, ChickenSpicy spicy0, ChickenSpicy spicy1, ChickenState chickenState,
                            bool hasDrink, bool hasPickle)
    {
        vinylAni.gameObject.SetActive(true);

        StartCoroutine(RunCor());
        IEnumerator RunCor()
        {
            yield return new WaitForSeconds(1.5f);

            //손님의 평가 진행
            float defaultPoint = gameMgr.playData.GetDefaultPoint();
            float menuPoint = guestObj.ChickenPoint(chickenCnt, spicy0, spicy1, chickenState, hasDrink, hasPickle);
            if(menuPoint < defaultPoint)
            {
                guestObj.AngryGuest();
            }
            else
            {
                //돈지불
                gameMgr.dayMoney += gameMgr.playData.GetMenuValue();
                ui.nowMoney.SetMoney(gameMgr.dayMoney);

                guestObj.ThankGuest();
            }

            gameMgr.sellChickenCnt += 1;


            yield return new WaitForSeconds(3f);

            //대화창 닫기
            CloseTalkBox();

            yield return new WaitForSeconds(0.5f);

            if(tutoMgr.tutoComplete == false)
            {
                //튜토리얼 완료
                tutoMgr.tutoComplete = true;
                PlayerPrefs.SetInt("TUTO", 1);
            }

            //손님떠남처리
            guestObj.LeaveGuest();
            guestcnt--;

            yield return new WaitForSeconds(1f);

            if (guestPool.ContainsKey(guestObj.guest) == false)
            {
                guestPool[guestObj.guest] = new Queue<GuestObj>();
            }
            guestObj.gameObject.SetActive(false);
            guestPool[guestObj.guest].Enqueue(guestObj);

            vinylAni.gameObject.SetActive(false);
            nowOrder = false;

            waitGuest[0] = null;
            for(int i = 0; i < waitGuest.Length-1; i++)
            {
                waitGuest[i] = waitGuest[i + 1];
                waitGuest[i + 1] = null;
                if (waitGuest[i] == null)
                    continue;
                waitGuest[i].SetOrderSprite(guestPos[i].sortingOrder);
            }

            //대기 손님 이동 애니메이션
            if (guestcnt > 0)
            {
                moveGuest = true;
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

                    lerpTime += Time.deltaTime;
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

                //이동후 다음 손님 주문
                StartCoroutine(GuestOrder());
            }
        }
    }

    private IEnumerator GuestOrder()
    {
        yield return new WaitForSeconds(1f);

        if (guestObj == null)
            yield break;
      
        float orderTime = ui.timer.time;
        guestObj.CreateMenu(orderTime);
        guestObj.OrderGuest();

        ui.goKitchen.OpenBtn();
    }
}
