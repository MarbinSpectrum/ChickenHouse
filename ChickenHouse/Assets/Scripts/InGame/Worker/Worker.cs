using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : Mgr
{
    private const float DEFAULT_SPEED = 25;

    /** 알바생 손 **/
    [SerializeField] private Worker_Hand        leftHand;
    [SerializeField] private Worker_Hand        rightHand;

    /** 계란물 통 **/
    [SerializeField] private TrayEgg            trayEgg;
    /** 밀가루 통 **/
    [SerializeField] private TrayFlour          trayFlour;
    /** 치킨 건지 **/
    [SerializeField] private ChickenStrainter   chickenStrainter;

    [System.Serializable]
    public struct MOVEAREA
    {
        /** 치킨 더미 **/
        public RectTransform                    chickenDummyPos;
        /** 계란물 위치 **/
        public RectTransform                    trayEggPos;
        /** 밀가루 위치 **/
        public RectTransform                    trayFlourPos;
        /** 치킨건지 위치 **/ 
        public RectTransform                    chickenStrainterPos;
    }
    [SerializeField] private MOVEAREA           leftHandArea;
    [SerializeField] private MOVEAREA           rightHandArea;

    private Dictionary<int, IEnumerator>        moveHandCor;
    private DragArea                            leftArea;
    private DragArea                            rightArea;

    public void UpdateHandMoveArea()
    {
        leftHand.gameObject.SetActive(true);
        rightHand.gameObject.SetActive(true);

        if (gameMgr.playData.hasItem[(int)ShopItem.Worker_1] == false)
        {
            leftHand.gameObject.SetActive(false);
            rightHand.gameObject.SetActive(false);
            return;
        }

        if (gameMgr.playData.hasItem[(int)ShopItem.Worker_3] == false)
        {
            rightHand.gameObject.SetActive(false);
        }

        //왼손이할거
        //1.계란물 묻힌 치킨이없으면 계란물 묻히기
        if (trayEgg.IsMax() == false)
        {
            if (leftHand.handState == WorkerHandState.None)
                leftArea = DragArea.Chicken_Box;
            else if (leftHand.handState == WorkerHandState.NormalChicken)
                leftArea = DragArea.Tray_Egg;
        }
        else 
        {
            leftArea = DragArea.None;
        }

        //오른손이할거
        //1.계란물 묻힌 치킨을 밀가루를 묻히러감
        //2.치킨건지가 있으면 밀가루묻힌 치킨을 치킨건지에 넣어줌
        if (trayFlour.IsMax() == false && trayFlour.IsMax() == false && 
            rightHand.handState != WorkerHandState.FlourChicken && gameMgr.playData.hasItem[(int)ShopItem.Worker_3])
        {
            if (rightHand.handState == WorkerHandState.None)
            {
                if(trayEgg.HasChicken())
                    rightArea = DragArea.Tray_Egg;
                else
                    rightArea = DragArea.None;
            }
            else if (rightHand.handState == WorkerHandState.EggChicken)
                rightArea = DragArea.Tray_Flour;
        }
        else if (chickenStrainter.isRun && chickenStrainter.IsMax() == false && gameMgr.playData.hasItem[(int)ShopItem.Worker_5])
        {
            if (rightHand.handState == WorkerHandState.None)
            {
                if (trayFlour.HasChicken())
                    rightArea = DragArea.Tray_Flour;
                else if (trayEgg.HasChicken())
                    rightArea = DragArea.Tray_Egg;
                else
                    rightArea = DragArea.None;
            }
            else if (rightHand.handState == WorkerHandState.FlourChicken)
                rightArea = DragArea.Chicken_Strainter;
            else if (rightHand.handState == WorkerHandState.EggChicken)
                rightArea = DragArea.Tray_Flour;
        }
        else
        {
            rightArea = DragArea.None;
        }

        MoveHand(0, leftHandArea, leftHand, leftArea);
        if (gameMgr.playData.hasItem[(int)ShopItem.Worker_3])
            MoveHand(1, rightHandArea, rightHand, rightArea);
    }

    public void MoveHand(int handNum, MOVEAREA moveArea, Worker_Hand hand, DragArea pArea)
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
        moveHandCor[handNum] = MoveHandCor(moveArea, hand, pArea);
        StartCoroutine(moveHandCor[handNum]);
    }

    private IEnumerator MoveHandCor(MOVEAREA moveArea, Worker_Hand hand, DragArea pArea)
    {
        if(pArea == DragArea.None)
        {
            hand.SetState(WorkerHandState.None);
            yield break;
        }

        float moveSpeed = DEFAULT_SPEED;
        if (gameMgr.playData.hasItem[(int)ShopItem.Worker_2])
            moveSpeed *= 2;
        if (gameMgr.playData.hasItem[(int)ShopItem.Worker_4])
            moveSpeed *= 2;
        if (gameMgr.playData.hasItem[(int)ShopItem.Worker_6])
            moveSpeed *= 2;

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
            case DragArea.Tray_Egg:
                {
                    //계란물 묻히러 이동
                    movePos = moveArea.trayEggPos;
                }
                break;
            case DragArea.Tray_Flour:
                {
                    //밀가루로 이동
                    movePos = moveArea.trayFlourPos;
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

        hand.transform.position = movePos.position;

        switch (pArea)
        {
            case DragArea.Chicken_Box:
                {
                    hand.SetState(WorkerHandState.NormalChicken);
                    UpdateHandMoveArea();
                }
                break;
            case DragArea.Tray_Egg:
                {
                    if(hand.handState == WorkerHandState.NormalChicken)
                    {
                        hand.SetState(WorkerHandState.None);
                        trayEgg.AddChicken();
                    }
                    else if (hand.handState == WorkerHandState.None)
                    {
                        hand.SetState(WorkerHandState.EggChicken);
                        trayEgg.RemoveChicken();
                        trayEgg.RemoveEggSlotbyWorker();
                    }
                }
                break;
            case DragArea.Tray_Flour:
                {
                    //밀가루로 이동
                    if (hand.handState == WorkerHandState.EggChicken)
                    {
                        hand.SetState(WorkerHandState.None);
                        trayFlour.AddChicken();
                    }
                    else if (hand.handState == WorkerHandState.None)
                    {
                        hand.SetState(WorkerHandState.FlourChicken);
                        trayFlour.RemoveChicken();
                        trayFlour.RemoveFlourSlotbyWorker();
                    }
                }
                break;
            case DragArea.Chicken_Strainter:
                {
                    //치킨 건지로 이동
                    if (hand.handState == WorkerHandState.FlourChicken)
                    {
                        hand.SetState(WorkerHandState.None);
                        chickenStrainter.AddChicken();
                    }
                }
                break;
        }
    }
}
