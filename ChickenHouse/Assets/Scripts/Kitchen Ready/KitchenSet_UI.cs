using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;

public class KitchenSet_UI : Mgr
{
    [SerializeField] private RectTransform workerTokenArea;
    [SerializeField] private KitchenSetWorkerToken workerToken;
    [SerializeField] private KitchenSetWorkerToken[] workerTokenPoint;
    [SerializeField] private KitchenSetWorkerToken dragToken;
    [SerializeField] private TextMeshProUGUI workerMaxText;
    [SerializeField] private RectTransform skillRect;
    [SerializeField] private List<KitchenWorkerInfoList> skillList;
    private List<KitchenSetWorkerToken> tokens = new List<KitchenSetWorkerToken>();

    private EWorker[] workerArea = new EWorker[3];
    private EWorker dragWorker = EWorker.None;

    public enum KitchenSetWorkerPos
    {
        CounterWorker = 0,    //카운터 업무
        KitchenWorker0 = 1,    //치킨 계란물/밀가루 묻히기 업무
        KitchenWorker1 = 2,    //치킨 튀기기 업무

        MAX,
        None
    }
    private KitchenSetWorkerPos dropPos = KitchenSetWorkerPos.None;

    private int maxWorker = 1;

    public void Init()
    {
        workerArea[(int)KitchenSetWorkerPos.CounterWorker] = (EWorker)gameMgr.playData.workerPos[(int)KitchenSetWorkerPos.CounterWorker];
        workerArea[(int)KitchenSetWorkerPos.KitchenWorker0] = (EWorker)gameMgr.playData.workerPos[(int)KitchenSetWorkerPos.KitchenWorker0];
        workerArea[(int)KitchenSetWorkerPos.KitchenWorker1] = (EWorker)gameMgr.playData.workerPos[(int)KitchenSetWorkerPos.KitchenWorker1];

        RefreshToken();
    }

    private void Update()
    {
        if (dragWorker == EWorker.None)
        {
            dragToken.gameObject.SetActive(false);
            dropPos = KitchenSetWorkerPos.None;
        }
        else
        {
            dragToken.SetUI(dragWorker);
            dragToken.gameObject.SetActive(true);

            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dragToken.transform.position = new Vector3(pos.x, pos.y, 0);
        }
    }

    private void RefreshToken()
    {
        PlayData playData = gameMgr.playData;
        EWorker counterWorker = workerArea[(int)KitchenSetWorkerPos.CounterWorker];
        EWorker kitchenWorker0 = workerArea[(int)KitchenSetWorkerPos.KitchenWorker0];
        EWorker kitchenWorker1 = workerArea[(int)KitchenSetWorkerPos.KitchenWorker1];

        List<EWorker> workerList = new List<EWorker>();
        for (EWorker worker = EWorker.Worker_1; worker < EWorker.MAX; worker++)
        {
            int idx = (int)worker;
            bool hasWorker = playData.hasWorker[idx];
            if (hasWorker == false)
            {
                //고용하지 않은 직원은 표시되지 않음
                continue;
            }

            if (counterWorker == worker || kitchenWorker0 == worker || kitchenWorker1 == worker)
            {
                //해당 직원은 이미 배치되어있음
                continue;
            }

            workerList.Add(worker);
        }

        tokens.ForEach((x) => x.gameObject.SetActive(false));
        for (int i = 0; i < workerList.Count; i++)
        {
            if (tokens.Count <= i)
            {
                KitchenSetWorkerToken newToken = Instantiate(workerToken, workerTokenArea);
                tokens.Add(newToken);
            }
            float alphaValue = 1f;
            if (dragWorker == workerList[i])
            {
                //해당 직원은 드래그 상태
                alphaValue = 0f;
            }

            tokens[i].SetUI(workerList[i], alphaValue);
            tokens[i].gameObject.SetActive(true);
        }

        {
            //배치한 토큰 표시

            float alphaValue = (dragWorker == counterWorker) ? 0 : 1;
            workerTokenPoint[(int)KitchenSetWorkerPos.CounterWorker].SetUI(counterWorker, alphaValue);

            alphaValue = (dragWorker == kitchenWorker0) ? 0 : 1;
            workerTokenPoint[(int)KitchenSetWorkerPos.KitchenWorker0].SetUI(kitchenWorker0, alphaValue);

            alphaValue = (dragWorker == kitchenWorker1) ? 0 : 1;
            workerTokenPoint[(int)KitchenSetWorkerPos.KitchenWorker1].SetUI(kitchenWorker1, alphaValue);
        }

        {
            //갯수 표시

            int useWorkerCnt = GetUseWorkerCnt();

            string numString = string.Format("({0}/{1})", useWorkerCnt, maxWorker);
            LanguageMgr.SetText(workerMaxText, numString);
        }

        if (dragWorker != EWorker.None)
        {
            skillRect.gameObject.SetActive(true);

            WorkerData workerData = workerMgr.GetWorkerData(dragWorker);
            skillList.ForEach((x) => x.gameObject.SetActive(false));
            for (int i = 0; i < skillList.Count; i++)
            {
                if (workerData.skill.Count <= i)
                    break;
                skillList[i].SetUI(workerData.skill[i]);
                skillList[i].gameObject.SetActive(true);
            }
        }
        else
        {
            skillRect.gameObject.SetActive(false);
        }
    }

