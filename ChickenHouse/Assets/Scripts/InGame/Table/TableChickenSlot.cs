using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TableChickenSlot : Mgr
{
    public bool hasChicken { get; private set; }
    /** ġŲ ���� **/
    public ChickenState chickenState { get; private set; }

    /** �ҽ�0 **/
    public ChickenSpicy source0 { get; private set; }
    /** �ҽ�1 **/
    public ChickenSpicy source1 { get; private set; }

    private float chickenLerpValue;
    private bool chickenMode;

    [System.Serializable]
    public struct CHICKEN_OBJ
    {
        //������Ʈ ��������Ʈ �̹���
        public Image[] chickenObj;
        public Image bottomSource;
    }
    [SerializeField] private CHICKEN_OBJ normalChicken;
    [SerializeField] private CHICKEN_OBJ spicyChicken;


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
            //ġŲ�� �������ִ� �����̱��ϴ�.
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
            //Ʃ�丮�� �߿��� �巡�� �Ұ���
            return;
        }

        if (hasChicken == false)
        {
            //ġŲ�� ���ο� �����ؾ��� �巡�װ� ����
            return;
        }

        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if (kitchenMgr.cameraObj.lookArea != LookArea.Kitchen)
        {
            //�ֹ��� �����ִ� ���¿����� ��ȣ �ۿ� ����
            return;
        }

        kitchenMgr.dragState = DragState.Chicken_Pack_Holl;
        boxImg.alpha = 0;

        //������ ��ư�� ǥ�����ش�.
        kitchenMgr.ui.takeOut.OpenBtn();
    }

    public void OnMouseUp()
    {
        if (tutoMgr.tutoComplete == false && tutoMgr.nowTuto != Tutorial.Tuto_12)
        {
            //Ʃ�丮�� �߿��� �巡�� �Ұ���
            return;
        }

        KitchenMgr kitchenMgr = KitchenMgr.Instance;

        if (kitchenMgr.dragState != DragState.Chicken_Pack_Holl)
        {
            //�ش� ������Ʈ�� �巡�����̶�� �ǴܵǾ������� ����
            return;
        }

        //������ ��ư ��Ȱ��
        kitchenMgr.ui.takeOut.CloseBtn();

        //�������� ġŲ �ڽ� ������
        kitchenMgr.dragState = DragState.None;

        boxImg.alpha = 0;
        if (kitchenMgr.mouseArea == DragArea.Trash_Btn)
        {
            //������ ��ưó��
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
            //�̹� ġŲ�� ����
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


        UpdatePack();

        return true;
    }

    public void UpdatePack()
    {
        //ġŲ�� ǥ��
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
        //ġŲ ������ŭ�� ġŲ�� ��������.
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
        //�ʱ�ȭ ��
        hasChicken = false;
        chickenState = ChickenState.NotCook;
        source0 = ChickenSpicy.None;
        source1 = ChickenSpicy.None;

        UpdatePack();
    }
}
