using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KitchenDrinkDrag : Mgr
{
    [SerializeField] private Image face;

    public Drink drink { private set; get; }

    public void SetUI(Drink pDrink)
    {
        drink = pDrink;
        DrinkData drinkData = subMenuMgr.GetDrinkData(pDrink);
        if (drinkData == null)
            return;
        face.sprite = drinkData.img;
    }

    private void Update()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(pos.x, pos.y, 0);
    }
}

