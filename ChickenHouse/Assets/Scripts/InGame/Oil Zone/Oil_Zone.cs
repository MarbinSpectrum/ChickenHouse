using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oil_Zone : Mgr
{
    private const float COOK_TIME_0 = 14f;
    private const float COOK_TIME_1 = 5f;
    private const float COOK_TIME_2 = 4.5f;

    [System.Serializable]
    public struct SPITE_IMG
    {
        //������Ʈ ��������Ʈ �̹���
        public SpriteRenderer   spriteImg;
        public Sprite           normalSprite;
        public Sprite           canUseSprite;
    }
    [SerializeField] private SPITE_IMG sprite;

    [SerializeField] private List<SpriteRenderer>   notCookSprite;
    [SerializeField] private List<SpriteRenderer>   runCookSprite;
    [SerializeField] private Animator               animator;
    [SerializeField] private Oil_Zone_Shader        oilShader;
    [SerializeField] private Collider2D             onCollider2D;
    [SerializeField] private Collider2D             offCollider2D;


    /**�� ���� **/
    private int             chickenCnt;     
    /** ġŲ �丮 ���� **/
    private ChickenState    chickenState = ChickenState.NotCook;
    /** �丮 �Ͻ� ���� ���� **/
    private bool            pauseCook;
    /** �丮 �ڷ�ƾ **/
    private IEnumerator     cookCor;

    private void OnMouseDrag()
    {
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
        kitchenMgr.ui.takeOut.OpenBtn();

        onCollider2D.enabled = false;
        offCollider2D.enabled = true;
        notCookSprite.ForEach((x) => x.enabled = true);
        runCookSprite.ForEach((x) => x.enabled = false);
    }

    private void OnMouseUp()
    {
        KitchenMgr kitchenMgr = KitchenMgr.Instance;

        if(kitchenMgr.dragState != DragState.Fry_Chicken)
        {
            //Ƣ��ġŲ�� �巡�� ���϶� ���콺�� ���� ��츸 üũ�Ѵ�.
            return;
        }

        //������ ��ư ��Ȱ��
        kitchenMgr.ui.takeOut.CloseBtn();

        kitchenMgr.dragState = DragState.None;
        if(kitchenMgr.mouseArea == DragArea.Trash_Btn)
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
                if(kitchenMgr.chickenStrainter != null)
                    kitchenMgr.chickenStrainter.Init();
                return;
            }
        }

        if (pauseCook)
        {
            //�丮 �ٽý���
            Cook_Pause(false);
        }

        onCollider2D.enabled = true;
        offCollider2D.enabled = false;
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
                //�ش� �⸧Ƣ��°��� �������� �ƴ�
                sprite.spriteImg.sprite = sprite.canUseSprite;
            }
            else
            {
                //������ ���¸� �̹����� ������ �ʴ´�.
                sprite.spriteImg.sprite = sprite.normalSprite;
            }
        }
        else
        {
            //���콺�� ������ �������� ����Ʈ ��Ȱ��ȭ
            sprite.spriteImg.sprite = sprite.normalSprite;
        }
    }

    public bool Cook_Start(int pChickenCnt)
    {
        if(chickenState != ChickenState.NotCook)
        {
            //���� ���������� �丮������ ����
            return false;
        }

        //�丮 ����

        Cook_Pause(false);

        //���� ġŲ���� �ľ�
        chickenCnt = pChickenCnt;

        onCollider2D.enabled = true;
        offCollider2D.enabled = false;
        notCookSprite.ForEach((x) => x.enabled = false);
        runCookSprite.ForEach((x) => x.enabled = true);

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

        //------------------------------------------------------------------
        //���� ���ۺ�
        animator.SetTrigger("Cook");
        chickenState = ChickenState.BadChicken_0;

        //------------------------------------------------------------------
        //20�� ��� �����Ϸ��
        float tTime = 0;
        while(tTime < COOK_TIME_0)
        {
            yield return null;
            if(pauseCook == false)
            {
                //�Ͻ������� �ƴ�
                tTime += Time.deltaTime;
            }
        }
        chickenState = ChickenState.GoodChicken;

        //------------------------------------------------------------------
        //5�� ��� Ÿ�� �����ϴ� �κ�
        tTime = 0;
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
        onCollider2D.enabled = false;
        offCollider2D.enabled = true;
        notCookSprite.ForEach((x) => x.enabled = true);
        runCookSprite.ForEach((x) => x.enabled = false);

        if (cookCor != null)
        {
            //�������� �ڷ�ƾ�� �������������ϱ�. ����ó��
            StopCoroutine(cookCor);
            cookCor = null;
        }

        //���� ���������� ����
        chickenState = ChickenState.NotCook;
    }

    public void Cook_Pause(bool state)
    {
        pauseCook = state;
        if(state)
        {
            //�ִϸ��̼ǵ� �Ͻ�����
            animator.speed = 0;
            soundMgr.StopLoopSE(Sound.Oil_SE);
        }
        else
        {
            //�ִϸ��̼� �ٽ� ����
            animator.speed = 1;
            soundMgr.PlayLoopSE(Sound.Oil_SE);
        }
    }
}
