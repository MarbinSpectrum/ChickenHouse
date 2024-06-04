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
        //ī���ͷ� ȭ�� ��ȯ
        if (canUse == false)
            return;

        if (tutoMgr.tutoComplete == false)
            return;

        GuestMgr guestMgr = GuestMgr.Instance;
        if (guestMgr.guestcnt <= 0)
            return;

        soundMgr.PlaySE(Sound.NewOrder_SE);

        CloseBtn();
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        guestMgr.SetSkipTalkBtnState(true);

        kitchenMgr.cameraObj.ChangeLook(LookArea.Counter, () =>
        {
            //guestMgr.ui.goKitchen.OpenBtn();
        });


        kitchenMgr.cameraObj.lookArea = LookArea.Counter;
        kitchenMgr.ui.memo.CloseTriggerBox();
        kitchenMgr.ui.workerUI.OffBox();
    }
}
