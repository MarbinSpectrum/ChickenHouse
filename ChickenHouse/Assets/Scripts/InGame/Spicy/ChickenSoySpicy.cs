using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenSoySpicy : Mgr
{
    [SerializeField] private GameObject obj;

    public void OnMouseDrag()
    {
        if (tutoMgr.tutoComplete == false)
        {
            //튜토리얼이 아직 완료안된듯 막아놓자
            return;
        }

        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if (kitchenMgr.dragState != DragState.None)
        {
            //빈손인 상태에서 드래그해야됨
            return;
        }
        if (kitchenMgr.cameraObj.lookArea != LookArea.Kitchen)
        {
            //주방을 보고있는 상태에서만 상호 작용 가능
            return;
        }
        kitchenMgr.dragState = DragState.Soy_Spicy;

        obj.gameObject.SetActive(false);
    }

    public void OnMouseUp()
    {
        if (tutoMgr.tutoComplete == false)
        {
            //튜토리얼이 아직 완료안된듯 막아놓자
            return;
        }

        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if (kitchenMgr.dragState != DragState.Soy_Spicy)
        {
            //해당 오브젝트를 드래그중이라고 판단되었을때만 적용
            return;
        }

        //손을때면 치킨소스가 떨어짐
        kitchenMgr.dragState = DragState.None;

        if (kitchenMgr.mouseArea == DragArea.Chicken_Pack)
        {
            //치킨 양념 넣기
            if (kitchenMgr.chickenPack.AddChickenSource(ChickenSpicy.Soy))
            {
                kitchenMgr.chickenPack.UpdatePack();
            }
        }

        obj.gameObject.SetActive(true);
    }
}
