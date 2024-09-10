using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TableChickenSlot : Mgr
{
    public bool hasChicken { get; private set; }
    /** 치킨 상태 **/
    public ChickenState chickenState { get; private set; }

    /** 소스0 **/
    public ChickenSpicy source0 { get; private set; }
    /** 소스1 **/
    public ChickenSpicy source1 { get; private set; }

    private float chickenLerpValue;
    private bool chickenMode;

    [System.Serializable]
    public struct CHICKEN_OBJ
    {
        //오브젝트 스프라이트 이미지
        public Image[] chickenObj;
        public Image bottomSource;
    }
    [SerializeField] private CHICKEN_OBJ normalChicken;
    [SerializeField] private CHICKEN_OBJ spicyChicken;


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
    [SerializeField] private CHICKEN_SPRITE chickenSprite;

    [SerializeField] private Image          smoke;
    [SerializeField] private CanvasGroup    boxImg;
    [SerializeField] private GameObject     slotUI;
    [SerializeField] private ScrollObj[]    scrollObj;
    [SerializeField] private ChickenPackList chickenPackList;
    public void OnMouseEnter()
    {
        if (hasChicken)
            return;

        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if (kitchenMgr.dragState == DragState.Chicken_Pack)
        {
            //치킨을 놓을수있는 상태이긴하다.
            boxImg.alpha = 0.5f;
        }
        else
        {
            boxImg.alpha = 0;
        }
        kitchenMgr.chickenSlot = this;
        kitchenMgr.mouseArea = DragArea.Chicken_Slot;
    }

    public void OnMouseExit()
    {
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        kitchenMgr.mouseArea = DragArea.None;
        kitchenMgr.chickenSlot = null;

        if (hasChicken)
            return;

        chickenState = ChickenState.NotCook;
        source0 = ChickenSpicy.None;
        source1 = ChickenSpicy.None;
        boxImg.alpha = 0;
    }


    public void OnMouseDrag()
    {
        if (tutoMgr.tutoComplete == false)
        {
            //튜토리얼 중에는 드래그 불가능
            return;
        }

        if (hasChicken == false)
        {
            //치킨이 내부에 존재해야지 드래그가 가능
            return;
        }

        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if (kitchenMgr.cameraObj.lookArea != LookArea.Kitchen)
        {
            //주방을 보고있는 상태에서만 상호 작용 가능
            return;
        }

        kitchenMgr.dragState = DragState.Chicken_Pack_Holl;
        boxImg.alpha = 0;

        //버리기 버튼도 표시해준다.
        kitchenMgr.ui.takeOut.OpenBtn();
    }

    public void OnMouseUp()
    {
        if (tutoMgr.tutoComplete == false && tutoMgr.nowTuto != Tutorial.Tuto_12)
        {
            //튜토리얼 중에는 드래그 불가능
            return;
        }

        KitchenMgr kitchenMgr = KitchenMgr.Instance;

        if (kitchenMgr.dragState != DragState.Chicken_Pack_Holl)
        {
            //해당 오브젝트를 드래그중이라고 판단되었을때만 적용
            return;
        }

        //버리기 버튼 비활성
        kitchenMgr.ui.takeOut.CloseBtn();

        //손을때면 치킨 박스 떨어짐
        kitchenMgr.dragState = DragState.None;

        boxImg.alpha = 0;
        if (kitchenMgr.mouseArea == DragArea.Trash_Btn)
        {
            //버리기 버튼처리
            Init();
            return;
        }
        else if (kitchenMgr.mouseArea == DragArea.Chicken_Pack)
        {
            if (kitchenMgr.chickenPack.PackCkicken(4, chickenState,source0, source1))
            {
                kitchenMgr.chickenPack.Set_ChickenShader(chickenMode, chickenLerpValue);
                Init();
                return;
            }
            else if (chickenPackList.AddChickenPack(4, chickenState, source0, source1, chickenMode, chickenLerpValue))
            {
                Init();
                return;
            }
        }

        boxImg.alpha = 1;
    }

    public bool Put_ChickenPack(ChickenState pChickenState, ChickenSpicy pSource0, ChickenSpicy pSource1)
    {
        if (hasChicken)
        {
            //이미 치킨이 놓임
            return false;
        }

        boxImg.alpha = 1;

        soundMgr.PlaySE(Sound.Put_SE);
        hasChicken = true;
        chickenState = pChickenState;
        source0 = pSource0;
        source1 = pSource1;

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


        UpdatePack();

        return true;
    }

    public void UpdatePack()
    {
        //치킨을 표시
        System.Array.ForEach(normalChicken.chickenObj, (x) => x.gameObject.SetActive(false));
        System.Array.ForEach(spicyChicken.chickenObj, (x) => x.gameObject.SetActive(false));

        for (int i = 0; i < 4; i++)
        {
            ChickenSpicy chickenSpicy = ChickenSpicy.None;
            if (i < 2)
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

        if (hasChicken)
            boxImg.alpha = 1;
        else
            boxImg.alpha = 0;
    }

    public void Set_ChickenShader(bool pMode, float pLerpValue)
    {
        //치킨 갯수만큼만 치킨을 보여주자.
        for (int i = 0; i < normalChicken.chickenObj.Length; i++)
        {
            bool actChicken = (i < 4);
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
        hasChicken = false;
        chickenState = ChickenState.NotCook;
        source0 = ChickenSpicy.None;
        source1 = ChickenSpicy.None;

        UpdatePack();
    }
}
