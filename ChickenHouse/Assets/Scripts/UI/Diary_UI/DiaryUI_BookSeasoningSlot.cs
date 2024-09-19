using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DiaryUI_BookSeasoningSlot : Mgr, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Material               notHasMat;
    [SerializeField] private SeasoningFace          seasoningFace;
    [SerializeField] private Image                  selectImg;
    [SerializeField] private DiaryUI_Book           diary;
    [SerializeField] private LoopVerticalScrollRect scrollRect;

    /** 슬롯 클릭 이벤트 **/
    private NoParaDel clickEvent;

    public void SetData(ChickenSpicy pChickenSpicy)
    {
        seasoningFace.SetUI(pChickenSpicy);

        bool isAct = BookMgr.IsActSpicy(pChickenSpicy);

        //얻은 양념은 이미지 활성화
        Image img = seasoningFace.GetNowImg();
        img.material = isAct ? null : notHasMat;

        selectImg.enabled = (diary.selectSpicySlot == pChickenSpicy);
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
