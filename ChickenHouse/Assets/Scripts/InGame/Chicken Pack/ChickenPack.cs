using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenPack : Mgr
{
    /** ����ִ� ġŲ ���� **/
    private int         chickenCnt;
    /** ġŲ ���� **/
    private ChickenState chickenState;

    /** �ҽ�0 **/
    private ChickenSpicy source0;
    /** �ҽ�1 **/
    private ChickenSpicy source1;

    [System.Serializable]
    public struct SPITE_IMG
    {
        //������Ʈ ��������Ʈ �̹���
        public SpriteRenderer   spriteImg;
        public Sprite           normalSprite;
        public Sprite           canUseSprite;
    }

    [System.Serializable]
    public struct CHICKEN_OBJ
    {
        //������Ʈ ��������Ʈ �̹���
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
            //ġŲ�� ���ο� �����ؾ��� �巡�װ� ����
            return;
        }

        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        kitchenMgr.dragState = DragState.Chicken_Pack;
        if (kitchenMgr.cameraObj.lookArea != LookArea.Kitchen)
        {
            //�ֹ��� �����ִ� ���¿����� ��ȣ �ۿ� ����
            return;
        }

        obj.gameObject.SetActive(false);

        //������ ��ư�� ǥ�����ش�.
        kitchenMgr.ui.takeOut.OpenBtn();
    }

    private void OnMouseUp()
    {
        KitchenMgr kitchenMgr = KitchenMgr.Instance;

        if (kitchenMgr.dragState != DragState.Chicken_Pack)
        {
            //�ش� ������Ʈ�� �巡�����̶�� �ǴܵǾ������� ����
            return;
        }

        obj.gameObject.SetActive(true);

        //������ ��ư ��Ȱ��
        kitchenMgr.ui.takeOut.CloseBtn();

        //�������� ġŲ �ڽ� ������
        kitchenMgr.dragState = DragState.None;
        if (kitchenMgr.mouseArea == DragArea.Trash_Btn)
        {
            //������ ��ưó��
            Init();
            return;
        }

        if (kitchenMgr.mouseArea == DragArea.Chicken_Slot)
        {
            //ġŲ�� �÷����´�.
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
                //ġŲ�� ����Ǿ����� ����
                //�ش� ��⸦ ��� �����ϴ�.
                sprite.spriteImg.sprite = sprite.canUseSprite;
            }
            else if (kitchenMgr.dragState == DragState.Hot_Spicy
                && chickenCnt > 0 && (source0 == ChickenSpicy.None || source1 == ChickenSpicy.None))
            {
                //ġŲ�� �������
                //ġŲ �ҽ� ��� ����
                sprite.spriteImg.sprite = sprite.canUseSprite;
            }
            else
            {
                //ġŲ ����ڽ��� ��� ���̴�.
                sprite.spriteImg.sprite = sprite.normalSprite;
            }
        }
        else
        {
            //���콺�� ������ �������� ����Ʈ ��Ȱ��ȭ
            sprite.spriteImg.sprite = sprite.normalSprite;
        }
    }

    public bool PackCkicken(int pChickenCnt, ChickenState pChickenState)
    {
        if (chickenCnt > 0)
        {
            //�̹� ġŲ�� ����ִ�.
            return false;
        }

        //ġŲ�� ��µ� ����
        chickenCnt = pChickenCnt;
        chickenState = pChickenState;

        if (chickenState == ChickenState.GoodChicken || chickenState == ChickenState.BadChicken_1)
        {
            //���� ġŲ or ���� �¿� ġŲ
            //�Ͼ� ����ũ
            Color newColor = smoke.color;
            newColor.r = 1;
            newColor.g = 1;
            newColor.b = 1;
            smoke.color = newColor;
            smoke.gameObject.SetActive(true);
        }
        else if (chickenState == ChickenState.BadChicken_2)
        {
            //�¿� ġŲ
            //���� ����ũ
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
            //ġŲ�� ����.
            return false;
        }

        if (source0 != ChickenSpicy.None && source1 != ChickenSpicy.None)
        {
            //ġŲ�� �ҽ��� �̹� �ѷ����ִ�.
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
        //ġŲ�� ǥ��
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
        //ġŲ ������ŭ�� ġŲ�� ��������.
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
        //�ʱ�ȭ ��
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
