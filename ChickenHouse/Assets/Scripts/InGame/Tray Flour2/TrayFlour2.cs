using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrayFlour2 : Mgr
{
    private const int MAX_CHICKEN_SLOT = 4;
    private const float DEFAULT_SLOT_WIDTH = 9.5f;
    private const float DEFAULT_SLOT_HEIGHT = 3.5f;

    /**닭 갯수 **/
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
        //오브젝트 스프라이트 이미지
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
            //치킨을 드래그해서 가져왔으며
            //슬롯이 비어져있다.
            image.sprite = sprite.canUseSprite;
        }
        else
        {
            //나머지 상태면 이미지가 보이지 않는다.
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
            //해당 상태아니면 안되야됨
            return false;
        }

        //트레이에 올려져있는 닭 증가
        soundMgr.PlaySE(Sound.Put_SE);

        image.sprite = sprite.normalSprite;

        foreach (FlourChicken flourChicken in flourChickens)
        {
            if (flourChicken.isEmpty)
            {
                //빈슬롯에 해당하는 곳에 닭을 넣는다.
                flourChicken.gameObject.SetActive(true);
                flourChicken.isEmpty = false;
                flourChicken.SpawnChicken();

                RefreshSlotCollider();

                if (gameMgr.playData != null && gameMgr.playData.tutoComplete1 == false)
                {
                    //튜토리얼을 진행안한듯?
                    //튜토리얼로 진입
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
        //트레이에 올려져있는 닭 감소
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
        //슬롯 콜라이더 수정하는 부분임
        //트레이 전체에서 드래그 가능할수있도록 수정함

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
            //해당 상태아니면 안되야됨
            return;
        }

        //인스펙터에서 끌어서 사용되는 함수
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
                //튜토리얼을 진행안한듯?
                //튜토리얼로 진입

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