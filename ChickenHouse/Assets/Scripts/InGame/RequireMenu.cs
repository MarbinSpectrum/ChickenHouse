using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequireMenu
{
    //손님이 원하는 메뉴에 대한 정보가 담긴 객체입니다.

    /** 손님이 기다려주는 시간 값임 **/
    private const int WAIT_TIME = 90;

    /** 기본 메뉴 가중치 **/
    private const int BASE_MENU_WEIGHT          = 1000;
    /** 매운맛 매니아 특성 보유시 메뉴 가중치 **/
    private const int HOT_MANIA_MENU_WEIGHT     = 5000;
    /** 단맛 매니아 특성 보유시 메뉴 가중치 **/
    private const int SWEET_MANIA_MENU_WEIGHT   = 5000;

    /** 반은 다른 맛 치킨을 시킬 확률 **/
    private const int HALF_PER                  = 25;

    /** 피클을 요구할 확률 **/
    private const int PICKLE_PER                = 60;

    /** 콜라를 요구할 확률 **/
    private const int COLA_PER                  = 60;

    /** 해당 시간 전까지 메뉴가 나와야된다. **/
    private float           utilTime;
    /** 원하는 치킨 갯수(0이면 아무 갯수나 상관없다) **/
    private int             chickenCnt;
    /** 원하는 치킨 소스 **/
    private ChickenSpicy[]  chickenSpicy = new ChickenSpicy[2];
    /** 콜라 필요 여부 **/
    private bool            cola;
    /** 피클 필요 여부 **/
    private bool            pickle;

    public void CreateMenu(GuestData pGuestData, float nowTime)
    {
        //pGuestData를 토대로 손님이 원하는 메뉴를 생성합니다.
        HashSet<GuestType> guestTypes = new HashSet<GuestType>();
        foreach (GuestType type in pGuestData.guestTypes)
        {
            //보유여부 파악을 편하게하기 위해서 set으로 넣어줌
            guestTypes.Add(type);
        }

        //-------------------------------------------------------------------------------------
        //기다려줄 시간의 마지노선
        utilTime = nowTime + (float)WAIT_TIME;

        //-------------------------------------------------------------------------------------
        //원하는 치킨 갯수 설정
        chickenCnt = 0;
        if (guestTypes.Contains(GuestType.Big_Eater))
        {
            //대식가 속성 보유
            chickenCnt = 6;
        }
        else if (guestTypes.Contains(GuestType.Light_Eater))
        {
            //소식가 속성 보유
            chickenCnt = 4;
        }

        //-------------------------------------------------------------------------------------
        //치킨 맛 설정
        ChickenSpicy spicy = GetSpicy(guestTypes, ChickenSpicy.Not);
        chickenSpicy[0] = spicy;
        chickenSpicy[1] = spicy;

        int halfRandomRange = Random.Range(0, 100);
        if (halfRandomRange < HALF_PER)
        {
            //반반치킨을 선택함
            //다른 소스로 선택하도록함
            ChickenSpicy spicy2 = GetSpicy(guestTypes, spicy);
            chickenSpicy[1] = spicy2;
        }

        //-------------------------------------------------------------------------------------
        //콜라 설정
        int colaRandomRange = Random.Range(0, 100);
        if (colaRandomRange < COLA_PER || guestTypes.Contains(GuestType.Cola_Mania))
        {
            //콜라를 주문하도록 결정
            cola = true;
        }
        else
        {
            //콜라 안시킴
            cola = false;
        }

        //-------------------------------------------------------------------------------------
        //피클 설정
        int pickleRandomRange = Random.Range(0, 100);
        if (pickleRandomRange < PICKLE_PER || guestTypes.Contains(GuestType.Pickle_Mania))
        {
            //피클을 주문하도록 결정
            pickle = true;
        }
        else
        {
            //피클 안시킴
            pickle = false;
        }
    }

    //---------------------------------------------------------------------------------------------------
    private ChickenSpicy GetSpicy(GuestData pGuestData, ChickenSpicy ignoreSpicy)
    {
        //손님 정보를 토대로 원하는 맛을 도출한다.
        HashSet<GuestType> guestTypes = new HashSet<GuestType>();
        foreach (GuestType type in pGuestData.guestTypes)
        {
            //보유여부 파악을 편하게하기 위해서 set으로 넣어줌
            guestTypes.Add(type);
        }
        return GetSpicy(guestTypes, ignoreSpicy);
    }

    private ChickenSpicy GetSpicy(HashSet<GuestType> pGuestSet, ChickenSpicy ignoreSpicy)
    {
        List<int>           randomRangeValue    = new List<int>();
        List<ChickenSpicy>  randomChickenValue  = new List<ChickenSpicy>();

        //-------------------------------------------------------------------------------------
        //기본맛
        if(ignoreSpicy != ChickenSpicy.None)
        {
            randomRangeValue.Add(BASE_MENU_WEIGHT);
            randomChickenValue.Add(ChickenSpicy.None);
        }

        //-------------------------------------------------------------------------------------
        //매운맛
        if (ignoreSpicy != ChickenSpicy.Hot)
        {
            if (pGuestSet.Contains(GuestType.Hot_Mania))
            {
                //매운맛 매니아 특성을 보유중임
                randomRangeValue.Add(HOT_MANIA_MENU_WEIGHT);
            }
            else
            {
                randomRangeValue.Add(BASE_MENU_WEIGHT);
            }
            randomChickenValue.Add(ChickenSpicy.Hot);
        }

        //-------------------------------------------------------------------------------------
        //어떤 맛 치킨인지 랜덤하게 설정
        int randomRange = 0;
        randomRangeValue.ForEach((x) => randomRange += x);

        int randomValue = Random.Range(0, randomRange);
        for(int i = 0; i < randomChickenValue.Count; i++)
        {
            int checkValue = randomRangeValue[i];
            if(randomValue < checkValue)
            {
                return randomChickenValue[i];
            }
            randomValue -= checkValue;
        }

        //-------------------------------------------------------------------------------------
        //여기 오면 안되는데 만약 오게되면 기본 맛 호출
        return ChickenSpicy.None;
    }

    public string GetChickenName()
    {
        //주문한 치킨 이름 반환
        switch(chickenSpicy[0])
        {
            case ChickenSpicy.None:
                {
                    switch (chickenSpicy[1])
                    {
                        case ChickenSpicy.None:
                            return LanguageMgr.GetText("FRIED_CHICKEN");
                        case ChickenSpicy.Hot:
                            return LanguageMgr.GetText("FRIED_AND_SEASONED_CHICKEN_IN_HALF_AND_HALF_EACH");
                    }
                }
                break;
            case ChickenSpicy.Hot:
                {
                    switch (chickenSpicy[1])
                    {
                        case ChickenSpicy.None:
                            return LanguageMgr.GetText("FRIED_AND_SEASONED_CHICKEN_IN_HALF_AND_HALF_EACH");
                        case ChickenSpicy.Hot:
                            return LanguageMgr.GetText("HOT_SPICY_CHICKEN");
                    }
                }
                break;
        }
        return string.Empty;
    }

    public List<string> GetSideMenuName()
    {
        //보조 메뉴들의 이름을 반환
        List<string> result = new List<string>();
        if(cola)
        {
            result.Add(LanguageMgr.GetText("COLA"));
        }
        if (pickle)
        {
            result.Add(LanguageMgr.GetText("PICKLE"));
        }
        return result;
    }
}
