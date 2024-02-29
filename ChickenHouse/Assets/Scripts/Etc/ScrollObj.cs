using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollObj : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    [SerializeField] private ScrollRect scroll;
    public bool isRun = true;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isRun == false)
            return;
        scroll.OnBeginDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isRun == false)
            return;
        scroll.OnDrag(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isRun == false)
            return;
        scroll.OnEndDrag(eventData);
    }
}
