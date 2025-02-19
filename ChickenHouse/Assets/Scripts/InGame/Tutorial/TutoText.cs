using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutoText : Mgr
{
    [SerializeField] private TextMeshProUGUI    text;
    [SerializeField] private GameObject         tutoObj;
    private void Start()
    {
        tutoMgr.RegistTutoText(this);
    }

    public void ShowText(Tutorial tutotype)
    {
        string str = string.Empty;
        switch(tutotype)
        {
            case Tutorial.Tuto_0:
                str = LanguageMgr.GetText("TUTO_0");
                break;
            case Tutorial.Tuto_1:
                str = LanguageMgr.GetText("TUTO_1");
                break;
            case Tutorial.Tuto_2:
                str = LanguageMgr.GetText("TUTO_2");
                break;
            case Tutorial.Tuto_3:
                str = LanguageMgr.GetText("TUTO_3");
                break;
            case Tutorial.Tuto_4:
                str = LanguageMgr.GetText("TUTO_4");
                break;
            case Tutorial.Tuto_5:
                str = LanguageMgr.GetText("TUTO_5");
                break;
            case Tutorial.Tuto_6:
                str = LanguageMgr.GetText("TUTO_6");
                break;
            case Tutorial.Tuto_7:
                str = LanguageMgr.GetText("TUTO_7");
                break;
            case Tutorial.Tuto_8_0:
                str = LanguageMgr.GetText("TUTO_8_0");
                break;
            case Tutorial.Tuto_8_1:
                str = LanguageMgr.GetText("TUTO_8_1");
                break;
            case Tutorial.Tuto_8_2:
                str = LanguageMgr.GetText("TUTO_8_2");
                break;
            case Tutorial.Tuto_9_0:
                str = LanguageMgr.GetText("TUTO_9_0");
                break;
            case Tutorial.Tuto_9_1:
                str = LanguageMgr.GetText("TUTO_9_1");
                break;
            case Tutorial.Tuto_10:
                str = LanguageMgr.GetText("TUTO_10");
                break;
            case Tutorial.Tuto_11:
                str = LanguageMgr.GetText("TUTO_11");
                break;
            case Tutorial.Tuto_12:
                str = LanguageMgr.GetText("TUTO_12");
                break;
            case Tutorial.Worker_Tuto_1_0:
                str = LanguageMgr.GetText("WORKER_TUTO_1_0");
                break;
            case Tutorial.Worker_Tuto_1_1:
                str = LanguageMgr.GetText("WORKER_TUTO_1_1");
                break;
            case Tutorial.Worker_Tuto_2_0:
                str = LanguageMgr.GetText("WORKER_TUTO_2_0");
                break;
            case Tutorial.Worker_Tuto_2_1:
                str = LanguageMgr.GetText("WORKER_TUTO_2_1");
                break;
            case Tutorial.Worker_Tuto_2_2:
                str = LanguageMgr.GetText("WORKER_TUTO_2_2");
                break;
            case Tutorial.Menu_Tuto_1:
                str = LanguageMgr.GetText("MENU_TUTO_1");
                break;
            case Tutorial.Menu_Tuto_2_0:
                str = LanguageMgr.GetText("MENU_TUTO_2_0");
                break;
            case Tutorial.Menu_Tuto_2_1:
                str = LanguageMgr.GetText("MENU_TUTO_2_1");
                break;
            case Tutorial.Menu_Tuto_2_2:
                str = LanguageMgr.GetText("MENU_TUTO_2_2");
                break;
            case Tutorial.Town_Tuto_1:
                str = LanguageMgr.GetText("TOWN_TUTO_1");
                break;
            case Tutorial.Town_Tuto_2:
                str = LanguageMgr.GetText("TOWN_TUTO_2");
                break;
            case Tutorial.Town_Tuto_3:
                str = LanguageMgr.GetText("TOWN_TUTO_3");
                break;
            case Tutorial.Town_Tuto_3_3:
                str = LanguageMgr.GetText("TOWN_TUTO_3_3");
                break;
            case Tutorial.Town_Tuto_4:
                str = LanguageMgr.GetText("TOWN_TUTO_4");
                break;
            case Tutorial.Town_Tuto_5:
                str = LanguageMgr.GetText("TOWN_TUTO_5");
                break;
            case Tutorial.Town_Tuto_6:
                str = LanguageMgr.GetText("TOWN_TUTO_6");
                break;
            case Tutorial.Town_Tuto_7:
                str = LanguageMgr.GetText("TOWN_TUTO_7");
                break;
            case Tutorial.Town_Tuto_8:
                str = LanguageMgr.GetText("TOWN_TUTO_8");
                break;
            case Tutorial.Town_Tuto_9:
                str = LanguageMgr.GetText("TOWN_TUTO_9");
                break;
            case Tutorial.Town_Tuto_10:
                str = LanguageMgr.GetText("TOWN_TUTO_10");
                break;
            case Tutorial.Town_Tuto_11:
                str = LanguageMgr.GetText("TOWN_TUTO_11");
                break;
            case Tutorial.Town_Tuto_12:
                str = LanguageMgr.GetText("TOWN_TUTO_12");
                break;
            case Tutorial.Town_Tuto_13:
                str = LanguageMgr.GetText("TOWN_TUTO_13");
                break;
            case Tutorial.Town_Tuto_14:
                str = LanguageMgr.GetText("TOWN_TUTO_14");
                break;
            case Tutorial.Event_0_Tuto_1:
                str = LanguageMgr.GetText("EVENT_0_TUTO_1");
                break;
            case Tutorial.Event_0_Tuto_2:
                str = LanguageMgr.GetText("EVENT_0_TUTO_2");
                break;
            case Tutorial.Event_0_Tuto_3:
                str = LanguageMgr.GetText("EVENT_0_TUTO_3");
                break;
            case Tutorial.Event_0_Tuto_4:
                str = LanguageMgr.GetText("EVENT_0_TUTO_4");
                break;
            case Tutorial.Event_0_Tuto_5:
                str = LanguageMgr.GetText("EVENT_0_TUTO_5");
                break;
            case Tutorial.Event_0_Tuto_6:
                str = LanguageMgr.GetText("EVENT_0_TUTO_6");
                break;
        }

        LanguageMgr.SetText(text, str);
        tutoObj.gameObject.SetActive(str != string.Empty);
    }

    public void CloseText()
    {
        tutoObj.gameObject.SetActive(false);
    }
}
