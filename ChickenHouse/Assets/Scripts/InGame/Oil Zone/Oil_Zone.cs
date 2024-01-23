using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oil_Zone : Mgr
{
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

    /**�� ���� **/
    private int         chickenCnt;


        
    /** ġŲ �丮 ���� **/
    private ChickenState chickenState = ChickenState.NotCook;
    /** �丮 �Ͻ� ���� ���� **/
    private bool        pauseCook;
    /** �丮 �ڷ�ƾ **/
    private IEnumerator cookCor;

    private void OnMouseDrag()
    {
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
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        kitchenMgr.dragObj.DragStrainter(chickenCnt, DragState.Fry_Chicken, oilShader.Mode, oilShader.LerpValue);

        //������ ��ư�� ǥ�����ش�.
        kitchenMgr.ui.takeOut.OpenBtn();

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
            kitchenMgr.ui.takeOut.RunBtn();
            return;
        }
        else if(kitchenMgr.mouseArea == DragArea.Chicken_Pack)
        {
            //ġŲ�뿡 ġŲ �ֱ�
            if(kitchenMgr.chickenPack.PackCkicken(chickenCnt))
            {
                //ġŲ�ѿ� ġŲ �ֱ�
                kitchenMgr.chickenPack.Set_ChickenShader(oilShader.Mode, oilShader.LerpValue);
                kitchenMgr.chickenPack.Show_Chicken(false);

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

        pauseCook = false;
        animator.speed = 1;

        //���� ġŲ���� �ľ�
        chickenCnt = pChickenCnt;

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
        while(tTime < 20)
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
        while (tTime < 5)
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
        while (tTime < 4.5f)
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
        }
        else
        {
            //�ִϸ��̼� �ٽ� ����
            animator.speed = 1;
        }
    }
}
