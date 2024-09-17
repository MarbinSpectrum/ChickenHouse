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
    [SerializeField] private UtensilShop_UI utensilshopUI;

    public void SetInit()
    {
        oner.talkBox.CloseTalkBox();
        showMenu.gameObject.SetActive(false);
        header.gameObject.SetActive(false);

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

    public void ShowUtensilsUI()
    {
        //인스펙터에 끌어서 사용하는 함수
        soundMgr.StopLoopSE(Sound.Voice25_SE);
        soundMgr.PlaySE(Sound.Btn_SE);
        oner.talkBox.CloseTalkBox();
        utensilshopUI.SetUI();
        utensilshopUI.gameObject.SetActive(true);
    }

    public void CloseUtensilsUI()
    {
        //인스펙터에 끌어서 사용하는 함수
        soundMgr.StopLoopSE(Sound.Voice25_SE);
        soundMgr.PlaySE(Sound.Btn_SE);
        utensilshopUI.gameObject.SetActive(false);
    }

    public void StopTalk()
    {
        //인스펙터에서 끌어서 사용하는 함수
        soundMgr.StopLoopSE(Sound.Voice25_SE);
    }
}
