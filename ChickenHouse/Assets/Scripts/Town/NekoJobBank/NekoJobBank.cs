using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NekoJobBank : Mgr
{
    public struct Oner
    {
        public RectTransform    rect;
        public Animator         animator;
        public TalkBox_UI       talkBox;
    }

    [SerializeField] private Oner           oner;
    [SerializeField] private Animation      showMenu;
    [SerializeField] private RectTransform  resumeMenuBtn;
    [SerializeField] private RectTransform  header;

    public struct Resume
    {
        public RectTransform    rect;
        public Resume_UI        resumeUI;
        public RectTransform    leftArrow;
        public RectTransform    rightArrow;
        public RectTransform    employMenu;
        public TextMeshProUGUI  deposit;
        public Animator         stampAni;
        public Animator         resumeUIAni;
    }
    [SerializeField] private Resume                 resume;
    [SerializeField] private WorkerContractCheck    employMsgBox;
    [SerializeField] private Money_UI               moneyUI;
    [SerializeField] private TownMove               exitNekoJobBank;

    [SerializeField] private CanvasGroup            tutoDontTouch;
    [SerializeField] private TutoObj                tutoObj3;
    [SerializeField] private TutoObj                tutoObj3_1;
    [SerializeField] private TutoObj                tutoObj3_2;
    [SerializeField] private TutoObj                tutoObj3_3;
    [SerializeField] private TutoObj                tutoObj3_4;
    [SerializeField] private TutoObj                tutoObj3_5;
    private List<EWorker> workerList = new List<EWorker>();
    private int resumeSelectIdx = 0;
    private bool actResumeUI;

    public bool isOpen { private set; get; } = false;
    public bool run { private set; get; } = false;

    public void SetInit()
    {
        isOpen = true;
        resume.stampAni.gameObject.SetActive(false);

        UpdateWorkerList();

        //���� ��� Ȱ��ȭ ����
        bool resumeMenuAct = (workerList.Count != 0);
        resumeMenuBtn.gameObject.SetActive(resumeMenuAct);

        oner.talkBox.CloseTalkBox();
        showMenu.gameObject.SetActive(false);
        header.gameObject.SetActive(false);

        tutoDontTouch.blocksRaycasts = gameMgr.playData.tutoComplete4;

        IEnumerator Run()
        {
            oner.animator.Play("Hide");

            yield return new WaitForSeconds(1f);

            header.gameObject.SetActive(true);
            oner.animator.Play("Show");

            yield return new WaitForSeconds(1f);

            oner.animator.Play("Talk");

            string str = GetNPC_Talk_Text();
            soundMgr.PlayLoopSE(Sound.Voice26_SE);
            oner.talkBox.ShowText(str, TalkBoxType.Normal, () =>
            {
                soundMgr.StopLoopSE(Sound.Voice26_SE);
                oner.animator.Play("Idle");

                if (gameMgr.playData.tutoComplete4 == false)
                {
                    tutoDontTouch.blocksRaycasts = true;
                    tutoObj3.PlayTuto();
                }
            });

            showMenu.gameObject.SetActive(true);
            showMenu.Play();
            run = true;
        }
        StartCoroutine(Run());
    }

    private string GetNPC_Talk_Text()
    {
        int r = Random.Range(0, 4);
        switch(r)
        {
            case 0:
                return LanguageMgr.GetText("EMPLOYMENT_NPC_TALK_0");
            case 1:
                return LanguageMgr.GetText("EMPLOYMENT_NPC_TALK_1");
            case 2:
                return LanguageMgr.GetText("EMPLOYMENT_NPC_TALK_2");
            case 3:
                return LanguageMgr.GetText("EMPLOYMENT_NPC_TALK_3");
        }
        return string.Empty;
    }

    private void UpdateWorkerList()
    {
        //��밡���� ���� ����� ����
        workerList.Clear();
        for (EWorker worker = EWorker.Worker_1; worker < EWorker.MAX; worker++)
        {
            int idx = (int)worker;
            bool hasWorker = gameMgr.playData.hasWorker[idx];
            if (hasWorker)
            {
                //�̹� �����
                continue;
            }
            workerList.Add(worker);
        }
    }

    public void ShowResumeUI()
    {
        //�ν����Ϳ� ��� ����ϴ� �Լ�
        soundMgr.StopLoopSE(Sound.Voice26_SE);
        soundMgr.PlaySE(Sound.Btn_SE);
        oner.talkBox.CloseTalkBox();

        resume.rect.gameObject.SetActive(true);
        resumeSelectIdx = 0;
        ShowResumeUI(resumeSelectIdx);

        tutoDontTouch.blocksRaycasts = gameMgr.playData.tutoComplete4;
        if (gameMgr.playData.tutoComplete4 == false)
        {
            IEnumerator Run()
            {
                tutoObj3.CloseTuto();

                yield return new WaitUntil(
                      () => resume.resumeUIAni.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);
                yield return new WaitForSeconds(1f);
                tutoObj3_1.PlayTuto();
                tutoDontTouch.blocksRaycasts = true;

            }
            StartCoroutine(Run());
        }
    }

    public void CloseResumeUI()
    {
        //�ν����Ϳ� ��� ����ϴ� �Լ�

        if (actResumeUI == false)
            return;
        if (gameMgr.playData.tutoComplete4 == false)
        {
            if (tutoMgr.nowTuto == Tutorial.Town_Tuto_3_3)
            {
                tutoObj3_4.CloseTuto();
                tutoObj3_5.PlayTuto();
            }
            else
                return;
        }

        soundMgr.StopLoopSE(Sound.Voice26_SE);
        soundMgr.PlaySE(Sound.Btn_SE);
        resume.rect.gameObject.SetActive(false);
    }

    public void ExitNekoJobBank()
    {
        if (isOpen == false)
            return;
        if (run == false)
            return;
        isOpen = false;
        run = false;
        exitNekoJobBank.MoveTown();
        StopTalk();

        if (gameMgr.playData.tutoComplete4 == false && tutoMgr.nowTuto == Tutorial.Town_Tuto_3_4)
        {
            tutoObj3_5.CloseTuto();
        }
    }

    public void EscapeNekoJobBank()
    {
        if (resume.rect.gameObject.activeSelf)
            CloseResumeUI();
        else if (isOpen)
            ExitNekoJobBank();
    }

    public void ResumeArrowBtn(int dic)
    {
        //�ν����Ϳ� ��� ����ϴ� �Լ�
        soundMgr.PlaySE(Sound.Btn_SE);
        int nextIdx = resumeSelectIdx + dic;
        if (workerList.Count <= nextIdx || nextIdx < 0)
            return;
        resumeSelectIdx = nextIdx;
        ShowResumeUI(resumeSelectIdx);
    }

    private void ShowResumeUI(int pIdx)
    {
        //pIdx�� �ش��ϴ� �������� ���������� UI�� ǥ��

        UpdateWorkerList();

        if (workerList.Count <= pIdx || pIdx < 0)
            return;

        EWorker worker = workerList[pIdx];
        WorkerData workerData = workerMgr.GetWorkerData(worker);
        bool leftArrowAct = (pIdx - 1 >= 0);
        bool rightArrowAct = (pIdx + 1 < workerList.Count);
        resume.stampAni.gameObject.SetActive(false);
        resume.leftArrow.gameObject.SetActive(false);
        resume.rightArrow.gameObject.SetActive(false);
        resume.employMenu.gameObject.SetActive(false);
        actResumeUI = false;
        moneyUI.SetMoney(gameMgr.playData.money);

        resume.resumeUI.SetData(workerData,()=>
        {
            resume.leftArrow.gameObject.SetActive(leftArrowAct);
            resume.rightArrow.gameObject.SetActive(rightArrowAct);
            resume.employMenu.gameObject.SetActive(true);
            actResumeUI = true;
        });
        string depositText = LanguageMgr.GetText("DEPOSIT");
        int newDeposit = (int)(workerData.deposit * (100f - gameMgr.playData.ShopSaleValue()) / 100f);
        string newDepositStr = LanguageMgr.GetMoneyStr(resume.deposit.fontSize, newDeposit);
        string depositString = string.Format("{0} : {1}", depositText, newDepositStr);

        LanguageMgr.SetText(resume.deposit, depositString);
    }

    public void EmployBtn()
    {
        //�ν����Ϳ� ��� ����ϴ� �Լ�
        soundMgr.PlaySE(Sound.Btn_SE);
        EWorker worker = workerList[resumeSelectIdx];
        WorkerData workerData = workerMgr.GetWorkerData(worker);
        int newDeposit = (int)(workerData.deposit * (100f - gameMgr.playData.ShopSaleValue()) / 100f);
        if (newDeposit > gameMgr.playData.money)
        {
            //���� ����
            return;
        }

        employMsgBox.SetUI(()=>
        {
            soundMgr.PlaySE(Sound.Stamp_SE);

            resume.leftArrow.gameObject.SetActive(false);
            resume.rightArrow.gameObject.SetActive(false);
            resume.employMenu.gameObject.SetActive(false);

            PlayData playData = gameMgr.playData;
            playData.hasWorker[(int)worker] = true;
            playData.money -= newDeposit;
            moneyUI.SetMoney(playData.money);
            resume.stampAni.gameObject.SetActive(true);

            tutoDontTouch.blocksRaycasts = gameMgr.playData.tutoComplete4;
            if (gameMgr.playData.tutoComplete4 == false)
            {
                IEnumerator Run()
                {
                    tutoObj3_2.CloseTuto();
                    yield return new WaitUntil(
                        () => resume.stampAni.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);

                    tutoDontTouch.blocksRaycasts = true;
                    tutoObj3_3.PlayTuto();
                }
                StartCoroutine(Run());
            }
        });

        if (gameMgr.playData.tutoComplete4 == false)
        {
            tutoObj3_1.CloseTuto();
            tutoObj3_2.PlayTuto();
        }
    }

    public void StopTalk()
    {
        //�ν����Ϳ��� ��� ����ϴ� �Լ�
        soundMgr.StopLoopSE(Sound.Voice26_SE);
    }
}
