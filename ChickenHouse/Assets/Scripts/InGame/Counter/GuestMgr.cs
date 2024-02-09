using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestMgr : Mgr
{
    public static GuestMgr Instance;

    [SerializeField] private Dictionary<Guest, GuestObj> guests;

    /** ���� �湮�� �մ� **/
    private HashSet<Guest>  visitedGuest = new HashSet<Guest>();
    /** ���� �ֹ����� ���� **/
    private bool            nowOrder     = false;

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

    private void Start()
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
            yield return new WaitForSeconds(5f);

            //�մ��� �������ش�.
            CreateGuest();

            //�ֹ� �޴� ������ ���
            yield return new WaitWhile(() => nowOrder);
        }
    }


    public void CreateGuest()
    {
        //�մ��� ����

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

        GuestObj findObj = null;
        if(guests.ContainsKey(pGuest))
        {
            findObj = guests[pGuest];
        }
        else
        {
            //�ش� �մ��� �������� �ʴ´�.
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
