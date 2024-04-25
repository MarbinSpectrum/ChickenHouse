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
        //��ư Ȱ��ȭ
        animator.SetBool("Open", true);
        canUse = true;
        fun?.Invoke();
    }

    public void CloseBtn(NoParaDel fun = null)
    {
        //��ư ��Ȱ��ȭ
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

        //ī���ͷ� ȭ�� ��ȯ
        if (canUse == false)
            return;

        CloseBtn();
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        kitchenMgr.cameraObj.ChangeLook(LookArea.Counter,()=>
        {
            Drink drink = tableDrinkSlot.hasDrink ? Drink.Cola : Drink.None;
            SideMenu sideMenu = tablePickleSlot.hasPickle ? SideMenu.None : SideMenu.None;
            GuestMgr guestMgr = GuestMgr.Instance;
            guestMgr.GiveChicken(tableChicken.source0, tableChicken.source1, tableChicken.chickenState,
              drink, sideMenu);

            tableChicken.Init();
            tableDrinkSlot.Init();
            tablePickleSlot.Init();
        });
        kitchenMgr.cameraObj.lookArea = LookArea.Counter;

        kitchenMgr.ui.memo.CloseTriggerBox();
    }
}
