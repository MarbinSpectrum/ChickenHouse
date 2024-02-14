using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SaveSlot_UI : Mgr
{
    public const int MAX_SLOT = 3;

    [System.Serializable]
    public struct SlotObj
    {
        //빈 슬롯 오브젝트
        public RectTransform    emptyObj;
        public TextMeshProUGUI  emptyText;

        //세이브 데이터가 있을때 오브젝트
        public RectTransform    saveObj;
        public TextMeshProUGUI  dayText;
        public TextMeshProUGUI  moneyText;
        public Button           loadBtn;
    }

    [SerializeField] private List<SlotObj> slotObjs = new List<SlotObj>();
    [SerializeField] private Button closeBtn;


    public void SetSlot_UI(List<PlayData> playDatas)
    {
        gameObject.SetActive(true);

        SetButtonEvent();

        for (int i = 0; i < playDatas.Count; i++)
        {
            PlayData data = playDatas[i];
            SlotObj slotObj = slotObjs[i];
            if (data == null)
            {
                slotObj.emptyObj.gameObject.SetActive(true);
                slotObj.saveObj.gameObject.SetActive(false);
                continue;
            }
            slotObj.emptyObj.gameObject.SetActive(false);
            slotObj.saveObj.gameObject.SetActive(true);

            string dayText = string.Format(LanguageMgr.GetText("DAY"), data.day);
            LanguageMgr.SetText(slotObj.dayText, dayText);

            string strNum = string.Format("{0:N0} $", data.money);
            LanguageMgr.SetText(slotObj.moneyText, strNum);
        }
    }

    private void SetButtonEvent()
    {
        closeBtn.onClick.RemoveAllListeners();
        closeBtn.onClick.AddListener(() => gameObject.SetActive(false));

        for(int i = 0; i < slotObjs.Count; i++)
        {
            SlotObj slotObj = slotObjs[i];
            int num = i;
            slotObj.loadBtn.onClick.RemoveAllListeners();
            slotObj.loadBtn.onClick.AddListener(() =>
            {
                gameMgr.selectSaveSlot = num;
                SceneManager.LoadScene("InGame");
            });
        }
    }
}
