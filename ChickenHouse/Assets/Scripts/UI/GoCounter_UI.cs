using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
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
        SetScale();

        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(() => GoCounter());
    }

    private void Update()
    {
#if UNITY_EDITOR
        SetScale();
#endif
        if(followTrans != null)
        {
            transform.position = new Vector3(followTrans.position.x, transform.position.y, transform.position.y);
        }
    }

    private void SetScale()
    {
        //ȭ�� ������ ����ؼ�
        //canvas�� ũ�⸦ �����Ѵ�.
        float height = Camera.main.orthographicSize * 2.0f;
        float width = height * (float)Screen.width / (float)Screen.height;

        rect ??= GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(width, height);
        SafeArea.SetSafeArea(rect);

        //rect.anchoredPosition = new Vector2(-3.6f, 3);
        btn.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
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
            GuestMgr guestMgr = GuestMgr.Instance;
            guestMgr.GiveChicken(tableChicken.chickenCnt, tableChicken.source0, tableChicken.source1, tableChicken.chickenState,
                tableDrinkSlot.hasDrink, tablePickleSlot.hasPickle);

            tableChicken.Init();
            tableDrinkSlot.Init();
            tablePickleSlot.Init();
        });
        kitchenMgr.cameraObj.lookArea = LookArea.Counter;
    }
}
