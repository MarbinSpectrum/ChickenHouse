using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestMgr : AwakeSingleton<GuestMgr>
{
    private Dictionary<Guest, GuestData>   guestData   = new();
    private Dictionary<Guest, GuestObj>    guests      = new();

    /** 손님 객체 관리용 풀링 **/
    private Dictionary<Guest, Queue<GuestObj>> guestPool = new Dictionary<Guest, Queue<GuestObj>>();

    private List<GuestObj> guestList = new List<GuestObj>();

    private static bool init = false;

    protected override void Awake()
    {
        base.Awake();

        if (init)
            return;
        init = true;
        for (Guest guest = Guest.Fox; guest < Guest.MAX; guest++)
        {
            GuestData gData = Resources.Load<GuestData>($"GuestData/ScriptableObj/{guest.ToString()}");
            guestData.Add(guest, gData);

            GuestObj gObj = Resources.Load<GuestObj>($"GuestData/Prefab/{guest.ToString()}");
            guests.Add(guest, gObj);
        }
    }

    public GuestData GetGuestData(Guest pGuest)
    {
        //손님 정보 얻기
        if (guestData.ContainsKey(pGuest))
            return guestData[pGuest];
        return null;
    }

    public GuestObj GetGuest(Guest pGuest)
    {
        //손님 객체(원본) 얻기
        if (guests.ContainsKey(pGuest))
            return guests[pGuest];
        return null;
    }

    
    public GuestObj GetGuestObj(Guest pGuest)
    {
        //풀링에서 손님객체를 가져온다.

        Queue<GuestObj> queue = null;
        if (guestPool.ContainsKey(pGuest) == false)
        {
            //풀링이 없다. 생성
            guestPool[pGuest] = new Queue<GuestObj>();
        }
        queue = guestPool[pGuest];

        GuestObj guest = null;
        if (queue.Count > 0)
        {
            //풀링에 손님객체 존재
            guest = queue.Dequeue();
            guest.gameObject.SetActive(true);
            return guest;
        }

        //풀링에 객체가 없다 생성
        if (guests.ContainsKey(pGuest))
        {
            guest = Instantiate(guests[pGuest], transform);
            guestList.Add(guest);
        }
        return guest;
    }

    public void RemoveGuest(GuestObj obj)
    {
        if (guestPool.ContainsKey(obj.guest) == false)
        {
            guestPool[obj.guest] = new Queue<GuestObj>();
        }
        obj.transform.parent = transform;
        obj.gameObject.SetActive(false);
        guestPool[obj.guest].Enqueue(obj);
    }

    public void RemoveAllGuest()
    {
        foreach(GuestObj guest in guestList)
        {
            if (guestPool.ContainsKey(guest.guest) == false)
            {
                guestPool[guest.guest] = new Queue<GuestObj>();
            }
            if (guestPool[guest.guest].Contains(guest))
                continue;
            RemoveGuest(guest);
        }
    }
}
