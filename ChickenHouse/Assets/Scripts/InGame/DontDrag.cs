using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDrag : MonoBehaviour
{
    //드래그 금지 구역

    private void OnMouseDrag()
    {
        //드래그 금지 설정
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        kitchenMgr.dragState = DragState.DontDrag;
    }

    private void OnMouseUp()
    {
        //드래그 금지 해제
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        kitchenMgr.dragState = DragState.None;
    }
}
