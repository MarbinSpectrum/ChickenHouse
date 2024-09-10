using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BowlEgg : Mgr
{
    private const int MAX_CHICKEN_SLOT = 1;
    /**�� ���� **/
    private int chickenCnt;

    [System.Serializable]
    public struct SPITE_IMG
    {
        //������Ʈ ��������Ʈ �̹���
        public Sprite normalSprite;
        public Sprite canUseSprite;
    }
    [SerializeField] private SPITE_IMG          sprite;
    [SerializeField] private Image              image;
    //Tuto_2
    [SerializeField] private TutoObj            tutoObj;
    [SerializeField] private BowlChicken        bowlChicken;

    public bool isDrag => bowlChicken.isDrag;

    public void OnMouseEnter()
    {
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if (kitchenMgr.dragState == DragState.Normal && chickenCnt < MAX_CHICKEN_SLOT)
        {
            //ġŲ�� �巡���ؼ� ����������
            //������ ������ִ�.
            image.sprite = sprite.canUseSprite;
        }
        else
        {
            //������ ���¸� �̹����� ������ �ʴ´�.
            image.sprite = sprite.normalSprite;
        }
        kitchenMgr.bowlEgg = this;
        kitchenMgr.mouseArea = DragArea.Bowl_Egg;
    }

    public void OnMouseExit()
    {
        image.sprite = sprite.normalSprite;

        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        kitchenMgr.trayEgg = null;
        kitchenMgr.mouseArea = DragArea.None;
    }

    public bool AddChicken()
    {
        if (IsMax())
            return false;

        //Ʈ���̿� �÷����ִ� �� ����
        chickenCnt++;
        soundMgr.PlaySE(Sound.Put_SE);
        image.sprite = sprite.normalSprite;

        bowlChicken.Init();
        bowlChicken.gameObject.SetActive(true);

        if (IsMax())
        {
            if (tutoMgr.tutoComplete == false)
            {
                //Ʃ�丮���� ������ѵ�?
                //Ʃ�丮��� ����
                tutoObj.PlayTuto();
            }
        }

        return true;
    }

    public void WorkerEggChickenPutAway()
    {
        bowlChicken.WorkerEggChickenPutAway();
    }

    public bool IsMax() => (chickenCnt == MAX_CHICKEN_SLOT);

    public bool RemoveChicken()
    {
        //Ʈ���̿� �÷����ִ� �� ����
        if (chickenCnt <= 0)
            return false;
        chickenCnt--;

        bowlChicken.gameObject.SetActive(false);
        bowlChicken.Init();

        return true;
    }

    public bool CompleteEgg()
    {
        //����� ������ �۾��� �Ϸ����� ���θ� ��ȯ
        return bowlChicken.CompleteEgg();
    }

    public void WorkerDragChicken(float v)
    {
        bowlChicken.WorkerDragChicken(v);
    }
}
