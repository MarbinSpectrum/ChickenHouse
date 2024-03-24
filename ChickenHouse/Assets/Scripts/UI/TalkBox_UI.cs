using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TalkBox_UI : Mgr
{
    [SerializeField] private TextMeshProUGUI    textUI;
    [SerializeField] private GameObject         obj;
    public string talkStr { private set; get; }

    public void ShowText(string str, NoParaDel fun)
    {
        talkStr = str;
        obj.gameObject.SetActive(true);

        StartCoroutine(RunCor(str,0.1f));
        IEnumerator RunCor(string str,float delayTime)
        {
            for (int i = 1; i <= str.Length; i++)
            {
                string front = str.Substring(0, i);
                string tail = str.Substring(i, str.Length - i);

                tail = "<color=#FFFFFF00>" + tail + "</color>";
                front += tail;

                textUI.text = front;

                yield return new WaitForSeconds(delayTime);
            }

            fun?.Invoke();
        }
    }

    public void CloseTalkBox()
    {
        obj.gameObject.SetActive(false);
    }

}
