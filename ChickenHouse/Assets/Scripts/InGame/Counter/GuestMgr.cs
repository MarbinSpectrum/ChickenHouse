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
        //ī���� ���� UI

        /** �ֹ����� �̵��ϱ� ��ư **/
        public GoKitchen_UI goKitchen;
        /** ���� ��ȭ **/
        public Money_UI     nowMoney;
        /** �ð� �� ��¥ ǥ�� **/
        public Timer_UI     timer;
    }
    public UI ui;

    /** ���� �湮�� �մ� **/
    private HashSet<Guest>  visitedGuest = new HashSet<Guest>();
    /** ���� �ֹ����� ���� **/
    private bool            nowOrder     = false;
    /** ���� �մ� **/
    private GuestObj        guestObj;


    private void Awake()
    {
        SetSingleton();
    }

    protected void SetSingleton()
    {
        //�̱��� ����
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
        //�ΰ��� �ڷ�ƾ

        bool close = false;
        while(close == false)
        {
            //���� ������ �������� �մ� �����̰� ���������� ����
            yield return new WaitForSeconds(2f);

            //�մ��� �������ش�.
            CreateGuest();

            //�ֹ� �޴� ������ ���
            yield return new WaitWhile(() => nowOrder);
        }
    }


    public void CreateGuest()
    {
        //�մ��� ����
        vinylAni.gameObject.SetActive(false);

        //�����ϰ� �մ���ȣ���ϵ�
        //���ù湮�� �մ��� �ٽ� ��������
        List<Guest> guests = new List<Guest>();
        for (Guest guest = Guest.Fox; guest < Guest.MAX; guest++)
        {
            if(visitedGuest.Contains(guest))
            {
                //���� �湮�� �մ��� �ٽ� ���� ����
                continue;
            }
            guests.Add(guest);
        }

        if(guests.Count == 0)
        {
            //�ظ��ϸ� ����� ���� �ʵ��� �մ�Ǯ�� �ø��� �������� ���ߵɵ�
            //�湮�մ��� �ʱ�ȭ
            //���ü��ִ� �մ� ����Ʈ�� ����
            visitedGuest.Clear();
            for (Guest guest = Guest.Fox; guest < Guest.MAX; guest++)
            {
                guests.Add(guest);
            }
        }

        //�մ��� ȣ��
        int guestRandom = Random.Range(0, guests.Count);
        Guest nowGuest = guests[guestRandom];
        visitedGuest.Add(nowGuest);

        ShowGuest(nowGuest);
    }

    public void ShowGuest(Guest pGuest)
    {
        //pGuest�� �ش��ϴ� �մ��� ȣ���Ѵ�.
        if(nowOrder)
        {
            //���� �ٸ� �մ��� �ֹ����̴�.
            return;
        }

        if(guests.ContainsKey(pGuest))
        {
            guestObj = guests[pGuest];
        }
        else
        {
            //�ش� �մ��� �������� �ʴ´�.
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
                //Ʃ�丮�� �Ϸ�
                tutoMgr.tutoComplete = true;
                PlayerPrefs.SetInt("TUTO", 1);
            }

            guestObj.LeaveGuest();
            vinylAni.gameObject.SetActive(false);

            nowOrder = false;
        }
    }
}
