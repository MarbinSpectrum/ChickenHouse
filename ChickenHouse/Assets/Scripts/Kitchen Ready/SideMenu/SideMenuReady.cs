using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SideMenuReady : Mgr
{
    public struct SideMenuList
    {
        public RectTransform            sideMenuListLock;
        public RectTransform            sideMenuListContents;
        public KitchenSideMenuSlot      sideMenuSlot;
        [System.NonSerialized] public List<KitchenSideMenuSlot> slotList;
    }
    [SerializeField] private SideMenuList                   sideMenuList;
    [SerializeField] private List<KitchenSideMenuSetSlot>   sideMenuSlots;
    [SerializeField] private TextMeshProUGUI                sideMenuCnt;
    [SerializeField] private KitchenSideMenuInfo            sideMenuInfo;
    [SerializeField] private MenuReady menuReady;
    [SerializeField] private KitchenSideMenuDrag dragObj;

    private SideMenu selectSideMenu;

    public void SetUI()
    {
        PlayData playData = gameMgr.playData;
        if (playData == null)
            return;

        UpdateSideMenuList();
        SelectSideMenuCancelBtn();
    }

    private void UpdateSideMenuList()
    {
        PlayData playData = gameMgr.playData;
        if (playData == null)
            return;

        int setSideMenuCnt = 0;
        List<SideMenu>      sideMenus = new List<SideMenu>();
        HashSet<SideMenu>   setSideMenu = new HashSet<SideMenu>();
        for (SideMenu eSideMenu = SideMenu.Pickle; eSideMenu < SideMenu.MAX; eSideMenu++)
        {
            if (playData.HasSideMenu(eSideMenu) == false)
            {
                //보유하지 레시피
                continue;
            }

            for (MenuSetPos sideMenuPos = MenuSetPos.SideMenu0;
                sideMenuPos < MenuSetPos.SideMenuMAX; sideMenuPos++)
            {
                SideMenu sideMenu = (SideMenu)playData.sideMenu[(int)sideMenuPos];
                if (eSideMenu == sideMenu)
                {
                    //해당 메뉴는 이미 배치되어 있다.
                    setSideMenu.Add(sideMenu);
                    setSideMenuCnt++;
                    break;
                }
            }

            sideMenus.Add(eSideMenu);
        }

        sideMenuList.sideMenuListLock.gameObject.SetActive(setSideMenuCnt == (int)MenuSetPos.SideMenuMAX);

        sideMenuList.slotList ??= new List<KitchenSideMenuSlot>();
        foreach (KitchenSideMenuSlot slot in sideMenuList.slotList)
            slot.gameObject.SetActive(false);
        for (int i = 0; i < sideMenus.Count; i++)
        {
            while (sideMenuList.slotList.Count <= i)
            {
                KitchenSideMenuSlot sideMenuSlot =
                    Instantiate(sideMenuList.sideMenuSlot, sideMenuList.sideMenuListContents);
                sideMenuList.slotList.Add(sideMenuSlot);
            }
            SideMenu eSideMenu = sideMenus[i];
            bool isSelect = (eSideMenu == selectSideMenu);
            bool isParty = setSideMenu.Contains(eSideMenu);
            sideMenuList.slotList[i].SetUI(eSideMenu, isSelect, isParty, () =>
            {
                menuReady.AllCancel();
                if (eSideMenu == SideMenu.None)
                    return;
                if (eSideMenu == selectSideMenu)
                    selectSideMenu = SideMenu.None;
                else
                    selectSideMenu = eSideMenu;
                sideMenuInfo.SetUI(selectSideMenu);
                UpdateSideMenuList();
            },
            () =>
            {
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                dragObj.transform.position = new Vector3(pos.x, pos.y, 0);
                dragObj.SetUI(eSideMenu);
                dragObj.gameObject.SetActive(true);
            },
            () =>
            {
                dragObj.gameObject.SetActive(false);
                dragObj.SetUI(SideMenu.None);
            });
        }
    }

    private void UpdateSideMenuSet()
    {
        PlayData playData = gameMgr.playData;
        if (playData == null)
            return;

        int setSideMenuCnt = 0;
        for (MenuSetPos sideMenuPos = MenuSetPos.SideMenu0;
            sideMenuPos < MenuSetPos.SideMenuMAX; sideMenuPos++)
        {
            MenuSetPos tempSideMenuPos = sideMenuPos;
            SideMenu sideMenu = (SideMenu)playData.sideMenu[(int)sideMenuPos];
            sideMenuSlots[(int)sideMenuPos].SetUI(sideMenu, () =>
            {
                if (playData.sideMenu[(int)tempSideMenuPos] != (int)SideMenu.None)
                {
                    playData.sideMenu[(int)tempSideMenuPos] = (int)SideMenu.None;
                    menuReady.AllCancel();
                    sideMenuInfo.SetUI(selectSideMenu);
                    return;
                }

                for (MenuSetPos setCheck = MenuSetPos.SideMenu0;
                    setCheck < MenuSetPos.SideMenuMAX; setCheck++)
                {
                    SideMenu sideMenu = (SideMenu)playData.sideMenu[(int)setCheck];
                    if (selectSideMenu == sideMenu)
                    {
                        //해당 메뉴는 이미 배치되어 있다.
                        selectSideMenu = SideMenu.None;
                        menuReady.AllCancel();
                        return;
                    }
                }

                if (selectSideMenu == SideMenu.None)
                    return;

                playData.sideMenu[(int)tempSideMenuPos] = (int)selectSideMenu;
                menuReady.AllCancel();
                sideMenuInfo.SetUI(selectSideMenu);
            });
            if (sideMenu != SideMenu.None)
                setSideMenuCnt++;

        }
        sideMenuCnt.text = string.Format("({0}/{1})", setSideMenuCnt, (int)MenuSetPos.SideMenuMAX);
    }

    public void SelectSideMenuCancelBtn()
    {
        selectSideMenu = SideMenu.None;
        UpdateSideMenuList();
        UpdateSideMenuSet();
        sideMenuInfo.SetUI(selectSideMenu);
    }
}
