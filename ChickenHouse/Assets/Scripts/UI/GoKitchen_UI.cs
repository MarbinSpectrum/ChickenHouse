using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoKitchen_UI : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Button btn;

    private bool canUse = false;

    private void Awake()
    {
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(() => GoCounter());
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

    private void GoCounter()
    {
        //�ֹ����� ȭ�� ��ȯ
        if (canUse == false)
            return;
        CloseBtn();
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        kitchenMgr.cameraObj.ChangeLook(LookArea.Kitchen, () =>
        {
            kitchenMgr.cameraObj.lookArea = LookArea.Kitchen;
        });

        GuestMgr guestMgr = GuestMgr.Instance;
        guestMgr.CloseTalkBox();
    }
}
