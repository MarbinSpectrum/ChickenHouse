using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GuestMgr : Mgr
{
    public static GuestMgr Instance;

    private const float START_GUEST_WAIT    = 3f;
    private const float GUEST_DELAY_TIME    = 30f;
    private const int   GUEST_MAX           = 6;

    [SerializeField] private SpriteRenderer[] guestPos = new SpriteRenderer[GUEST_MAX];
    [SerializeField] private Dictionary<Guest, GuestObj> guests;
    [SerializeField] private Animator vinylAni;
    [SerializeField] private Button skipTalkBtn;
    [SerializeField] private Button gotoKitchenBtn;
    [System.Serializable]
    public struct UI
    {
        //ī���� ���� UI

        /** �ֹ����� �̵��ϱ� ��ư **/
        public GoKitchen_UI goKitchen;
        /** ���� ��ȭ **/
        public Money_UI     nowMoney;
        /** �� ȹ�� **/
        public GetMoney_UI  getMoney;
        /** �ð� �� ��¥ ǥ�� **/
        public Timer_UI     timer;
        /** ��¥ ���� **/
        public DayEnd_UI    dayEnd;
    }
    public UI ui;

    /** �մ� ��ü ������ Ǯ�� **/
    private Dictionary<Guest, Queue<GuestObj>> guestPool = new Dictionary<Guest, Queue<GuestObj>>();
    /** �̹� �湮���ִ� �մ� **/
    private HashSet<Guest>  visitedGuest = new HashSet<Guest>();


    private Dictionary<Guest, int> guestWeight = new Dictionary<Guest, int>();
    /** ���� �ֹ����� ���� **/
    private bool            nowOrder     = false;
    /** ���� �մ� **/
    private GuestObj        guestObj => waitGuest[0];
    /** ��� �մ� **/
    private GuestObj[]      waitGuest = new GuestObj[GUEST_MAX];
    public int guestcnt { private set; get; }
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

    public void Init()
    {
        skipTalkBtn.onClick.RemoveAllListeners();
        skipTalkBtn.onClick.AddListener(() =>
        {
            if (guestObj == null || nowOrder == false)
                return;
            guestObj.SkipTalk();
        });

        gotoKitchenBtn.onClick.RemoveAllListeners();
        gotoKitchenBtn.onClick.AddListener(() =>
        {
            ui.goKitchen.GoKitchen();
        });
        gotoKitchenBtn.gameObject.SetActive(false);

        ui.nowMoney.SetMoney(gameMgr.playData.money);

        StartCoroutine(RunGuestCycle());
        StartCoroutine(EndCheck());

        ui.timer.RunTimer();
        //ui.goKitchen.OpenBtn();
    }

    public IEnumerator RunGuestCycle()
    {
        for (Guest guest = Guest.Fox; guest < Guest.MAX; guest++)
            guestWeight[guest] = 0;

        yield return new WaitForSeconds(START_GUEST_WAIT);

        //�ΰ��� �ڷ�ƾ
        while (true)
        {
            if (tutoMgr.tutoComplete == false)
            {
                //�մ��� �̵����ϴ�� ���
                yield return new WaitWhile(() => moveGuest);

                //Ʃ�丮��� �մ� ����
                CreateGuest();

                //Ʃ�丮�󵿾� ���
                yield return new WaitUntil(() => tutoMgr.tutoComplete);

                //��� ���
                yield return new WaitForSeconds(2f);
            }
            else
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
                float dayRate = 1;
                if (gameMgr.playData.day == 1)
                    dayRate = 1;
                else if (gameMgr.playData.day == 2)
                    dayRate = 0.95f;
                else if (gameMgr.playData.day == 3)
                    dayRate = 0.9f;
                else if (gameMgr.playData.day == 4)
                    dayRate = 0.85f;
                else
                    dayRate = 0.8f;

                float upgradeRate = 1;
                if (gameMgr.playData.hasItem[(int)ShopItem.Advertisement_5])
                    upgradeRate -= 0.05f;
                if (gameMgr.playData.hasItem[(int)ShopItem.Advertisement_4])
                    upgradeRate -= 0.07f;
                if (gameMgr.playData.hasItem[(int)ShopItem.Advertisement_3])
                    upgradeRate -= 0.1f;
                if (gameMgr.playData.hasItem[(int)ShopItem.Advertisement_2])
                    upgradeRate -= 0.15f;
                if (gameMgr.playData.hasItem[(int)ShopItem.Advertisement_1])
                    upgradeRate -= 0.2f;

                ResumeData resumeData = gameMgr.playData.GetNowWorkerData();
                if (resumeData != null)
                {
                    if (resumeData.skill.Contains(WorkerSkill.WorkerSkill_3))
                        upgradeRate -= 0.07f;
                }

                delayValue = GUEST_DELAY_TIME * upgradeRate* dayRate;

                yield return new WaitForSeconds(delayValue);
            }

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
                //�����ϰ� �մ���ȣ���ϵ�
                //���ù湮�� �մ��� �ٽ� ��������
                List<Guest> guestList = new List<Guest>();
                for (Guest guest = Guest.Fox; guest < Guest.MAX; guest++)
                {
                    GuestObj guestObj = guests[guest];
                    if (guestObj.GetShowDay() > gameMgr.playData.day)
                        continue;
                    if (guestWeight[guest] > 0)
                        continue;
                    //������ �մ��� �����ϱ��Ѵ�.
                    if (visitedGuest.Contains(guest))
                    {
                        //�̹� �ִ� �մ��� �ٽ� ���� ����
                        continue;
                    }
                    guestList.Add(guest);
                }

                if (guestList.Count == 0)
                {
                    //�ظ��ϸ� ����� ���� �ʵ��� �մ�Ǯ�� �ø��� �������� ���ߵɵ�
                    //�湮�մ��� �ʱ�ȭ
                    //���ü��ִ� �մ� ����Ʈ�� ����'

                    for (Guest guest = Guest.Fox; guest < Guest.MAX; guest++)
                        guestWeight[guest] = 0;

                    for (Guest guest = Guest.Fox; guest < Guest.MAX; guest++)
                    {
                        GuestObj guestObj = guests[guest];
                        if (guestObj.GetShowDay() > gameMgr.playData.day)
                            continue;
                        if (guestWeight[guest] > 0)
                            continue;
                        //������ �մ��� �����ϱ��Ѵ�.
                        if (visitedGuest.Contains(guest))
                        {
                            //�̹� �ִ� �մ��� �ٽ� ���� ����
                            continue;
                        }
                        guestList.Add(guest);
                    }
                }

                if (guestList.Count == 0)
                    continue;

                //�մ��� ȣ��
                int guestRandom = Random.Range(0, guestList.Count);
                Guest nowGuest = guestList[guestRandom];
                if(tutoMgr.tutoComplete == false)
                {
                    //Ʃ�丮�󿡼��� �������� ���찡 ����
                    nowGuest = Guest.Fox;
                }

                visitedGuest.Add(nowGuest);

                for (Guest guest = Guest.Fox; guest < Guest.MAX; guest++)
                    guestWeight[guest]--;
                guestWeight[nowGuest] = 3;

                //�մԻ���
                GuestObj newGuest = GetGuestObj(nowGuest);
                float orderTime = ui.timer.time;
                if (newGuest.CreateMenu(orderTime) == false)
                {
                    //������ ������ �ٸ� �մ� ����
                    RemoveGuest(newGuest);
                    i--;
                    continue;
                }

                //�մ��� ����     
                guestcnt++;

                //�ֹ濡������ �̸� �ֹ� ������ ������

                newGuest.OrderGuest();
                KitchenMgr kitchenMgr = KitchenMgr.Instance;
                if(kitchenMgr.cameraObj.lookArea == LookArea.Kitchen)
                    soundMgr.PlaySE(Sound.NewOrder_SE);
                string talkStr = newGuest.GetTalkText();
                Sprite guestFace = newGuest.GetGuestFace();
                kitchenMgr.ui.memo.AddMemo(talkStr, guestFace, Memo_UI.MAX_TIME, newGuest);

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
                    //ù�մ� �ֹ�
                    StartCoroutine(GuestOrder(false));

                    if (kitchenMgr.cameraObj.lookArea == LookArea.Counter)
                        ui.goKitchen.OpenBtn();
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

    public void RemoveGuest(GuestObj obj)
    {
        if (guestPool.ContainsKey(obj.guest) == false)
        {
            guestPool[obj.guest] = new Queue<GuestObj>();
        }
        obj.gameObject.SetActive(false);
        guestPool[obj.guest].Enqueue(obj);
    }

    public void LeaveGuest(int idx)
    {
        //�մԶ���ó��
        if (waitGuest.Length <= idx)
            return;
        if (waitGuest[idx] == null)
            return;

        waitGuest[idx].LeaveGuest();
        guestcnt--;
        visitedGuest.Remove(waitGuest[idx].guest);
        RemoveGuest(waitGuest[idx]);

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

    public void SetSkipTalkBtnState(bool state)
    {
        skipTalkBtn.gameObject.SetActive(state);
    }

    public void SetGotoKitchenBtnState(bool state)
    {
        gotoKitchenBtn.gameObject.SetActive(state);
    }

    public void GiveChicken(ChickenSpicy spicy0, ChickenSpicy spicy1, ChickenState chickenState,
                            Drink pDrink, SideMenu pSideMenu)
    {
        vinylAni.gameObject.SetActive(true);

        StartCoroutine(RunCor());
        IEnumerator RunCor()
        {
            yield return new WaitForSeconds(0.5f);

            //�մ��� �� ����
            GuestReviews result = guestObj.ChickenPoint(spicy0, spicy1, chickenState, pDrink, pSideMenu);
            bool chickenStateResult = guestObj.CheckChickenState(chickenState);
            bool spicyResult = guestObj.CheckSpicy(spicy0, spicy1);
            bool drinkResult = guestObj.CheckDrink(pDrink);
            bool sideResult = guestObj.CheckSide(pSideMenu);

            switch (result)
            {
                case GuestReviews.Bad:
                    {
                        guestObj.AngryGuest(() => NextOrder());
                    }
                    break;
                case GuestReviews.Normal:
                    {
                        //������
                        int getValue = gameMgr.playData.GetMenuValue(result, spicy0, spicy1, chickenState, pDrink, pSideMenu);
                        ui.getMoney.RunAni(getValue);

                        gameMgr.dayMoney += getValue;
                        ui.nowMoney.SetMoney(gameMgr.playData.money + gameMgr.dayMoney);

                        guestObj.ThankGuest(() => NextOrder());
                    }
                    break;
                case GuestReviews.Happy:
                    {
                        //������
                        int getValue = gameMgr.playData.GetMenuValue(result, spicy0, spicy1, chickenState, pDrink, pSideMenu);
                        ui.getMoney.RunAni(getValue);

                        gameMgr.dayMoney += getValue;
                        ui.nowMoney.SetMoney(gameMgr.playData.money + gameMgr.dayMoney);

                        guestObj.HappyGuest(() => NextOrder());
                    }
                    break;
            }
            guestObj.ShowResult(spicyResult, chickenStateResult, drinkResult, sideResult);
            gameMgr.sellChickenCnt += 1;
            if (pDrink != Drink.None)
            {
                if (gameMgr.sellDrinkCnt.ContainsKey(pDrink) == false)
                    gameMgr.sellDrinkCnt[pDrink] = 0;
                gameMgr.sellDrinkCnt[pDrink]++;
            }
            if (pSideMenu != SideMenu.None)
            {
                if (gameMgr.sellSideMenuCnt.ContainsKey(pSideMenu) == false)
                    gameMgr.sellSideMenuCnt[pSideMenu] = 0;
                gameMgr.sellSideMenuCnt[pSideMenu]++;
            }

        }
    }

    private void NextOrder()
    {
        StartCoroutine(RunCor());
        IEnumerator RunCor()
        {
            KitchenMgr kitchenMgr = KitchenMgr.Instance;

            if (kitchenMgr.cameraObj.lookArea == LookArea.Counter)
                ui.goKitchen.OpenBtn();

            GuestObj leaveGuest = guestObj;

            waitGuest[0] = null;
            for (int i = 0; i < waitGuest.Length - 1; i++)
            {
                waitGuest[i] = waitGuest[i + 1];
                waitGuest[i + 1] = null;
                if (waitGuest[i] == null)
                    continue;
            }

            yield return new WaitForSeconds(1f);

            for (int i = 0; i < waitGuest.Length; i++)
            {
                if (waitGuest[i] == null)
                    continue;
                waitGuest[i].SetOrderSprite(guestPos[i].sortingOrder);
            }

            //��ȭâ �ݱ�
            leaveGuest.CloseTalkBox();

            if (tutoMgr.tutoComplete == false)
            {
                //Ʃ�丮�� �Ϸ�
                tutoMgr.tutoComplete = true;
                PlayerPrefs.SetInt("TUTO", 1);
            }

            //�մԶ���ó��
            StartCoroutine(LeaveGueset(leaveGuest));

            vinylAni.gameObject.SetActive(false);
            nowOrder = false;

            //��� �մ� �̵� �ִϸ��̼�
            if (guestcnt > 0)
            {
                bool alreadyOrder = kitchenMgr.ui.memo.HasGuestMemo(guestObj);
                alreadyOrder = true;
                //�ٷ� �̵��ϰ� ����

                if (alreadyOrder)
                {
                    StartCoroutine(MoveGuest());
                    StartCoroutine(GuestOrder(alreadyOrder));
                }
                else
                {
                    //�̵��� ���� �մ� �ֹ�
                    yield return StartCoroutine(MoveGuest());
                    StartCoroutine(GuestOrder(alreadyOrder));

                }
            }
        }
    }

    private IEnumerator LeaveGueset(GuestObj pGuestObj)
    {
        pGuestObj.LeaveGuest();
        guestcnt--;

        yield return new WaitForSeconds(2f);

        visitedGuest.Remove(pGuestObj.guest);
        RemoveGuest(pGuestObj);
    }

    private IEnumerator MoveGuest()
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
    }

    private IEnumerator GuestOrder(bool pAlreadyOrder)
    {
        KitchenMgr kitchenMgr = KitchenMgr.Instance;

        if (nowOrder)
            yield break;
        if (guestObj == null)
            yield break;

        guestObj.CloseResult();
        //if (pAlreadyOrder)
        //{
        //    //�̹� �ֹ��� �Ϸ��Ѱɷ� ����

        //    //if (kitchenMgr.cameraObj.lookArea == LookArea.Counter)
        //    //    ui.goKitchen.OpenBtn();

        //    yield return new WaitForSeconds(1f);

        //    guestObj.WaitGuest();
        //}
        //else
        {
            yield return new WaitForSeconds(1f);

            if (guestObj == null)
                yield break;

            nowOrder = true;
            if (kitchenMgr.cameraObj.lookArea == LookArea.Counter)
            {
                TalkOrder();
                //ui.goKitchen.OpenBtn();
            }
        }
    }

    public void TalkOrder()
    {
        if (guestObj == null)
            return;

        gotoKitchenBtn.gameObject.SetActive(false);
        skipTalkBtn.gameObject.SetActive(true);
        guestObj.TalkOrder(() =>
        {
            gotoKitchenBtn.gameObject.SetActive(true);
            skipTalkBtn.gameObject.SetActive(false);
        });
    }
}
