using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeKeyCheck : Mgr
{
    [SerializeField] private Memo_UI memoUI;
    [SerializeField] private Option_UI option;

    //타운관련
    [SerializeField] private Diary_UI diaryUI;
    [SerializeField] private Town town;

    [SerializeField] private RestaurantOpenCheck restaurantOpenCheck;
    [SerializeField] private WorkerContractCheck workerContractCheck;
    [SerializeField] private UtensilPurchaseCheck utensilPurchaseCheck;
    [SerializeField] private LongNoseContractCheck longNoseContractCheck;
    [SerializeField] private WakeupMsg wakeupMsg;
    [SerializeField] private GameObject warningMsgBox;

    [SerializeField] private NekoJobBank nekoJobBank;
    [SerializeField] private ChefPauxsCookingUtensils utensils;
    [SerializeField] private LongNose longNose;
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
            Run();
    }

    private void Run()
    {
        if (option != null && option.isOpen)
        {
            option.Close_UI();
            return;
        }

        if (gameMgr != null && gameMgr.gameRecordOpen)
        {
            gameMgr.CloseRecordUI(false);
            return;
        }

        if (sceneMgr != null && sceneMgr.nowSceneChange)
            return;

        if (restaurantOpenCheck != null && restaurantOpenCheck.gameObject.activeSelf)
        {
            restaurantOpenCheck.OpenNo();
            return;
        }
        if (workerContractCheck != null && workerContractCheck.gameObject.activeSelf)
        {
            workerContractCheck.OpenNo();
            return;
        }
        if (utensilPurchaseCheck != null && utensilPurchaseCheck.gameObject.activeSelf)
        {
            utensilPurchaseCheck.OpenNo();
            return;
        }
        if (longNoseContractCheck != null && longNoseContractCheck.gameObject.activeSelf)
        {
            longNoseContractCheck.OpenNo();
            return;
        }
        if (wakeupMsg != null && wakeupMsg.gameObject.activeSelf)
        {
            wakeupMsg.CloseMsgBox();
            return;
        }
        if (warningMsgBox != null && warningMsgBox.gameObject.activeSelf)
        {
            warningMsgBox.gameObject.SetActive(false);
            return;
        }

        if (town != null && town.nowSceneChange)
            return;
        if (nekoJobBank != null && nekoJobBank.isOpen && 
            gameMgr?.playData != null && gameMgr.playData.tutoComplete4)
        {
            nekoJobBank.EscapeNekoJobBank();
            return;
        }
        if (utensils != null && utensils.isOpen)
        {
            utensils.EscapeUtensils();
            return;
        }
        if (longNose != null && longNose.isOpen)
        {
            longNose.EscapeLongNose();
            return;
        }
        if (diaryUI != null && diaryUI.isOpen)
        {
            diaryUI.CloseUI(false);
            return;
        }

        if (memoUI != null && memoUI.isOpen)
        {
            memoUI.CloseMemo();
            return;
        }

        if (option != null)
        {
            if(option.isOpen)
                option.Close_UI();
            else
                option.Set_UI();
            return;
        }
    }
}
