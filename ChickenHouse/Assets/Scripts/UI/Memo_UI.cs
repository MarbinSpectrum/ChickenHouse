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
    [SerializeField] private Timer_UI           timerUI;

    public const float MAX_TIME = 60;

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

        //�޸��� ���� ����
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
            if (kitchenMgr.cameraObj.lookArea == LookArea.Kitchen && gameMgr.playData != null && gameMgr.playData.tutoComplete1)
            {
                if (timerUI.IsEndTime())
                    timeValue[i] -= Time.deltaTime*1.2f;
                else if (i == 0)
                    timeValue[i] -= Time.deltaTime;
            }

            timeValue[i] = Mathf.Clamp(timeValue[i], 0, MAX_TIME);

            if (timeValue[i] > 0)
            {                
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
                if (timerUI.IsEndTime())
                    colorValue = Color.Lerp(badColor, goodColor, 0);

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

    public void AddMemo(string str,Sprite guestFace,GuestObj pGuestObj)
    {
        float playDataPatience = 100f;
        if (gameMgr.playData != null)
            playDataPatience = gameMgr.playData.GuestPatience();
        float waitTime = MAX_TIME * playDataPatience / 100f;
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
        //�ν����Ϳ��� ��� ����ϴ� �Լ���
        //�޸����� Ȯ����
        animator.Play("Open");

        string newTextUIStr = LanguageMgr.GetSmartText(memoStr[num - 1], 40, memoText);

        memoText.text = newTextUIStr;

        gameMgr.StopGame(true);
        deep.gameObject.SetActive(true);
    }

    public void CloseMemo()
    {
        //�ν����Ϳ��� ��� ����ϴ� �Լ���
        //�޸����� ����
        animator.Play("Close");
        gameMgr.StopGame(false);
        deep.gameObject.SetActive(false);
    }
}
