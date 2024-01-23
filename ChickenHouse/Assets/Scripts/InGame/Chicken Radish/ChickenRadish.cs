using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenRadish : MonoBehaviour
{
    private void OnMouseDrag()
    {
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        kitchenMgr.dragState = DragState.Chicken_Radish;
    }

    private void OnMouseUp()
    {
        KitchenMgr kitchenMgr = KitchenMgr.Instance;

        //�������� ġŲ ���� ������
        kitchenMgr.dragState = DragState.None;
        if (kitchenMgr.mouseArea == DragArea.Chicken_Pack)
        {
            //ġŲ �� �ֱ�
            if (kitchenMgr.chickenPack.AddChickenRadish())
            {

            }
        }
    } 
}
