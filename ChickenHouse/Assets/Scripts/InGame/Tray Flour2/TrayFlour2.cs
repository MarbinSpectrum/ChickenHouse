using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrayFlour2 : Mgr
{
    private const int MAX_CHICKEN_SLOT = 4;
    private const float DEFAULT_SLOT_WIDTH = 9.5f;
    private const float DEFAULT_SLOT_HEIGHT = 3.5f;

    /**�� ���� **/
    public int chickenCnt
    {
        get
        {
            int cnt = 0;
            foreach (FlourChicken flourChicken in flourChickens)
            {
                if (flourChicken.isEmpty)
                    continue;
                cnt++;
            }
            return cnt;
        }


    }

    [System.Serializable]
    public struct SPITE_IMG
    {
        //������Ʈ ��������Ʈ �̹���
        public Sprite normalSprite;
        public Sprite canUseSprite;
    }
    [SerializeField] private SPITE_IMG sprite;
    [SerializeField] private Image image;
    [SerializeField] private FlourChicken[] flourChickens;
    [SerializeField] private TutoObj tutoObj;
    [SerializeField] private TutoObj tutoObj2;
    [SerializeField] private TutoObj tutoObj3;
    [SerializeField] private ParticleSystem particleSystem;

    public void OnMouseEnter()
    {
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if (kitchenMgr.dragState == DragState.Egg && chickenCnt < MAX_CHICKEN_SLOT)
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
        kitchenMgr.trayFlour2 = this;
        kitchenMgr.mouseArea = DragArea.Tray_Flour2;
    }

    public void OnMouseExit()
    {
        image.sprite = sprite.normalSprite;

        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        kitchenMgr.trayFlour = null;
        kitchenMgr.mouseArea = DragArea.None;
    }

    public bool AddChicken()
    {
        if (IsMax())
            return false;

        if (gameMgr.playData != null && gameMgr.playData.tutoComplete1 == false && (tutoMgr.nowTuto == Tutorial.Tuto_3 || tutoMgr.nowTuto == Tutorial.Tuto_5) == false)
        {
            //�ش� ���¾ƴϸ� �ȵǾߵ�
            return false;
        }

        //Ʈ���̿� �÷����ִ� �� ����
        soundMgr.PlaySE(Sound.Put_SE);

        image.sprite = sprite.normalSprite;

        foreach (FlourChicken flourChicken in flourChickens)
        {
            if (flourChicken.isEmpty)
            {
                //�󽽷Կ� �ش��ϴ� ���� ���� �ִ´�.
                flourChicken.gameObject.SetActive(true);
                flourChicken.isEmpty = false;
                flourChicken.SpawnChicken();

                RefreshSlotCollider();

                if (gameMgr.playData != null && gameMgr.playData.tutoComplete1 == false)
                {
                    //Ʃ�丮���� ������ѵ�?
                    //Ʃ�丮��� ����
                    tutoObj.PlayTuto();
                }

                return true;
            }
        }

        return false;
    }

    public bool IsMax() => (chickenCnt == MAX_CHICKEN_SLOT);

    public bool HasChicken()
    {
        int cnt = 0;
        foreach (FlourChicken flourChicken in flourChickens)
        {
            if (flourChicken.isEmpty)
                continue;
            if (flourChicken.isDrag)
                continue;
            cnt++;
        }
        return cnt > 0;
    }

    public bool IsComplete()
    {
        foreach (FlourChicken flourChicken in flourChickens)
        {
            if (flourChicken.isEmpty)
                continue;
            if (flourChicken.isDrag)
                continue;
            if (flourChicken.IsComplete())
                return true;
        }
        return false;
    }

    public bool HasNotComplete()
    {
        foreach (FlourChicken flourChicken in flourChickens)
        {
            if (flourChicken.isEmpty)
                continue;
            if (flourChicken.isDrag)
                continue;
            if (flourChicken.IsComplete() == false)
                return true;
        }
        return false;
    }

    public bool RemoveChicken()
    {
        //Ʈ���̿� �÷����ִ� �� ����
        if (chickenCnt <= 0)
            return false;

        RefreshSlotCollider();

        return true;
    }

    public void WorkerRemoveChicken()
    {
        foreach (FlourChicken flourChicken in flourChickens)
        {
            if (flourChicken.isEmpty)
                continue;
            if (flourChicken.isDrag)
                continue;
            if (flourChicken.IsComplete() == false)
                continue;
            flourChicken.RemoveChicken();
            break;
        }
    }

    public void RefreshSlotCollider()
    {
        //���� �ݶ��̴� �����ϴ� �κ���
        //Ʈ���� ��ü���� �巡�� �����Ҽ��ֵ��� ������

        bool frontCheck = false;
        for (int i = 0; i < 4; i++)
        {
            if (flourChickens[i].isEmpty)
                continue;
            float headValue = 0;
            float tailValue = 0;
            if (frontCheck == false)
            {
                frontCheck = true;

                for (int j = 0; j < i - 1; j++)
                {
                    if (flourChickens[j].isEmpty == false)
                        break;
                    headValue++;
                }
            }

            for (int j = i + 1; j < MAX_CHICKEN_SLOT; j++)
            {
                if (flourChickens[j].isEmpty == false)
                    break;
                tailValue++;
            }

            Vector2 newPos = new Vector2(0, (headValue * (headValue + 1) / 2 - tailValue * (tailValue + 1) / 2) / (headValue + 1 + tailValue)) * DEFAULT_SLOT_HEIGHT;
            Vector2 newSize = new Vector2(DEFAULT_SLOT_WIDTH, (headValue + 1 + tailValue) * DEFAULT_SLOT_HEIGHT);
            flourChickens[i].SetRect(newPos, newSize);
        }
    }

    public void ClickChickens(float v)
    {

        if (gameMgr.playData != null && gameMgr.playData.tutoComplete1 == false && (tutoMgr.nowTuto == Tutorial.Tuto_4 || tutoMgr.nowTuto == Tutorial.Tuto_5) == false)
        {
            //�ش� ���¾ƴϸ� �ȵǾߵ�
            return;
        }

        //�ν����Ϳ��� ��� ���Ǵ� �Լ�
        bool playEffect = false;
        foreach (FlourChicken flourChicken in flourChickens)
        {
            if (flourChicken.isEmpty)
                continue;
            if (flourChicken.isDrag)
            {
                playEffect = true;
                continue;
            }
            if (flourChicken.IsComplete())
                continue;
            playEffect = true;
            flourChicken.ClickChicken(v);
        }
        if (playEffect == false)
            return;
        particleSystem.Play();

        {
            int randomSE = Random.Range(0, 3);
            switch(randomSE)
            {
                case 0:
                    soundMgr.PlaySE(Sound.Flour_0_SE);
                    break;
                case 1:
                    soundMgr.PlaySE(Sound.Flour_1_SE);
                    break;
                case 2:
                    soundMgr.PlaySE(Sound.Flour_2_SE);
                    break;
            }
        }

        if(gameMgr.playData != null && gameMgr.playData.tutoComplete1 == false && tutoMgr.nowTuto == Tutorial.Tuto_4 && IsComplete())
        {
            tutoObj2.PlayTuto();
        }

        if (IsMax())
        {
            bool allComplete = true;
            foreach (FlourChicken flourChicken in flourChickens)
                if (flourChicken.IsComplete() == false)
                    allComplete = false;

            if (gameMgr.playData != null && gameMgr.playData.tutoComplete1 == false && allComplete)
            {
                //Ʃ�丮���� ������ѵ�?
                //Ʃ�丮��� ����

                tutoObj3.PlayTuto();
            }
        }
    }

    public void WorkerFlourChickenPutAway()
    {
        foreach (FlourChicken flourChicken in flourChickens)
        {
            if (flourChicken.isEmpty == false)
                continue;
            if (flourChicken.isDrag)
                continue;
            flourChicken.WorkerFlourChickenPutAway();
            break;
        }
    }

    public bool NowDrag()
    {
        foreach (FlourChicken flourChicken in flourChickens)
        {
            if (flourChicken.isEmpty)
                continue;
            if (flourChicken.isDrag)
                return true;
        }
        return false;
    }
}