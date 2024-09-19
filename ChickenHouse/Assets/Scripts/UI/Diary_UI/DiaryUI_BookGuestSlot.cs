using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DiaryUI_BookGuestSlot : Mgr, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private GuestFace              guestFace;
    [SerializeField] private Material               notHasMat;
    [SerializeField] private Image                  selectImg;
    [SerializeField] private DiaryUI_Book           diary;
    [SerializeField] private LoopVerticalScrollRect scrollRect;
    /** 슬롯 클릭 이벤트 **/
    private NoParaDel clickEvent;

    public void SetData(Guest pGuest)
    {
        guestFace.SetUI(pGuest);

        bool isAct = BookMgr.IsActGuest(pGuest);

        //만나본 손님은 이미지 활성화
        Image img = guestFace.GetNowImg();
        img.material = isAct ? null : notHasMat;

        selectImg.enabled = (diary.selectGuestSlot == pGuest);
    }

    public void SetClickEvent(NoParaDel fun) => clickEvent = fun;

    public void RunClickEvent()
    {
        //인스펙터로 끌어서 사용하는 함수임
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
