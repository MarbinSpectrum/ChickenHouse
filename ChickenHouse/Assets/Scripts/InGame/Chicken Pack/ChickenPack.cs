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
        //������Ʈ ��������Ʈ �̹���
        public SpriteRenderer   spriteImg;
        public Sprite           normalSprite;
        public Sprite           canUseSprite;
    }

    [System.Serializable]
    public struct CHICKEN_OBJ
    {
        //������Ʈ ��������Ʈ �̹���
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
                //�ش� ��⸦ ��� �����ϴ�.
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
        //ġŲ ������ŭ�� ġŲ�� ��������.
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
