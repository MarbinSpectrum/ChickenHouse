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
                    //아무것도 선택하지 않은 상태
                    //드래그한 이미지가 보이지 않는다.
                    chickenImg.enabled = false;
                }
                return;
            case DragState.Fry_Chicken:
            case DragState.Chicken_Strainter:
                {
                    //치킨이 아닌것을 드래그한 경우
                    chickenImg.enabled = false;
                }
                return;
            case DragState.Normal:
                {
                    //치킨을 드래그한 상태
                    chickenImg.sprite = chickenSprite.normal;
                }
                break;
            case DragState.Egg:
                {
                    //계란물 묻힌 치킨을 드래그한 상태
                    chickenImg.sprite = chickenSprite.egg;
                }
                break;
            case DragState.Flour:
                {
                    //밀라구를 묻힌 치킨을 드래그한 상태
                    chickenImg.sprite = chickenSprite.flour;
                }
                break;
        }

        //이미지 활성화
        chickenImg.enabled = true;

        //치킨이미지를 드래그 포인트로 이동
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(pos.x, pos.y, 0);
    }
}
