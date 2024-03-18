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
        //������Ʈ ��������Ʈ �̹���
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
    /**�� ���� **/
    private int             chickenCnt;     
    /** ġŲ �丮 ���� **/
    private ChickenState    chickenState = ChickenState.NotCook;
    /** �丮 �Ͻ� ���� ���� **/
    private bool            pauseCook;
    /** �丮 �ڷ�ƾ **/
    private IEnumerator     cookCor;

    private ChickenStrainter chickenStrainter;

    public void OnMouseEnter()
    {
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if (kitchenMgr.dragState == DragState.Chicken_Strainter && chickenState == ChickenState.NotCook)
        {
            //�ش� �⸧Ƣ��°��� �������� �ƴ�
            image.sprite = sprite.canUseSprite;
        }
        else
        {
            //������ ���¸� �̹����� ������ �ʴ´�.
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
            //Ʃ�丮���� ���� �Ϸ�ȵȵ�
            //Ȥ�ø𸣴� Ʃ�丮�� Ÿ�ֶ̹��� �۵��ϵ��� ���Ƴ���
            return;
        }

        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if (kitchenMgr.cameraObj.lookArea != LookArea.Kitchen)
        {
            //�ֹ��� �����ִ� ���¿����� ��ȣ �ۿ� ����
            return;
        }

        if (chickenState == ChickenState.NotCook)
        {
            //���� �������̸� �翬�� �巡�׾ȵ�
            return;
        }

        if (pauseCook == false)
        {
            //�丮 �Ͻ�����
            Cook_Pause(true);
        }

        //���������� ġŲ������ ������. �巡�� ����
        kitchenMgr.dragObj.DragStrainter(chickenCnt, DragState.Fry_Chicken, oilShader.Mode, oilShader.LerpValue);

        //������ ��ư�� ǥ�����ش�.
        if (tutoMgr.tutoComplete == false && tutoMgr.nowTuto == Tutorial.Tuto_5_2)
        {
            //Ʃ�丮���߿��� ������ ��ư�� ǥ�õ�������
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
            //Ʃ�丮���� ���� �Ϸ�ȵȵ�
            //Ȥ�ø𸣴� Ʃ�丮�� Ÿ�ֶ̹��� �۵��ϵ��� ���Ƴ���
            return;
        }

        KitchenMgr kitchenMgr = KitchenMgr.Instance;

        if(kitchenMgr.dragState != DragState.Fry_Chicken)
        {
            //Ƣ��ġŲ�� �巡�� ���϶� ���콺�� ���� ��츸 üũ�Ѵ�.
            return;
        }

        //������ ��ư ��Ȱ��
        kitchenMgr.ui.takeOut.CloseBtn();

        kitchenMgr.dragState = DragState.None;

        if (kitchenMgr.mouseArea == DragArea.Trash_Btn)
        {
            //������ ��ưó��
            kitchenMgr.ui.takeOut.ChickenStrainter_TakeOut();
            return;
        }
        else if(kitchenMgr.mouseArea == DragArea.Chicken_Pack)
        {
            //ġŲ�뿡 ġŲ �ֱ�
            if(kitchenMgr.chickenPack.PackCkicken(chickenCnt, chickenState))
            {
                //ġŲ�ѿ� ġŲ �ֱ�
                kitchenMgr.chickenPack.Set_ChickenShader(oilShader.Mode, oilShader.LerpValue);
                kitchenMgr.chickenPack.UpdatePack();

                //�丮 ����
                Cook_Stop();

                //ġŲ ������ ����� �������� �ʱ�ȭ
                if(chickenStrainter != null)
                {
                    chickenStrainter.Init();
                    chickenStrainter = null;
                }

                if (tutoMgr.tutoComplete == false && tutoMgr.nowTuto == Tutorial.Tuto_5_2)
                {
                    //Ʃ�丮�󿡼��� ī���� �̵���ư�� �ȳ���
                    tutoObj2.PlayTuto();
                }
                else
                {
                    //ġŲ�� �÷����� ī���ͷ� �̵��� ����
                    kitchenMgr.ui.goCounter.OpenBtn();
                }

                return;
            }
        }

        if (pauseCook)
        {
            //�丮 �ٽý���
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
        //���׷��̵� �ӵ��� ���� ���� ����
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
            //���� ���������� �丮������ ����
            return false;
        }

        //�丮 ����

        chickenStrainter = pChickenStrainter;
        image.sprite = sprite.normalSprite;
        Cook_Pause(false);

        //���� ġŲ���� �ľ�
        chickenCnt = pChickenCnt;

        notCookImg.ForEach((x) => x.enabled = false);
        runCookImg.ForEach((x) => x.enabled = true);

        if (cookCor != null)
        {
            //�������� �ڷ�ƾ�� �������������ϱ�. ����ó��
            StopCoroutine(cookCor);
            cookCor = null;
        }

        //����ó�� ����
        cookCor = RunningCook();
        StartCoroutine(cookCor);

        return true;
    }

    private IEnumerator RunningCook()
    {
        //����ó���� �ڷ�ƾ

        float   speedRate   = GetCookSpeedRate();
        bool    notFire     = false;

        //���׷��̵� �ӵ��� ���� ���� ����
        if(gameMgr.playData.upgradeState[(int)Upgrade.OIL_Zone_6])
        {
            //�ִ� ������ ��� Ÿ���ʴ´�.
            notFire = true;
        }

        //------------------------------------------------------------------
        //���� ���ۺ�
        animator.speed = speedRate;
        animator.SetTrigger("Cook");
        chickenState = ChickenState.BadChicken_0;

        //------------------------------------------------------------------
        //20�� ��� �����Ϸ��
        float tTime = 0;
        gaugeTime = 0;
        while (tTime < COOK_TIME_0 / speedRate)
        {
            yield return null;
            if(pauseCook == false)
            {
                //�Ͻ������� �ƴ�
                tTime += Time.deltaTime;
                gaugeTime += Time.deltaTime;
            }
        }
        chickenState = ChickenState.GoodChicken;

        if (tutoMgr.tutoComplete == false && tutoMgr.nowTuto == Tutorial.Tuto_4)
        {
            //Ʃ�丮�󿡼��� ���⼭ ������ ����
            tutoObj.PlayTuto();
            yield break;
        }

        if(notFire)
        {
            //Ÿ�� �ʰ� ������
            yield break;
        }

        animator.speed = 1;
        //------------------------------------------------------------------
        //5�� ��� Ÿ�� �����ϴ� �κ�
        tTime = 0;
        gaugeTime = COOK_TIME_0;
        while (tTime < COOK_TIME_1)
        {
            yield return null;
            if (pauseCook == false)
            {
                //�Ͻ������� �ƴ�
                tTime += Time.deltaTime;
            }
        }

        animator.SetTrigger("Fire");
        chickenState = ChickenState.BadChicken_1;

        //------------------------------------------------------------------
        //4.5�� ��� Ÿ�� ������ ġŲ�� �Ǵ� �κ�
        tTime = 0;
        while (tTime < COOK_TIME_2)
        {
            yield return null;
            if (pauseCook == false)
            {
                //�Ͻ������� �ƴ�
                tTime += Time.deltaTime;
            }
        }
        chickenState = ChickenState.BadChicken_2;
    }

    public void Cook_Stop()
    {
        //�丮 ����
        gaugeTime = 0;
        notCookImg.ForEach((x) => x.enabled = true);
        runCookImg.ForEach((x) => x.enabled = false);

        if (cookCor != null)
        {
            //�������� �ڷ�ƾ�� �������������ϱ�. ����ó��
            StopCoroutine(cookCor);
            cookCor = null;
        }

        //���� ���������� ����
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
            //�ִϸ��̼ǵ� �Ͻ�����
            animator.speed = 0;
            foreach (ScrollObj sObj in scrollObj)
            {
                sObj.isRun = false;
            }
            soundMgr.StopLoopSE(Sound.Oil_SE);
        }
        else
        {
            //�ִϸ��̼� �ٽ� ����
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
