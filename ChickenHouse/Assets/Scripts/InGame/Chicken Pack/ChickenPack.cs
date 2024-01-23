using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenPack : MonoBehaviour
{
    /** ����ִ� ġŲ ���� **/
    private int     chickenCnt;
    /** ġŲ ��� ���� **/
    private bool    hasSource;
    /** ġŲ �� ���� **/
    private bool    hasRadish;

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
            if (kitchenMgr.dragState == DragState.Fry_Chicken 
                && chickenCnt <= 0)
            {
                //ġŲ�� ����Ǿ����� ����
                //�ش� ��⸦ ��� �����ϴ�.
                sprite.spriteImg.sprite = sprite.canUseSprite;
            }
            else if (kitchenMgr.dragState == DragState.Chicken_Source 
                && chickenCnt > 0 && hasSource == false)
            {
                //ġŲ�� �������
                //ġŲ �ҽ� ��� ����
                sprite.spriteImg.sprite = sprite.canUseSprite;
            }
            else if (kitchenMgr.dragState == DragState.Chicken_Radish
                && hasRadish == false)
            {
                //ġŲ ���� �ȵ�����
                //ġŲ���� ���� �� ����
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
        if (chickenCnt > 0)
        {
            //�̹� ġŲ�� ����ִ�.
            return false;
        }

        //ġŲ�� ��µ� ����
        chickenCnt = pChickenCnt;

        return true;
    }

    public bool AddChickenSource()
    {
        if (chickenCnt <= 0)
        {
            //ġŲ�� ����.
            return false;
        }

        if (hasSource)
        {
            //ġŲ�� �ҽ��� �̹� �ѷ����ִ�.
            return false;
        }
        hasSource = true;

        return true;
    }

    public bool AddChickenRadish()
    {
        if(hasRadish)
        {
            //�̹� ġŲ���� ����ִ�.
            return false;
        }
        hasRadish = true;

        return true;
    }

    public void Show_Chicken(bool isSource)
    {
        //ġŲ�� ǥ��
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
        //ġŲ ������ŭ�� ġŲ�� ��������.
        for (int i = 0; i < normalChicken.chickenObj.Length; i++)
        {
            bool actChicken = (i < chickenCnt);
            Chicken_Shader chickenShader = normalChicken.chickenObj[i].GetComponent<Chicken_Shader>();
            chickenShader.gameObject.SetActive(actChicken);
            chickenShader.Set_Shader(pMode, pLerpValue);
        }
    }
}
