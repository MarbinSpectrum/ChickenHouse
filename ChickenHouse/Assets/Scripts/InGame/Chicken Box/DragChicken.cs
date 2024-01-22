using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragChicken : Mgr
{
    [SerializeField] private SpriteRenderer chickenImg;

    [System.Serializable]
    public struct CHICKEN_SPRITE
    {
        public Sprite normal;
        public Sprite egg;
        public Sprite flour;
    }
    [SerializeField] private CHICKEN_SPRITE chickenSprite;

    private void Update()
    {
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        switch (kitchenMgr.dragState)
        {
            case DragState.None:
            case DragState.DontDrag:
                {
                    //�ƹ��͵� �������� ���� ����
                    //�巡���� �̹����� ������ �ʴ´�.
                    chickenImg.enabled = false;
                }
                return;
            case DragState.Fry_Chicken:
            case DragState.Chicken_Strainter:
                {
                    //ġŲ�� �ƴѰ��� �巡���� ���
                    chickenImg.enabled = false;
                }
                return;
            case DragState.Normal:
                {
                    //ġŲ�� �巡���� ����
                    chickenImg.sprite = chickenSprite.normal;
                }
                break;
            case DragState.Egg:
                {
                    //����� ���� ġŲ�� �巡���� ����
                    chickenImg.sprite = chickenSprite.egg;
                }
                break;
            case DragState.Flour:
                {
                    //�ж󱸸� ���� ġŲ�� �巡���� ����
                    chickenImg.sprite = chickenSprite.flour;
                }
                break;
        }

        //�̹��� Ȱ��ȭ
        chickenImg.enabled = true;

        //ġŲ�̹����� �巡�� ����Ʈ�� �̵�
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(pos.x, pos.y, 0);
    }
}
