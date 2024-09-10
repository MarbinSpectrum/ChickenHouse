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

    public const float MAX_TIME = 80;

    private List<GuestObj> guestObjs    = new List<GuestObj>();
    private List<string> memoStr        = new List<string>();
    private List<Sprite> guestSprite    = new List<Sprite>();
    private List<float> timeValue       = new List<float>();

    [SerializeField] private List<RectTransform> btns;
    [SerializeField] private List<Image> face;
    [SerializeField] private List<Image> gauge;
    [SerializeField] private Color badColor;
    [SerializeField] private Color goodColor;

    private void Start()
    {
        UpdateMemoBtns();

        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if (kitchenMgr == null)
            return;

        //메모지 상태 설정
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

    private void Update()
    {
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if (kitchenMgr == null)
            return;

        for (int i = 0; i < timeValue.Count; i++)
        {
            if (kitchenMgr.cameraObj.lookArea == LookArea.Kitchen && tutoMgr.tutoComplete)
                timeValue[i] -= Time.deltaTime;

            if (timeValue[i] > 0)
            {
                timeValue[i] = Mathf.Max(0, timeValue[i]);

                float lerpValue = timeValue[i] / MAX_TIME;
                Color colorValue = goodColor;
                const float goodColorValue = 0.8f;
                const float badColorValue = 0.2f;
                if (lerpValue > goodColorValue)
                    colorValue = goodColor;
                else if (lerpValue < badColorValue)
                    colorValue = badColor;
                else
                {
                    float tempValue = lerpValue - badColorValue;
                    tempValue /= (goodColorValue - badColorValue);
                    colorValue = Color.Lerp(badColor, goodColor, tempValue);
                }
                gauge[i].fillAmount = lerpValue;
                gauge[i].color = colorValue;
            }
            else
            {
                GuestSystem guestMgr = GuestSystem.Instance;
                guestMgr.LeaveGuest(i);
                RemoveMemo(i);
                return;
            }
        }
    }

    public void AddMemo(string str,Sprite guestFace, float waitTime,GuestObj pGuestObj)
    {
        timeValue.Add(waitTime);
        guestSprite.Add(guestFace);
        memoStr.Add(str);
        guestObjs.Add(pGuestObj);
        UpdateMemoBtns();
    }

    public void RemoveMemo(int idx)
    {
        timeValue.RemoveAt(idx);
        memoStr.RemoveAt(idx);
        guestSprite.RemoveAt(idx);
        guestObjs.RemoveAt(idx);
        UpdateMemoBtns();
    }

    public bool HasGuestMemo(GuestObj pGuestObj)
    {
        foreach (GuestObj obj in guestObjs)
            if (pGuestObj == obj)
                return true;
        return false;
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
        memoObjs.blocksRaycasts = true;
    }

    public void CloseTriggerBox()
    {
        memoObjs.alpha = 0;
        memoObjs.blocksRaycasts = false;
    }

    public void OpenMemo(int num)
    {
        //인스펙터에서 끌어서 사용하는 함수임
        //메모지를 확대함
        animator.Play("Open");
        memoText.text = memoStr[num - 1];
        gameMgr.StopGame(true);
        deep.gameObject.SetActive(true);
    }

    public void CloseMemo()
    {
        //인스펙터에서 끌어서 사용하는 함수임
        //메모지를 닫음
        animator.Play("Close");
        gameMgr.StopGame(false);
        deep.gameObject.SetActive(false);
    }
}
