using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeSlot_UI : Mgr, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Animator animator;

    public void OnPointerClick(PointerEventData eventData)
    {
        animator.SetTrigger("Normal");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        animator.SetTrigger("Pressed");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        animator.SetTrigger("Highlighted");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        animator.SetTrigger("Normal");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        animator.SetTrigger("Normal");
    }
}
