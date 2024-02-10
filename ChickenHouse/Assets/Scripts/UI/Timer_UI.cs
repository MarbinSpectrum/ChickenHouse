using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer_UI : MonoBehaviour
{
    [SerializeField] private Image guage;
    [SerializeField] private RectTransform clockHand;
    private float time = 0;

    public void SetTime(float nowTime,float maxTime)
    {
        float fillAmount = Mathf.Min(1, nowTime / maxTime);
        guage.fillAmount = fillAmount;
        clockHand.transform.eulerAngles = new Vector3(0, 0, -fillAmount * 360);
    }

    private void Update()
    {
        time += Time.deltaTime;

        SetTime(time, 300);
    }
}
