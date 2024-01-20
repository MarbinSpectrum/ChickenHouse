using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDrag : Mgr
{
    //�巡�� ���� ����

    private void OnMouseDrag()
    {
        //�巡�� ���� ����
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        kitchenMgr.dragState = DragState.DontDrag;
    }

    private void OnMouseUp()
    {
        //�巡�� ���� ����
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        kitchenMgr.dragState = DragState.None;
    }
}
