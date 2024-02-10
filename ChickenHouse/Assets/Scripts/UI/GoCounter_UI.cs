using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class GoCounter_UI : Mgr
{
    [SerializeField] private Animator   animator;
    [SerializeField] private Button     btn;
    [SerializeField] private TableChickenSlot   tableChicken;
    [SerializeField] private TableDrinkSlot     tableDrinkSlot;
    [SerializeField] private TablePickleSlot    tablePickleSlot;

    private RectTransform   rect        = null;

    private void Awake()
    {
        SetScale();

        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(() => GoCounter());
    }

#if UNITY_EDITOR
    private void Update()
    {
        SetScale();
    }
#endif

    private void SetScale()
    {
        //ȭ�� ������ ����ؼ�
        //canvas�� ũ�⸦ �����Ѵ�.
        float height = Camera.main.orthographicSize * 2.0f;
        float width = height * (float)Screen.width / (float)Screen.height;

        rect ??= GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(width, height);
        SafeArea.SetSafeArea(rect);

        rect.anchoredPosition = new Vector2(73, 0);
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
        //ī���ͷ� ȭ�� ��ȯ
        CloseBtn();
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        kitchenMgr.cameraObj.ChangeLook(LookArea.Counter,()=>
        {
            GuestMgr guestMgr = GuestMgr.Instance;
            guestMgr.GiveChicken(tableChicken.chickenCnt, tableChicken.source0, tableChicken.source1, tableChicken.chickenState,
                tableDrinkSlot.hasDrink, tablePickleSlot.hasPickle);

            tableChicken.Init();
            tableDrinkSlot.Init();
            tablePickleSlot.Init();
        });
    }
}
