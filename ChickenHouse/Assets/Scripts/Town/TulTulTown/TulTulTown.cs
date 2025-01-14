using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TulTulTown : Mgr
{
    public enum Zone
    {
        None,

        ChickenHeaven,
        JobBank,
        CookingUtensils,
        LongNose,

        MAX
    }

    [SerializeField] private Dictionary<Zone, RectTransform> housePos = new Dictionary<Zone, RectTransform>();
    [SerializeField] private RectTransform                  cameraPos;
    [SerializeField] private RectTransform[]                moveBtn;
    [SerializeField] private RectTransform                  townMapUI;
    [SerializeField] private RectTransform                  dontTouch;

    public struct MenuBtn
    {
        public RectTransform    rect;
        public TextMeshProUGUI  menuText;
    }

    [SerializeField] private MenuBtn menuBtn;

    public struct Header
    {
        public RectTransform rect;
        public TextMeshProUGUI mainText;
        public TextMeshProUGUI subText;
    }

    [SerializeField] private Header header;

    [SerializeField] private RestaurantOpenCheck restaurantMsgBox;
    [SerializeField] private TownMove nekotownMove;
    [SerializeField] private TownMove chefPauxsCookingUtensilsMove;
    [SerializeField] private TownMove longnoseCompany;

    private Zone nowZone = Zone.None;
    private bool nowMove = false;

    public void SetInit(Zone pZone)
    {
        MoveZone(pZone);
    }

    public void MoveDic(int dic)
    {
        //인스펙터로 끌어서 사용할 함수임
        //특정Zone에서 특정 dic으로 이동시 이동될Zone을 임의로 정해줌
        if (gameMgr.playData.tutoComplete4 == false)
            return;

        soundMgr.PlaySE(Sound.Walk_SE);
        switch(nowZone)
        {
            case Zone.ChickenHeaven:
                {
                    if (dic == 1)
                        MoveZone(Zone.JobBank);
                }
                break;
            case Zone.JobBank:
                {
                    if (dic == 0)
                        MoveZone(Zone.ChickenHeaven);
                    else if (dic == 1)
                        MoveZone(Zone.CookingUtensils);
                }
                break;
            case Zone.CookingUtensils:
                {
                    if (dic == 0)
                        MoveZone(Zone.JobBank);
                    else if (dic == 1)
                        MoveZone(Zone.LongNose);
                }
                break;
            case Zone.LongNose:
                {
                    if (dic == 0)
                        MoveZone(Zone.CookingUtensils);
                }
                break;
        }
    }

    public void MoveZone(int pZone) => MoveZone((Zone)pZone);

    public void MoveZone(Zone pZone,NoParaDel fun = null)
    {
        if (nowMove)
            return;
        if (nowZone == pZone)
            return;

        nowMove = true;
        nowZone = pZone;

        IEnumerator Run()
        {
            moveBtn[0].gameObject.SetActive(false);
            moveBtn[1].gameObject.SetActive(false);
            dontTouch.gameObject.SetActive(true);
            header.rect.gameObject.SetActive(false);
            menuBtn.rect.gameObject.SetActive(false);
            townMapUI.gameObject.SetActive(false);

            float dis = 0;
            do
            {
                float cPos = Mathf.Lerp(cameraPos.transform.position.x, housePos[pZone].transform.position.x, 0.1f);
                cameraPos.transform.position = new Vector3(cPos, cameraPos.transform.position.y, cameraPos.transform.position.z);
                yield return null;
                dis = Mathf.Abs(cameraPos.transform.position.x - housePos[pZone].transform.position.x);
            } while (dis > 0.01f);
            nowMove = false;

            menuBtn.rect.gameObject.SetActive(true);
            header.rect.gameObject.SetActive(true);
            townMapUI.gameObject.SetActive(true);

            switch (pZone)
            {
                case Zone.ChickenHeaven:
                    {
                        moveBtn[0].gameObject.SetActive(false);
                        moveBtn[1].gameObject.SetActive(true);
                        LanguageMgr.SetString(menuBtn.menuText, "OPEN_RESTAURANT");
                        LanguageMgr.SetString(header.mainText, "TULTUL_TOWN");
                        LanguageMgr.SetString(header.subText, "CHICKEN_RESTAURANT");
                    }
                    break;
                case Zone.JobBank:
                    {
                        moveBtn[0].gameObject.SetActive(true);
                        moveBtn[1].gameObject.SetActive(true);
                        LanguageMgr.SetString(menuBtn.menuText, "EMPLOY_WORKER");
                        LanguageMgr.SetString(header.mainText, "TULTUL_TOWN");
                        LanguageMgr.SetString(header.subText, "NEKO_JOB_BANK");
                    }
                    break;
                case Zone.CookingUtensils:
                    {
                        moveBtn[0].gameObject.SetActive(true);
                        moveBtn[1].gameObject.SetActive(true);
                        LanguageMgr.SetString(menuBtn.menuText, "USE_SHOP");
                        LanguageMgr.SetString(header.mainText, "TULTUL_TOWN");
                        LanguageMgr.SetString(header.subText, "CHEF_PAUXS_COOKING_UTENSILS");
                    }
                    break;
                case Zone.LongNose:
                    {
                        moveBtn[0].gameObject.SetActive(true);
                        moveBtn[1].gameObject.SetActive(false);
                        LanguageMgr.SetString(menuBtn.menuText, "ADVERTISEMENT_CONTRACT");
                        LanguageMgr.SetString(header.mainText, "TULTUL_TOWN");
                        LanguageMgr.SetString(header.subText, "LONG_NOSE_COMPANY");
                    }
                    break;
            }
            dontTouch.gameObject.SetActive(false);
            fun?.Invoke();
        }
        StartCoroutine(Run());
    }

    public void RunMenuBtn()
    {
        //인스펙터로 끌어서 사용하는 함수
        if (gameMgr.playData.tutoComplete4 == false)
            return;

        soundMgr.PlaySE(Sound.Btn_SE);
        switch (nowZone)
        {
            case Zone.ChickenHeaven:
                {
                    restaurantMsgBox.SetUI();
                }
                break;
            case Zone.JobBank:
                {
                    nekotownMove.MoveTown();
                }
                break;
            case Zone.CookingUtensils:
                {
                    chefPauxsCookingUtensilsMove.MoveTown();
                }
                break;
            case Zone.LongNose:
                {
                    longnoseCompany.MoveTown();
                }
                break;
        }
    }
}
