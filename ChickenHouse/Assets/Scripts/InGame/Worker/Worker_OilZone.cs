using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker_OilZone : Mgr
{
    [SerializeField] private float moveSpeed = 35;
    [SerializeField] private float handDelay = 0.1f;

    /** �˹ٻ� �� **/
    [SerializeField] private Worker_Hand leftHand;
    [SerializeField] private Worker_Hand rightHand;

    /** ġŲ���� **/
    [SerializeField] private ChickenStrainter   chickenStrainter;

    /** ġŲ���� **/
    [SerializeField] private Transform          chickenStrainterTrans;
    /** Ƣ��� ��ġ **/
    [SerializeField] private List<Transform>    oliZoneTrans;
    /** �޼��� ��� ��ġ **/
    [SerializeField] private Transform          leftWaitTrans;

    /** ġŲ�ڽ� ��ġ **/
    [SerializeField] private Transform          chickenPackTrans;
    /** ������ ��� ��ġ **/
    [SerializeField] private Transform          rightWaitTrans;

    /** Ƣ��� **/
    [SerializeField] private List<Oil_Zone>     oliZone;
    /** ġŲ�� **/
    [SerializeField] private ChickenPackList    chickenPackList;

    private ChickenState    chickenState;
    private bool            chickenShaderMode;
    private float           chickenShaderLerp;

    private Dictionary<int, IEnumerator> moveHandCor;

    private WorkerData resumeData
    {
        get
        {
            EWorker eWorker = (EWorker)gameMgr.playData.workerPos[(int)KitchenSet_UI.KitchenSetWorkerPos.KitchenWorker1];
            return workerMgr.GetWorkerData(eWorker);
        }
    }
    private EWorker eWorker
    {
        get
        {
            EWorker eWorker = (EWorker)gameMgr.playData.workerPos[(int)KitchenSet_UI.KitchenSetWorkerPos.KitchenWorker1];
            return eWorker;
        }
    }

    public void WorkerAct()
    {
        leftHand.gameObject.SetActive(true);
        rightHand.gameObject.SetActive(true);

        if (resumeData == null)
        {
            leftHand.gameObject.SetActive(false);
            rightHand.gameObject.SetActive(false);
            return;
        }

        leftHand.SetWorkerImg(eWorker);
        rightHand.SetWorkerImg(eWorker);

        LeftHandAct();
        RightHandAct();
    }

    private void LeftHandAct()
    {
        Oil_Zone canUseOilZone = CanOilZone();
        Transform oilZoneTrans = CanOilZoneTrans();

        if (leftHand.handState == WorkerHandState.StrainterFlour && canUseOilZone != null)
        {
            MoveHand(0, leftHand, oilZoneTrans, handDelay, () =>
            {
                if(canUseOilZone.IsRun() == false)
                {
                    leftHand.SetState(WorkerHandState.None);
                    canUseOilZone.Cook_Start(4);
                }
                LeftHandAct();
            });
        }
        else if (leftHand.handState == WorkerHandState.StrainterFlour && canUseOilZone == null)
        {
            MoveHand(0, leftHand, leftWaitTrans, handDelay, () =>
            {
                LeftHandAct();
            });
        }
        else if (chickenStrainter.isHold)
        {
            MoveHand(0, leftHand, leftWaitTrans, handDelay, () =>
            {
                LeftHandAct();
            });
        }
        else if (leftHand.handState == WorkerHandState.None  && chickenStrainter.IsMax() && chickenStrainter.isHold == false)
        {
            MoveHand(0, leftHand, chickenStrainterTrans, handDelay, () =>
            {
                if(chickenStrainter.IsMax() && chickenStrainter.isHold == false)
                {
                    for(int i = 0; i < 4; i++)
                    chickenStrainter.RemoveChicken();
                    leftHand.SetState(WorkerHandState.StrainterFlour);
                }
                LeftHandAct();
            });
        }
        else
        {
            MoveHand(0, leftHand, leftWaitTrans, handDelay, () =>
            {
                LeftHandAct();
            });
        }
    }

    private Transform CanOilZoneTrans()
    {
        //����� �� �ִ� Ƣ��⸦ ��ȯ
        for (int i = 0; i < oliZone.Count; i++)
        {
            Oil_Zone oil = oliZone[i];
            if (oil.gameObject.activeSelf == false)
                continue;
            if (oil.IsRun())
                continue;
            return oliZoneTrans[i];
        }
        return null;
    }

    private Oil_Zone CanOilZone()
    {
        //����� �� �ִ� Ƣ��⸦ ��ȯ
        for (int i = 0; i < oliZone.Count; i++)
        {
            Oil_Zone oil = oliZone[i];
            if (oil.gameObject.activeSelf == false)
                continue;
            if (oil.IsRun())
                continue;
            return oil;
        }
        return null;
    }

    private void RightHandAct()
    {
        Oil_Zone    completeOilZone         = CompleteOilZone();
        Transform   completeOilZoneTrans    = CompleteOilZoneTrans();

        if (rightHand.handState == WorkerHandState.StrainterFry && chickenPackList.CanAddChickenPack())
        {
            MoveHand(1, rightHand, chickenPackTrans, handDelay, () =>
            {
                if (chickenPackList.AddChickenPack(4,chickenState,ChickenSpicy.None, ChickenSpicy.None,chickenShaderMode,chickenShaderLerp))
                {
                    rightHand.SetState(WorkerHandState.None);
                }
                RightHandAct();
            });
        }
        else if (rightHand.handState == WorkerHandState.StrainterFlour && chickenPackList.CanAddChickenPack() == false)
        {
            MoveHand(1, rightHand, rightWaitTrans, handDelay, () =>
            {
                RightHandAct();
            });
        }
        else if (rightHand.handState == WorkerHandState.None && completeOilZone != null)
        {
            MoveHand(1, rightHand, completeOilZoneTrans, handDelay, () =>
            {
                if (completeOilZone.IsComplete())
                {
                    completeOilZone.WorkerCookStopChicken(this);
                    rightHand.SetState(WorkerHandState.StrainterFry);
                }
                RightHandAct();
            });
        }
        else
        {
            MoveHand(1, rightHand, rightWaitTrans, handDelay, () =>
            {
                RightHandAct();
            });
        }
    }

    private Oil_Zone CompleteOilZone()
    {
        //������ �Ϸ�� Ƣ��⸦ ��ȯ
        for (int i = 0; i < oliZone.Count; i++)
        {
            Oil_Zone oil = oliZone[i];
            if (oil.gameObject.activeSelf == false)
                continue;
            if (oil.isHold)
                continue;
            if (oil.IsComplete())
                return oil;
        }
        return null;
    }

    private Transform CompleteOilZoneTrans()
    {
        //������ �Ϸ�� Ƣ��⸦ ��ġ ��ȯ
        for (int i = 0; i < oliZone.Count; i++)
        {
            Oil_Zone oil = oliZone[i];
            if (oil.gameObject.activeSelf == false)
                continue;
            if (oil.isHold)
                continue;
            if (oil.IsComplete())
                return oliZoneTrans[i];
        }
        return null;
    }

    public void SetStrainterState(ChickenState pChickenState,bool pShaderMode,float pShaderLerp)
    {
        //ġŲ ���� ����
        chickenState = pChickenState;
        chickenShaderMode = pShaderMode;
        chickenShaderLerp = pShaderLerp;
        rightHand.SetStrainter(4, chickenShaderMode, chickenShaderLerp);
    }

    private void MoveHand(int handNum, Worker_Hand hand, Transform targetTrans, float pDelay, NoParaDel fun)
    {
        moveHandCor ??= new Dictionary<int, IEnumerator>();

        //�ش���� �ش� ��ġ�� �̵���Ų��.
        if (moveHandCor.ContainsKey(handNum) == false)
            moveHandCor[handNum] = null;

        if (moveHandCor[handNum] != null)
        {
            StopCoroutine(moveHandCor[handNum]);
            moveHandCor[handNum] = null;
        }
        moveHandCor[handNum] = MoveHandCor(hand, targetTrans, pDelay, fun);
        StartCoroutine(moveHandCor[handNum]);
    }

    private IEnumerator MoveHandCor(Worker_Hand hand, Transform targetTrans, float pDelay, NoParaDel fun)
    {

        float speedValue = 100;
        if (resumeData.skill.Contains(WorkerSkill.WorkerSkill_1))
        {
            //�ֹ溸�� �����(���� �ӵ� +50%)
            speedValue += 50;
        }
        if (resumeData.skill.Contains(WorkerSkill.WorkerSkill_2))
        {
            //ġŲ���� �����(���� �ӵ� +100%)
            speedValue += 100;
        }
        speedValue /= 100f;

        float moveSpeed = this.moveSpeed * speedValue;
        float handWaitTime = pDelay / speedValue;

        if (targetTrans == null)
        {
            hand.SetState(WorkerHandState.None);
            yield return new WaitForSeconds(handWaitTime);
            fun?.Invoke();
            yield break;
        }

        //�� �̵�
        Transform movePos = targetTrans.transform;

        while (Vector2.Distance(hand.transform.position, movePos.position) > 0.01f)
        {
            hand.transform.position = Vector3.Lerp(hand.transform.position, movePos.position, 0.001f * moveSpeed);
            yield return null;
        }


        yield return new WaitForSeconds(handWaitTime);

        hand.transform.position = movePos.position;

        fun?.Invoke();
    }
}
