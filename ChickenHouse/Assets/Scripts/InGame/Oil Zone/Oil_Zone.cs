using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Oil_Zone : Mgr
{
    private const float COOK_TIME_0 = 14f;
    private const float COOK_TIME_1 = 5f;
    private const float COOK_TIME_2 = 4.5f;

    [System.Serializable]
    public struct SPITE_IMG
    {
        //오브젝트 스프라이트 이미지
        public Sprite           normalSprite;
        public Sprite           canUseSprite;
    }
    [SerializeField] private SPITE_IMG      sprite;
    [SerializeField] private Image          image;
    [SerializeField] private ScrollObj[]    scrollObj;
    [System.Serializable]
    public struct OIL_GAUGE
    {
        public Transform    hand;
        public Image        bar;
    }
    [SerializeField] private OIL_GAUGE gauge;

    [SerializeField] private List<Image>            notCookImg;
    [SerializeField] private List<Image>            runCookImg;
    [SerializeField] private Animator               animator;
    [SerializeField] private Oil_Zone_Shader        oilShader;
    [SerializeField] private TutoObj                tutoObj;
    [SerializeField] private TutoObj                tutoObj2;

    private float           gaugeTime;
    /**닭 갯수 **/
    private int             chickenCnt;     
    /** 치킨 요리 정도 **/
    private ChickenState    chickenState = ChickenState.NotCook;
    /** 요리 일시 정지 여부 **/
    private bool            pauseCook;
    /** 요리 코루틴 **/
    private IEnumerator     cookCor;

    private ChickenStrainter chickenStrainter;

    public void OnMouseEnter()
    {
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if (kitchenMgr.dragState == DragState.Chicken_Strainter && chickenState == ChickenState.NotCook)
        {
            //해당 기름튀기는곳이 조리중이 아님
            image.sprite = sprite.canUseSprite;
        }
        else
        {
            //나머지 상태면 이미지가 보이지 않는다.
            image.sprite = sprite.normalSprite;
        }

        kitchenMgr.oilZone = this;
        kitchenMgr.mouseArea = DragArea.Oil_Zone;
    }

    public void OnMouseExit()
    {
        image.sprite = sprite.normalSprite;

        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        kitchenMgr.oilZone = null;
        kitchenMgr.mouseArea = DragArea.None;
    }

    public void OnMouseDrag()
    {
        if (tutoMgr.tutoComplete == false && tutoMgr.nowTuto != Tutorial.Tuto_5_2)
        {
            //튜토리얼이 아직 완료안된듯
            //혹시모르니 튜토리얼 타이밍때만 작동하도록 막아놓자
            return;
        }

        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if (kitchenMgr.cameraObj.lookArea != LookArea.Kitchen)
        {
            //주방을 보고있는 상태에서만 상호 작용 가능
            return;
        }

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
        kitchenMgr.dragObj.DragStrainter(chickenCnt, DragState.Fry_Chicken, oilShader.Mode, oilShader.LerpValue);

        //버리기 버튼도 표시해준다.
        if (tutoMgr.tutoComplete == false && tutoMgr.nowTuto == Tutorial.Tuto_5_2)
        {
            //튜토리얼중에는 버리기 버튼이 표시되지않음
        }
        else
        {
            kitchenMgr.ui.takeOut.OpenBtn();
        }

        notCookImg.ForEach((x) => x.enabled = true);
        runCookImg.ForEach((x) => x.enabled = false);
    }

    public void OnMouseUp()
    {
        if (tutoMgr.tutoComplete == false && tutoMgr.nowTuto != Tutorial.Tuto_5_2)
        {
            //튜토리얼이 아직 완료안된듯
            //혹시모르니 튜토리얼 타이밍때만 작동하도록 막아놓자
            return;
        }

        KitchenMgr kitchenMgr = KitchenMgr.Instance;

        if(kitchenMgr.dragState != DragState.Fry_Chicken)
        {
            //튀긴치킨을 드래그 중일때 마우스를 놓은 경우만 체크한다.
            return;
        }

        //버리기 버튼 비활성
        kitchenMgr.ui.takeOut.CloseBtn();

        kitchenMgr.dragState = DragState.None;

        if (kitchenMgr.mouseArea == DragArea.Trash_Btn)
        {
            //버리기 버튼처리
            kitchenMgr.ui.takeOut.ChickenStrainter_TakeOut();
            return;
        }
        else if(kitchenMgr.mouseArea == DragArea.Chicken_Pack)
        {
            //치킨통에 치킨 넣기
            if(kitchenMgr.chickenPack.PackCkicken(chickenCnt, chickenState))
            {
                //치킨팩에 치킨 넣기
                kitchenMgr.chickenPack.Set_ChickenShader(oilShader.Mode, oilShader.LerpValue);
                kitchenMgr.chickenPack.UpdatePack();

                //요리 종료
                Cook_Stop();

                //치킨 건지를 사용을 끝냈으니 초기화
                if(chickenStrainter != null)
                {
                    chickenStrainter.Init();
                    chickenStrainter = null;
                }

                if (tutoMgr.tutoComplete == false && tutoMgr.nowTuto == Tutorial.Tuto_5_2)
                {
                    //튜토리얼에서는 카운터 이동버튼이 안나옴
                    tutoObj2.PlayTuto();
                }
                else
                {
                    //치킨을 올려놓음 카운터로 이동은 가능
                    kitchenMgr.ui.goCounter.OpenBtn();
                }

                return;
            }
        }

        if (pauseCook)
        {
            //요리 다시시작
            Cook_Pause(false);
        }

        notCookImg.ForEach((x) => x.enabled = false);
        runCookImg.ForEach((x) => x.enabled = true);
    }

    private void Update()
    {
        UpdateGauge();
    }

    private void UpdateGauge()
    {
        float speedRate = GetCookSpeedRate();
        float lerpValue = gaugeTime / (COOK_TIME_0 / speedRate);
        lerpValue = Mathf.Clamp(lerpValue, 0, 1);
        float Angle = Mathf.Lerp(90, 270, 1 - lerpValue);
        if (gauge.hand != null)
        {
            float r = Mathf.Lerp(gauge.hand.localEulerAngles.z, Angle,0.2f);
            gauge.hand.localEulerAngles = new Vector3(0, 0, r);
            gauge.bar.fillAmount = lerpValue;
        }
    }

    private float GetCookSpeedRate()
    {
        //업그레이드 속도에 따라서 상태 설정
        if (gameMgr.playData.upgradeState[(int)Upgrade.OIL_Zone_6])
        {
            return 3.0f;
        }
        else if (gameMgr.playData.upgradeState[(int)Upgrade.OIL_Zone_5])
        {
            return 3.0f;
        }
        else if (gameMgr.playData.upgradeState[(int)Upgrade.OIL_Zone_4])
        {
            return 2.6f;
        }
        else if (gameMgr.playData.upgradeState[(int)Upgrade.OIL_Zone_3])
        {
            return 2.2f;
        }
        else if (gameMgr.playData.upgradeState[(int)Upgrade.OIL_Zone_2])
        {
            return 1.8f;
        }
        else if (gameMgr.playData.upgradeState[(int)Upgrade.OIL_Zone_1])
        {
            return 1.4f;
        }
        return 1;
    }

    public bool Cook_Start(int pChickenCnt,ChickenStrainter pChickenStrainter)
    {
        if(chickenState != ChickenState.NotCook)
        {
            //조리 시작전에만 요리시작이 가능
            return false;
        }

        //요리 시작

        chickenStrainter = pChickenStrainter;
        image.sprite = sprite.normalSprite;
        Cook_Pause(false);

        //넣은 치킨갯수 파악
        chickenCnt = pChickenCnt;

        notCookImg.ForEach((x) => x.enabled = false);
        runCookImg.ForEach((x) => x.enabled = true);

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

        float   speedRate   = GetCookSpeedRate();
        bool    notFire     = false;

        //업그레이드 속도에 따라서 상태 설정
        if(gameMgr.playData.upgradeState[(int)Upgrade.OIL_Zone_6])
        {
            //최대 레벨일 경우 타지않는다.
            notFire = true;
        }

        //------------------------------------------------------------------
        //조리 시작부
        animator.speed = speedRate;
        animator.SetTrigger("Cook");
        chickenState = ChickenState.BadChicken_0;

        //------------------------------------------------------------------
        //20초 경과 조리완료부
        float tTime = 0;
        gaugeTime = 0;
        while (tTime < COOK_TIME_0 / speedRate)
        {
            yield return null;
            if(pauseCook == false)
            {
                //일시정지가 아님
                tTime += Time.deltaTime;
                gaugeTime += Time.deltaTime;
            }
        }
        chickenState = ChickenState.GoodChicken;

        if (tutoMgr.tutoComplete == false && tutoMgr.nowTuto == Tutorial.Tuto_4)
        {
            //튜토리얼에서는 여기서 조리가 끝남
            tutoObj.PlayTuto();
            yield break;
        }

        if(notFire)
        {
            //타지 않게 설정됨
            yield break;
        }

        animator.speed = 1;
        //------------------------------------------------------------------
        //5초 경과 타기 시작하는 부분
        tTime = 0;
        gaugeTime = COOK_TIME_0;
        while (tTime < COOK_TIME_1)
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
        while (tTime < COOK_TIME_2)
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
        gaugeTime = 0;
        notCookImg.ForEach((x) => x.enabled = true);
        runCookImg.ForEach((x) => x.enabled = false);

        if (cookCor != null)
        {
            //실행중인 코루틴이 있을수도있으니까. 정지처리
            StopCoroutine(cookCor);
            cookCor = null;
        }

        //조리 시작전으로 돌림
        chickenState = ChickenState.NotCook;

        foreach (ScrollObj sObj in scrollObj)
        {
            sObj.isRun = true;
        }
    }

    public void Cook_Pause(bool state)
    {
        pauseCook = state;
        if(state)
        {
            //애니메이션도 일시정지
            animator.speed = 0;
            foreach (ScrollObj sObj in scrollObj)
            {
                sObj.isRun = false;
            }
            soundMgr.StopLoopSE(Sound.Oil_SE);
        }
        else
        {
            //애니메이션 다시 실행
            float speedRate = GetCookSpeedRate();
            animator.speed = speedRate;
            foreach (ScrollObj sObj in scrollObj)
            {
                sObj.isRun = true;
            }
            soundMgr.PlayLoopSE(Sound.Oil_SE);
        }
    }
}
