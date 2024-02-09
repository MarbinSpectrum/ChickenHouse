using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestMgr : Mgr
{
    public static GuestMgr Instance;

    [SerializeField] private Dictionary<Guest, GuestObj> guests;

    /** 오늘 방문한 손님 **/
    private HashSet<Guest>  visitedGuest = new HashSet<Guest>();
    /** 현재 주문중인 여부 **/
    private bool            nowOrder     = false;

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

    private void Start()
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
            yield return new WaitForSeconds(5f);

            //손님을 생성해준다.
            CreateGuest();

            //주문 받는 동안은 대기
            yield return new WaitWhile(() => nowOrder);
        }
    }


    public void CreateGuest()
    {
        //손님을 만듬

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

        GuestObj findObj = null;
        if(guests.ContainsKey(pGuest))
        {
            findObj = guests[pGuest];
        }
        else
        {
            //해당 손님이 존재하지 않는다.
            return;
        }

        nowOrder = true;
        findObj.gameObject.SetActive(true);
        findObj.ShowGuest();
        findObj.CreateMenu();

        StartCoroutine(RunCor());
        IEnumerator RunCor()
        {
            yield return new WaitForSeconds(1f);

            findObj.OrderGuest();
        }
    }
}
