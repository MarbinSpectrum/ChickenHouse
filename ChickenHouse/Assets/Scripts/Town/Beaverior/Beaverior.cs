using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Beaverior : Mgr
{
    public struct Oner
    {
        public RectTransform rect;
        public Animator animator;
        public TalkBox_UI talkBox;
    }

    [SerializeField] private Oner oner;
    [SerializeField] private Animation showMenu;
    [SerializeField] private RectTransform header;
    [SerializeField] private RectTransform adUIBtn;
    [SerializeField] private Beaverior_UI beaveriorUI;
    [SerializeField] private TownMove exitBeaverior;
    public List<InteriorItem> itemList { get; private set; } = new List<InteriorItem>();
    public bool isOpen { private set; get; } = false;
    public bool run { private set; get; } = false;

    public void SetInit()
    {
        isOpen = true;
        oner.talkBox.CloseTalkBox();
        showMenu.gameObject.SetActive(false);
        header.gameObject.SetActive(false);

        UpdateList(InteriorTab.Wall);

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

    public void UpdateList(InteriorTab pTab)
    {
        void AddItemList(InteriorItem pItem)
        {
            itemList.Add(pItem);
        }

        itemList.Clear();
        
        switch(pTab)
        {
            case InteriorTab.Wall:
                {
                    AddItemList(InteriorItem.Interior_Wall_0);
                    AddItemList(InteriorItem.Interior_Wall_1);
                    AddItemList(InteriorItem.Interior_Wall_2);
                    AddItemList(InteriorItem.Interior_Wall_3);
                    AddItemList(InteriorItem.Interior_Wall_4);
                }
                break;
            case InteriorTab.Desk:
                {
                    AddItemList(InteriorItem.Interior_Desk_0);
                    AddItemList(InteriorItem.Interior_Desk_1);
                    AddItemList(InteriorItem.Interior_Desk_2);
                    AddItemList(InteriorItem.Interior_Desk_3);
                    AddItemList(InteriorItem.Interior_Desk_4);
                }
                break;
            case InteriorTab.Floor:
                {
                    AddItemList(InteriorItem.Interior_Floor_0);
                    AddItemList(InteriorItem.Interior_Floor_1);
                    AddItemList(InteriorItem.Interior_Floor_2);
                    AddItemList(InteriorItem.Interior_Floor_3);
                    AddItemList(InteriorItem.Interior_Floor_4);
                }
                break;
            case InteriorTab.Table:
                {
                    AddItemList(InteriorItem.Interior_Table_0);
                    AddItemList(InteriorItem.Interior_Table_1);
                    AddItemList(InteriorItem.Interior_Table_2);
                    AddItemList(InteriorItem.Interior_Table_3);
                    AddItemList(InteriorItem.Interior_Table_4);
                }
                break;
        }
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

    public void OpenShopUI()
    {
        //인스펙터에 끌어서 사용하는 함수
        soundMgr.StopLoopSE(Sound.Voice27_SE);
        soundMgr.PlaySE(Sound.Btn_SE);
        oner.talkBox.CloseTalkBox();

        UpdateList(InteriorTab.Wall);
        beaveriorUI.SetUI();
        beaveriorUI.gameObject.SetActive(true);
    }

    public void CloseShopUI()
    {
        //인스펙터에 끌어서 사용하는 함수
        soundMgr.StopLoopSE(Sound.Voice27_SE);
        soundMgr.PlaySE(Sound.Btn_SE);
        beaveriorUI.gameObject.SetActive(false);
    }

    public void ExitBeaverior()
    {
        if (isOpen == false)
            return;
        if (run == false)
            return;
        isOpen = false;
        run = false;
        exitBeaverior.MoveTown();
        StopTalk();
    }

    public void EscapeBeaverior()
    {
        if (beaveriorUI.gameObject.activeSelf)
            CloseShopUI();
        else if (isOpen)
            ExitBeaverior();
    }

    public void StopTalk()
    {
        //인스펙터에서 끌어서 사용하는 함수
        soundMgr.StopLoopSE(Sound.Voice27_SE);
    }
}
