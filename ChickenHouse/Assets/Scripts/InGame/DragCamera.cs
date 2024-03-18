using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragCamera : Mgr
{
    //카메라 드래그

    [SerializeField] private float speed = 0.25f;
    [SerializeField] private float dragDis = 150f;

    private Vector2 prevPos;

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
            if (!(prevPos.x - nowPos.x < 0 && nowPos.x >= Screen.width - dragDis)
                && !(prevPos.x - nowPos.x > 0 && nowPos.x <= dragDis))
                return;
            movePos = (Vector3)(prevPos - nowPos).normalized * Time.deltaTime * speed;
            movePos = new Vector3(movePos.x, 0, 0);
        }

        kitchenMgr.SetkitchenSetPos(movePos);
    }
}
