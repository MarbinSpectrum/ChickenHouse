using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Town : Mgr
{
    [SerializeField] private Dictionary<TownMap, RectTransform> townMap = new Dictionary<TownMap, RectTransform>();
    [SerializeField] private Animator                           moveAni;
    [SerializeField] private RectTransform                      diaryBtn;

    /** 현재 지역 **/
    private TownMap nowArea = TownMap.TulTulTown;
    /** 중복 이동 방지용 **/
    private bool isMove = false;

    private void Start()
    {
        ActTownMove(TownMap.None,nowArea);
        isMove = false;
    }

    public void ActTownMove(TownMap pPrevMap, TownMap pTownMap)
    {
        foreach (var pair in townMap)
            pair.Value.gameObject.SetActive(false);
        townMap[pTownMap].gameObject.SetActive(true);
        switch(pTownMap)
        {
            case TownMap.TulTulTown:
                {
                    TulTulTown tultulTown = townMap[pTownMap].GetComponent<TulTulTown>();
                    if(pPrevMap == TownMap.None)
                        tultulTown.SetInit(TulTulTown.Zone.ChickenHeaven);
                    else if (pPrevMap == TownMap.NekoJobBank)
                        tultulTown.SetInit(TulTulTown.Zone.JobBank);
                    diaryBtn.gameObject.SetActive(true);
                    soundMgr.PlayBGM(Sound.Town_BG);
                }
                break;
            case TownMap.NekoJobBank:
                {
                    NekoJobBank nekoJobBank = townMap[pTownMap].GetComponent<NekoJobBank>();
                    nekoJobBank.SetInit();
                    diaryBtn.gameObject.SetActive(false);
                    soundMgr.StopBGM();
                }
                break;
            case TownMap.ChefPauxsCookingUtensils:
                {
                    ChefPauxsCookingUtensils chefPauxsCookingUtensils = townMap[pTownMap].GetComponent<ChefPauxsCookingUtensils>();
                    chefPauxsCookingUtensils.SetInit();
                    diaryBtn.gameObject.SetActive(false);
                    soundMgr.StopBGM();
                }
                break;
            case TownMap.LongNoseCompany:
                {
                    LongNose longNose = townMap[pTownMap].GetComponent<LongNose>();
                    longNose.SetInit();
                    diaryBtn.gameObject.SetActive(false);
                    soundMgr.StopBGM();
                }
                break;
        }
    }

    public void TownMapLoad(TownMap pTownMap)
    {
        if (nowArea == pTownMap)
            return;
        if (isMove)
            return;
        isMove = true;



        IEnumerator Run()
        {
            //맵 이동 코루틴
            switch(nowArea)
            {
                case TownMap.TulTulTown:
                case TownMap.NekoJobBank:
                case TownMap.ChefPauxsCookingUtensils:
                case TownMap.LongNoseCompany:
                    moveAni.Play("FadeOn");
                    break;

            }


            yield return new WaitForSeconds(1f);

            switch (pTownMap)
            {
                case TownMap.TulTulTown:
                    moveAni.Play("FadeOff");
                    break;
                case TownMap.NekoJobBank:
                case TownMap.ChefPauxsCookingUtensils:
                case TownMap.LongNoseCompany:
                    moveAni.Play("CircleOff");
                    break;
            }

            ActTownMove(nowArea, pTownMap);
            nowArea = pTownMap;
            isMove = false;
        }
        StartCoroutine(Run());
    }
}
