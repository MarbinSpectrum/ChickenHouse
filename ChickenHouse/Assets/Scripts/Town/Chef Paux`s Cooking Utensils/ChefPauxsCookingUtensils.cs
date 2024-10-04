using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChefPauxsCookingUtensils : Mgr
{
    public struct Oner
    {
        public RectTransform rect;
        public Animator animator;
        public TalkBox_UI talkBox;
    }

    [SerializeField] private Oner           oner;
    [SerializeField] private Animation      showMenu;
    [SerializeField] private RectTransform  header;
    [SerializeField] private RectTransform  buyBtn;
    [SerializeField] private UtensilShop_UI utensilshopUI;
    public List<ShopItem> itemList { get; private set; } = new List<ShopItem>();

    public void SetInit()
    {
        oner.talkBox.CloseTalkBox();
        showMenu.gameObject.SetActive(false);
        header.gameObject.SetActive(false);


        //�޴� Ȱ��ȭ ����
        UpdateList(UtensilShopMenu.Fryer_Add);
        int cnt1 = itemList.Count;
        UpdateList(UtensilShopMenu.Fryer_Buy);
        int cnt2 = itemList.Count;

        buyBtn.gameObject.SetActive(cnt1 != 0 || cnt2 != 0);

        IEnumerator Run()
        {
            oner.animator.Play("Hide");

            yield return new WaitForSeconds(1f);

            header.gameObject.SetActive(true);
            oner.animator.Play("Show");

            yield return new WaitForSeconds(1f);

            oner.animator.Play("Talk");

            string str = GetNPC_Talk_Text();
            soundMgr.PlayLoopSE(Sound.Voice25_SE);
            oner.talkBox.ShowText(str, TalkBoxType.Normal, () =>
            {
                soundMgr.StopLoopSE(Sound.Voice25_SE);
                oner.animator.Play("Idle");
            });

            showMenu.gameObject.SetActive(true);
            showMenu.Play();
        }
        StartCoroutine(Run());
    }

    private string GetNPC_Talk_Text()
    {
        int r = Random.Range(0, 4);
        switch (r)
        {
            case 0:
                return LanguageMgr.GetText("UTENSILS_NPC_TALK_0");
            case 1:
                return LanguageMgr.GetText("UTENSILS_NPC_TALK_1");
            case 2:
                return LanguageMgr.GetText("UTENSILS_NPC_TALK_2");
            case 3:
                return LanguageMgr.GetText("UTENSILS_NPC_TALK_3");
        }
        return string.Empty;
    }

    public void UpdateList(UtensilShopMenu pMenu)
    {
        void AddItemList(ShopItem pItem)
        {
            PlayData playData = gameMgr.playData;
            if (playData.hasItem[(int)pItem])
                return;
            itemList.Add(pItem);
        }

        itemList.Clear();
        switch (pMenu)
        {
            case UtensilShopMenu.Fryer_Buy:
                {
                    AddItemList(ShopItem.OIL_Zone_1);
                    AddItemList(ShopItem.OIL_Zone_2);
                    AddItemList(ShopItem.OIL_Zone_3);
                    AddItemList(ShopItem.OIL_Zone_4);
                }
                break;
            case UtensilShopMenu.Fryer_Add:
                {
                    AddItemList(ShopItem.NEW_OIL_ZONE_1);
                    AddItemList(ShopItem.NEW_OIL_ZONE_2);
                    AddItemList(ShopItem.NEW_OIL_ZONE_3);
                }
                break;
        }
    }

    public void ShowUtensilsUI()
    {
        //�ν����Ϳ� ��� ����ϴ� �Լ�
        soundMgr.StopLoopSE(Sound.Voice25_SE);
        soundMgr.PlaySE(Sound.Btn_SE);
        oner.talkBox.CloseTalkBox();
        utensilshopUI.SetUI();
        utensilshopUI.gameObject.SetActive(true);
    }

    public void CloseUtensilsUI()
    {
        //�ν����Ϳ� ��� ����ϴ� �Լ�
        soundMgr.StopLoopSE(Sound.Voice25_SE);
        soundMgr.PlaySE(Sound.Btn_SE);
        utensilshopUI.gameObject.SetActive(false);
    }

    public void StopTalk()
    {
        //�ν����Ϳ��� ��� ����ϴ� �Լ�
        soundMgr.StopLoopSE(Sound.Voice25_SE);
    }
}
