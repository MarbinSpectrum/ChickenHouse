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
    /** ���� Ŭ�� �̺�Ʈ **/
    private NoParaDel clickEvent;

    public void SetData(Guest pGuest)
    {
        guestFace.SetUI(pGuest);

        bool isAct = BookMgr.IsActGuest(pGuest);

        //������ �մ��� �̹��� Ȱ��ȭ
        Image img = guestFace.GetNowImg();
        img.material = isAct ? null : notHasMat;

        selectImg.enabled = (diary.selectGuestSlot == pGuest);
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
