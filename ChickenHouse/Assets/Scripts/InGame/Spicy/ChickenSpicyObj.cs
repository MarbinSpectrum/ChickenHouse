using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChickenSpicyObj : Mgr
{
    [SerializeField] private Image      spicyImg;
    private ChickenSpicy    eChickenSpicy;
    private DragState       eDragState;

    public void SetObj(ChickenSpicy pChickenSpicy)
    {
        eChickenSpicy = pChickenSpicy;
        eDragState = SpicyMgr.GetSpicyDragState(eChickenSpicy);

        SpicyData spicyData = spicyMgr.GetSpicyData(eChickenSpicy);
        spicyImg.sprite = spicyData.img;
    }

    public void OnMouseDrag()
    {
        if (gameMgr.playData.tutoComplete1 == false)
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
        kitchenMgr.dragState = eDragState;

        spicyImg.color = new Color(0, 0, 0, 0);
    }

    public void OnMouseUp()
    {
        if (gameMgr.playData.tutoComplete1 == false)
        {
            //튜토리얼이 아직 완료안된듯 막아놓자
            return;
        }

        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if (kitchenMgr.dragState != eDragState)
        {
            //해당 오브젝트를 드래그중이라고 판단되었을때만 적용
            return;
        }

        //손을때면 치킨소스가 떨어짐
        kitchenMgr.dragState = DragState.None;

        if (kitchenMgr.mouseArea == DragArea.Chicken_Pack)
        {
            //치킨 양념 넣기
            if (kitchenMgr.chickenPack.AddChickenSource(eChickenSpicy))
            {
                kitchenMgr.chickenPack.UpdatePack();
            }
        }

        spicyImg.color = new Color(1, 1, 1, 1);
    }
}
