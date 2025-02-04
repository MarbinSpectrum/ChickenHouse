using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenSpicyDrag : Mgr
{
    [SerializeField] private SeasoningFace face;

    public ChickenSpicy spicy { private set; get; }

    public void SetUI(ChickenSpicy pChickenSpicy)
    {
        spicy = pChickenSpicy;
        SpicyData spicyData = spicyMgr.GetSpicyData(pChickenSpicy);
        if (spicyData == null)
            return;
        face.SetUI(spicy);
    }

    private void Update()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(pos.x, pos.y, 0);
    }
}
