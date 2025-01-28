using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DrinkReady : Mgr
{
    public struct DrinkList
    {
        public RectTransform        drinkListLock;
        public RectTransform        drinkContents;
        public KitchenDrinkSlot     drinkSlot;
        [System.NonSerialized] public List<KitchenDrinkSlot> slotList;
    }
    [SerializeField] private DrinkList                   drinkList;
    [SerializeField] private List<KitchenDrinkSetSlot>   drinkSlots;
    [SerializeField] private TextMeshProUGUI             drinkCnt;
    [SerializeField] private KitchenDrinkInfo            drinkInfo;
    [SerializeField] private MenuReady menuReady;
    private Drink selectDrink;

    public void SetUI()
    {
        PlayData playData = gameMgr.playData;
        if (playData == null)
            return;

        UpdateDrinkList();
        UpdateDrinkSet();
    }

    private void UpdateDrinkList()
    {
        PlayData playData = gameMgr.playData;
        if (playData == null)
            return;

        int setDrinkCnt = 0;
        List<Drink> drinkMenus = new List<Drink>();
        HashSet<Drink> setDrink = new HashSet<Drink>();
        for (Drink eDrink = Drink.Cola; eDrink < Drink.MAX; eDrink++)
        {
            if (playData.HasDrink(eDrink) == false)
            {
                //보유하지 음료
                continue;
            }

            for (MenuSetPos drinkPos = MenuSetPos.Drink0;
                drinkPos < MenuSetPos.DrinkMAX; drinkPos++)
            {
                Drink drink = (Drink)playData.drink[(int)drinkPos];
                if (eDrink == drink)
                {
                    //해당 음료는 이미 배치되어 있다.
                    setDrink.Add(drink);
                    setDrinkCnt++;
                    break;
                }
            }

            drinkMenus.Add(eDrink);
        }

        drinkList.drinkListLock.gameObject.SetActive(setDrinkCnt == (int)MenuSetPos.DrinkMAX);

        drinkList.slotList ??= new List<KitchenDrinkSlot>();
        foreach (KitchenDrinkSlot slot in drinkList.slotList)
            slot.gameObject.SetActive(false);
        for (int i = 0; i < drinkMenus.Count; i++)
        {
            while (drinkList.slotList.Count <= i)
            {
                KitchenDrinkSlot drinkSlot =
                    Instantiate(drinkList.drinkSlot, drinkList.drinkContents);
                drinkList.slotList.Add(drinkSlot);
            }
            Drink eDrink = drinkMenus[i];
            bool isSelect = (eDrink == selectDrink);
            bool isParty = setDrink.Contains(eDrink);
            drinkList.slotList[i].SetUI(eDrink, isSelect, isParty, () =>
            {
                menuReady.AllCancel();
                if (eDrink == Drink.None)
                    return;
                if (eDrink == selectDrink)
                    selectDrink = Drink.None;
                else
                    selectDrink = eDrink;
                drinkInfo.SetUI(selectDrink);
                UpdateDrinkList();
            });
        }
    }

    private void UpdateDrinkSet()
    {
        PlayData playData = gameMgr.playData;
        if (playData == null)
            return;

        int setDrinkCnt = 0;
        for (MenuSetPos drinkPos = MenuSetPos.Drink0;
            drinkPos < MenuSetPos.DrinkMAX; drinkPos++)
        {
            MenuSetPos tempDrinkPos = drinkPos;
            Drink drink = (Drink)playData.drink[(int)drinkPos];
            drinkSlots[(int)drinkPos].SetUI(drink, () =>
            {
                if (playData.drink[(int)tempDrinkPos] != (int)Drink.None)
                {
                    playData.drink[(int)tempDrinkPos] = (int)Drink.None;
                    menuReady.AllCancel();
                    drinkInfo.SetUI(selectDrink);
                    return;
                }

                if (selectDrink == Drink.None)
                    return;

                playData.drink[(int)tempDrinkPos] = (int)selectDrink;
                menuReady.AllCancel();
                drinkInfo.SetUI(selectDrink);
            });
            if (drink != Drink.None)
                setDrinkCnt++;

        }
        drinkCnt.text = string.Format("({0}/{1})", setDrinkCnt, (int)MenuSetPos.DrinkMAX);
    }

    public void SelectDrinkCancelBtn()
    {
        selectDrink = Drink.None;
        UpdateDrinkList();
        UpdateDrinkSet();
        drinkInfo.SetUI(selectDrink);
    }
}
