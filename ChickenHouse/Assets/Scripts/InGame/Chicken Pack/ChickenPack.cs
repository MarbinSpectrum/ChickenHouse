using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenPack : MonoBehaviour
{
    /** 담겨있는 치킨 갯수 **/
    private int     chickenCnt;
    /** 치킨 양념 여부 **/
    private bool    hasSource;
    /** 치킨 무 여부 **/
    private bool    hasRadish;

    [System.Serializable]
    public struct SPITE_IMG
    {
        //오브젝트 스프라이트 이미지
        public SpriteRenderer   spriteImg;
        public Sprite           normalSprite;
        public Sprite           canUseSprite;
    }

    [System.Serializable]
    public struct CHICKEN_OBJ
    {
        //오브젝트 스프라이트 이미지
        public GameObject           group;
        public GameObject[]         chickenObj;
    }

    [SerializeField] private SPITE_IMG          sprite;
    [SerializeField] private CHICKEN_OBJ        normalChicken;
    [SerializeField] private CHICKEN_OBJ        sourceChicken;

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
                //치킨이 포장되어있지 않음
                //해당 용기를 사용 가능하다.
                sprite.spriteImg.sprite = sprite.canUseSprite;
            }
            else if (kitchenMgr.dragState == DragState.Chicken_Source 
                && chickenCnt > 0 && hasSource == false)
            {
                //치킨이 들어있음
                //치킨 소스 사용 가능
                sprite.spriteImg.sprite = sprite.canUseSprite;
            }
            else if (kitchenMgr.dragState == DragState.Chicken_Radish
                && hasRadish == false)
            {
                //치킨 무가 안들어가있음
                //치킨무를 넣을 수 있음
                sprite.spriteImg.sprite = sprite.canUseSprite;
            }
            else
            {
                //치킨 포장박스를 사용 중이다.
                sprite.spriteImg.sprite = sprite.normalSprite;
            }
        }
        else
        {
            //마우스를 밖으로 내보내면 이펙트 비활성화
            sprite.spriteImg.sprite = sprite.normalSprite;
        }
    }

    public bool PackCkicken(int pChickenCnt)
    {
        if (chickenCnt > 0)
        {
            //이미 치킨이 담겨있다.
            return false;
        }

        //치킨을 담는데 성공
        chickenCnt = pChickenCnt;

        return true;
    }

    public bool AddChickenSource()
    {
        if (chickenCnt <= 0)
        {
            //치킨이 없다.
            return false;
        }

        if (hasSource)
        {
            //치킨이 소스가 이미 뿌려져있다.
            return false;
        }
        hasSource = true;

        return true;
    }

    public bool AddChickenRadish()
    {
        if(hasRadish)
        {
            //이미 치킨무가 들어있다.
            return false;
        }
        hasRadish = true;

        return true;
    }

    public void Show_Chicken(bool isSource)
    {
        //치킨을 표시
        if (isSource)
        {
            normalChicken.group.gameObject.SetActive(false);
            sourceChicken.group.gameObject.SetActive(true);
            for (int i = 0; i < sourceChicken.chickenObj.Length; i++)
            {
                bool actChicken = (i < chickenCnt);
                sourceChicken.chickenObj[i].SetActive(actChicken);
            }
        }
        else
        {
            normalChicken.group.gameObject.SetActive(true);
            sourceChicken.group.gameObject.SetActive(false);
            for (int i = 0; i < normalChicken.chickenObj.Length; i++)
            {
                bool actChicken = (i < chickenCnt);
                Chicken_Shader chickenShader = normalChicken.chickenObj[i].GetComponent<Chicken_Shader>();
                chickenShader.gameObject.SetActive(actChicken);
            }
        }
    }

    public void Set_ChickenShader(bool pMode , float pLerpValue)
    {
        //치킨 갯수만큼만 치킨을 보여주자.
        for (int i = 0; i < normalChicken.chickenObj.Length; i++)
        {
            bool actChicken = (i < chickenCnt);
            Chicken_Shader chickenShader = normalChicken.chickenObj[i].GetComponent<Chicken_Shader>();
            chickenShader.gameObject.SetActive(actChicken);
            chickenShader.Set_Shader(pMode, pLerpValue);
        }
    }
}
