using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class KitchenTableMenuRect : Mgr
{
    [SerializeField] private RectTransform menuRect;
    [SerializeField] private RectTransform tableRect;
    [SerializeField] private RectTransform thisRect;

    [SerializeField] private GridLayoutGroup layoutGroup;
    private void Update()
    {
        thisRect.sizeDelta = new Vector2(
               tableRect.sizeDelta.x + 3.5f, thisRect.sizeDelta.y);
    }
}
