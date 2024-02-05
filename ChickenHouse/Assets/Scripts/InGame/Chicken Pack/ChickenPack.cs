using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenPack : Mgr
{
    /** 담겨있는 치킨 갯수 **/
    private int         chickenCnt;
    /** 치킨 상태 **/
    private ChickenState chickenState;

    /** 소스0 **/
    private ChickenSpicy source0;
    /** 소스1 **/
    private ChickenSpicy source1;

    [System.Serializable]
    public struct SPITE_IMG
    {
        //오브젝트 스프라이트 이미지
        public SpriteRenderer   spriteImg;
        public Sprite           normalSprite;
        public Sprite           canUseSprite;
    }

    [System.Serializable]
    public struct CHICKEN_OBJ
    {
        //오브젝트 스프라이트 이미지
        public GameObject[]         chickenObj;
        public SpriteRenderer       bottomSource;
    }

    [SerializeField] private SPITE_IMG          sprite;
    [SerializeField] private CHICKEN_OBJ        normalChicken;
    [SerializeField] private CHICKEN_OBJ        hotChicken;
    [SerializeField] private SpriteRenderer     smoke;
    [SerializeField] private GameObject         obj;

    private void OnMouseDrag()
    {
        if (chickenCnt == 0)
        {
            //치킨이 내부에 존재해야지 드래그가 가능
            return;
        }

        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        kitchenMgr.dragState = DragState.Chicken_Pack;
        if (kitchenMgr.cameraObj.lookArea != LookArea.Kitchen)
        {
            //주방을 보고있는 상태에서만 상호 작용 가능
            return;
        }

        obj.gameObject.SetActive(false);

        //버리기 버튼도 표시해준다.
        kitchenMgr.ui.takeOut.OpenBtn();
    }

    private void OnMouseUp()
    {
        KitchenMgr kitchenMgr = KitchenMgr.Instance;

        if (kitchenMgr.dragState != DragState.Chicken_Pack)
        {
            //해당 오브젝트를 드래그중이라고 판단되었을때만 적용
            return;
        }

        obj.gameObject.SetActive(true);

        //버리기 버튼 비활성
        kitchenMgr.ui.takeOut.CloseBtn();

        //손을때면 치킨 박스 떨어짐
        kitchenMgr.dragState = DragState.None;
        if (kitchenMgr.mouseArea == DragArea.Trash_Btn)
        {
            //버리기 버튼처리
            Init();
            return;
        }

        if (kitchenMgr.mouseArea == DragArea.Chicken_Slot)
        {
            //치킨을 올려놓는다.
            if (kitchenMgr.chickenSlot.Put_ChickenPack(chickenCnt, chickenState, source0, source1))
            {
                Init();

                kitchenMgr.ui.goCounter.OpenBtn();

                return;
            }
        }

        obj.gameObject.SetActive(true);
    }

    private void Update()
    {
        UpdateChickenPack();
    }

    private void UpdateChickenPack()
    {
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if (kitchenMgr.mouseArea == DragArea.Chicken_Pack &&
            (kitchenMgr.chickenPack != null && kitchenMgr.chickenPack == this))
        {
            if (kitchenMgr.dragState == DragState.Fry_Chicken 
                && chickenCnt <= 0)
            {
                //치킨이 포장되어있지 않음
                //해당 용기를 사용 가능하다.
                sprite.spriteImg.sprite = sprite.canUseSprite;
            }
            else if (kitchenMgr.dragState == DragState.Hot_Spicy
                && chickenCnt > 0 && (source0 == ChickenSpicy.None || source1 == ChickenSpicy.None))
            {
                //치킨이 들어있음
                //치킨 소스 사용 가능
                sprite.spriteImg.sprite = sprite.canUseSprite;
            }
            else
            {
                //치킨 포장박스를 사용 중이다.
                sprite.spriteImg.sprite = sprite.normalSprite;
            }
        }
        else
        {
            //마우스를 밖으로 내보내면 이펙트 비활성화
            sprite.spriteImg.sprite = sprite.normalSprite;
        }
    }

    public bool PackCkicken(int pChickenCnt, ChickenState pChickenState)
    {
        if (chickenCnt > 0)
        {
            //이미 치킨이 담겨있다.
            return false;
        }

        //치킨을 담는데 성공
        chickenCnt = pChickenCnt;
        chickenState = pChickenState;

        if (chickenState == ChickenState.GoodChicken || chickenState == ChickenState.BadChicken_1)
        {
            //좋은 치킨 or 조금 태운 치킨
            //하얀 스모크
            Color newColor = smoke.color;
            newColor.r = 1;
            newColor.g = 1;
            newColor.b = 1;
            smoke.color = newColor;
            smoke.gameObject.SetActive(true);
        }
        else if (chickenState == ChickenState.BadChicken_2)
        {
            //태운 치킨
            //검은 스모크
            Color newColor = smoke.color;
            newColor.r = 0;
            newColor.g = 0;
            newColor.b = 0;
            smoke.color = newColor;
            smoke.gameObject.SetActive(true);
        }

        return true;
    }

    public bool AddChickenSource(ChickenSpicy spicy)
    {
        if (chickenCnt <= 0)
        {
            //치킨이 없다.
            return false;
        }

        if (source0 != ChickenSpicy.None && source1 != ChickenSpicy.None)
        {
            //치킨이 소스가 이미 뿌려져있다.
            return false;
        }

        if(source0 == ChickenSpicy.None)
        {
            source0 = spicy;
            return true;
        }
        if (source1 == ChickenSpicy.None)
        {
            source1 = spicy;
            return true;
        }

        return false;
    }

    public void UpdatePack()
    {
        //치킨을 표시
        System.Array.ForEach(normalChicken.chickenObj, (x) => x.gameObject.SetActive(false));
        System.Array.ForEach(hotChicken.chickenObj, (x) => x.gameObject.SetActive(false));

        for (int i = 0; i < chickenCnt; i++)
        {
            ChickenSpicy chickenSpicy = ChickenSpicy.None;
            if (i < chickenCnt / 2)
            {
                chickenSpicy = source0;
            }
            else
            {
                chickenSpicy = source1;
            }

            switch (chickenSpicy)
            {
                case ChickenSpicy.None:
                    normalChicken.bottomSource.gameObject.SetActive(true);
                    normalChicken.chickenObj[i].gameObject.SetActive(true);
                    break;
                case ChickenSpicy.Hot:
                    hotChicken.bottomSource.gameObject.SetActive(true);
                    hotChicken.chickenObj[i].gameObject.SetActive(true);
                    break;
            }
        }
    }

    public void Set_ChickenShader(bool pMode, float pLerpValue)
    {
        //치킨 갯수만큼만 치킨을 보여주자.
        for (int i = 0; i < normalChicken.chickenObj.Length; i++)
        {
            bool actChicken = (i < chickenCnt);
            Chicken_Shader chickenShader = normalChicken.chickenObj[i].GetComponent<Chicken_Shader>();
            chickenShader.gameObject.SetActive(actChicken);
            chickenShader.Set_Shader(pMode, pLerpValue);
        }
    }

    public void Init()
    {
        //초기화 함
        chickenCnt = 0;
        source0 = ChickenSpicy.None;
        source1 = ChickenSpicy.None;
        System.Array.ForEach(normalChicken.chickenObj, (x) => x.gameObject.SetActive(false));
        System.Array.ForEach(hotChicken.chickenObj, (x) => x.gameObject.SetActive(false));
        normalChicken.bottomSource.gameObject.SetActive(false);
        hotChicken.bottomSource.gameObject.SetActive(false);

        smoke.gameObject.SetActive(false);
    }
}
