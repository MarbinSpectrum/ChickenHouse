using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoKitchen_UI : Mgr
{
    [SerializeField] private Animator   animator;
    [SerializeField] private Button     btn;
    [SerializeField] private TutoObj    tutoObj0;
    [SerializeField] private TutoObj    tutoObj1;

    private bool canUse = false;

    private void Awake()
    {
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(() => GoKitchen());
    }

    private void Update()
    {
        GuestSystem guestSystem = GuestSystem.Instance;
        if (guestSystem == null)
        {
            Debug.LogError("[Unity Error - GoKitchen_UI.Update()]: guestSystem is Null");
            return;
        }

        if(CheckMode.IsWindow() && gameMgr.stopGame == false && guestSystem.closeShop == false)
        {
            //PC���������� �����̽��ٷ� �ֹ����� �̵� ����
            if(Input.GetKeyUp(KeyCode.Space))
            {
                GoKitchen();
            }
        }
    }

    public void OpenBtn(NoParaDel fun = null)
    {
        //��ư Ȱ��ȭ
        canUse = true;
        animator.SetBool("Open", true);
        fun?.Invoke();
    }

    public void CloseBtn(NoParaDel fun = null)
    {
        //��ư ��Ȱ��ȭ
        canUse = false;
        animator.SetBool("Open", false);
        fun?.Invoke();
    }

    public void GoKitchen()
    {
        //�ֹ����� ȭ�� ��ȯ
        if (canUse == false)
            return;

        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if (kitchenMgr.cameraObj.lookArea == LookArea.Kitchen)
            return;

        CloseBtn();
        if (gameMgr.playData != null && gameMgr.playData.tutoComplete1 == false)
            tutoObj0.CloseTuto();

        kitchenMgr.ui.showKeyBoardKey.ActKeyBoard(0);
        kitchenMgr.cameraObj.ChangeLook(LookArea.Kitchen, () =>
        {
            kitchenMgr.ui.showKeyBoardKey.ActKeyBoard(1);
            if (gameMgr.playData != null && gameMgr.playData.tutoComplete1 == false && tutoMgr.nowTuto == Tutorial.Tuto_0)
            {
                //Ʃ�丮��� ����
                tutoObj1.PlayTuto();
            }
            else
            {
                //kitchenMgr.ui.goCounter.OpenBtn();
            }

        });

        GuestSystem guestMgr = GuestSystem.Instance;
        guestMgr.CloseTalkBox();
        guestMgr.SkipTalk();
        guestMgr.SetSkipTalkBtnState(false);
        guestMgr.SetGotoKitchenBtnState(false);

        kitchenMgr.ui.memo.OpenTriggerBox();
        kitchenMgr.ui.workerUI.OnBox();
    } 
}
