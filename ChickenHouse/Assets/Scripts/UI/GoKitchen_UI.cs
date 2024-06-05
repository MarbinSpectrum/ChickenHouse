using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoKitchen_UI : Mgr
{
    [SerializeField] private Animator   animator;
    [SerializeField] private Button     btn;
    [SerializeField] private TutoObj    tutoObj;

    private bool canUse = false;

    private void Awake()
    {
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(() => GoKitchen());
    }

    public void OpenBtn(NoParaDel fun = null)
    {
        //버튼 활성화
        canUse = true;
        animator.SetBool("Open", true);
        fun?.Invoke();
    }

    public void CloseBtn(NoParaDel fun = null)
    {
        //버튼 비활성화
        canUse = false;
        animator.SetBool("Open", false);
        fun?.Invoke();
    }

    public void GoKitchen()
    {
        //주방으로 화면 전환
        //if (canUse == false)
        //    return;

        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if (kitchenMgr.cameraObj.lookArea == LookArea.Kitchen)
            return;

        CloseBtn();
        kitchenMgr.cameraObj.ChangeLook(LookArea.Kitchen, () =>
        {
            kitchenMgr.cameraObj.lookArea = LookArea.Kitchen;
            if(tutoMgr.tutoComplete == false)
            {
                //튜토리얼을 진행안한듯?
                //튜토리얼로 진입
                tutoObj.PlayTuto();
            }
            else
            {
                //kitchenMgr.ui.goCounter.OpenBtn();
            }

        });

        GuestMgr guestMgr = GuestMgr.Instance;
        guestMgr.CloseTalkBox();
        guestMgr.SkipTalk();
        guestMgr.SetSkipTalkBtnState(false);
        guestMgr.SetGotoKitchenBtnState(false);

        kitchenMgr.ui.memo.OpenTriggerBox();
        kitchenMgr.ui.workerUI.OnBox();
    } 
}
