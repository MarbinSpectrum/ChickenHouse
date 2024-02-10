using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequireMenu
{
    //�մ��� ���ϴ� �޴��� ���� ������ ��� ��ü�Դϴ�.

    /** �մ��� ��ٷ��ִ� �ð� ���� **/
    private const int WAIT_TIME = 90;

    /** �⺻ �޴� ����ġ **/
    private const int BASE_MENU_WEIGHT          = 1000;
    /** �ſ�� �ŴϾ� Ư�� ������ �޴� ����ġ **/
    private const int HOT_MANIA_MENU_WEIGHT     = 5000;
    /** �ܸ� �ŴϾ� Ư�� ������ �޴� ����ġ **/
    private const int SWEET_MANIA_MENU_WEIGHT   = 5000;

    /** ���� �ٸ� �� ġŲ�� ��ų Ȯ�� **/
    private const int HALF_PER                  = 25;

    /** ��Ŭ�� �䱸�� Ȯ�� **/
    private const int PICKLE_PER                = 60;

    /** �ݶ� �䱸�� Ȯ�� **/
    private const int COLA_PER                  = 60;

    /** �ش� �ð� ������ �޴��� ���;ߵȴ�. **/
    private float           utilTime;
    /** ���ϴ� ġŲ ����(0�̸� �ƹ� ������ �������) **/
    private int             chickenCnt;
    /** ���ϴ� ġŲ �ҽ� **/
    private ChickenSpicy[]  chickenSpicy = new ChickenSpicy[2];
    /** �ݶ� �ʿ� ���� **/
    private bool            cola;
    /** ��Ŭ �ʿ� ���� **/
    private bool            pickle;

    public void CreateMenu(GuestData pGuestData, float nowTime)
    {
        //pGuestData�� ���� �մ��� ���ϴ� �޴��� �����մϴ�.
        HashSet<GuestType> guestTypes = new HashSet<GuestType>();
        foreach (GuestType type in pGuestData.guestTypes)
        {
            //�������� �ľ��� ���ϰ��ϱ� ���ؼ� set���� �־���
            guestTypes.Add(type);
        }

        //-------------------------------------------------------------------------------------
        //��ٷ��� �ð��� �����뼱
        utilTime = nowTime + (float)WAIT_TIME;

        //-------------------------------------------------------------------------------------
        //���ϴ� ġŲ ���� ����
        chickenCnt = 0;
        if (guestTypes.Contains(GuestType.Big_Eater))
        {
            //��İ� �Ӽ� ����
            chickenCnt = 6;
        }
        else if (guestTypes.Contains(GuestType.Light_Eater))
        {
            //�ҽİ� �Ӽ� ����
            chickenCnt = 4;
        }

        //-------------------------------------------------------------------------------------
        //ġŲ �� ����
        ChickenSpicy spicy = GetSpicy(guestTypes, ChickenSpicy.Not);
        chickenSpicy[0] = spicy;
        chickenSpicy[1] = spicy;

        int halfRandomRange = Random.Range(0, 100);
        if (halfRandomRange < HALF_PER)
        {
            //�ݹ�ġŲ�� ������
            //�ٸ� �ҽ��� �����ϵ�����
            ChickenSpicy spicy2 = GetSpicy(guestTypes, spicy);
            chickenSpicy[1] = spicy2;
        }

        //-------------------------------------------------------------------------------------
        //�ݶ� ����
        int colaRandomRange = Random.Range(0, 100);
        if (colaRandomRange < COLA_PER || guestTypes.Contains(GuestType.Cola_Mania))
        {
            //�ݶ� �ֹ��ϵ��� ����
            cola = true;
        }
        else
        {
            //�ݶ� �Ƚ�Ŵ
            cola = false;
        }

        //-------------------------------------------------------------------------------------
        //��Ŭ ����
        int pickleRandomRange = Random.Range(0, 100);
        if (pickleRandomRange < PICKLE_PER || guestTypes.Contains(GuestType.Pickle_Mania))
        {
            //��Ŭ�� �ֹ��ϵ��� ����
            pickle = true;
        }
        else
        {
            //��Ŭ �Ƚ�Ŵ
            pickle = false;
        }
    }

    //---------------------------------------------------------------------------------------------------
    private ChickenSpicy GetSpicy(GuestData pGuestData, ChickenSpicy ignoreSpicy)
    {
        //�մ� ������ ���� ���ϴ� ���� �����Ѵ�.
        HashSet<GuestType> guestTypes = new HashSet<GuestType>();
        foreach (GuestType type in pGuestData.guestTypes)
        {
            //�������� �ľ��� ���ϰ��ϱ� ���ؼ� set���� �־���
            guestTypes.Add(type);
        }
        return GetSpicy(guestTypes, ignoreSpicy);
    }

    private ChickenSpicy GetSpicy(HashSet<GuestType> pGuestSet, ChickenSpicy ignoreSpicy)
    {
        List<int>           randomRangeValue    = new List<int>();
        List<ChickenSpicy>  randomChickenValue  = new List<ChickenSpicy>();

        //-------------------------------------------------------------------------------------
        //�⺻��
        if(ignoreSpicy != ChickenSpicy.None)
        {
            randomRangeValue.Add(BASE_MENU_WEIGHT);
            randomChickenValue.Add(ChickenSpicy.None);
        }

        //-------------------------------------------------------------------------------------
        //�ſ��
        if (ignoreSpicy != ChickenSpicy.Hot)
        {
            if (pGuestSet.Contains(GuestType.Hot_Mania))
            {
                //�ſ�� �ŴϾ� Ư���� ��������
                randomRangeValue.Add(HOT_MANIA_MENU_WEIGHT);
            }
            else
            {
                randomRangeValue.Add(BASE_MENU_WEIGHT);
            }
            randomChickenValue.Add(ChickenSpicy.Hot);
        }

        //-------------------------------------------------------------------------------------
        //� �� ġŲ���� �����ϰ� ����
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
        //���� ���� �ȵǴµ� ���� ���ԵǸ� �⺻ �� ȣ��
        return ChickenSpicy.None;
    }

    public string GetChickenName()
    {
        //�ֹ��� ġŲ �̸� ��ȯ
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
        //���� �޴����� �̸��� ��ȯ
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
