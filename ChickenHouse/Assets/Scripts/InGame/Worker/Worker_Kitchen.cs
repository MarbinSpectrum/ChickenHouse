using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker_Kitchen : Mgr
{
    private const float DEFAULT_MOVE_SPEED = 35;
    private const float DEFAULT_HAND_DELAY = 0.1f;

    /** �˹ٻ� �� **/
    [SerializeField] private Worker_Hand        leftHand;
    [SerializeField] private Worker_Hand        rightHand;

    /** ����� �� **/
    [SerializeField] private BowlEgg            bowlEgg;
    /** �а��� �� **/
    [SerializeField] private TrayFlour2         trayFlour;
    /** ġŲ ���� **/
    [SerializeField] private ChickenStrainter   chickenStrainter;

    [System.Serializable]
    public struct MOVEAREA
    {
        /** ġŲ ���� **/
        public RectTransform                    chickenDummyPos;
        /** ����� ��ġ **/
        public RectTransform                    bowlEggPos;
        /** �а��� ��ġ **/
        public RectTransform                    trayFlourPos2;
        /** ġŲ���� ��ġ **/ 
        public RectTransform                    chickenStrainterPos;
    }
    [SerializeField] private MOVEAREA           leftHandArea;
    [SerializeField] private MOVEAREA           rightHandArea;


    private Dictionary<int, IEnumerator>        moveHandCor;

    private WorkerData resumeData
    {
        get
        {
            EWorker eWorker = (EWorker)gameMgr.playData.workerPos[(int)KitchenSet_UI.KitchenSetWorkerPos.KitchenWorker0];
            return workerMgr.GetWorkerData(eWorker);
        }
    }
    private EWorker eWorker
    {
        get
        {
            EWorker eWorker = (EWorker)gameMgr.playData.workerPos[(int)KitchenSet_UI.KitchenSetWorkerPos.KitchenWorker0];
            return eWorker;
        }
    }

    public void UpdateWorkerAct()
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

        //�޼����Ұ�
        //�޼��� ��������� ġŲ�� ��������� �а����������� ġŲ�� ����´�.(���� ���߿� �а������� ���������� ����뿡 ġŲ������ �ٽ� ����)
        //�޼��� ����̰� ������뿡 ������� �� ���� ġŲ�������� ������뿡�� ������� ������ �۾�����
        //�޼��� ����̰� ������뿡 ������� �� ���� ġŲ�� ������ ������뿡�� ġŲ�����´�.(�а������� ���������� �����ʴ´�.)
        //�޼տ� ġŲ����������� ������ִµ��� �̵��ؼ� ġŲ�� ����������(ġŲ�� ����ִµ� ��������� �����ִ� ���� ����������)
        //�޼��� ����̰� ������뿡 ġŲ�� ������ ġŲ�ڽ��� �̵��ؼ� ġŲ�� ���´�.(���߿� �����ԵǸ� �ش� �۾��� ��ҵ�)

        if(leftHand.handState == WorkerHandState.EggChicken && trayFlour.IsMax() == false)
        {
            MoveHand(0, leftHandArea, leftHand, DragArea.Tray_Flour2, () =>
            {
                if (trayFlour.IsMax())
                {
                    leftHand.SetState(WorkerHandState.None);
                    bowlEgg.WorkerEggChickenPutAway();
                }
                else
                {
                    leftHand.SetState(WorkerHandState.None);
                    trayFlour.AddChicken();
                }
            });
        }
        else if (leftHand.handState == WorkerHandState.EggChicken && trayFlour.IsMax() && bowlEgg.CompleteEgg() == false)
        {
            leftHand.SetState(WorkerHandState.None);
            bowlEgg.WorkerEggChickenPutAway();
        }
        else if (leftHand.handState == WorkerHandState.EggChicken && trayFlour.IsMax() && bowlEgg.CompleteEgg())
        {
            leftHand.SetState(WorkerHandState.None);
        }
        else if(bowlEgg.isDrag)
        {
            leftHand.SetState(WorkerHandState.None);
        }
        else if (leftHand.handState == WorkerHandState.None && bowlEgg.IsMax() && bowlEgg.CompleteEgg() == false)
        {
            MoveHand(0, leftHandArea, leftHand, DragArea.Bowl_Egg, () =>
            {
                if(bowlEgg.isDrag)
                {
                    leftHand.SetState(WorkerHandState.None);
                }
                else if (bowlEgg.CompleteEgg())
                {
                    if(trayFlour.IsMax() == false)
                    {
                        leftHand.SetState(WorkerHandState.EggChicken);
                        bowlEgg.RemoveChicken();
                    }
                    else
                    {
                        leftHand.SetState(WorkerHandState.None);
                    }
                }
                else
                {
                    bowlEgg.WorkerDragChicken(0.1f);
                }
            });
        }
        else if (leftHand.handState == WorkerHandState.None && bowlEgg.IsMax() && bowlEgg.CompleteEgg())
        {
            MoveHand(0, leftHandArea, leftHand, DragArea.Bowl_Egg, () =>
            {
                if (bowlEgg.isDrag)
                {
                    leftHand.SetState(WorkerHandState.None);
                }
                else if (trayFlour.IsMax())
                {
                    leftHand.SetState(WorkerHandState.None);
                }
                else if (bowlEgg.CompleteEgg())
                {
                    leftHand.SetState(WorkerHandState.EggChicken);
                    bowlEgg.RemoveChicken();
                }
            });
        }
        else if (leftHand.handState == WorkerHandState.NormalChicken && bowlEgg.IsMax() == false)
        {
            MoveHand(0, leftHandArea, leftHand, DragArea.Bowl_Egg, () =>
            {
                if (bowlEgg.IsMax())
                {
                    leftHand.SetState(WorkerHandState.None);
                }
                else
                {
                    leftHand.SetState(WorkerHandState.None);
                    bowlEgg.AddChicken();
                }
            });
        }
        else if (leftHand.handState == WorkerHandState.NormalChicken && bowlEgg.IsMax())
        {
            leftHand.SetState(WorkerHandState.None);
        }
        else if (leftHand.handState == WorkerHandState.None && bowlEgg.IsMax() == false)
        {
            MoveHand(0, leftHandArea, leftHand, DragArea.Chicken_Box, () =>
            {
                if (bowlEgg.IsMax())
                {
                    leftHand.SetState(WorkerHandState.None);
                }
                else
                {
                    leftHand.SetState(WorkerHandState.NormalChicken);
                }
            });
        }
        else if (leftHand.handState == WorkerHandState.None && bowlEgg.IsMax())
        {
            leftHand.SetState(WorkerHandState.None);
        }
        else
        {
            leftHand.SetState(WorkerHandState.None);
        }

        //���������Ұ�
        //1.�⺻ ���´� "�а��� ������" �����
        //2.����� ���� ġŲ�� �а��縦 ��������
        //3."ġŲ������ �ű��" ����� �а��� ���� �ִ� ġŲ�� ��� ������ �ű�°��� �켱����
        //4."ġŲ������ �ű��" ���� ġŲ ������ �������ų� �а������� ġŲ�� ���ԵǸ� "�а��� ������"�� ��ȯ��
        //5. "�а��� ������" ���� �а������� �а��縦 �� ���� ġŲ ������������ �а������� �а��� �۾��� ������
        //6. "�а��� ������" ��忡�� �а������� ġŲ�� ������ �а��縦 �� ���� ġŲ�� �����ϰ� �Ǹ� "ġŲ������ �ű��"���� ��ȯ��

        if(rightHand.handState == WorkerHandState.FlourChicken && chickenStrainter.IsMax() == false)
        {
            MoveHand(1, rightHandArea, rightHand, DragArea.Chicken_Strainter, () =>
            {
                if (chickenStrainter.IsMax())
                {
                    rightHand.SetState(WorkerHandState.None);
                    trayFlour.WorkerFlourChickenPutAway();
                }
                else
                {
                    rightHand.SetState(WorkerHandState.None);
                    chickenStrainter.AddChicken();
                }
            });
        }
        else if (rightHand.handState == WorkerHandState.FlourChicken && chickenStrainter.IsMax() && trayFlour.IsMax() == false)
        {
            rightHand.SetState(WorkerHandState.None);
            trayFlour.WorkerFlourChickenPutAway();
        }
        else if (rightHand.handState == WorkerHandState.FlourChicken && chickenStrainter.IsMax() && trayFlour.IsMax() && trayFlour.IsComplete())
        {
            rightHand.SetState(WorkerHandState.None);
        }
        else if (trayFlour.NowDrag())
        {
            rightHand.SetState(WorkerHandState.None);
        }
        else if (rightHand.handState == WorkerHandState.None && trayFlour.IsComplete() == false)
        {
            MoveHand(1, rightHandArea, rightHand, DragArea.Tray_Flour2, () =>
            {
                if (trayFlour.NowDrag())
                {
                    rightHand.SetState(WorkerHandState.None);
                }
                else if (trayFlour.IsComplete())
                {
                    if (chickenStrainter.IsMax() == false)
                    {
                        rightHand.SetState(WorkerHandState.FlourChicken);
                        trayFlour.RemoveChicken();
                    }
                    else
                    {
                        rightHand.SetState(WorkerHandState.None);
                    }
                }
                else
                {
                    trayFlour.ClickChickens(1);
                }
            });
        }
        else if (rightHand.handState == WorkerHandState.None && trayFlour.IsComplete())
        {
            MoveHand(1, rightHandArea, rightHand, DragArea.Tray_Flour2, () =>
            {
                if (trayFlour.NowDrag())
                {
                    rightHand.SetState(WorkerHandState.None);
                }
                else if (chickenStrainter.IsMax())
                {
                    rightHand.SetState(WorkerHandState.None);
                }
                else if (trayFlour.IsComplete())
                {
                    rightHand.SetState(WorkerHandState.FlourChicken);
                    trayFlour.RemoveChicken();
                }
            });
        }
        else
        {
            rightHand.SetState(WorkerHandState.None);
        }
    }

    public void MoveHand(int handNum, MOVEAREA moveArea, Worker_Hand hand, DragArea pArea, NoParaDel fun)
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
        moveHandCor[handNum] = MoveHandCor(moveArea, hand, pArea, fun);
        StartCoroutine(moveHandCor[handNum]);
    }

    private IEnumerator MoveHandCor(MOVEAREA moveArea, Worker_Hand hand, DragArea pArea, NoParaDel fun)
    {
        if(pArea == DragArea.None)
        {
            hand.SetState(WorkerHandState.None);
            yield break;
        }

        float speedValue = 100;
        if (resumeData.skill.Contains(WorkerSkill.WorkerSkill_1))
            speedValue += 50;
        if (resumeData.skill.Contains(WorkerSkill.WorkerSkill_2))
            speedValue += 100;
        speedValue /= 100f;

        float moveSpeed = DEFAULT_MOVE_SPEED * speedValue;

        //�� �̵�
        Transform movePos = hand.transform;
        switch(pArea)
        {
            case DragArea.Chicken_Box:
                {
                    //ġŲ ����
                    movePos = moveArea.chickenDummyPos;
                }
                break;
            case DragArea.Bowl_Egg:
                {
                    //����� ������ �̵�
                    movePos = moveArea.bowlEggPos;
                }
                break;
            case DragArea.Tray_Flour2:
                {
                    //�а���� �̵�
                    movePos = moveArea.trayFlourPos2;
                }
                break;
            case DragArea.Chicken_Strainter:
                {
                    //ġŲ ������ �̵�
                    movePos = moveArea.chickenStrainterPos;
                }
                break;
        }

        while(Vector2.Distance(hand.transform.position,movePos.position) > 0.01f)
        {
            hand.transform.position = Vector3.Lerp(hand.transform.position, movePos.position, 0.001f * moveSpeed);
            yield return null;
        }

        float handWaitTime = DEFAULT_HAND_DELAY / speedValue;

        yield return new WaitForSeconds(handWaitTime);

        hand.transform.position = movePos.position;

        fun?.Invoke();

        UpdateWorkerAct();
    }
}
