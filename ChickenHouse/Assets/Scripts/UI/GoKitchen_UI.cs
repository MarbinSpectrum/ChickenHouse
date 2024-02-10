using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoKitchen_UI : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Button btn;

    private void Awake()
    {
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(() => GoCounter());
    }

    public void OpenBtn(NoParaDel fun = null)
    {
        //��ư Ȱ��ȭ
        animator.SetBool("Open", true);
        fun?.Invoke();
    }

    public void CloseBtn(NoParaDel fun = null)
    {
        //��ư ��Ȱ��ȭ
        animator.SetBool("Open", false);
        fun?.Invoke();
    }

    private void GoCounter()
    {
        //�ֹ����� ȭ�� ��ȯ
        CloseBtn();
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        kitchenMgr.cameraObj.ChangeLook(LookArea.Kitchen);

        GuestMgr guestMgr = GuestMgr.Instance;
        guestMgr.CloseTalkBox();
    }
}