    public void DragToken(EWorker pWorker)
    {
        if (dragWorker != EWorker.None)
            return;
        dragWorker = pWorker;
        RefreshToken();
    }

    public void DropToken()
    {
        //인스펙터에서 끌어서 사용하는 함수임
        KitchenSetWorkerPos prevPos = KitchenSetWorkerPos.None;
        if (workerArea[(int)KitchenSetWorkerPos.CounterWorker] == dragWorker)
        {
            prevPos = KitchenSetWorkerPos.CounterWorker;
            workerArea[(int)prevPos] = EWorker.None;
        }
        if (workerArea[(int)KitchenSetWorkerPos.KitchenWorker0] == dragWorker)
        {
            prevPos = KitchenSetWorkerPos.KitchenWorker0;
            workerArea[(int)prevPos] = EWorker.None;
        }
        if (workerArea[(int)KitchenSetWorkerPos.KitchenWorker1] == dragWorker)
        {
            prevPos = KitchenSetWorkerPos.KitchenWorker1;
            workerArea[(int)prevPos] = EWorker.None;
        }

        int useWorkerCnt = GetUseWorkerCnt();

        if (dropPos != KitchenSetWorkerPos.None)
        {
            if (useWorkerCnt >= maxWorker && workerArea[(int)dropPos] != EWorker.None)
            {
                if (prevPos != KitchenSetWorkerPos.None)
                {
                    //스왑처리
                    workerArea[(int)prevPos] = workerArea[(int)dropPos];
                }
                workerArea[(int)dropPos] = dragWorker;
            }
            else if (useWorkerCnt < maxWorker)
            {
                if (prevPos != KitchenSetWorkerPos.None)
                {
                    //스왑처리
                    workerArea[(int)prevPos] = workerArea[(int)dropPos];
                }
                workerArea[(int)dropPos] = dragWorker;
            }
        }

        dragWorker = EWorker.None;
        RefreshToken();
    }

    public void EnterTokenPoint(int pArea)
    {
        //인스펙터에서 끌어서 사용하는 함수임
        if (dragWorker == EWorker.None)
            return;

        int useWorkerCnt = GetUseWorkerCnt();

        //이미 놓여져 있는 녀석인가?
        bool isSetToken = false;
        if (workerArea[(int)KitchenSetWorkerPos.CounterWorker] == dragWorker)
            isSetToken = true;
        if (workerArea[(int)KitchenSetWorkerPos.KitchenWorker0] == dragWorker)
            isSetToken = true;
        if (workerArea[(int)KitchenSetWorkerPos.KitchenWorker1] == dragWorker)
            isSetToken = true;

        if (useWorkerCnt >= maxWorker && workerArea[pArea] == EWorker.None && isSetToken == false)
        {
            //더이상 못넣음
            return;
        }

        workerTokenPoint[pArea].SetUI(dragWorker, 0.5f);
        dropPos = (KitchenSetWorkerPos)pArea;
    }

    public void ExitTokenPoint(int pArea)
    {
        //인스펙터에서 끌어서 사용하는 함수임
        if (dragWorker == EWorker.None)
            return;

        workerTokenPoint[pArea].SetUI(workerArea[pArea], 1f);
        dropPos = KitchenSetWorkerPos.None;
    }

    public int GetUseWorkerCnt()
    {
        //사용한 직원 갯수
        int useWorkerCnt = 0;
        if (workerArea[(int)KitchenSetWorkerPos.CounterWorker] != EWorker.None)
            useWorkerCnt++;
        if (workerArea[(int)KitchenSetWorkerPos.KitchenWorker0] != EWorker.None)
            useWorkerCnt++;
        if (workerArea[(int)KitchenSetWorkerPos.KitchenWorker1] != EWorker.None)
            useWorkerCnt++;
        return useWorkerCnt;
    }

    public void ApplyKitchenSet()
    {
        gameMgr.playData.workerPos[(int)KitchenSetWorkerPos.CounterWorker] = (int)workerArea[(int)KitchenSetWorkerPos.CounterWorker];
        gameMgr.playData.workerPos[(int)KitchenSetWorkerPos.KitchenWorker0] = (int)workerArea[(int)KitchenSetWorkerPos.KitchenWorker0];
        gameMgr.playData.workerPos[(int)KitchenSetWorkerPos.KitchenWorker1] = (int)workerArea[(int)KitchenSetWorkerPos.KitchenWorker1];
    }
}