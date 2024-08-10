using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Oil_Zone : Mgr
{
    private const float COOK_TIME_0 = 14f;
    private const float COOK_TIME_1 = 5f;
    private const float COOK_TIME_2 = 4.5f;

    [SerializeField] private Image          selectImg;
    [SerializeField] private ScrollObj[]    scrollObj;
    [SerializeField] private ChickenPackList chickenPackList;

    [System.Serializable]
    public struct OilMachine
    {
        //튀김기
        public Image machine0;
        //튀김기 입구
        public Image machine1;
        /** 기기 이미지 **/
        public Sprite[] machineImg0;
        /** 기기 이미지 **/
        public Sprite[] machineImg1;
    }
    public OilMachine oilMahcine;

    [System.Serializable]
    public struct OIL_GAUGE
    {
        public Transform    hand;
        public Image        bar;
    }
    [SerializeField] private OIL_GAUGE gauge;

    [SerializeField] private List<Image>            runCookImg;
    [SerializeField] private Animator               animator;
    [SerializeField] private Oil_Zone_Shader        oilShader;
    [SerializeField] private TutoObj                tutoObj;
    [SerializeField] private TutoObj                tutoObj2;

    public bool isHold { get; private set; } = false;

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

    private float           chickenTime;

    public void OnMouseEnter()
    {
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if (kitchenMgr.dragState == DragState.Chicken_Strainter && IsRun() == false)
        {
            //해당 기름튀기는곳이 조리중이 아님
            selectImg.enabled = true;
        }
        else
        {
            //나머지 상태면 이미지가 보이지 않는다.
            selectImg.enabled = false;
        }

        kitchenMgr.oilZone = this;
        kitchenMgr.mouseArea = DragArea.Oil_Zone;
    }

    public void OnMouseExit()
    {
        selectImg.enabled = false;

        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        kitchenMgr.oilZone = null;
        kitchenMgr.mouseArea = DragArea.None;
    }

    public void HoldStrainter()
    {
        if (CheckMode.IsDropMode() == false)
            return;

        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if (kitchenMgr.dragObj.holdGameObj == null)
        {
            if (tutoMgr.tutoComplete == false && tutoMgr.nowTuto != Tutorial.Tuto_5_2)
            {
                //튜토리얼이 아직 완료안된듯
                //혹시모르니 튜토리얼 타이밍때만 작동하도록 막아놓자
                return;
            }

            HoldStrainter(true);
        }
        else if (kitchenMgr.dragObj.holdGameObj == gameObject)
        {
            HoldStrainter(false);
        }
        else if (kitchenMgr.dragObj.holdGameObj != gameObject)
        {
            ChickenStrainter chickenStrainter = kitchenMgr.dragObj.holdGameObj.GetComponent<ChickenStrainter>();
            Oil_Zone oilZone = kitchenMgr.dragObj.holdGameObj.GetComponent<Oil_Zone>();

            if (chickenStrainter)
            {
                if (tutoMgr.tutoComplete == false && tutoMgr.nowTuto != Tutorial.Tuto_4)
                {
                    //튜토리얼이 아직 완료안된듯
                    //혹시모르니 튜토리얼 타이밍때만 작동하도록 막아놓자
                    return;
                }

                chickenStrainter.PutDown(this);
                return;
            }

            if (oilZone)
            {
                oilZone.PutDown(this);
                return;
            }

            kitchenMgr.dragState = DragState.None;
            kitchenMgr.dragObj.HoldObj(null);
            kitchenMgr.dragObj.HoldOut();
        }
    }

    public void HoldStrainter(bool state)
    {
        //인스펙터에 끌어서 사용하는 함수임

        if (CheckMode.IsDropMode() == false)
            return;

        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if (state)
        {
            if (kitchenMgr.cameraObj.lookArea != LookArea.Kitchen)
            {
                //주방을 보고있는 상태에서만 상호 작용 가능
                return;
            }

            if (IsRun() == false)
            {
                //조리 시작전이면 당연히 드래그안됨
                return;
            }

            if (isHold)
            {
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
            isHold = true;
            kitchenMgr.dragObj.HoldObj(gameObject);
            runCookImg.ForEach((x) => x.enabled = false);
        }
        else
        {
            if (isHold == false)
            {
                return;
            }

            if (pauseCook)
            {
                //요리 다시시작
                Cook_Pause(false);
            }

            kitchenMgr.ui.takeOut.CloseBtn();
            kitchenMgr.dragObj.HoldObj(null);
            kitchenMgr.dragState = DragState.None;
            isHold = false;
            runCookImg.ForEach((x) => x.enabled = true);
        }
    }

    public void PutDown(ChickenPack pChickenPack)
    {
        if (isHold == false)
            return;

        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if (pChickenPack.PackCkicken(chickenCnt, chickenState, ChickenSpicy.None, ChickenSpicy.None))
        {

            //치킨팩에 치킨 넣기
            pChickenPack.Set_ChickenShader(oilShader.Mode, oilShader.LerpValue);
            pChickenPack.UpdatePack();

            //요리 종료
            Cook_Stop();

            if (tutoMgr.tutoComplete == false && tutoMgr.nowTuto == Tutorial.Tuto_5_2)
            {
                //튜토리얼에서는 카운터 이동버튼이 안나옴
                tutoObj2.PlayTuto();
            }
            else
            {
                //치킨을 올려놓음 카운터로 이동은 가능
                //kitchenMgr.ui.goCounter.OpenBtn();
            }

            kitchenMgr.ui.takeOut.CloseBtn();
            kitchenMgr.dragObj.HoldObj(null);
            kitchenMgr.dragState = DragState.None;
            isHold = false;
            runCookImg.ForEach((x) => x.enabled = false);
        }
        else
        {
            HoldStrainter(false);
        }
    }

    private void PutDown(Oil_Zone pOilZone)
    {
        if (isHold == false)
            return;

        if (pOilZone.ChangeOilZone(chickenCnt, chickenTime, gaugeTime))
        {
            Cook_Stop();
            HoldStrainter(false);
            runCookImg.ForEach((x) => x.enabled = false);
        }
        else
        {
            HoldStrainter(false);
        }
    }

    public void OnMouseDrag()
    {
        if (CheckMode.IsDropMode())
            return;

        if (tutoMgr.tutoComplete == false && tutoMgr.nowTuto != Tutorial.Tuto_5_2)
        {
            return;
        }

        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if (kitchenMgr.cameraObj.lookArea != LookArea.Kitchen)
        {
            return;
        }

        if (IsRun() == false)
        {
            return;
        }

        if (pauseCook == false)
        {
            Cook_Pause(true);
        }

        isHold = true;
        kitchenMgr.dragObj.DragStrainter(chickenCnt, DragState.Fry_Chicken, oilShader.Mode, oilShader.LerpValue);

        if (tutoMgr.tutoComplete == false && tutoMgr.nowTuto == Tutorial.Tuto_5_2)
        {

        }
        else
        {
            kitchenMgr.ui.takeOut.OpenBtn();
        }

        runCookImg.ForEach((x) => x.enabled = false);
    }

    public void OnMouseUp()
    {
        if (CheckMode.IsDropMode())
            return;

        if (tutoMgr.tutoComplete == false && tutoMgr.nowTuto != Tutorial.Tuto_5_2)
        {
            return;
        }

        KitchenMgr kitchenMgr = KitchenMgr.Instance;

        if (kitchenMgr.dragState != DragState.Fry_Chicken)
        {
            return;
        }
        isHold = false;
        kitchenMgr.ui.takeOut.CloseBtn();

        kitchenMgr.dragState = DragState.None;

        if (kitchenMgr.mouseArea == DragArea.Trash_Btn)
        {
            kitchenMgr.ui.takeOut.ChickenStrainter_TakeOut(this);
            return;
        }
        else if (kitchenMgr.mouseArea == DragArea.Oil_Zone)
        {
            if (kitchenMgr.oilZone.ChangeOilZone(chickenCnt, chickenTime, gaugeTime))
            {
                Cook_Stop();
                return;
            }
        }
        else if (kitchenMgr.mouseArea == DragArea.Chicken_Pack)
        {
            if (kitchenMgr.chickenPack.PackCkicken(chickenCnt, chickenState, ChickenSpicy.None, ChickenSpicy.None))
            {
                kitchenMgr.chickenPack.Set_ChickenShader(oilShader.Mode, oilShader.LerpValue);
                kitchenMgr.chickenPack.UpdatePack();

                Cook_Stop();

                if (tutoMgr.tutoComplete == false && tutoMgr.nowTuto == Tutorial.Tuto_5_2)
                {
                    tutoObj2.PlayTuto();
                }
                else
                {
                    //kitchenMgr.ui.goCounter.OpenBtn();
                }

                return;
            }
            else if (chickenPackList.AddChickenPack(chickenCnt, chickenState, ChickenSpicy.None, ChickenSpicy.None,
                oilShader.Mode, oilShader.LerpValue))
            {
                Cook_Stop();

                if (tutoMgr.tutoComplete == false && tutoMgr.nowTuto == Tutorial.Tuto_5_2)
                {
       
                    tutoObj2.PlayTuto();
                }
                else
                {
                    //kitchenMgr.ui.goCounter.OpenBtn();
                }

                return;
            }
        }

        if (pauseCook)
        {
            Cook_Pause(false);
        }

        runCookImg.ForEach((x) => x.enabled = true);
    }


    private void Update()
    {
        UpdateGauge();
    }

    private void UpdateGauge()
    {
        float speedRate = gameMgr.playData.GetCookSpeedRate();
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

    public bool Cook_Start(int pChickenCnt,ChickenStrainter pChickenStrainter)
    {
        if(IsRun())
        {
            //조리 시작전에만 요리시작이 가능
            return false;
        }

        //요리 시작

        chickenStrainter = pChickenStrainter;
        selectImg.enabled = false;
        Cook_Pause(false);

        //넣은 치킨갯수 파악
        chickenCnt = pChickenCnt;

        runCookImg.ForEach((x) => x.enabled = true);

        if (cookCor != null)
        {
            //실행중인 코루틴이 있을수도있으니까. 정지처리
            StopCoroutine(cookCor);
            cookCor = null;
        }

        //조리처리 시작
        cookCor = RunningCook(0, 0);
        StartCoroutine(cookCor);

        return true;
    }

    public bool ChangeOilZone(int pChickenCnt, float pChickenTime, float pGaugeTime)
    {
        if (IsRun())
        {
            //조리 시작전에만 요리시작이 가능
            return false;
        }

        //요리 시작
        selectImg.enabled = false;
        Cook_Pause(false);

        //넣은 치킨갯수 파악
        chickenCnt = pChickenCnt;

        runCookImg.ForEach((x) => x.enabled = true);

        if (cookCor != null)
        {
            //실행중인 코루틴이 있을수도있으니까. 정지처리
            StopCoroutine(cookCor);
            cookCor = null;
        }

        //조리처리 시작
        cookCor = RunningCook(pChickenTime, pGaugeTime);
        StartCoroutine(cookCor);

        return true;
    }

    private IEnumerator RunningCook(float pChickenTime, float pGaugeTime)
    {
        //조리처리용 코루틴
        float   speedRate   = gameMgr.playData.GetCookSpeedRate();
        bool    notFire     = false;

        //업그레이드 속도에 따라서 상태 설정
        if(gameMgr.playData.useItem[(int)ShopItem.OIL_Zone_4])
        {
            //최대 레벨일 경우 타지않는다.
            notFire = true;
        }

        //------------------------------------------------------------------
        //조리 시작부
        animator.speed = speedRate;
        chickenState = ChickenState.BadChicken_0;

        //------------------------------------------------------------------
        //조리완료부
        chickenTime = pChickenTime;
        animator.Play("Oil_Zone_Good", 0, 0);
        gaugeTime = pGaugeTime;
        while (chickenTime < (COOK_TIME_0 / speedRate))
        {
            yield return null;
            if(pauseCook == false)
            {
                //일시정지가 아님
                chickenTime += Time.deltaTime;
                gaugeTime += Time.deltaTime;

                float lerpValue = chickenTime / (COOK_TIME_0 / speedRate);
                animator.Play("Oil_Zone_Good", 0, lerpValue);
            }
        }
        animator.Play("Oil_Zone_Good", 0, 1);
        chickenState = ChickenState.GoodChicken;

        if (tutoMgr.tutoComplete == false && tutoMgr.nowTuto == Tutorial.Tuto_4)
        {
            //튜토리얼에서는 여기서 조리가 끝남
            tutoObj.PlayTuto();
            yield break;
        }

        soundMgr.PlaySE(Sound.Oil_Zone_End_SE);

        if(notFire)
        {
            //타지 않게 설정됨
            yield break;
        }

        animator.speed = 1;

        //------------------------------------------------------------------
        //타기 시작하는 부분

        gaugeTime = COOK_TIME_0;
        while (chickenTime < (COOK_TIME_0 / speedRate) + COOK_TIME_1)
        {
            yield return null;
            if (pauseCook == false)
            {
                //일시정지가 아님
                chickenTime += Time.deltaTime;
            }
        }

        chickenState = ChickenState.BadChicken_1;

        //------------------------------------------------------------------
        //4.5초 경과 타기 쓰레기 치킨이 되는 부분
        animator.Play("Oil_Zone_Bad", 0, 0);
        while (chickenTime < (COOK_TIME_0 / speedRate) + COOK_TIME_1 + COOK_TIME_2)
        {
            yield return null;
            if (pauseCook == false)
            {
                //일시정지가 아님
                chickenTime += Time.deltaTime;
                float lerpValue = (chickenTime - (COOK_TIME_0 / speedRate) - COOK_TIME_1) / COOK_TIME_2;
                animator.Play("Oil_Zone_Bad", 0, lerpValue);
            }
        }

        animator.Play("Oil_Zone_Bad", 0, 1);
        chickenState = ChickenState.BadChicken_2;
    }

    public void Cook_Stop()
    {
        //요리 종료
        gaugeTime = 0;
        chickenTime = 0;
        isHold = false;
        runCookImg.ForEach((x) => x.enabled = false);

        if (cookCor != null)
        {
            //실행중인 코루틴이 있을수도있으니까. 정지처리
            StopCoroutine(cookCor);
            cookCor = null;
        }

        //조리 시작전으로 돌림
        chickenState = ChickenState.NotCook;
        if (CheckMode.IsDropMode() == false)
        {
            foreach (ScrollObj sObj in scrollObj)
            {
                sObj.isRun = true;
            }
        }

        soundMgr.StopLoopSE(Sound.Oil_SE);
    }

    public void Cook_Pause(bool state)
    {
        pauseCook = state;
        if(state)
        {
            //애니메이션도 일시정지
            animator.speed = 0;
            if (CheckMode.IsDropMode() == false)
            {
                foreach (ScrollObj sObj in scrollObj)
                {
                    sObj.isRun = false;
                }
            }
            soundMgr.StopLoopSE(Sound.Oil_SE);
        }
        else
        {
            //애니메이션 다시 실행
            float speedRate = gameMgr.playData.GetCookSpeedRate();
            animator.speed = speedRate;

            if (CheckMode.IsDropMode() == false)
            {
                foreach (ScrollObj sObj in scrollObj)
                {
                    sObj.isRun = true;
                }
            }
            soundMgr.PlayLoopSE(Sound.Oil_SE);
        }
    }

    public bool IsRun()
    {
        //작동중
        if (chickenState == ChickenState.NotCook)
            return false;
        return true;
    }

    public void Init()
    {
        /////////////////////////////////////////////////////////////////////////////////
        //기름통 세팅
        if (gameMgr.playData.useItem[(int)ShopItem.OIL_Zone_1])
        {
            oilMahcine.machine0.sprite = oilMahcine.machineImg0[0];
            oilMahcine.machine1.sprite = oilMahcine.machineImg1[0];
        }
        else if (gameMgr.playData.useItem[(int)ShopItem.OIL_Zone_2])
        {
            oilMahcine.machine0.sprite = oilMahcine.machineImg0[1];
            oilMahcine.machine1.sprite = oilMahcine.machineImg1[1];
        }
        else if (gameMgr.playData.useItem[(int)ShopItem.OIL_Zone_3])
        {
            oilMahcine.machine0.sprite = oilMahcine.machineImg0[2];
            oilMahcine.machine1.sprite = oilMahcine.machineImg1[2];
        }
        else if (gameMgr.playData.useItem[(int)ShopItem.OIL_Zone_4])
        {
            oilMahcine.machine0.sprite = oilMahcine.machineImg0[3];
            oilMahcine.machine1.sprite = oilMahcine.machineImg1[3];
        }
        isHold = false;
    }
}
