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
    [SerializeField] private TableSideMenuSlot    tablePickleSlot;
    [SerializeField] private Transform followTrans;

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
        //카운터로 화면 전환
        if (canUse == false)
            return;

        if (gameMgr.playData.tutoComplete1 == false)
            return;

        GuestSystem guestMgr = GuestSystem.Instance;
        if (guestMgr.guestcnt <= 0)
            return;

        soundMgr.PlaySE(Sound.NewOrder_SE);

        CloseBtn();
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        kitchenMgr.ui.showKeyBoardKey.ActKeyBoard(0);
        kitchenMgr.cameraObj.ChangeLook(LookArea.Counter, () =>
        {
            kitchenMgr.ui.showKeyBoardKey.ActKeyBoard(2);
            //guestMgr.ui.goKitchen.OpenBtn();
        });

        kitchenMgr.ui.memo.CloseTriggerBox();
        kitchenMgr.ui.workerUI.OffBox();
    }
}
