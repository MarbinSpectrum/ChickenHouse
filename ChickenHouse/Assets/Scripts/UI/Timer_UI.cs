using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer_UI : Mgr
{
    private const int MAX_TIME  = 180;
    private const int BASE_HOUR = 9;

    [SerializeField] private Image              guage;
    [SerializeField] private RectTransform      clockHand;
    [SerializeField] private TextMeshProUGUI    dayText;
    [SerializeField] private TextMeshProUGUI    timeText;

    [SerializeField] private TextMeshProUGUI    dayTitle;

    public float time = 0;

    private bool run = false;

    private void Update()
    {
        if (run)
            time += Time.deltaTime;

        if (tutoMgr.tutoComplete == false)
        {
            //튜토리얼 완료를 아직 못해서 시간은 안간다.
            time = 0;
        }

        SetTime(time, MAX_TIME);

        if (gameMgr?.playData != null)
        {
            string daySr = string.Format(LanguageMgr.GetText("DAY"), gameMgr.playData.day);
            LanguageMgr.SetText(dayText, daySr);
            LanguageMgr.SetText(dayTitle, daySr);
        }

        int addHour = (int)(time / 15f);

        int hour = Mathf.Min(24, BASE_HOUR + addHour);
        string timeStr = string.Empty;
        if (hour <= 12)
            timeStr = string.Format("AM {0:D2}:00", hour);
        else
            timeStr = string.Format("PM {0:D2}:00", hour-12);
        LanguageMgr.SetText(timeText, timeStr);

#if UNITY_EDITOR
        if(Input.GetKeyDown(KeyCode.Space))
        {
            time = MAX_TIME;
        }
#endif
    }

    public void RunTimer()
    {
        run = true;
    }

    public void SetTime(float nowTime, float maxTime)
    {
        float fillAmount = Mathf.Min(1, nowTime / maxTime);
        guage.fillAmount = fillAmount;
        clockHand.transform.eulerAngles = new Vector3(0, 0, -fillAmount * 360);
    }

    public bool IsEndTime()
    {
        //종료시간인가?
        return time >= MAX_TIME; 
    }


}
