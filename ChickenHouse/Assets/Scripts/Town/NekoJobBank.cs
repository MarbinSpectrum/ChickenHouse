using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NekoJobBank : Mgr
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

            bool waitFlag = false;
            string str = LanguageMgr.GetText("EMPLOYMENT_NPC_TALK_0");
            oner.talkBox.ShowText(str, TalkBoxType.Normal, () =>
            {
                oner.animator.Play("Idle");
                showMenu.gameObject.SetActive(true);
                showMenu.Play();

                waitFlag = true;
            });

            yield return new WaitUntil(() => waitFlag);
        }
        StartCoroutine(Run());
    }


}
