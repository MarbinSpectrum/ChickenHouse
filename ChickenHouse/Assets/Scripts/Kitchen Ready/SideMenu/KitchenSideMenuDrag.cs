using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KitchenSideMenuDrag : Mgr
{
    [SerializeField] private Image face;

    public SideMenu sideMenu { private set; get; }

    public void SetUI(SideMenu pSideMenu)
    {
        sideMenu = pSideMenu;
        SideMenuData sideData = subMenuMgr.GetSideMenuData(pSideMenu);
        if (sideData == null)
            return;
        face.sprite = sideData.img;
    }

    private void Update()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(pos.x, pos.y, 0);
    }
}
