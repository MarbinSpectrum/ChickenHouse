using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer_UI : Mgr
{
    private const int MAX_TIME = 180;

    [SerializeField] private Image              guage;
    [SerializeField] private RectTransform      clockHand;
    [SerializeField] private TextMeshProUGUI    textMesh;
    [SerializeField] private Counter_BG         counterBg;
    public float time = 0;

    private void Update()
    {
        time += Time.deltaTime;

        if(tutoMgr.tutoComplete == false)
        {
            //튜토리얼 완료를 아직 못해서 시간은 안간다.
            time = 0;
        }

        SetTime(time, MAX_TIME);
        counterBg.SetLerpValue(time / MAX_TIME);
        if (gameMgr?.playData != null)
        {
            string dayText = string.Format(LanguageMgr.GetText("DAY"), gameMgr.playData.day);
            LanguageMgr.SetText(textMesh, dayText);
        }
    }

    public void SetTime(float nowTime, float maxTime)
    {
        float fillAmount = Mathf.Min(1, nowTime / maxTime);
        guage.fillAmount = fillAmount;
        clockHand.transform.eulerAngles = new Vector3(0, 0, -fillAmount * 360);
    }

}
