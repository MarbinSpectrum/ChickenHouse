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
        //버튼 활성화
        animator.SetBool("Open", true);
        fun?.Invoke();
    }

    public void CloseBtn(NoParaDel fun = null)
    {
        //버튼 비활성화
        animator.SetBool("Open", false);
        fun?.Invoke();
    }

    private void GoCounter()
    {
        //주방으로 화면 전환
        CloseBtn();
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        kitchenMgr.cameraObj.ChangeLook(LookArea.Kitchen);

        GuestMgr guestMgr = GuestMgr.Instance;
        guestMgr.CloseTalkBox();
    }
}
