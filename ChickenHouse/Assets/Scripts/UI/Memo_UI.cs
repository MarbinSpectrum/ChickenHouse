using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class Memo_UI : Mgr
{
    [SerializeField] private TextMeshProUGUI    memoText;
    [SerializeField] private Animator           animator;
    [SerializeField] private Image              deep;
    [SerializeField] private CanvasGroup        memoObjs;

    private List<string> memoStr = new List<string>();
    private List<Sprite> guestSprite = new List<Sprite>();
    [SerializeField] private List<RectTransform> btns;
    [SerializeField] private List<Image> face;
    private void Start()
    {
        UpdateMemoBtns();

        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if (kitchenMgr == null)
            return;

        //초기 메모지 상태 설정
        if(kitchenMgr.cameraObj.lookArea == LookArea.Counter)
        {
            memoObjs.alpha = 0;
            deep.gameObject.SetActive(false);
        }
        else
        {
            memoObjs.alpha = 1;
            deep.gameObject.SetActive(false);
        }
    }


    public void AddMemo(string str,Sprite guestFace)
    {
        guestSprite.Add(guestFace);
        memoStr.Add(str);
        UpdateMemoBtns();
    }

    public void RemoveMemo()
    {
        memoStr.RemoveAt(0);
        guestSprite.RemoveAt(0);
        UpdateMemoBtns();
    }

    public void UpdateMemoBtns()
    {
        int cnt = guestSprite.Count;
        for (int i = 0; i < btns.Count; i++)
        {
            if(i < cnt)
            {
                btns[i].gameObject.SetActive(true);
                face[i].sprite = guestSprite[i];
            }
            else
                btns[i].gameObject.SetActive(false);
        }
    }


    public void OpenTriggerBox()
    {
        memoObjs.alpha = 1;
    }

    public void CloseTriggerBox()
    {
        memoObjs.alpha = 0;
    }

    public void OpenMemo(int num)
    {
        //인스펙터에서 끌어서 사용하는 함수임
        //메모지를 확대함
        animator.Play("Open");
        memoText.text = memoStr[num - 1];
        deep.gameObject.SetActive(true);
    }

    public void CloseMemo()
    {
        //인스펙터에서 끌어서 사용하는 함수임
        //메모지를 닫음
        animator.Play("Close");
        deep.gameObject.SetActive(false);
    }
}
