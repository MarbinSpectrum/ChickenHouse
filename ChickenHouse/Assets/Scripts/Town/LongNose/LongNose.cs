using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class LongNose : Mgr
{
    public struct Oner
    {
        public RectTransform    rect;
        public Animator         animator;
        public TalkBox_UI       talkBox;
    }

    [SerializeField] private Oner oner;
    [SerializeField] private Animation showMenu;
    [SerializeField] private RectTransform header;
    [SerializeField] private RectTransform adUIBtn;
    [SerializeField] private LongNoseContractAD_UI longNoseContractUI;
    [SerializeField] private TownMove exitLongNose;
    public List<ShopItem> itemList { get; private set; } = new List<ShopItem>();
    public bool isOpen { private set; get; } = false;
    public bool run { private set; get; } = false;

    public void SetInit()
    {
        isOpen = true;
        oner.talkBox.CloseTalkBox();
        showMenu.gameObject.SetActive(false);
        header.gameObject.SetActive(false);

        UpdateList();

        //메뉴 활성화 여부
        adUIBtn.gameObject.SetActive(itemList.Count != 0);

        IEnumerator Run()
        {
            oner.animator.Play("Hide");

            yield return new WaitForSeconds(1f);

            header.gameObject.SetActive(true);
            oner.animator.Play("Show");

            yield return new WaitForSeconds(1f);

            oner.animator.Play("Talk");

            string str = GetNPC_Talk_Text();
            soundMgr.PlayLoopSE(Sound.Voice27_SE);
            oner.talkBox.ShowText(str, TalkBoxType.Normal, () =>
            {
                soundMgr.StopLoopSE(Sound.Voice27_SE);
                oner.animator.Play("Idle");
            });

            showMenu.gameObject.SetActive(true);
            showMenu.Play();
            run = true;
        }
        StartCoroutine(Run());
    }

    public void UpdateList()
    {
        void AddItemList(ShopItem pItem)
        {
            PlayData playData = gameMgr.playData;
            if (playData.hasItem[(int)pItem])
                return;
            itemList.Add(pItem);
        }

        itemList.Clear();
        if (gameMgr.playData.hasItem[(int)ShopItem.Advertisement_1] == false)
            AddItemList(ShopItem.Advertisement_1);
        else if (gameMgr.playData.hasItem[(int)ShopItem.Advertisement_2] == false)
            AddItemList(ShopItem.Advertisement_2);
        else if (gameMgr.playData.hasItem[(int)ShopItem.Advertisement_3] == false)
            AddItemList(ShopItem.Advertisement_3);
        else if (gameMgr.playData.hasItem[(int)ShopItem.Advertisement_4] == false)
            AddItemList(ShopItem.Advertisement_4);
        else if (gameMgr.playData.hasItem[(int)ShopItem.Advertisement_5] == false)
            AddItemList(ShopItem.Advertisement_5);
    }

    private string GetNPC_Talk_Text()
    {
        int r = Random.Range(0, 4);
        switch (r)
        {
            case 0:
                return LanguageMgr.GetText("LONGNOSE_NPC_TALK_0");
            case 1:
                return LanguageMgr.GetText("LONGNOSE_NPC_TALK_1");
            case 2:
                return LanguageMgr.GetText("LONGNOSE_NPC_TALK_2");
            case 3:
                return LanguageMgr.GetText("LONGNOSE_NPC_TALK_3");
        }
        return string.Empty;
    }

    public void ShowContractUI()
    {
        //인스펙터에 끌어서 사용하는 함수
        soundMgr.StopLoopSE(Sound.Voice27_SE);
        soundMgr.PlaySE(Sound.Btn_SE);
        oner.talkBox.CloseTalkBox();

        UpdateList();
        longNoseContractUI.SetUI();
        longNoseContractUI.gameObject.SetActive(true);
    }

    public void CloseContractUI()
    {
        //인스펙터에 끌어서 사용하는 함수
        soundMgr.StopLoopSE(Sound.Voice27_SE);
        soundMgr.PlaySE(Sound.Btn_SE);
        longNoseContractUI.gameObject.SetActive(false);
    }

    public void ExitLongNose()
    {
        if (isOpen == false)
            return;
        if (run == false)
            return;
        isOpen = false;
        run = false;
        exitLongNose.MoveTown();
        StopTalk();
    }

    public void EscapeLongNose()
    {
        if (longNoseContractUI.gameObject.activeSelf)
            CloseContractUI();
        else if (isOpen)
            ExitLongNose();
    }

    public void StopTalk()
    {
        //인스펙터에서 끌어서 사용하는 함수
        soundMgr.StopLoopSE(Sound.Voice27_SE);
    }
}
