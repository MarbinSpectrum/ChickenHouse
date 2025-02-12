using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KitchenTableMenuRect : Mgr
{
    [SerializeField] private RectTransform tableRect;
    [SerializeField] private RectTransform thisRect;

    [SerializeField] private GridLayoutGroup layoutGroup;

    [SerializeField] private List<GameObject> chickenPackslots = new List<GameObject>();

    public void UpdateTable()
    {
        if (gameMgr.playData != null && gameMgr.playData.hasItem[(int)ShopItem.NEW_OIL_ZONE_1])
            chickenPackslots[1].gameObject.SetActive(true);
        else
            chickenPackslots[1].gameObject.SetActive(false);

        if (gameMgr.playData != null && gameMgr.playData.hasItem[(int)ShopItem.NEW_OIL_ZONE_2])
            chickenPackslots[2].gameObject.SetActive(true);
        else
            chickenPackslots[2].gameObject.SetActive(false);

        if (gameMgr.playData != null && gameMgr.playData.hasItem[(int)ShopItem.NEW_OIL_ZONE_3])
            chickenPackslots[3].gameObject.SetActive(true);
        else
            chickenPackslots[3].gameObject.SetActive(false);

        LayoutRebuilder.ForceRebuildLayoutImmediate(tableRect);

        thisRect.sizeDelta = new Vector2(
               tableRect.sizeDelta.x + 3.5f, thisRect.sizeDelta.y);
    }
}
