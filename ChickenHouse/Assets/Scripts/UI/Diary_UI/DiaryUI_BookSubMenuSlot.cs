using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DiaryUI_BookSubMenuSlot : Mgr, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Material       notHasMat;
    [SerializeField] private Image          face;
    [SerializeField] private Image          selectImg;
    [SerializeField] private DiaryUI_Book   diary;
    [SerializeField] private LoopVerticalScrollRect scrollRect;

    /** ���� Ŭ�� �̺�Ʈ **/
    private NoParaDel clickEvent;

    public void SetData(Drink pDrink)
    {
        DrinkData drinkData = subMenuMgr.GetDrinkData(pDrink);

        face.sprite = drinkData.img;

        bool isAct = BookMgr.IsActDrink(pDrink);

        //���� ����� �̹��� Ȱ��ȭ
        face.material = isAct ? null : notHasMat;
        face.GetComponent<RectTransform>().sizeDelta = new Vector2(55, 70);
        selectImg.enabled = (diary.selectDrinkSlot == pDrink);
    }

    public void SetData(SideMenu pSideMenu)
    {
        SideMenuData sideMenuData = subMenuMgr.GetSideMenuData(pSideMenu);

        face.sprite = sideMenuData.img;

        bool isAct = BookMgr.IsActSideMenu(pSideMenu);

        //���� ����� �̹��� Ȱ��ȭ
        face.material = isAct ? null : notHasMat;
        face.GetComponent<RectTransform>().sizeDelta = new Vector2(55, 55);
        selectImg.enabled = (diary.selectSideMenuSlot == pSideMenu);
    }

    public void SetClickEvent(NoParaDel fun) => clickEvent = fun;

    public void RunClickEvent()
    {
        //�ν����ͷ� ��� ����ϴ� �Լ���
        clickEvent?.Invoke();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        scrollRect.OnBeginDrag(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        scrollRect.OnEndDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        scrollRect.OnDrag(eventData);
    }
}
