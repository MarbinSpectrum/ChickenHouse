using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpicyReady : Mgr
{
    public struct SpicyList
    {
        public RectTransform spicyListLock;
        public RectTransform spicyListContents;
        public KitchenSpicySlot spicySlot;
        [System.NonSerialized] public List<KitchenSpicySlot> slotList;
    }
    [SerializeField] private SpicyList                  spicyList;
    [SerializeField] private List<KitchenSpicySetSlot>  spicySetSlots;
    [SerializeField] private TextMeshProUGUI            spicyCnt;
    [SerializeField] private KitchenSpicyInfo           spicyInfo;
    [SerializeField] private MenuReady menuReady;
    private ChickenSpicy selectSpicy;

    public void SetUI()
    {
        PlayData playData = gameMgr.playData;
        if (playData == null)
            return;

        UpdateSpicyList();
        UpdateSpicySet();
    }

    private void UpdateSpicyList()
    {
        PlayData playData = gameMgr.playData;
        if (playData == null)
            return;

        int setSpicyCnt = 0;
        List<ChickenSpicy> spicys = new List<ChickenSpicy>();
        HashSet<ChickenSpicy> setSpicy = new HashSet<ChickenSpicy>();
        for (ChickenSpicy eSpicy = ChickenSpicy.Hot; eSpicy < ChickenSpicy.MAX; eSpicy++)
        {
            if (playData.HasRecipe(eSpicy) == false)
            {
                //보유하지 레시피
                continue;
            }

            for (MenuSetPos spicyPos = MenuSetPos.Spicy0;
                spicyPos < MenuSetPos.SpicyMAX; spicyPos++)
            {
                ChickenSpicy spicy = (ChickenSpicy)playData.spicy[(int)spicyPos];
                if (eSpicy == spicy)
                {
                    //해당 양념은 이미 배치되어 있다.
                    setSpicy.Add(spicy);
                    setSpicyCnt++;
                    break;
                }
            }

            spicys.Add(eSpicy);
        }

        spicyList.spicyListLock.gameObject.SetActive(setSpicyCnt == (int)MenuSetPos.SpicyMAX);

        spicyList.slotList ??= new List<KitchenSpicySlot>();
        foreach (KitchenSpicySlot slot in spicyList.slotList)
            slot.gameObject.SetActive(false);
        for (int i = 0; i < spicys.Count; i++)
        {
            while (spicyList.slotList.Count <= i)
            {
                KitchenSpicySlot spicySlot =
                    Instantiate(spicyList.spicySlot, spicyList.spicyListContents);
                spicyList.slotList.Add(spicySlot);
            }
            ChickenSpicy eSpicy = spicys[i];
            bool isSelect = (eSpicy == selectSpicy);
            bool isParty = setSpicy.Contains(eSpicy);
            spicyList.slotList[i].SetUI(eSpicy, isSelect, isParty, () =>
            {
                menuReady.AllCancel();
                if (eSpicy == ChickenSpicy.None)
                    return;
                if (eSpicy == selectSpicy)
                    selectSpicy = ChickenSpicy.None;
                else
                    selectSpicy = eSpicy;
                spicyInfo.SetUI(selectSpicy);
                UpdateSpicyList();
            });
        }
    }

    private void UpdateSpicySet()
    {
        PlayData playData = gameMgr.playData;
        if (playData == null)
            return;

        int setSpicyCnt = 0;
        for (MenuSetPos spicyPos = MenuSetPos.Spicy0;
                spicyPos < MenuSetPos.SpicyMAX; spicyPos++)
        {
            MenuSetPos tempSpicyPos = spicyPos;
            ChickenSpicy eSpicy = (ChickenSpicy)playData.spicy[(int)spicyPos];
            spicySetSlots[(int)spicyPos].SetUI(eSpicy, () =>
            {
                if (playData.spicy[(int)tempSpicyPos] != (int)ChickenSpicy.None)
                {
                    playData.spicy[(int)tempSpicyPos] = (int)EWorker.None;
                    menuReady.AllCancel();
                    spicyInfo.SetUI(selectSpicy);
                    return;
                }

                for (MenuSetPos setCheck = MenuSetPos.Spicy0;
                    setCheck < MenuSetPos.SpicyMAX; setCheck++)
                {
                    ChickenSpicy spicy = (ChickenSpicy)playData.spicy[(int)setCheck];
                    if (selectSpicy == spicy)
                    {
                        //해당 양념은 이미 배치되어 있다.
                        selectSpicy = ChickenSpicy.None;
                        menuReady.AllCancel();
                        return;
                    }
                }

                if (selectSpicy == ChickenSpicy.None)
                    return;

                playData.spicy[(int)tempSpicyPos] = (int)selectSpicy;
                menuReady.AllCancel();
                spicyInfo.SetUI(selectSpicy);
            });
            if (eSpicy != ChickenSpicy.None)
                setSpicyCnt++;

        }
        spicyCnt.text = string.Format("({0}/{1})", setSpicyCnt, (int)MenuSetPos.SpicyMAX);
    }

    public void SelectSpicyCancelBtn()
    {
        selectSpicy = ChickenSpicy.None;
        UpdateSpicyList();
        UpdateSpicySet();
        spicyInfo.SetUI(selectSpicy);
    }
}
