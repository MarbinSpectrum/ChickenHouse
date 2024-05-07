using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChickenPack : Mgr
{
    /** ����ִ� ġŲ ���� **/
    public int chickenCnt { get; private set; }
    /** ġŲ ���� **/
    public ChickenState chickenState { get; private set; }

    /** �ҽ�0 **/
    public ChickenSpicy source0 { get; private set; }
    /** �ҽ�1 **/
    public ChickenSpicy source1 { get; private set; }

    private float   chickenLerpValue;
    private bool    chickenMode;

    [System.Serializable]
    public struct SPITE_IMG
    {
        //������Ʈ ��������Ʈ �̹���
        public Sprite           normalSprite;
        public Sprite           canUseSprite;
    }

    [System.Serializable]
    public struct CHICKEN_SPRITE
    {
        //������Ʈ ��������Ʈ �̹���
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
        //������Ʈ ��������Ʈ �̹���
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
            //ġŲ�� ����Ǿ����� ����
            //�ش� ��⸦ ��� �����ϴ�.
            body.sprite = sprite.canUseSprite;
        }
        else if ((kitchenMgr.dragState == DragState.Hot_Spicy || kitchenMgr.dragState == DragState.Soy_Spicy
            || kitchenMgr.dragState == DragState.Hell_Spicy || kitchenMgr.dragState == DragState.Prinkle_Spicy
             || kitchenMgr.dragState == DragState.Carbonara_Spicy || kitchenMgr.dragState == DragState.BBQ_Spicy)
            && chickenCnt > 0 && (source0 == ChickenSpicy.None || source1 == ChickenSpicy.None))
        {
            //ġŲ�� �������
            //ġŲ �ҽ� ��� ����
            body.sprite = sprite.canUseSprite;
        }
        else if (kitchenMgr.dragState == DragState.Chicken_Pack_Holl && chickenCnt <= 0)
        {
            //ġŲ�� ����Ǿ����� ����
            //�ش� ��⸦ ��� �����ϴ�.
            body.sprite = sprite.canUseSprite;
        }
        else
        {
            //ġŲ ����ڽ��� ��� ���̴�.
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
            //Ʃ�丮�� �߿��� �巡�� �Ұ���
            return;
        }

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
        if (tutoMgr.tutoComplete)
            kitchenMgr.ui.takeOut.OpenBtn();
    }

    public void OnMouseUp()
    {
        if (tutoMgr.tutoComplete == false && tutoMgr.nowTuto != Tutorial.Tuto_7)
        {
            //Ʃ�丮�� �߿��� �巡�� �Ұ���
            return;
        }

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

        if (kitchenMgr.mouseArea == DragArea.Trash_Btn && tutoMgr.tutoComplete)
        {
            //������ ��ưó��
            Init();

            //ī���� ��ư�� ��Ȱ��ȭ��
            kitchenMgr.ui.goCounter.CloseBtn();
            return;
        }

        if (kitchenMgr.mouseArea == DragArea.Chicken_Slot)
        {
            //ġŲ�� �÷����´�.
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
            //�̹� ġŲ�� ����ִ�.
            return false;
        }

        //ġŲ�� ��µ� ����
        chickenCnt = pChickenCnt;
        chickenState = pChickenState;
        source0 = spicy0;
        source1 = spicy1;
        soundMgr.PlaySE(Sound.Put_SE);

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
        //ġŲ�� ǥ��
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
        //ġŲ ������ŭ�� ġŲ�� ��������.
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
        //�ʱ�ȭ ��
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
