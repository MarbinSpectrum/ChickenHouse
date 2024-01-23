using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenSource : MonoBehaviour
{
    [SerializeField] private GameObject obj;

    private void OnMouseDrag()
    {
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if (kitchenMgr.dragState != DragState.None)
        {
            //빈손인 상태에서 드래그해야됨
            return;
        }
        kitchenMgr.dragState = DragState.Chicken_Source;
        obj.gameObject.SetActive(false);
    }

    private void OnMouseUp()
    {
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if (kitchenMgr.dragState != DragState.Chicken_Source)
        {
            //해당 오브젝트를 드래그중이라고 판단되었을때만 적용
            return;
        }

        //손을때면 치킨소스가 떨어짐
        kitchenMgr.dragState = DragState.None;
        if (kitchenMgr.mouseArea == DragArea.Chicken_Pack)
        {
            //치킨 양념 넣기
            if (kitchenMgr.chickenPack.AddChickenSource())
            {
                kitchenMgr.chickenPack.Show_Chicken(true);
            }
        }

        obj.gameObject.SetActive(true);
    }
}
