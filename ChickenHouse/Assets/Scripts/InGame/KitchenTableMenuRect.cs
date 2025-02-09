using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class KitchenTableMenuRect : Mgr
{
    [SerializeField] private List<GameObject> sideMenus;
    [SerializeField] private List<GameObject> sideEmpty;
    [SerializeField] private List<GameObject> drinkMenus;
    [SerializeField] private RectTransform menuRect;
    [SerializeField] private RectTransform tableRect;
    [SerializeField] private RectTransform thisRect;
    [SerializeField] private GridLayoutGroup layoutGroup;
    private void Update()
    {
        int sideActCnt = 0;
        for (int i = 0; i < sideMenus.Count; i++)
            if (sideMenus[i].gameObject.activeSelf)
                sideActCnt++;
        foreach (GameObject obj in sideEmpty)
            obj.gameObject.SetActive(false);

        int drinkActCnt = 0;
        for (int i = 0; i < drinkMenus.Count; i++)
            if (drinkMenus[i].gameObject.activeSelf)
                drinkActCnt++;

        int actCnt = Mathf.Max(sideActCnt, drinkActCnt);
        if (actCnt >= 3)
        {
            layoutGroup.constraintCount = 2;
            menuRect.sizeDelta = new Vector2(
                layoutGroup.spacing.x + layoutGroup.cellSize.x * 2, menuRect.sizeDelta.y);
            if (sideActCnt == 1)
                sideEmpty[0].gameObject.SetActive(true);
            else if (sideActCnt == 3)
                sideEmpty[1].gameObject.SetActive(true);
        }
        else
        {
            layoutGroup.constraintCount = 1;
            menuRect.sizeDelta = new Vector2(
                layoutGroup.spacing.x + layoutGroup.cellSize.x, menuRect.sizeDelta.y);
        }

        thisRect.sizeDelta = new Vector2(
               tableRect.sizeDelta.x + 3.5f, thisRect.sizeDelta.y);


    }
}
