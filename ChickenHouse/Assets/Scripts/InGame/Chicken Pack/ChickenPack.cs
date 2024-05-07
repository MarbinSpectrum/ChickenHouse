using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChickenPack : Mgr
{
    /** 담겨있는 치킨 갯수 **/
    public int chickenCnt { get; private set; }
    /** 치킨 상태 **/
    public ChickenState chickenState { get; private set; }

    /** 소스0 **/
    public ChickenSpicy source0 { get; private set; }
    /** 소스1 **/
    public ChickenSpicy source1 { get; private set; }

    private float   chickenLerpValue;
    private bool    chickenMode;

    [System.Serializable]
    public struct SPITE_IMG
    {
        //오브젝트 스프라이트 이미지
        public Sprite           normalSprite;
        public Sprite           canUseSprite;
    }

    [System.Serializable]
    public struct CHICKEN_SPRITE
    {
        //오브젝트 스프라이트 이미지
        public Sprite normalChicken;
        public Sprite hotChicken;
        public Sprite soyChicken;
        public Sprite hellChicken;
        public Sprite prinkleChicken;
        public Sprite carbonaraChicken;
        public Sprite bbqChicken;
    }

    [System.Serializable]
    public struct CHICKEN_OBJ
    {
        //오브젝트 스프라이트 이미지
        public Image[]              chickenObj;
        public Image                bottomSource;
    }

    [SerializeField] private SPITE_IMG          sprite;
    [SerializeField] private CHICKEN_SPRITE     chickenSprite;

    [SerializeField] private Image              body;

    [SerializeField] private CHICKEN_OBJ        normalChicken;
    [SerializeField] private CHICKEN_OBJ        spicyChicken;

    [SerializeField] private Image              smoke;
    [SerializeField] private ScrollObj[]        scrollObj;
    [SerializeField] private GameObject         obj;
    [SerializeField] private TutoObj            tutoObj;

    public void OnMouseEnter()
    {
        KitchenMgr kitchenMgr = KitchenMgr.Instance;

        if (kitchenMgr.dragState == DragState.Fry_Chicken && chickenCnt <= 0)
        {
            //치킨이 포장되어있지 않음
            //해당 용기를 사용 가능하다.
            body.sprite = sprite.canUseSprite;
        }
        else if ((kitchenMgr.dragState == DragState.Hot_Spicy || kitchenMgr.dragState == DragState.Soy_Spicy
            || kitchenMgr.dragState == DragState.Hell_Spicy || kitchenMgr.dragState == DragState.Prinkle_Spicy
             || kitchenMgr.dragState == DragState.Carbonara_Spicy || kitchenMgr.dragState == DragState.BBQ_Spicy)
            && chickenCnt > 0 && (source0 == ChickenSpicy.None || source1 == ChickenSpicy.None))
        {
            //치킨이 들어있음
            //치킨 소스 사용 가능
            body.sprite = sprite.canUseSprite;
        }
        else if (kitchenMgr.dragState == DragState.Chicken_Pack_Holl && chickenCnt <= 0)
        {
            //치킨이 포장되어있지 않음
            //해당 용기를 사용 가능하다.
            body.sprite = sprite.canUseSprite;
        }
        else
        {
            //치킨 포장박스를 사용 중이다.
            body.sprite = sprite.normalSprite;
        }

        kitchenMgr.chickenPack = this;
        kitchenMgr.mouseArea = DragArea.Chicken_Pack;
    }

    public void OnMouseExit()
    {
        body.sprite = sprite.normalSprite;

        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        kitchenMgr.chickenPack = null;
        kitchenMgr.mouseArea = DragArea.None;
    }


    public void OnMouseDrag()
    {
        if (tutoMgr.tutoComplete == false && tutoMgr.nowTuto != Tutorial.Tuto_7)
        {
            //튜토리얼 중에는 드래그 불가능
            return;
        }

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
        if (tutoMgr.tutoComplete)
            kitchenMgr.ui.takeOut.OpenBtn();
    }

    public void OnMouseUp()
    {
        if (tutoMgr.tutoComplete == false && tutoMgr.nowTuto != Tutorial.Tuto_7)
        {
            //튜토리얼 중에는 드래그 불가능
            return;
        }

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

        if (kitchenMgr.mouseArea == DragArea.Trash_Btn && tutoMgr.tutoComplete)
        {
            //버리기 버튼처리
            Init();

            //카운터 버튼이 비활성화됨
            kitchenMgr.ui.goCounter.CloseBtn();
            return;
        }

        if (kitchenMgr.mouseArea == DragArea.Chicken_Slot)
        {
            //치킨을 올려놓는다.
            if (kitchenMgr.chickenSlot.Put_ChickenPack(chickenState, source0, source1))
            {
                Init();

                kitchenMgr.chickenSlot.Set_ChickenShader(chickenMode, chickenLerpValue);
                kitchenMgr.chickenSlot.UpdatePack();

                if (tutoMgr.tutoComplete == false && tutoMgr.nowTuto == Tutorial.Tuto_7)
                {
                    tutoObj.PlayTuto();
                }

                return;
            }
        }

        obj.gameObject.SetActive(true);
    }

    public bool PackCkicken(int pChickenCnt, ChickenState pChickenState, ChickenSpicy spicy0, ChickenSpicy spicy1)
    {
        if (chickenCnt > 0)
        {
            //이미 치킨이 담겨있다.
            return false;
        }

        //치킨을 담는데 성공
        chickenCnt = pChickenCnt;
        chickenState = pChickenState;
        source0 = spicy0;
        source1 = spicy1;
        soundMgr.PlaySE(Sound.Put_SE);

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

        foreach (ScrollObj sObj in scrollObj)
        {
            sObj.isRun = false;
        }

        body.sprite = sprite.normalSprite;

        UpdatePack();

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
            soundMgr.PlaySE(Sound.Put_SE);
            source0 = spicy;

            body.sprite = sprite.normalSprite;

            return true;
        }
        if (source1 == ChickenSpicy.None)
        {
            soundMgr.PlaySE(Sound.Put_SE);
            source1 = spicy;

            body.sprite = sprite.normalSprite;

            return true;
        }

        return false;
    }

    public void UpdatePack()
    {
        //치킨을 표시
        System.Array.ForEach(normalChicken.chickenObj, (x) => x.gameObject.SetActive(false));
        System.Array.ForEach(spicyChicken.chickenObj, (x) => x.gameObject.SetActive(false));

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
                    spicyChicken.bottomSource.gameObject.SetActive(true);
                    spicyChicken.chickenObj[i].gameObject.SetActive(true);
                    spicyChicken.chickenObj[i].sprite = chickenSprite.hotChicken;
                    break;
                case ChickenSpicy.Soy:
                    spicyChicken.bottomSource.gameObject.SetActive(true);
                    spicyChicken.chickenObj[i].gameObject.SetActive(true);
                    spicyChicken.chickenObj[i].sprite = chickenSprite.soyChicken;
                    break;
                case ChickenSpicy.Hell:
                    spicyChicken.bottomSource.gameObject.SetActive(true);
                    spicyChicken.chickenObj[i].gameObject.SetActive(true);
                    spicyChicken.chickenObj[i].sprite = chickenSprite.hellChicken;
                    break;
                case ChickenSpicy.Prinkle:
                    spicyChicken.bottomSource.gameObject.SetActive(true);
                    spicyChicken.chickenObj[i].gameObject.SetActive(true);
                    spicyChicken.chickenObj[i].sprite = chickenSprite.prinkleChicken;
                    break;
                case ChickenSpicy.Carbonara:
                    spicyChicken.bottomSource.gameObject.SetActive(true);
                    spicyChicken.chickenObj[i].gameObject.SetActive(true);
                    spicyChicken.chickenObj[i].sprite = chickenSprite.carbonaraChicken;
                    break;
                case ChickenSpicy.BBQ:
                    spicyChicken.bottomSource.gameObject.SetActive(true);
                    spicyChicken.chickenObj[i].gameObject.SetActive(true);
                    spicyChicken.chickenObj[i].sprite = chickenSprite.bbqChicken;
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
        chickenMode = pMode;
        chickenLerpValue = pLerpValue;
    }

    public void Init()
    {
        //초기화 함
        chickenCnt = 0;
        source0 = ChickenSpicy.None;
        source1 = ChickenSpicy.None;
        System.Array.ForEach(normalChicken.chickenObj, (x) => x.gameObject.SetActive(false));
        System.Array.ForEach(spicyChicken.chickenObj, (x) => x.gameObject.SetActive(false));

        normalChicken.bottomSource.gameObject.SetActive(false);
        spicyChicken.bottomSource.gameObject.SetActive(false);

        foreach (ScrollObj sObj in scrollObj)
        {
            sObj.isRun = true;
        }

        smoke.gameObject.SetActive(false);
    }
}
