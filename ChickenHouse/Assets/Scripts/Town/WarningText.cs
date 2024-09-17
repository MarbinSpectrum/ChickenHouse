using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WarningText : Mgr
{
    [SerializeField] private TextMeshProUGUI warningText;
    [SerializeField] private RectTransform rect;

    [SerializeField] private TextMeshProUGUI warningMsgText;
    [SerializeField] private RectTransform msgRect;

    public void SetText(string str)
    {
        rect.gameObject.SetActive(true);
        warningText.text = str;
    }

    public void SetWarningMsgText(string str)
    {
        msgRect.gameObject.SetActive(true);
        warningMsgText.text = str;
    }

    public void CloseWarningMsgBox()
    {
        //�ν����Ϳ� ��� ����ϴ� �Լ�
        msgRect.gameObject.SetActive(false);
    }
}
