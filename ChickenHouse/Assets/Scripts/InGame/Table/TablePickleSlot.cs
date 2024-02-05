using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TablePickleSlot : Mgr
{
    /** 피클이 올려졌는지 여부 **/
    private bool hasPickle;

    [SerializeField] private SpriteRenderer pickleImg;
    [SerializeField] private GameObject     slotUI;

    private void OnMouseDown()
    {
        //올려져있는 피클을 제거할때 사용
        if (hasPickle == false)
            return;
        hasPickle = false;
    }

    private void Update()
    {
        UpdatePickleSlot();
    }

    private void UpdatePickleSlot()
    {
        if (hasPickle)
        {
            //치킨무가 이미 놓여있음
            pickleImg.color = new Color(1, 1, 1, 1);
            slotUI.gameObject.SetActive(false);
            return;
        }
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if (kitchenMgr.dragState == DragState.Chicken_Pickle)
        {
            //치킨무를 놓을수있는 상태이긴하다.
            pickleImg.color = new Color(1, 1, 1, 0.5f);
            slotUI.gameObject.SetActive(true);
        }
        else
        {
            //치킨무를 밖으로 내보내면 이펙트 비활성화
            pickleImg.color = new Color(0, 0, 0, 0);
            slotUI.gameObject.SetActive(true);
        }
    }

    public bool Put_Pickle()
    {
        if (hasPickle)
        {
            //이미 치킨무가 놓임
            return false;
        }
        hasPickle = true;
        return true;
    }
}
