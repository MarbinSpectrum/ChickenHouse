using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragCamera : Mgr
{
    //카메라 드래그

    [SerializeField] private float speed = 0.25f;
    [SerializeField] private float dragDis = 150f;
    [SerializeField] private ScrollRect scroll;

    private Vector2 prevPos;

    private void Update()
    {
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if (kitchenMgr == null)
        {
            scroll.enabled = false;
            return;
        }
        if (kitchenMgr.dragState == DragState.None)
        {
            prevPos = Input.mousePosition;
            scroll.enabled = true;  
        }
        else
        {
            MoveCamera();
            scroll.enabled = false;
        }
    }

    public void SetInitPos()
    {
        scroll.content.offsetMin = new Vector2(0, scroll.content.offsetMin.y);
        scroll.content.offsetMax = new Vector2(scroll.content.offsetMin.x + 51, scroll.content.offsetMax.y);
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

        scroll.content.transform.Translate(movePos);
        scroll.content.offsetMin = new Vector2(Mathf.Clamp(scroll.content.offsetMin.x, -51, 0), scroll.content.offsetMin.y);
        scroll.content.offsetMax = new Vector2(scroll.content.offsetMin.x + 51, scroll.content.offsetMax.y);
    }
}
