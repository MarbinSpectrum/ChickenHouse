using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragCamera : Mgr
{
    //카메라 드래그

    [SerializeField] private float speed = 0.25f;
    [SerializeField] private float dragDis = 150f;
    [SerializeField] private float slowDis = 150f;
    [SerializeField, Range(0, 1)] private float minSpeedRate;
    [SerializeField, Range(0, 1)] private float minSpeedLerpValue;

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
        if (kitchenMgr.dragState == DragState.None)
        {
            prevPos = Input.mousePosition;
     
        }
        else
        {
            MoveCamera();
        }
    }

    public void SetInitPos()
    {
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        kitchenMgr.SetkitchenSetPos(new Vector2(0, 0));
    }

    private void MoveCamera()
    {
        KitchenMgr  kitchenMgr      = KitchenMgr.Instance;
        Vector3     movePos         = Vector3.zero;
        Vector2     nowPos          = Input.mousePosition;

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
                float sideDis = ((float)Screen.width - sW)/2f;
                float sDragDis = (sW / (float)Screen.width) * dragDis;
                newDragDis = sideDis + sDragDis;
                newSpeedRate = (newDragDis / dragDis) * speed;
            }
            else
            {
                newDragDis = ((float)Screen.width / SafeArea.SCREEN_WIDTH) * dragDis;
                newSpeedRate = ((float)Screen.width / SafeArea.SCREEN_WIDTH) * speed;
            }

            if (!(prevPos.x - nowPos.x < 0 && nowPos.x >= Screen.width - newDragDis)
                && !(prevPos.x - nowPos.x > 0 && nowPos.x <= newDragDis))
                return;
            List<Transform> targetList = new List<Transform>();
            Transform moveTrans = null;
            Vector3 nowWorldPos = Camera.main.ScreenToWorldPoint(nowPos);

            switch (kitchenMgr.dragState)
            {
                case DragState.Normal:
                    targetList.Add(dragTarget.trayEgg);
                    break;
                case DragState.Egg:
                    targetList.Add(dragTarget.trayFlour);
                    break;
                case DragState.Flour:
                    targetList.Add(dragTarget.strainter);
                    break;
                case DragState.Chicken_Strainter:
                    if (dragTarget.oilZone1.IsRun() == false)
                        targetList.Add(dragTarget.oilZone1.transform);
                    if (dragTarget.oilZone2.IsRun() == false)
                        targetList.Add(dragTarget.oilZone2.transform);
                    if (dragTarget.oilZone3.IsRun() == false)
                        targetList.Add(dragTarget.oilZone3.transform);
                    if (dragTarget.oilZone4.IsRun() == false)
                        targetList.Add(dragTarget.oilZone4.transform);
                    break;
                case DragState.Fry_Chicken:
                    //if (dragTarget.oilZone1.IsRun() == false)
                    //    targetList.Add(dragTarget.oilZone1.transform);
                    //if (dragTarget.oilZone2.IsRun() == false)
                    //    targetList.Add(dragTarget.oilZone2.transform);
                    //if (dragTarget.oilZone3.IsRun() == false)
                    //    targetList.Add(dragTarget.oilZone3.transform);
                    //if (dragTarget.oilZone4.IsRun() == false)
                    //    targetList.Add(dragTarget.oilZone4.transform);
                    //targetList.Add(dragTarget.chickenPack);
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
                    targetList.Add(dragTarget.sideSlot);
                    break;

            }

            float minDis = int.MaxValue;
            foreach (Transform trans in targetList)
            {
                if (trans.gameObject.activeSelf == false)
                    continue;
                float dis = Vector3.Distance(trans.position, nowWorldPos);
                if(dis < minDis)
                {
                    //가장가까운놈을 체크
                    minDis = dis;
                    moveTrans = trans;
                }
            }

            float moveRate = 1;
            if (moveTrans != null)
            {
                moveRate = Vector3.Distance(moveTrans.position, nowWorldPos) / slowDis;
                moveRate = Mathf.Max(minSpeedRate, moveRate);
                moveRate = Mathf.Min(1, moveRate);

                float lerpValue = Mathf.Lerp(kitchenMgr.kitchenRect.content.offsetMin.x,
                    kitchenMgr.kitchenRect.content.offsetMin.x - moveTrans.position.x, minSpeedLerpValue * moveRate);
                movePos = new Vector3(lerpValue, 0, 0);
            }
            else
            {
                Vector3 newDic = new Vector3(prevPos.x - nowPos.x, 0, 0);
                movePos = newDic.normalized * Time.deltaTime * speed * moveRate;
                movePos = new Vector3(movePos.x + kitchenMgr.kitchenRect.content.offsetMin.x, 0, 0);
            }

            kitchenMgr.SetkitchenSetPos(movePos);
        }
    }
}
