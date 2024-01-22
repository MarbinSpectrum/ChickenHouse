using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragChickenStrainter : Mgr
{
    [SerializeField] private GameObject             strainterObj;
    [SerializeField] private Chicken_Shader[]       chickenObj;

    private void Update()
    {
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        switch (kitchenMgr.dragState)
        {
            case DragState.Fry_Chicken:
            case DragState.Chicken_Strainter:
                {
                    //해당 상태에서만 치킨이 드래그됨

                    //이미지 활성화
                    strainterObj.gameObject.SetActive(true);

                    //치킨이미지를 드래그 포인트로 이동
                    Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    transform.position = new Vector3(pos.x, pos.y, 0);
                }
                return;
            default:
                {
                    strainterObj.gameObject.SetActive(false);
                }
                break;
        }
    }

    public void DragStart(int pChickenCnt, DragState pDragState)
    {
        DragStart(pChickenCnt, pDragState, true, 0);
    }

    public void DragStart(int pChickenCnt, DragState pDragState, bool mode, float lerpValue)
    {
        for (int i = 0; i < chickenObj.Length; i++)
        {
            //치킨 갯수만큼만 치킨을 보여주자.
            bool actChicken = (i < pChickenCnt);
            chickenObj[i].gameObject.SetActive(actChicken);
            chickenObj[i].Set_Shader(mode, lerpValue);
        }

        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        kitchenMgr.dragState = pDragState;
    }
}
