using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragCamera : Mgr
{
    //카메라 드래그

    [SerializeField] private float speed = 0.25f;
    [SerializeField] private float dragDis = 150f;
    [SerializeField] private float lerpMoveValue = 0.25f;
    private Vector2 prevPos;

    [System.Serializable] public struct DRAG_TARGET
    {
        public Transform trayEgg;
        public Transform trayFlour;
        public Transform strainter;

        public Oil_Zone oilZone1;
        public Oil_Zone oilZone2;
        public Oil_Zone oilZone3;
        public Oil_Zone oilZone4;

        public Transform chickenPack;

        public Transform chickenSlot;

        public Transform sideSlot;
    }
    [SerializeField] private DRAG_TARGET dragTarget;

    private void Update()
    {
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        bool dragMove = false;
        if (kitchenMgr.dragState == DragState.None)
        {
            prevPos = Input.mousePosition;  
        }
        else if(kitchenMgr.cameraObj.lookArea == LookArea.Kitchen)
        {
            dragMove = DragMoveCamera();
        }

        if (dragMove)
            return;

        if (kitchenMgr.cameraObj.lookArea != LookArea.Kitchen)
            return;

        if (CheckMode.IsWindow() && gameMgr.stopGame == false)
        {
            //PC버전에서는 A D 키로 좌우이동 가능
            Vector3 newDic = Vector3.zero;
            if (Input.GetKey(KeyCode.A))
                newDic = new Vector3(+1, 0, 0);
            else if (Input.GetKey(KeyCode.D))
                newDic = new Vector3(-1, 0, 0);
            else
                return;

            Vector3 movePos = newDic.normalized * Time.deltaTime * speed;
            Vector3 nowWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 prevWorld = nowWorldPos + movePos;
            MoveCamera(nowWorldPos, prevWorld);
        }
    }

    public void SetInitPos()
    {
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        kitchenMgr.SetkitchenSetPos(new Vector2(0, 0));
    }

    private bool DragMoveCamera()
    {
        KitchenMgr  kitchenMgr      = KitchenMgr.Instance;
        Vector2     nowPos          = Input.mousePosition;
        Vector3     nowWorldPos     = Camera.main.ScreenToWorldPoint(nowPos);
  

        if (kitchenMgr.dragState != DragState.None)
        {
            float newDragDis = dragDis;
            float defaultRate = (float)SafeArea.SCREEN_WIDTH / (float)SafeArea.SCREEN_HEIGHT;
            float nowRate = (float)Screen.width / (float)Screen.height;
            float newSpeedRate = speed;

            if (defaultRate <= nowRate)
            {
                float sH = (float)Screen.height;
                float sW = sH * defaultRate;
                float sideDis = ((float)Screen.width - sW) / 2f;
                float sDragDis = (sW / (float)Screen.width) * dragDis;
                newDragDis = sideDis + sDragDis;
                newSpeedRate = (newDragDis / dragDis) * speed;
            }
            else
            {
                newDragDis = ((float)Screen.width / SafeArea.SCREEN_WIDTH) * dragDis;
                newSpeedRate = ((float)Screen.width / SafeArea.SCREEN_WIDTH) * speed;
            }

            List<Transform> targetList = DragTargetList();
            if (targetList.Count == 0)
            {
                if (!(prevPos.x - nowPos.x < 0 && nowPos.x >= Screen.width - newDragDis)
                    && !(prevPos.x - nowPos.x > 0 && nowPos.x <= newDragDis))
                    return false;

                Vector3 newDic = Vector3.zero;
                if (prevPos.x - nowPos.x < 0 && nowPos.x >= Screen.width - newDragDis)
                    newDic = new Vector3(-1, 0, 0);
                else if (prevPos.x - nowPos.x > 0 && nowPos.x <= newDragDis)
                    newDic = new Vector3(+1, 0, 0);
                else
                    return false;
                Vector3 movePos = newDic.normalized * Time.deltaTime * speed;

                Vector3 prevWorld = nowWorldPos + movePos;
                return MoveCamera(nowWorldPos, prevWorld);
            }
            else
            {
                Vector3 newDic = Vector3.zero;
                if (prevPos.x < nowPos.x)
                    newDic = new Vector3(-1, 0, 0);
                else
                    newDic = new Vector3(+1, 0, 0);
                Vector3 movePos = newDic.normalized * Time.deltaTime * speed;

                Vector3 prevWorld = nowWorldPos + movePos;
                return MoveCamera(nowWorldPos, prevWorld);
            }
        }
        return false;
    }

    private List<Transform> DragTargetList()
    {
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        List<Transform> targetList = new List<Transform>();
        switch (kitchenMgr.dragState)
        {
            case DragState.Normal:
                //targetList.Add(dragTarget.trayEgg);
                break;
            case DragState.Egg:
                //targetList.Add(dragTarget.trayFlour);
                break;
            case DragState.Flour:
                //targetList.Add(dragTarget.strainter);
                break;
            case DragState.Chicken_Strainter:
                if (dragTarget.oilZone1.IsRun() == false && dragTarget.oilZone1.gameObject.activeSelf)
                    targetList.Add(dragTarget.oilZone1.transform);
                if (dragTarget.oilZone2.IsRun() == false && dragTarget.oilZone2.gameObject.activeSelf)
                    targetList.Add(dragTarget.oilZone2.transform);
                if (dragTarget.oilZone3.IsRun() == false && dragTarget.oilZone3.gameObject.activeSelf)
                    targetList.Add(dragTarget.oilZone3.transform);
                if (dragTarget.oilZone4.IsRun() == false && dragTarget.oilZone4.gameObject.activeSelf)
                    targetList.Add(dragTarget.oilZone4.transform);
                break;
            case DragState.Fry_Chicken:
                if ((dragTarget.oilZone1.IsRun() == false || (dragTarget.oilZone1.IsRun() && dragTarget.oilZone1.isHold)) && dragTarget.oilZone1.gameObject.activeSelf)
                    targetList.Add(dragTarget.oilZone1.transform);
                if ((dragTarget.oilZone2.IsRun() == false || (dragTarget.oilZone2.IsRun() && dragTarget.oilZone2.isHold)) && dragTarget.oilZone2.gameObject.activeSelf)
                    targetList.Add(dragTarget.oilZone2.transform);
                if ((dragTarget.oilZone3.IsRun() == false || (dragTarget.oilZone3.IsRun() && dragTarget.oilZone3.isHold)) && dragTarget.oilZone3.gameObject.activeSelf)
                    targetList.Add(dragTarget.oilZone3.transform);
                if ((dragTarget.oilZone4.IsRun() == false || (dragTarget.oilZone4.IsRun() && dragTarget.oilZone4.isHold)) && dragTarget.oilZone4.gameObject.activeSelf)
                    targetList.Add(dragTarget.oilZone4.transform);
                targetList.Add(dragTarget.chickenPack);
                break;
            case DragState.Hot_Spicy:
            case DragState.Soy_Spicy:
            case DragState.Hell_Spicy:
            case DragState.Prinkle_Spicy:
            case DragState.Carbonara_Spicy:
            case DragState.BBQ_Spicy:
            case DragState.Chicken_Pack_Holl:
                //targetList.Add(dragTarget.chickenPack);
                break;
            case DragState.Chicken_Pack:
                //targetList.Add(dragTarget.chickenSlot);
                break;
            case DragState.Cola:
            case DragState.Chicken_Pickle:
                //targetList.Add(dragTarget.sideSlot);
                break;

        }
        return targetList;
    }

    private bool MoveCamera(Vector3 nowWorldPos,Vector3 prevWorld)
    {
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        Vector3 movePos = Vector3.zero;
        List<Transform> targetList = DragTargetList();
        if (targetList.Count > 0)
        {
            Transform moveTrans = null;
            float minDis = int.MaxValue;
            bool checkMove = false;
            foreach (Transform trans in targetList)
            {
                if (trans.gameObject.activeSelf == false)
                    continue;
                float prevDis = Vector3.Distance(trans.position, prevWorld);
                float dis = Vector3.Distance(trans.position, nowWorldPos);
                if (dis < minDis && dis < prevDis)
                {
                    //가장가까운놈을 체크
                    minDis = dis;
                    moveTrans = trans;
                    checkMove = true;
                }
            }

            if (checkMove == false)
            {
                //목표랑 가까워지는 방향이 존재하지 않음
                Vector3 newDic = new Vector3(prevWorld.x - nowWorldPos.x, 0, 0);
                movePos = newDic.normalized * Time.deltaTime * speed;
                movePos = new Vector3(movePos.x + kitchenMgr.kitchenRect.content.offsetMin.x, 0, 0);
            }
            else
            {
                float dic = 0;
                if (moveTrans.position.x < nowWorldPos.x)
                    dic = +1;
                else
                    dic = -1;
                movePos = Vector2.right * dic * Time.deltaTime * speed * (Mathf.Abs(moveTrans.position.x - nowWorldPos.x));
                float lerpValue = Mathf.Lerp(kitchenMgr.kitchenRect.content.offsetMin.x, movePos.x + kitchenMgr.kitchenRect.content.offsetMin.x, lerpMoveValue);
                movePos = new Vector3(lerpValue, 0, 0);
            }
        }
        else
        {
            Vector3 newDic = new Vector3(prevWorld.x - nowWorldPos.x, 0, 0);
            movePos = newDic.normalized * Time.deltaTime * speed;
            movePos = new Vector3(movePos.x + kitchenMgr.kitchenRect.content.offsetMin.x, 0, 0);
        }

        kitchenMgr.SetkitchenSetPos(movePos);


        return true;
    }
}
