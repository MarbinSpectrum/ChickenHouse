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
        //화면 비율을 계산해서
        //canvas의 크기를 조정한다.
        float height = Camera.main.orthographicSize * 2.0f;
        float width = height * (float)Screen.width / (float)Screen.height;

        rect ??= GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(width, height);
        SafeArea.SetSafeArea(rect);
    }

    public void OpenBtn()
    {
        //버튼 활성화
        animator.SetBool("Open", true);
    }

    public void CloseBtn()
    {
        //버튼 비활성화
        animator.SetBool("Open", false);
    }

    private void GoCounter()
    {
        //카운터로 화면 전환
        CloseBtn();
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        kitchenMgr.cameraObj.ChangeLook(LookArea.Counter);
    }
}
