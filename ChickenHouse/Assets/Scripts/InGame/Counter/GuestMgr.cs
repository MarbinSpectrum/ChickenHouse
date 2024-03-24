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
        //ī���� ���� UI

        /** �ֹ����� �̵��ϱ� ��ư **/
        public GoKitchen_UI goKitchen;
        /** ���� ��ȭ **/
        public Money_UI     nowMoney;
        /** �ð� �� ��¥ ǥ�� **/
        public Timer_UI     timer;
        /** ��¥ ���� **/
        public DayEnd_UI    dayEnd;
    }
    public UI ui;

    /** �մ� ��ü ������ Ǯ�� **/
    private Dictionary<Guest, Queue<GuestObj>> guestPool = new Dictionary<Guest, Queue<GuestObj>>();
    /** ���� �湮�� �մ� **/
    private HashSet<Guest>  visitedGuest = new HashSet<Guest>();
    /** ���� �ֹ����� ���� **/
    private bool            nowOrder     = false;
    /** ���� �մ� **/
    private GuestObj        guestObj => waitGuest[0];
    /** ��� �մ� **/
    private GuestObj[]      waitGuest = new GuestObj[GUEST_MAX];
    private int guestcnt = 0;
    private bool moveGuest = false;

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
        StartCoroutine(EndCheck());
    }

    public IEnumerator RunGuestCycle()
    {
        yield return new WaitForSeconds(START_GUEST_WAIT);

        //�ΰ��� �ڷ�ƾ
        while (true)
        {
            //�մ��� �̵����ϴ�� ���
            yield return new WaitWhile(() => moveGuest);

            if (ui.timer.IsEndTime() == false && guestcnt < GUEST_MAX)
            {
                //���� ���� �ð��� �ƴ� and �մ��ִ���� �ɸ��� ����
                //�մ��� �������ش�.
                CreateGuest();
            }

            //�մ� ������
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
                //�⺻ ������
                delayValue = GUEST_DELAY_TIME;
            }
            yield return new WaitForSeconds(delayValue);

            if (ui.timer.IsEndTime())
            {
                //����ð��̶�� Ż��
                break;
            }
        }
    }


    public IEnumerator EndCheck()
    {
        //��������ð����� ���
        yield return new WaitUntil(() => ui.timer.IsEndTime());

        //�մ��� 0���϶����� ���
        yield return new WaitUntil(() => (guestcnt == 0));

        //����
        ui.dayEnd.ShowResult();
    }


    private void CreateGuest()
    {
        for (int i = 0; i < waitGuest.Length; i++)
        {
            if (waitGuest[i] == null)
            {
                //�մ��� ����     
                guestcnt++;

                //�����ϰ� �մ���ȣ���ϵ�
                //���ù湮�� �մ��� �ٽ� ��������
                List<Guest> guests = new List<Guest>();
                for (Guest guest = Guest.Fox; guest < Guest.MAX; guest++)
                {
                    if (visitedGuest.Contains(guest))
                    {
                        //���� �湮�� �մ��� �ٽ� ���� ����
                        continue;
                    }
                    guests.Add(guest);
                }

                if (guests.Count == 0)
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


                //�մԻ���
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
                    //ù�մ� �ֹ�
                    StartCoroutine(GuestOrder());
                }

                break;
            }
        }
    }

    private GuestObj GetGuestObj(Guest pGuest)
    {
        //Ǯ������ �մ԰�ü�� �����´�.

        Queue<GuestObj> queue = null;
        if(guestPool.ContainsKey(pGuest) == false)
        {
            //Ǯ���� ����. ����
            guestPool[pGuest] = new Queue<GuestObj>();
        }
        queue = guestPool[pGuest];

        GuestObj guest = null;
        if(queue.Count > 0)
        {
            //Ǯ���� �մ԰�ü ����
            guest = queue.Dequeue();
            guest.gameObject.SetActive(true);
            return guest;
        }

        //Ǯ���� ��ü�� ���� ����
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

            //�մ��� �� ����
            float defaultPoint = gameMgr.playData.GetDefaultPoint();
            float menuPoint = guestObj.ChickenPoint(chickenCnt, spicy0, spicy1, chickenState, hasDrink, hasPickle);
            if(menuPoint < defaultPoint)
            {
                guestObj.AngryGuest();
            }
            else
            {
                //������
                gameMgr.dayMoney += gameMgr.playData.GetMenuValue();
                ui.nowMoney.SetMoney(gameMgr.dayMoney);

                guestObj.ThankGuest();
            }

            gameMgr.sellChickenCnt += 1;


            yield return new WaitForSeconds(3f);

            //��ȭâ �ݱ�
            CloseTalkBox();

            yield return new WaitForSeconds(0.5f);

            if(tutoMgr.tutoComplete == false)
            {
                //Ʃ�丮�� �Ϸ�
                tutoMgr.tutoComplete = true;
                PlayerPrefs.SetInt("TUTO", 1);
            }

            //�մԶ���ó��
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

            //��� �մ� �̵� �ִϸ��̼�
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

                //�̵��� ���� �մ� �ֹ�
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
