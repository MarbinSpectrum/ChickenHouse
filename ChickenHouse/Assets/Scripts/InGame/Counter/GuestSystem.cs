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
        /** �̺�Ʈ0 UI **/
        public Event0_UI    event0_UI;
    }
    public UI ui;

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
        //�̱��� ����
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

        //�ΰ��� �ڷ�ƾ
        while (true)
        {
            if (gameMgr.playData.tutoComplete1 == false)
            {
                //�մ��� �̵����ϴ�� ���
                yield return new WaitWhile(() => moveGuest);

                //Ʃ�丮��� �մ� ����
                CreateGuest();

                //Ʃ�丮�󵿾� ���
                yield return new WaitUntil(() => gameMgr.playData.tutoComplete1);

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
                float delayValue = GUEST_DELAY_TIME / (gameMgr.playData.GuestTotalDelayRate()/100f);
                if (counterWorker != null && counterWorker.skill.Contains(WorkerSkill.WorkerSkill_5))
                {
                    //ī���� ���� �����(ī���Ϳ� ��ġ�� �մ��� �湮�� +50%)
                    delayValue *= 0.5f;
                }

                while (delayValue > 0)
                {
                    delayValue -= Time.deltaTime;
                    if (guestcnt == 0)
                    {
                        //1�������� ����ϰ� �մ��� �ٷ� �־��ش�.
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
                //��Ʋ����
                break;
            }
            else if (ui.timer.IsEndTime())
            {
                //����ð��̶�� Ż��
                break;
            }
        }
    }


    public IEnumerator EndCheck()
    {
        if ((QuestState)gameMgr.playData.quest[(int)Quest.Event_0_Quest] == QuestState.Run)
        {
            yield return new WaitUntil(() => ui.event0_UI.battleResult != Event_0_Battle_Result.None);

            //����
            ui.dayEnd.ShowResult();
        }
        else
        {
            //��������ð����� ���
            yield return new WaitUntil(() => ui.timer.IsEndTime());

            //�մ��� 0���϶����� ���
            yield return new WaitUntil(() => (guestcnt == 0));

            //����
            ui.dayEnd.ShowResult();
        }
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
                    GuestObj guestObj = guestMgr.GetGuest(guest);
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
                        GuestObj guestObj = guestMgr.GetGuest(guest);
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
                if(gameMgr.playData.tutoComplete1 == false)
                {
                    //Ʃ�丮�󿡼��� �������� ���찡 ����
                    nowGuest = Guest.Fox;
                }

                visitedGuest.Add(nowGuest);

                for (Guest guest = Guest.Fox; guest < Guest.MAX; guest++)
                    guestWeight[guest]--;
                guestWeight[nowGuest] = 3;

                //�մԻ���
                GuestObj newGuest = guestMgr.GetGuestObj(nowGuest);
                float orderTime = ui.timer.time;
                if (newGuest.CreateMenu(orderTime) == false)
                {
                    //������ ������ �ٸ� �մ� ����
                    guestMgr.RemoveGuest(newGuest);
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
                    //ù�մ� �ֹ�

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
        //�մԶ���ó��
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
        //useWorker : ���� ��뿩��

        vinylAni.gameObject.SetActive(true);
        KitchenMgr kitchenMgr = KitchenMgr.Instance;

        StartCoroutine(RunCor());
        IEnumerator RunCor()
        {
            yield return new WaitForSeconds(0.5f);

            //�մ��� �� ����

            if (useWorker == false)
                skipTalkBtn.gameObject.SetActive(true);

            GuestReviews result = guestObj.ChickenPoint(spicy0, spicy1, chickenState, pDrink, pSideMenu);
            bool chickenStateResult = guestObj.CheckChickenState(chickenState);
            bool spicyResult = guestObj.CheckSpicy(spicy0, spicy1);
            bool drinkResult = guestObj.CheckDrink(pDrink);
            bool sideResult = guestObj.CheckSide(pSideMenu);

            //������ �ص� �մ� ���
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
                        //������
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
                        //������
                        int getValue = gameMgr.playData.GetMenuValue(result, spicy0, spicy1, chickenState, pDrink, pSideMenu);
                        float tipRate = gameMgr.playData.TipRate()/100f;
                        if(counterWorker != null && counterWorker.skill.Contains(WorkerSkill.WorkerSkill_4))
                        {
                            //�߻���ܸ�(�� ���� +100%)
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

            //��ȭâ �ݱ�
            leaveGuest.CloseTalkBox();

            if (gameMgr.playData.tutoComplete1 == false)
            {
                //Ʃ�丮�� �Ϸ�
                gameMgr.playData.tutoComplete1 = true;
            }

            //�մԶ���ó��
            StartCoroutine(LeaveGueset(leaveGuest));

            vinylAni.gameObject.SetActive(false);
            nowOrder = false;

            //��� �մ� �̵� �ִϸ��̼�
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
