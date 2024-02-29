using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestMgr : Mgr
{
    public static GuestMgr Instance;

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
    }
    public UI ui;

    /** 오늘 방문한 손님 **/
    private HashSet<Guest>  visitedGuest = new HashSet<Guest>();
    /** 현재 주문중인 여부 **/
    private bool            nowOrder     = false;
    /** 현재 손님 **/
    private GuestObj        guestObj;


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
    }

    public IEnumerator RunGuestCycle()
    {
        //인게임 코루틴

        bool close = false;
        while(close == false)
        {
            //가게 평점이 높을수록 손님 딜레이가 적어지게할 예정
            yield return new WaitForSeconds(2f);

            //손님을 생성해준다.
            CreateGuest();

            //주문 받는 동안은 대기
            yield return new WaitWhile(() => nowOrder);
        }
    }


    public void CreateGuest()
    {
        //손님을 만듬
        vinylAni.gameObject.SetActive(false);

        //랜덤하게 손님을호출하되
        //오늘방문한 손님은 다시 오지않음
        List<Guest> guests = new List<Guest>();
        for (Guest guest = Guest.Fox; guest < Guest.MAX; guest++)
        {
            if(visitedGuest.Contains(guest))
            {
                //오늘 방문한 손님은 다시 오지 않음
                continue;
            }
            guests.Add(guest);
        }

        if(guests.Count == 0)
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

        ShowGuest(nowGuest);
    }

    public void ShowGuest(Guest pGuest)
    {
        //pGuest에 해당하는 손님을 호출한다.
        if(nowOrder)
        {
            //현재 다른 손님이 주문중이다.
            return;
        }

        if(guests.ContainsKey(pGuest))
        {
            guestObj = guests[pGuest];
        }
        else
        {
            //해당 손님이 존재하지 않는다.
            return;
        }

        nowOrder = true;
        guestObj.gameObject.SetActive(true);
        guestObj.ShowGuest();

        float orderTime = ui.timer.time;
        guestObj.CreateMenu(orderTime);

        StartCoroutine(RunCor());
        IEnumerator RunCor()
        {
            yield return new WaitForSeconds(1f);

            guestObj.OrderGuest();

            ui.goKitchen.OpenBtn();
        }
    }

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

            float defaultPoint = gameMgr.playData.GetDefaultPoint();
            float menuPoint = guestObj.ChickenPoint(chickenCnt, spicy0, spicy1, chickenState, hasDrink, hasPickle);
            if(menuPoint < defaultPoint)
            {
                guestObj.AngryGuest();
            }
            else
            {
                guestObj.ThankGuest();
            }

            gameMgr.playData.money += 100;
            ui.nowMoney.SetMoney(gameMgr.playData.money);

            yield return new WaitForSeconds(3f);

            CloseTalkBox();
            yield return new WaitForSeconds(0.5f);

            if(tutoMgr.tutoComplete == false)
            {
                //튜토리얼 완료
                tutoMgr.tutoComplete = true;
                PlayerPrefs.SetInt("TUTO", 1);
            }

            guestObj.LeaveGuest();
            vinylAni.gameObject.SetActive(false);

            nowOrder = false;
        }
    }
}
