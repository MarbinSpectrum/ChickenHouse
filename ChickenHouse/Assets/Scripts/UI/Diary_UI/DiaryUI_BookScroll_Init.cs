using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaryUI_BookScroll_Init : LoopScrollInit
{
    private List<Guest>         guestList = new List<Guest>();
    private List<Transform> guestListObjs = new List<Transform>();

    private List<ChickenSpicy>  spicyList = new List<ChickenSpicy>();
    private List<Transform> spicyListObjs = new List<Transform>();

    [SerializeField] private DiaryUI_BookGuestSlot      guestSlot;
    [SerializeField] private DiaryUI_BookSeasoningSlot  seasoningSlot;
    
    private BookMenu slotMode;

    public void SetGuestList(List<Guest> newList)
    {
        ClearList();

        slotMode = BookMenu.Guest;

        AddList();

        guestList = newList;
        item = guestSlot.gameObject;

        Init(guestList.Count);
    }

    public void SetSpicyList(List<ChickenSpicy> newList)
    {
        ClearList();

        slotMode = BookMenu.Seasoning;

        AddList();

        spicyList = newList;
        item = seasoningSlot.gameObject;

        Init(spicyList.Count);
    }

    private void ClearList()
    {
        switch(slotMode)
        {
            case BookMenu.Guest:
                {
                    guestListObjs.Clear();
                    for (int i = 0; i < loopScrollRect.content.childCount; i++)
                    {
                        Transform trans = loopScrollRect.content.GetChild(i);
                        guestListObjs.Add(trans);
                    }
                    for (int i = 0; i < guestListObjs.Count; i++)
                    {
                        Transform trans = guestListObjs[i];
                        trans.parent = loopScrollRect.content.parent;
                        trans.gameObject.SetActive(false);
                    }
                }
                break;
            case BookMenu.Seasoning:
                {
                    spicyListObjs.Clear();
                    for (int i = 0; i < loopScrollRect.content.childCount; i++)
                    {
                        Transform trans = loopScrollRect.content.GetChild(i);
                        spicyListObjs.Add(trans);
                    }
                    for (int i = 0; i < spicyListObjs.Count; i++)
                    {
                        Transform trans = spicyListObjs[i];
                        trans.parent = loopScrollRect.content.parent;
                        trans.gameObject.SetActive(false);
                    }
                }
                break;

        }
    }

    private void AddList()
    {
        switch (slotMode)
        {
            case BookMenu.Guest:
                {
                    for (int i = 0; i < guestListObjs.Count; i++)
                    {
                        Transform trans = guestListObjs[i];
                        trans.parent = loopScrollRect.content;
                        trans.gameObject.SetActive(true);
                    }
                    guestListObjs.Clear();
                }
                break;
            case BookMenu.Seasoning:
                {
                    for (int i = 0; i < spicyListObjs.Count; i++)
                    {
                        Transform trans = spicyListObjs[i];
                        trans.parent = loopScrollRect.content;
                        trans.gameObject.SetActive(true);
                    }
                    spicyListObjs.Clear();
                }
                break;

        }
    }


    public override void ProvideData(Transform transform, int idx)
    {
        switch(slotMode)
        {
            case BookMenu.Guest:
                {
                    DiaryUI_BookGuestSlot diarySlot = transform.GetComponent<DiaryUI_BookGuestSlot>();
                    if (diarySlot == null)
                        return;

                    if (guestList.Count <= idx)
                        return;

                    Guest guest = guestList[idx];
                    diarySlot.SetData(guest);
                }
                break;
            case BookMenu.Seasoning:
                {
                    DiaryUI_BookSeasoningSlot diarySlot = transform.GetComponent<DiaryUI_BookSeasoningSlot>();
                    if (diarySlot == null)
                        return;

                    if (spicyList.Count <= idx)
                        return;

                    ChickenSpicy spicy = spicyList[idx];
                    diarySlot.SetData(spicy);
                }
                break;
        }

    }
}