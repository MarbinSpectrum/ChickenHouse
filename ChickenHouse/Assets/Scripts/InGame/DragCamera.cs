using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragCamera : Mgr
{
    //카메라 드래그

    [SerializeField] private float Speed = 0.25f;
    [SerializeField] private Transform leftTrans;
    [SerializeField] private Transform rightTrans;

    private Vector2 nowPos, prePos;
    private Vector3 movePos;

    public void ViewMoving()
    {
        KitchenMgr kitchenMgr = KitchenMgr.Instance;

        if (kitchenMgr.dragState != DragState.None)
            return;

        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    prePos = touch.position - touch.deltaPosition;
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    nowPos = touch.position - touch.deltaPosition;
                    movePos = (Vector3)(prePos - nowPos) * Time.deltaTime * Speed;
                    movePos = new Vector3(movePos.x, Camera.main.transform.position.y, movePos.z);
                    Camera.main.transform.Translate(movePos);

                    Vector3 cPos = Camera.main.transform.position;
                    float moveX = cPos.x;
                    moveX = Mathf.Min(moveX, rightTrans.position.x);
                    moveX = Mathf.Max(moveX, leftTrans.position.x);
                    cPos = new Vector3(moveX, cPos.y, cPos.z);

                    Camera.main.transform.position = cPos;

                    prePos = touch.position - touch.deltaPosition;
                }
            }
            else if (Input.touchCount == 2)
            {
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);

                if (touchZero.phase == TouchPhase.Began || touchOne.phase == TouchPhase.Began)//터치 시작하면
                {
                    prePos = ((touchZero.position + touchOne.position) / 2) - ((touchZero.deltaPosition + touchOne.deltaPosition) / 2);
                }

                else if (touchZero.phase == TouchPhase.Moved || touchOne.phase == TouchPhase.Moved)//드래그 중이면
                {
                    nowPos = ((touchZero.position + touchOne.position) / 2) - ((touchZero.deltaPosition + touchOne.deltaPosition) / 2);
                    movePos = (Vector3)(prePos - nowPos);
                    movePos = new Vector3(movePos.x, Camera.main.transform.position.y, movePos.z);
                    Camera.main.transform.Translate(movePos * Time.deltaTime * Speed);

                    Vector3 cPos = Camera.main.transform.position;
                    float moveX = cPos.x;
                    moveX = Mathf.Min(moveX, rightTrans.position.x);
                    moveX = Mathf.Max(moveX, leftTrans.position.x);
                    cPos = new Vector3(moveX, cPos.y, cPos.z);

                    Camera.main.transform.position = cPos;

                    prePos = ((touchZero.position + touchOne.position) / 2) - ((touchZero.deltaPosition + touchOne.deltaPosition) / 2);
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
                movePos = (Vector3)(prePos - nowPos) * Time.deltaTime * Speed;
                movePos = new Vector3(movePos.x, Camera.main.transform.position.y, movePos.z);
                Camera.main.transform.Translate(movePos);

                Vector3 cPos = Camera.main.transform.position;
                float moveX = cPos.x;
                moveX = Mathf.Min(moveX, rightTrans.position.x);
                moveX = Mathf.Max(moveX, leftTrans.position.x);
                cPos = new Vector3(moveX, cPos.y, cPos.z);

                Camera.main.transform.position = cPos;

                prePos = Input.mousePosition;
            }
        }
    }
}
