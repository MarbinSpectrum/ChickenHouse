using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaryUI_BookScroll_Init : LoopScrollInit
{
    [SerializeField] private DiaryUI_Book               diary;

    private BookMenu slotMode;

    public void SetMode(BookMenu pBookMenu)
    {
        slotMode = pBookMenu;

        switch (slotMode)
        {
            case BookMenu.Guest:
                {
                    int cnt = Mathf.CeilToInt(diary.guestList.Count / (float)4);
                    Init(cnt);
                }
                break;
            case BookMenu.Seasoning:
                {
                    int cnt = Mathf.CeilToInt(diary.spicyList.Count / (float)4);
                    Init(cnt);
                }
                break;
            case BookMenu.Etc:
                {
                    int cnt = diary.etcList.Count;
                    Init(cnt);
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
                    DiaryUI_BookScrollObj diarySlot = transform.GetComponent<DiaryUI_BookScrollObj>();
                    if (diarySlot == null)
                        return;

                    List<Guest> guests = new List<Guest>();
                    guests.Add(Guest.None);
                    guests.Add(Guest.None);
                    guests.Add(Guest.None);
                    guests.Add(Guest.None);

                    for(int i = 0; i < 4; i++)
                    {
                        int newIdx = idx * 4 + i;
                        if (newIdx >= diary.guestList.Count)
                            break;
                        guests[i] = diary.guestList[newIdx];
                    }

                    diarySlot.SetData(
                        guests[0], () =>
                        {
                            diary.SetSelectGuest(guests[0]);
                            loopScrollRect.RefillCells();
                        },
                        guests[1], () =>
                        {
                            diary.SetSelectGuest(guests[1]);
                            loopScrollRect.RefillCells();
                        },
                        guests[2], () =>
                        {
                            diary.SetSelectGuest(guests[2]);
                            loopScrollRect.RefillCells();
                        },
                        guests[3], () =>
                        {
                            diary.SetSelectGuest(guests[3]);
                            loopScrollRect.RefillCells();
                        });

                }
                break;
            case BookMenu.Seasoning:
                {
                    DiaryUI_BookScrollObj diarySlot = transform.GetComponent<DiaryUI_BookScrollObj>();
                    if (diarySlot == null)
                        return;

                    List<ChickenSpicy> spicys = new List<ChickenSpicy>();
                    spicys.Add(ChickenSpicy.Not);
                    spicys.Add(ChickenSpicy.Not);
                    spicys.Add(ChickenSpicy.Not);
                    spicys.Add(ChickenSpicy.Not);

                    for (int i = 0; i < 4; i++)
                    {
                        int newIdx = idx * 4 + i;
                        if (newIdx >= diary.spicyList.Count)
                            break;
                        spicys[i] = diary.spicyList[newIdx];
                    }

                    diarySlot.SetData(
                        spicys[0], () =>
                        {
                            diary.SetSelectSeasoning(spicys[0]);
                            loopScrollRect.RefillCells();
                        },
                        spicys[1], () =>
                        {
                            diary.SetSelectSeasoning(spicys[1]);
                            loopScrollRect.RefillCells();
                        },
                        spicys[2], () =>
                        {
                            diary.SetSelectSeasoning(spicys[2]);
                            loopScrollRect.RefillCells();
                        },
                        spicys[3], () =>
                        {
                            diary.SetSelectSeasoning(spicys[3]);
                            loopScrollRect.RefillCells();
                        });
                }
                break;
            case BookMenu.Etc:
                {
                    DiaryUI_BookScrollObj diarySlot = transform.GetComponent<DiaryUI_BookScrollObj>();
                    if (diarySlot == null)
                        return;

                    diarySlot.SetData(diary.etcList[idx]);
                }
                break;
        }

    }
}