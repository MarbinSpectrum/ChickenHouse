using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragCamera : Mgr
{
    //카메라 드래그

    [SerializeField] private float Speed1 = 0.25f;
    [SerializeField] private float Speed2 = 0.25f;
    [SerializeField] private float dragDis = 150f;
    [SerializeField] private float sideArea = 200f;
    [SerializeField] private Transform leftTrans;
    [SerializeField] private Transform rightTrans;

    private Vector2 nowPos, prePos;
    private Vector3 movePos;

    private void Awake()
    {
        MinMaxPos();
    }

    public void ViewMoving()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);
                Vector2 calPos = touch.position - touch.deltaPosition;
                if (touch.phase == TouchPhase.Began)
                {
                    prePos = calPos;
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    nowPos = calPos;
                    MoveCamera(nowPos, calPos);
                }
            }
            else if (Input.touchCount == 2)
            {
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);
                Vector2 calPos = ((touchZero.position + touchOne.position) / 2) - ((touchZero.deltaPosition + touchOne.deltaPosition) / 2);
                if (touchZero.phase == TouchPhase.Began || touchOne.phase == TouchPhase.Began)//터치 시작하면
                {
                    prePos = calPos;
                }

                else if (touchZero.phase == TouchPhase.Moved || touchOne.phase == TouchPhase.Moved)//드래그 중이면
                {
                    nowPos = calPos;
                    MoveCamera(nowPos, calPos);
                }
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                prePos = Input.mousePosition;
            }
            else if (Input.GetMouseButton(0))
            {
                nowPos = Input.mousePosition;
                MoveCamera(nowPos, Input.mousePosition);
            }
        }
    }

    private void MoveCamera(Vector2 nowPos,Vector2 pPrevPos)
    {
        bool        updatePrePos    = false;
        KitchenMgr  kitchenMgr      = KitchenMgr.Instance;
        Vector3     movePos         = Vector3.zero;

        float rate = ((float)Screen.width / (float)(Screen.height)) * (SafeArea.SCREEN_WIDTH / SafeArea.SCREEN_HEIGHT);
        float fSpeed1   = rate * Speed1;
        float fSpeed2   = rate * Speed2;
        float fDrag     = rate * dragDis;
        if (kitchenMgr.dragState != DragState.None)
        {
            if (kitchenMgr.mouseArea == DragArea.Trash_Btn)
            {
                //쓰레기 버튼위에서는 이동이안됨
                return;
            }


            if (!(prePos.x - nowPos.x < 0 && nowPos.x >= Screen.width - fDrag)
                && !(prePos.x - nowPos.x > 0 && nowPos.x <= fDrag))
                return;

            movePos = (Vector3)(nowPos - prePos).normalized * Time.deltaTime * fSpeed1;
            movePos = new Vector3(movePos.x, 0, 0);
        }
        else
        {
            updatePrePos = true;
            movePos = (Vector3)(prePos - nowPos) * Time.deltaTime * fSpeed2;
            movePos = new Vector3(movePos.x, 0, 0);
        }
        Camera.main.transform.Translate(movePos);

        MinMaxPos();

        if (updatePrePos)
        {
            prePos = pPrevPos;
        }
    }

    private void MinMaxPos()
    {
        Vector3 cPos = Camera.main.transform.position;

        float height = Camera.main.orthographicSize;
        float width = height * SafeArea.SCREEN_WIDTH / SafeArea.SCREEN_HEIGHT;
        float clampX = Mathf.Clamp(Camera.main.transform.position.x, leftTrans.position.x + width, rightTrans.position.x - width);

        cPos = new Vector3(clampX, cPos.y, cPos.z);

        Camera.main.transform.position = cPos;
    }
}
