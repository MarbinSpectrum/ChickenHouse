using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oil_Zone : Mgr
{
    [System.Serializable]
    public struct SPITE_IMG
    {
        //오브젝트 스프라이트 이미지
        public SpriteRenderer   spriteImg;
        public Sprite           normalSprite;
        public Sprite           canUseSprite;
    }
    [SerializeField] private SPITE_IMG sprite;

    [SerializeField] private List<SpriteRenderer>   notCookSprite;
    [SerializeField] private List<SpriteRenderer>   runCookSprite;
    [SerializeField] private Animator               animator;
    [SerializeField] private Oil_Zone_Shader        oilShader;

    /**닭 갯수 **/
    private int         chickenCnt;


        
    /** 치킨 요리 정도 **/
    private ChickenState chickenState = ChickenState.NotCook;
    /** 요리 일시 정지 여부 **/
    private bool        pauseCook;
    /** 요리 코루틴 **/
    private IEnumerator cookCor;

    private void OnMouseDrag()
    {
        if (chickenState == ChickenState.NotCook)
        {
            //조리 시작전이면 당연히 드래그안됨
            return;
        }

        if (pauseCook == false)
        {
            //요리 일시정지
            Cook_Pause(true);
        }

        //오일존에서 치킨건지를 꺼낸다. 드래그 시작
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        kitchenMgr.dragObj.DragStrainter(chickenCnt, DragState.Fry_Chicken, oilShader.Mode, oilShader.LerpValue);

        //버리기 버튼도 표시해준다.
        kitchenMgr.ui.takeOut.OpenBtn();

        notCookSprite.ForEach((x) => x.enabled = true);
        runCookSprite.ForEach((x) => x.enabled = false);
    }

    private void OnMouseUp()
    {
        KitchenMgr kitchenMgr = KitchenMgr.Instance;

        if(kitchenMgr.dragState != DragState.Fry_Chicken)
        {
            //튀긴치킨을 드래그 중일때 마우스를 놓은 경우만 체크한다.
            return;
        }

        //버리기 버튼 비활성
        kitchenMgr.ui.takeOut.CloseBtn();

        kitchenMgr.dragState = DragState.None;
        if(kitchenMgr.mouseArea == DragArea.Trash_Btn)
        {
            //버리기 버튼처리
            kitchenMgr.ui.takeOut.RunBtn();
            return;
        }
        else if(kitchenMgr.mouseArea == DragArea.Chicken_Pack)
        {
            //치킨통에 치킨 넣기
            if(kitchenMgr.chickenPack.PackCkicken(chickenCnt))
            {
                //치킨팩에 치킨 넣기
                kitchenMgr.chickenPack.Set_ChickenShader(oilShader.Mode, oilShader.LerpValue);
                kitchenMgr.chickenPack.Show_Chicken(false);

                //요리 종료
                Cook_Stop();

                //치킨 건지를 사용을 끝냈으니 초기화
                if(kitchenMgr.chickenStrainter != null)
                    kitchenMgr.chickenStrainter.Init();
                return;
            }
        }

        if (pauseCook)
        {
            //요리 다시시작
            Cook_Pause(false);
        }


        notCookSprite.ForEach((x) => x.enabled = false);
        runCookSprite.ForEach((x) => x.enabled = true);
    }

    private void Update()
    {
        UpdateOilZone();
    }

    private void UpdateOilZone()
    {
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if (kitchenMgr.mouseArea == DragArea.Oil_Zone &&
            (kitchenMgr.oilZone != null && kitchenMgr.oilZone == this))
        {
            if (kitchenMgr.dragState == DragState.Chicken_Strainter &&
                    chickenState == ChickenState.NotCook)
            {
                //해당 기름튀기는곳이 조리중이 아님
                sprite.spriteImg.sprite = sprite.canUseSprite;
            }
            else
            {
                //나머지 상태면 이미지가 보이지 않는다.
                sprite.spriteImg.sprite = sprite.normalSprite;
            }
        }
        else
        {
            //마우스를 밖으로 내보내면 이펙트 비활성화
            sprite.spriteImg.sprite = sprite.normalSprite;
        }
    }

    public bool Cook_Start(int pChickenCnt)
    {
        if(chickenState != ChickenState.NotCook)
        {
            //조리 시작전에만 요리시작이 가능
            return false;
        }

        //요리 시작

        pauseCook = false;
        animator.speed = 1;

        //넣은 치킨갯수 파악
        chickenCnt = pChickenCnt;

        notCookSprite.ForEach((x) => x.enabled = false);
        runCookSprite.ForEach((x) => x.enabled = true);

        if (cookCor != null)
        {
            //실행중인 코루틴이 있을수도있으니까. 정지처리
            StopCoroutine(cookCor);
            cookCor = null;
        }

        //조리처리 시작
        cookCor = RunningCook();
        StartCoroutine(cookCor);

        return true;
    }

    private IEnumerator RunningCook()
    {
        //조리처리용 코루틴

        //------------------------------------------------------------------
        //조리 시작부
        animator.SetTrigger("Cook");
        chickenState = ChickenState.BadChicken_0;

        //------------------------------------------------------------------
        //20초 경과 조리완료부
        float tTime = 0;
        while(tTime < 20)
        {
            yield return null;
            if(pauseCook == false)
            {
                //일시정지가 아님
                tTime += Time.deltaTime;
            }
        }
        chickenState = ChickenState.GoodChicken;

        //------------------------------------------------------------------
        //5초 경과 타기 시작하는 부분
        tTime = 0;
        while (tTime < 5)
        {
            yield return null;
            if (pauseCook == false)
            {
                //일시정지가 아님
                tTime += Time.deltaTime;
            }
        }

        animator.SetTrigger("Fire");
        chickenState = ChickenState.BadChicken_1;

        //------------------------------------------------------------------
        //4.5초 경과 타기 쓰레기 치킨이 되는 부분
        tTime = 0;
        while (tTime < 4.5f)
        {
            yield return null;
            if (pauseCook == false)
            {
                //일시정지가 아님
                tTime += Time.deltaTime;
            }
        }
        chickenState = ChickenState.BadChicken_2;
    }

    public void Cook_Stop()
    {
        //요리 종료
        notCookSprite.ForEach((x) => x.enabled = true);
        runCookSprite.ForEach((x) => x.enabled = false);

        if (cookCor != null)
        {
            //실행중인 코루틴이 있을수도있으니까. 정지처리
            StopCoroutine(cookCor);
            cookCor = null;
        }

        //조리 시작전으로 돌림
        chickenState = ChickenState.NotCook;
    }

    public void Cook_Pause(bool state)
    {
        pauseCook = state;
        if(state)
        {
            //애니메이션도 일시정지
            animator.speed = 0;
        }
        else
        {
            //애니메이션 다시 실행
            animator.speed = 1;
        }
    }
}
