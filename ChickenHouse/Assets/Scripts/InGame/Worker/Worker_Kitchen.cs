using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker_Kitchen : Mgr
{
    [SerializeField] private float moveSpeed = 35;
    [SerializeField] private float handDelay = 0.1f;
    [SerializeField] private float eggDelay = 0.1f;
    [SerializeField] private float flourDelay = 0.1f;
    /** 알바생 손 **/
    [SerializeField] private Worker_Hand        leftHand;
    [SerializeField] private Worker_Hand        rightHand;

    /** 계란물 통 **/
    [SerializeField] private BowlEgg            bowlEgg;
    /** 밀가루 통 **/
    [SerializeField] private TrayFlour2         trayFlour;
    /** 치킨 건지 **/
    [SerializeField] private ChickenStrainter   chickenStrainter;

    [System.Serializable]
    public struct MOVEAREA
    {
        /** 치킨 더미 **/
        public RectTransform                    chickenDummyPos;
        /** 계란물 위치 **/
        public RectTransform                    bowlEggPos;
        /** 밀가루 위치 **/
        public RectTransform                    trayFlourPos2;
        /** 치킨건지 위치 **/ 
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
        //왼손이할거
        //왼손이 계란물묻힌 치킨을 들고잇으면 밀가루존에가서 치킨을 놓고온다.(만약 도중에 밀가루존이 꽉차버리면 계란통에 치킨조각을 다시 넣자)
        //왼손이 빈손이고 계란물통에 계란물을 덜 묻힌 치킨이있으면 계란물통에서 계란물을 묻히는 작업진행
        //왼손이 빈손이고 계란물통에 계란물을 다 묻힌 치킨이 있으면 계란물통에서 치킨을집는다.(밀가루존이 꽉차있으면 집지않는다.)
        //왼손에 치킨을들고있으면 계란물있는데로 이동해서 치킨을 놓도록하자(치킨이 들고있는데 계란물존이 깍차있는 경우는 없도록하자)
        //왼손이 빈손이고 계란물통에 치킨이 없으면 치킨박스로 이동해서 치킨을 집는다.(도중에 꽉차게되면 해당 작업이 취소됨)

        if (leftHand.handState == WorkerHandState.EggChicken && trayFlour.IsMax() == false)
        {
            MoveHand(0, leftHandArea, leftHand, DragArea.Tray_Flour2, handDelay, () =>
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
                LeftHandAct();
            });
        }
        else if (leftHand.handState == WorkerHandState.EggChicken && trayFlour.IsMax() && bowlEgg.CompleteEgg() == false)
        {
            MoveHand(0, leftHandArea, leftHand, DragArea.None, handDelay, () =>
            {
                leftHand.SetState(WorkerHandState.None);
                bowlEgg.WorkerEggChickenPutAway();
                LeftHandAct();
            });
        }
        else if (leftHand.handState == WorkerHandState.EggChicken && trayFlour.IsMax() && bowlEgg.CompleteEgg())
        {
            MoveHand(0, leftHandArea, leftHand, DragArea.None, handDelay, () =>
            {
                leftHand.SetState(WorkerHandState.None);
                LeftHandAct();
            });
        }
        else if (bowlEgg.isDrag)
        {
            MoveHand(0, leftHandArea, leftHand, DragArea.None, handDelay, () =>
            {
                leftHand.SetState(WorkerHandState.None);
                LeftHandAct();
            });
        }
        else if ((leftHand.handState == WorkerHandState.None || leftHand.handState == WorkerHandState.HandShake) && bowlEgg.IsMax() && bowlEgg.CompleteEgg() == false)
        {
            MoveHand(0, leftHandArea, leftHand, DragArea.Bowl_Egg, eggDelay,() =>
            {
                if (bowlEgg.isDrag)
                {
                    leftHand.SetState(WorkerHandState.None);
                }
                else if (bowlEgg.CompleteEgg())
                {
                    if (trayFlour.IsMax() == false)
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
                    leftHand.SetState(WorkerHandState.HandShake);
                    bowlEgg.WorkerDragChicken(0.1f);
                }
                LeftHandAct();
            });
        }
        else if (leftHand.handState == WorkerHandState.None && bowlEgg.IsMax() && bowlEgg.CompleteEgg())
        {
            MoveHand(0, leftHandArea, leftHand, DragArea.Bowl_Egg, handDelay, () =>
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
                LeftHandAct();
            });
        }
        else if (leftHand.handState == WorkerHandState.NormalChicken && bowlEgg.IsMax() == false)
        {
            MoveHand(0, leftHandArea, leftHand, DragArea.Bowl_Egg, handDelay, () =>
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
                LeftHandAct();
            });
        }
        else if (leftHand.handState == WorkerHandState.NormalChicken && bowlEgg.IsMax())
        {
            MoveHand(0, leftHandArea, leftHand, DragArea.Chicken_Box, handDelay, () =>
            {
                leftHand.SetState(WorkerHandState.None);
                LeftHandAct();
            });
        }
        else if (leftHand.handState == WorkerHandState.None && bowlEgg.IsMax() == false)
        {
            MoveHand(0, leftHandArea, leftHand, DragArea.Chicken_Box, handDelay, () =>
            {
                if (bowlEgg.IsMax())
                {
                    leftHand.SetState(WorkerHandState.None);
                }
                else
                {
                    leftHand.SetState(WorkerHandState.NormalChicken);
                }
                LeftHandAct();
            });
        }
        else if (leftHand.handState == WorkerHandState.None && bowlEgg.IsMax())
        {
            MoveHand(0, leftHandArea, leftHand, DragArea.None, handDelay, () =>
            {
                leftHand.SetState(WorkerHandState.None);
                LeftHandAct();
            });
        }
        else
        {
            MoveHand(0, leftHandArea, leftHand, DragArea.None, handDelay, () =>
            {
                leftHand.SetState(WorkerHandState.None);
                LeftHandAct();
            });
        }
    }

    private void RightHandAct()
    {
        //오른손이할거
        //1.기본 상태는 "밀가루 묻히기" 모드임
        //2.계란물 묻힌 치킨을 밀가루를 묻히러감
        //3."치킨건지로 옮기기" 모드라면 밀가루 존에 있는 치킨을 모두 건지로 옮기는것을 우선시함
        //4."치킨건지로 옮기기" 모드는 치킨 건지가 가득차거나 밀가루존에 치킨이 없게되면 "밀가루 묻히기"로 전환됨
        //5. "밀가루 묻히기" 모드는 밀가루존에 밀가루를 덜 묻힌 치킨 조각이있으면 밀가루존에 밀가루 작업을 진행함
        //6. "밀가루 묻히기" 모드에서 밀가루존에 치킨이 가득차 밀가루를 다 묻힌 치킨만 존재하게 되면 "치킨건지로 옮기기"모드로 전환됨

        if (rightHand.handState == WorkerHandState.FlourChicken && chickenStrainter.IsMax() == false)
        {
            MoveHand(1, rightHandArea, rightHand, DragArea.Chicken_Strainter, handDelay, () =>
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
                RightHandAct();
            });
        }
        else if (rightHand.handState == WorkerHandState.FlourChicken && chickenStrainter.IsMax() && trayFlour.IsMax() == false)
        {
            MoveHand(1, rightHandArea, rightHand, DragArea.None, handDelay, () =>
            {
                rightHand.SetState(WorkerHandState.None);
                trayFlour.WorkerFlourChickenPutAway();
                RightHandAct();
            });
        }
        else if (rightHand.handState == WorkerHandState.FlourChicken && chickenStrainter.IsMax() && trayFlour.IsMax() && trayFlour.IsComplete())
        {
            MoveHand(1, rightHandArea, rightHand, DragArea.None, handDelay, () =>
            {
                rightHand.SetState(WorkerHandState.None);
                RightHandAct();
            });
        }
        else if (trayFlour.NowDrag())
        {
            MoveHand(1, rightHandArea, rightHand, DragArea.None, handDelay, () =>
            {
                rightHand.SetState(WorkerHandState.None);
                RightHandAct();
            });
        }
        else if (rightHand.handState == WorkerHandState.None && trayFlour.IsComplete() && chickenStrainter.IsMax() == false)
        {
            MoveHand(1, rightHandArea, rightHand, DragArea.Tray_Flour2, handDelay, () =>
            {
                if (chickenStrainter.IsMax())
                {
                    rightHand.SetState(WorkerHandState.None);
                }
                else
                {
                    rightHand.SetState(WorkerHandState.FlourChicken);
                    trayFlour.WorkerRemoveChicken();
                }
                RightHandAct();
            });
        }
        else if (rightHand.handState == WorkerHandState.None && trayFlour.HasNotComplete())
        {
            MoveHand(1, rightHandArea, rightHand, DragArea.Tray_Flour2, flourDelay,() =>
            {
                if (trayFlour.NowDrag())
                {
                    rightHand.SetState(WorkerHandState.None);
                }
                else if(trayFlour.HasNotComplete())
                {
                    trayFlour.ClickChickens(1);
                }
                else if (trayFlour.IsComplete())
                {
                    if (chickenStrainter.IsMax() == false)
                    {
                        rightHand.SetState(WorkerHandState.FlourChicken);
                        trayFlour.WorkerRemoveChicken();
                    }
                    else
                    {
                        rightHand.SetState(WorkerHandState.None);
                    }
                }
                RightHandAct();
            });
        }
        else if (rightHand.handState == WorkerHandState.None && trayFlour.IsComplete())
        {
            MoveHand(1, rightHandArea, rightHand, DragArea.Tray_Flour2, handDelay, () =>
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
                    trayFlour.WorkerRemoveChicken();
                }
                RightHandAct();
            });
        }
        else
        {
            MoveHand(1, rightHandArea, rightHand, DragArea.None, handDelay, () =>
            {
                rightHand.SetState(WorkerHandState.None);
                RightHandAct();
            });
        }
    }

    private void MoveHand(int handNum, MOVEAREA moveArea, Worker_Hand hand, DragArea pArea, float pDelay, NoParaDel fun)
    {
        moveHandCor ??= new Dictionary<int, IEnumerator>();

        //해당손을 해당 위치로 이동시킨다.
        if (moveHandCor.ContainsKey(handNum) == false)
            moveHandCor[handNum] = null;

        if (moveHandCor[handNum] != null)
        {
            StopCoroutine(moveHandCor[handNum]);
            moveHandCor[handNum] = null;
        }
        moveHandCor[handNum] = MoveHandCor(moveArea, hand, pArea, pDelay, fun);
        StartCoroutine(moveHandCor[handNum]);
    }

    private IEnumerator MoveHandCor(MOVEAREA moveArea, Worker_Hand hand, DragArea pArea, float pDelay, NoParaDel fun)
    {

        float speedValue = 100;
        if (resumeData.skill.Contains(WorkerSkill.WorkerSkill_1))
        {
            //주방보조 경력자(직원 속도 +50%)
            speedValue += 50;
        }
        if (resumeData.skill.Contains(WorkerSkill.WorkerSkill_2))
        {
            //치킨가게 경력자(직원 속도 +100%)
            speedValue += 100;
        }
        speedValue /= 100f;

        float moveSpeed = this.moveSpeed * speedValue;
        float handWaitTime = pDelay / speedValue;

        if(pArea == DragArea.None)
        {
            hand.SetState(WorkerHandState.None);
            yield return new WaitForSeconds(handWaitTime);
            fun?.Invoke();
            yield break;
        }

        //손 이동
        Transform movePos = hand.transform;
        switch(pArea)
        {
            case DragArea.Chicken_Box:
                {
                    //치킨 더미
                    movePos = moveArea.chickenDummyPos;
                }
                break;
            case DragArea.Bowl_Egg:
                {
                    //계란물 묻히러 이동
                    movePos = moveArea.bowlEggPos;
                }
                break;
            case DragArea.Tray_Flour2:
                {
                    //밀가루로 이동
                    movePos = moveArea.trayFlourPos2;
                }
                break;
            case DragArea.Chicken_Strainter:
                {
                    //치킨 건지로 이동
                    movePos = moveArea.chickenStrainterPos;
                }
                break;
        }

        while(Vector2.Distance(hand.transform.position,movePos.position) > 0.01f)
        {
            hand.transform.position = Vector3.Lerp(hand.transform.position, movePos.position, 0.001f * moveSpeed);
            yield return null;
        }


        yield return new WaitForSeconds(handWaitTime);

        hand.transform.position = movePos.position;

        fun?.Invoke();
    }
}
