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
        switch (slotMode)
        {
            case BookMenu.Guest:
                {
                    DiaryUI_BookScrollObj diarySlot = transform.GetComponent<DiaryUI_BookScrollObj>();
                    if (diarySlot == null)
                        return;

                    Guest[] guests = new Guest[4] { Guest.None, Guest.None, Guest.None, Guest.None };

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
                            UpdateCells(BookMenu.Guest);
                        },
                        guests[1], () =>
                        {
                            diary.SetSelectGuest(guests[1]);
                            UpdateCells(BookMenu.Guest);
                        },
                        guests[2], () =>
                        {
                            diary.SetSelectGuest(guests[2]);
                            UpdateCells(BookMenu.Guest);
                        },
                        guests[3], () =>
                        {
                            diary.SetSelectGuest(guests[3]);
                            UpdateCells(BookMenu.Guest);
                        });

                }
                break;
            case BookMenu.Seasoning:
                {
                    DiaryUI_BookScrollObj diarySlot = transform.GetComponent<DiaryUI_BookScrollObj>();
                    if (diarySlot == null)
                        return;

                    ChickenSpicy[] spicys = new ChickenSpicy[4] { ChickenSpicy.Not, ChickenSpicy.Not, ChickenSpicy.Not, ChickenSpicy.Not };

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
                            UpdateCells(BookMenu.Seasoning);
                        },
                        spicys[1], () =>
                        {
                            diary.SetSelectSeasoning(spicys[1]);
                            UpdateCells(BookMenu.Seasoning);
                        },
                        spicys[2], () =>
                        {
                            diary.SetSelectSeasoning(spicys[2]);
                            UpdateCells(BookMenu.Seasoning);
                        },
                        spicys[3], () =>
                        {
                            diary.SetSelectSeasoning(spicys[3]);
                            UpdateCells(BookMenu.Seasoning);
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

    public void UpdateCells(BookMenu pBookMenu)
    {
        switch (pBookMenu)
        {
            case BookMenu.Guest:
                {
                    foreach (GameObject obj in objList)
                    {
                        DiaryUI_BookScrollObj diarySlot = obj.GetComponent<DiaryUI_BookScrollObj>();
                        if (diarySlot == null)
                            continue;
                        diarySlot.UpdateGuestSlot();
                    }
                }
                break;
            case BookMenu.Seasoning:
                {
                    foreach (GameObject obj in objList)
                    {
                        DiaryUI_BookScrollObj diarySlot = obj.GetComponent<DiaryUI_BookScrollObj>();
                        if (diarySlot == null)
                            continue;
                        diarySlot.UpdateSpicySlot();
                    }
                }
                break;
            case BookMenu.Etc:
                {
                    foreach (GameObject obj in objList)
                    {
                        DiaryUI_BookScrollObj diarySlot = obj.GetComponent<DiaryUI_BookScrollObj>();
                        if (diarySlot == null)
                            continue;
                        diarySlot.UpdateEtcSlot();
                    }
                }
                break;
        }

    }
}