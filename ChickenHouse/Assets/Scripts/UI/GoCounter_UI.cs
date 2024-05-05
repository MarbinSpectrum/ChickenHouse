using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoCounter_UI : Mgr
{
    [SerializeField] private Animator   animator;
    [SerializeField] private Button     btn;
    [SerializeField] private ChickenPack        tableChicken;
    [SerializeField] private TableDrinkSlot     tableDrinkSlot;
    [SerializeField] private TablePickleSlot    tablePickleSlot;
    [SerializeField] private Transform followTrans;
    [SerializeField] private TutoObj tutoObj;

    private RectTransform   rect        = null;

    private bool canUse = false;

    private void Awake()
    {
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(() => GoCounter());
    }



    public void OpenBtn(NoParaDel fun = null)
    {
        //버튼 활성화
        animator.SetBool("Open", true);
        canUse = true;
        fun?.Invoke();
    }

    public void CloseBtn(NoParaDel fun = null)
    {
        //버튼 비활성화
        animator.SetBool("Open", false);
        canUse = false;
        fun?.Invoke();
    }

    private void GoCounter()
    {
        if(tutoMgr.tutoComplete == false)
        {
            tutoObj.CloseTuto();
        }

        //카운터로 화면 전환
        if (canUse == false)
            return;

        CloseBtn();
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        GuestMgr guestMgr = GuestMgr.Instance;
        kitchenMgr.cameraObj.ChangeLook(LookArea.Counter, () =>
        {
            //Drink drink = tableDrinkSlot.hasDrink ? Drink.Cola : Drink.None;
            //SideMenu sideMenu = tablePickleSlot.hasPickle ? SideMenu.None : SideMenu.None;
            //ChickenSpicy spicy0 = (ChickenSpicy)Mathf.Min((int)tableChicken.source0, (int)tableChicken.source1);
            //ChickenSpicy spicy1 = (ChickenSpicy)Mathf.Max((int)tableChicken.source0, (int)tableChicken.source1);
            //GuestMgr guestMgr = GuestMgr.Instance;
            //guestMgr.GiveChicken(spicy0, spicy1, tableChicken.chickenState,
            //  drink, sideMenu);

            //tableChicken.Init();
            //tableDrinkSlot.Init();
            //tablePickleSlot.Init();
            //kitchenMgr.ui.memo.RemoveMemo();
            guestMgr.ui.goKitchen.OpenBtn();
        });


        kitchenMgr.cameraObj.lookArea = LookArea.Counter;
        kitchenMgr.ui.memo.CloseTriggerBox();
    }
}
