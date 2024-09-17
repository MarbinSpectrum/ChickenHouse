using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlourChicken : Mgr
{
    [SerializeField] private ChickenLerp_Shader lerpImg;
    [SerializeField] private Animation animation;
    [SerializeField] private TrayFlour2 trayFlour;
    [SerializeField] private RectTransform eventBox;

    public bool isEmpty;
    public bool isDrag;
    private float value = 0;
    private const float MAX_VALUE = 2.5f;

    public void SpawnChicken()
    {
        animation.Play("AddChicken2");
        isEmpty = false;
        lerpImg.gameObject.SetActive(true);
        isDrag = false;
        value = 0;
        lerpImg.SetValue(0);
    }

    public void RemoveChicken()
    {
        gameObject.SetActive(false);
        isEmpty = true;
        lerpImg.gameObject.SetActive(false);
        isDrag = false;
        lerpImg.SetValue(0);
    }

    public void OnMouseDrag()
    {
        if (gameMgr.playData.tutoComplete1 == false && tutoMgr.nowTuto != Tutorial.Tuto_6)
        {
            //튜토리얼이 아직 완료안된듯
            //혹시모르니 튜토리얼 타이밍때만 작동하도록 막아놓자
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
        if(IsComplete() == false)
        {
            //완료되지않음
            return;
        }

        kitchenMgr.dragState = DragState.Flour;
        lerpImg.gameObject.SetActive(false);
        isDrag = true;
    }

    public void ClickChicken(float v)
    {
        if (IsComplete())
        {
            //이미 밀가루 다 묻힘
            return;
        }
        value+=v;
        lerpImg.SetValue((float)value / MAX_VALUE);
    }



    public void OnMouseUp()
    {
        if (gameMgr.playData.tutoComplete1 == false && tutoMgr.nowTuto != Tutorial.Tuto_6)
        {
            //튜토리얼이 아직 완료안된듯
            //혹시모르니 튜토리얼 타이밍때만 작동하도록 막아놓자
            return;
        }

        //손을때면 치킨이 떨어짐
        KitchenMgr kitchenMgr = KitchenMgr.Instance;

        if (kitchenMgr.mouseArea == DragArea.Chicken_Strainter &&
            kitchenMgr.dragState == DragState.Flour)
        {
            //치킨 건지 쪽으로 치킨을 드래그함
            if (kitchenMgr.chickenStrainter.AddChicken())
            {
                RemoveChicken();
                trayFlour.RemoveChicken();
                kitchenMgr.dragState = DragState.None;
                return;
            }
        }

        //손을때면 치킨이 떨어짐
        kitchenMgr.dragState = DragState.None;
        lerpImg.gameObject.SetActive(true);
        isDrag = false;
    }

    public void SetRect(Vector2 pos, Vector2 size)
    {
        eventBox.anchoredPosition = pos;
        eventBox.sizeDelta = size;
    }

    public bool IsComplete()
    {
        //밀가루 묻히기 작업이 완료됬는지 여부를 반환
        return value >= MAX_VALUE;
    }

    public void WorkerFlourChickenPutAway()
    {
        if (isDrag)
            return;
        value = MAX_VALUE;
        lerpImg.SetValue(1);
        lerpImg.gameObject.SetActive(true);
    }
}
