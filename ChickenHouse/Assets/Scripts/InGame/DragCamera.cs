using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragCamera : Mgr
{
    //카메라 드래그

    private enum CameraPos
    {
        ChickenZone,
        OilZone,
        TableZone,
    }

    private CameraPos cameraPos = CameraPos.ChickenZone;

    [SerializeField] private float speed = 0.25f;
    [SerializeField] private float autoMoveSpeed = 0.25f;
    [SerializeField] private float dragDis = 150f;
    [SerializeField] private float lerpMoveValue = 0.25f;
    [SerializeField] private Dictionary<DragZone, DRAG_OUTLINE> dragOutline;
    [SerializeField] private bool runAutoMove;

    [System.Serializable]
    public struct DRAG_OUTLINE
    {
        public RectTransform head;
        public RectTransform tail;
    }


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
    [SerializeField] private DRAG_TARGET    dragTarget;

    private bool autoDragMode   = false;
    private bool moveCamera     = false;

    private void Update()
    {
        KitchenMgr kitchenMgr = KitchenMgr.Instance;

        if (CheckMode.IsWindow() && kitchenMgr.cameraObj.lookArea == LookArea.Kitchen 
            && kitchenMgr.cameraObj.runAni == false)
        {
            CameraKeyBoardControl();
            return;
        }

        bool dragMove = false;

        if (kitchenMgr.dragState == DragState.None)
        {
            prevPos = Input.mousePosition;  
        }
        else if(kitchenMgr.cameraObj.lookArea == LookArea.Kitchen)
        {
            if (runAutoMove)
            {
                if (AutoMoveCamera() == false && autoDragMode == false)
                    dragMove = DragMoveCamera();
            }
            else
            {
                dragMove = DragMoveCamera();
            }
        }

        if (dragMove)
            return;

        if (kitchenMgr.cameraObj.lookArea != LookArea.Kitchen)
            return;
    }

    private void CameraKeyBoardControl()
    {
        if (gameMgr.stopGame)
            return;

        if (tutoMgr.runTutoFlag)
            return;

        if (moveCamera)
            return;



        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        RectTransform kitchenRect = kitchenMgr.KitchenContent();
        int actOilCnt = kitchenMgr.GetActiveOilZoneCnt();

        if (actOilCnt <= 1)
        {
            if(kitchenRect.anchoredPosition.x > -kitchenRect.sizeDelta.x)
                cameraPos = CameraPos.ChickenZone;
            else
                cameraPos = CameraPos.TableZone;
        }
        if (actOilCnt == 2)
        {
            if (kitchenRect.anchoredPosition.x > -50)
                cameraPos = CameraPos.ChickenZone;
            else
                cameraPos = CameraPos.TableZone;
        }
        else
        {
            if (kitchenRect.anchoredPosition.x > -50)
                cameraPos = CameraPos.ChickenZone;
            else if (kitchenRect.anchoredPosition.x > -kitchenRect.sizeDelta.x)
                cameraPos = CameraPos.OilZone;
            else
                cameraPos = CameraPos.TableZone;
        }

        if (Input.GetKeyDown(KeyMgr.GetKeyCode(KeyBoardValue.RIGHT)))
        {
            switch(cameraPos)
            {
                case CameraPos.ChickenZone:
                    if(actOilCnt <= 1)
                    {
                        moveCamera = true;
                        StartCoroutine(CameraMoveCor(-kitchenRect.sizeDelta.x));
                    }
                    else if (actOilCnt == 2)
                    {
                        moveCamera = true;
                        StartCoroutine(CameraMoveCor(-50));
                    }
                    else 
                    {
                        moveCamera = true;
                        StartCoroutine(CameraMoveCor(-50));
                    }
                    break;
                case CameraPos.OilZone:
                    moveCamera = true;
                    StartCoroutine(CameraMoveCor(-kitchenRect.sizeDelta.x));
                    break;
            }
        }
        else if (Input.GetKeyDown(KeyMgr.GetKeyCode(KeyBoardValue.LEFT)))
        {
            switch (cameraPos)
            {
                case CameraPos.OilZone:
                    moveCamera = true;
                    StartCoroutine(CameraMoveCor(0));
                    break;
                case CameraPos.TableZone:
                    if (actOilCnt <= 2)
                    {
                        moveCamera = true;
                        StartCoroutine(CameraMoveCor(0));
                    }
                    else
                    {
                        moveCamera = true;
                        StartCoroutine(CameraMoveCor(-50));
                    }
                    break;
            }
        }
    }

    IEnumerator CameraMoveCor(float pPosX)
    {
        DRAG_OUTLINE outline = GetOutline();
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        RectTransform kitchenRect = kitchenMgr.KitchenContent();

        float dic = (kitchenRect.anchoredPosition.x < pPosX) ? 1 : -1;
        float dis = Mathf.Abs(kitchenRect.anchoredPosition.x - pPosX);
        while (dis > 0)
        {
            float moveValue = dic * Time.deltaTime * autoMoveSpeed;
            dis -= Mathf.Abs(moveValue);
            if(dis > 0)
                kitchenMgr.SetkitchenSetPos(new Vector2(kitchenRect.offsetMin.x + moveValue, 0));
            else
                kitchenMgr.SetkitchenSetPos(new Vector2(pPosX, 0));
            yield return null;
        }

        moveCamera = false;
    }


    private bool AutoMoveCamera()
    {
        //카메라가 임의의 위치에 있어야하는 경우가있어서 여기서 처리하도록 시킴
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        DRAG_OUTLINE outline = GetOutline();
        RectTransform kitchenRect = kitchenMgr.KitchenContent();

        if (kitchenMgr.dragState == DragState.None)
            autoDragMode = false;

        if (kitchenRect.offsetMin.x < outline.head.offsetMin.x)
        {
            float moveValue = 1 * Time.deltaTime * autoMoveSpeed;
            if(kitchenRect.offsetMin.x + moveValue >= outline.head.offsetMin.x)
                kitchenMgr.SetkitchenSetPos(new Vector2(outline.head.offsetMin.x, kitchenRect.offsetMin.y));
            else
                kitchenMgr.AddkitchenSetPos(moveValue, outline);

            autoDragMode = true;
            return true;
        }
        else if(kitchenRect.offsetMin.x > outline.tail.offsetMin.x)
        {
            float moveValue = -1 * Time.deltaTime * autoMoveSpeed;

            if (kitchenRect.offsetMin.x <= outline.tail.offsetMin.x)
                kitchenMgr.SetkitchenSetPos(new Vector2(outline.tail.offsetMin.x, kitchenRect.offsetMin.y));
            else
                kitchenMgr.AddkitchenSetPos(moveValue, outline);
            autoDragMode = true;
            return true;
        }
        return false;
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
        DRAG_OUTLINE outline = GetOutline();
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

            if (!(prevPos.x - nowPos.x < 0 && nowPos.x >= Screen.width - newDragDis)
                && !(prevPos.x - nowPos.x > 0 && nowPos.x <= newDragDis))
                return false;

            int newDic = 0;
            if (prevPos.x - nowPos.x < 0 && nowPos.x >= Screen.width - newDragDis)
                newDic = -1;
            else if (prevPos.x - nowPos.x > 0 && nowPos.x <= newDragDis)
                newDic = +1;
            else
                return false;

            float moveValue = newDic * Time.deltaTime * speed;
            kitchenMgr.AddkitchenSetPos(moveValue, outline);
        }
        return false;
    }

    private DRAG_OUTLINE GetOutline()
    { 
        //드대그 범위 처리(해당범위 넘어서는 드래그 안되는 처리임)
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        switch (kitchenMgr.dragState)
        {
            case DragState.Normal:
            case DragState.Egg:
            case DragState.Flour:
                return dragOutline[DragZone.KitchenTable];
            case DragState.Chicken_Strainter:
            case DragState.Fry_Chicken:
                return dragOutline[DragZone.OilZone];
                //양념
            case DragState.Hot_Spicy:
            case DragState.Soy_Spicy:
            case DragState.Hell_Spicy:
            case DragState.Prinkle_Spicy:
            case DragState.Carbonara_Spicy:
            case DragState.BBQ_Spicy:
                //음료
            case DragState.Cola:
            case DragState.Beer:
            case DragState.SuperPower:
            case DragState.LoveMelon:
            case DragState.SodaSoda:
                //사이드메뉴
            case DragState.Chicken_Radish:
            case DragState.Pickle:
            case DragState.Coleslaw:
            case DragState.CornSalad:
            case DragState.FrenchFries:
            case DragState.ChickenNugget:
                //기타
            case DragState.Chicken_Pack_Holl:
            case DragState.Chicken_Pack:
                return dragOutline[DragZone.SpicyTable];
            default:
                return dragOutline[DragZone.None];
        }
    }
}
