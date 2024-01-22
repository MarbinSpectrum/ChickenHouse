using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenPack : MonoBehaviour
{
    private int     chickenCnt;
    private bool    isRun;

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
            if (kitchenMgr.dragState == DragState.Fry_Chicken && isRun == false)
            {
                //해당 용기를 사용 가능하다.
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
        if (isRun)
            return false;
        isRun = true;

        chickenCnt = pChickenCnt;

        return true;
    }

    public void Set_ChickenShader(bool pMode , float pLerpValue,bool isSource)
    {
        //치킨 갯수만큼만 치킨을 보여주자.
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
                chickenShader.Set_Shader(pMode, pLerpValue);
            }
        }

    }
}
