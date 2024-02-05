using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class GoCounter_UI : MonoBehaviour
{
    [SerializeField] private Animator   animator;
    [SerializeField] private Button     btn;
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
    }

    public void OpenBtn()
    {
        //��ư Ȱ��ȭ
        animator.SetBool("Open", true);
    }

    public void CloseBtn()
    {
        //��ư ��Ȱ��ȭ
        animator.SetBool("Open", false);
    }

    private void GoCounter()
    {
        //ī���ͷ� ȭ�� ��ȯ
        CloseBtn();
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        kitchenMgr.cameraObj.ChangeLook(LookArea.Counter);
    }
}
