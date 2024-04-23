using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequireMenu
{
    //�մ��� ���ϴ� �޴��� ���� ������ ��� ��ü�Դϴ�.

    /** �մ��� ��ٷ��ִ� �ð� ���� **/
    private const int WAIT_TIME = 40;

    /** �⺻ �޴� ����ġ **/
    private const int BASE_MENU_WEIGHT = 1000;
    /** �ſ�� �ŴϾ� Ư�� ������ �޴� ����ġ **/
    private const int HOT_MANIA_MENU_WEIGHT = 5000;
    /** �ܸ� �ŴϾ� Ư�� ������ �޴� ����ġ **/
    private const int SWEET_MANIA_MENU_WEIGHT = 5000;

    /** ���� �ٸ� �� ġŲ�� ��ų Ȯ�� **/
    private const int HALF_PER = 25;

    /** ��Ŭ�� �䱸�� Ȯ�� **/
    private const int PICKLE_PER = 60;

    /** �ݶ� �䱸�� Ȯ�� **/
    private const int COLA_PER = 60;

    /** �ش� �ð� ������ �޴��� ���;ߵȴ�. **/
    private float utilTime;
    /** ���ϴ� ġŲ ����(0�̸� �ƹ� ������ �������) **/
    private int chickenCnt;
    /** ���ϴ� ġŲ �ҽ� **/
    private ChickenSpicy[] chickenSpicy = new ChickenSpicy[2];
    /** �ݶ� �ʿ� ���� **/
    private bool cola;
    /** ��Ŭ �ʿ� ���� **/
    private bool pickle;

    public void CreateMenu(GuestData pGuestData, float nowTime,bool isTuto)
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
        if (isTuto)
        {
            //Ʃ�丮���� ġŲ ���� �������.
        }
        else
        {
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
        }

        //-------------------------------------------------------------------------------------
        //ġŲ �� ����
        if(isTuto)
        {
            //Ʃ�丮���� ������ �����̵� ġŲ
            chickenSpicy[0] = ChickenSpicy.None;
            chickenSpicy[1] = ChickenSpicy.None;
        }
        else
        {
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
        }

        //-------------------------------------------------------------------------------------
        //�ݶ� ����
        if (isTuto)
        {
            //Ʃ�丮���� ������ �ݶ��Ŵ
            cola = true;
        }
        else
        {
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
        }

        //-------------------------------------------------------------------------------------
        //��Ŭ ����
        if (isTuto)
        {
            //Ʃ�丮���� ������ ��Ŭ��Ŵ
            pickle = true;
        }
        else
        {
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
        List<int> randomRangeValue = new List<int>();
        List<ChickenSpicy> randomChickenValue = new List<ChickenSpicy>();
        GameMgr gameMgr = GameMgr.Instance;

        //-------------------------------------------------------------------------------------
        //�⺻��
        if (ignoreSpicy != ChickenSpicy.None)
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
        //�����
        if(gameMgr.playData.upgradeState[(int)Upgrade.Recipe_1])
        {
            if (ignoreSpicy != ChickenSpicy.Soy)
            {
                randomRangeValue.Add(BASE_MENU_WEIGHT);
                randomChickenValue.Add(ChickenSpicy.Soy);
            }
        }

        //-------------------------------------------------------------------------------------
        //�Ҵ߸�
        if (gameMgr.playData.upgradeState[(int)Upgrade.Recipe_2])
        {
            if (ignoreSpicy != ChickenSpicy.Hell)
            {
                randomRangeValue.Add(BASE_MENU_WEIGHT);
                randomChickenValue.Add(ChickenSpicy.Hell);
            }
        }

        //-------------------------------------------------------------------------------------
        //�Ѹ�Ŭ��
        if (gameMgr.playData.upgradeState[(int)Upgrade.Recipe_3])
        {
            if (ignoreSpicy != ChickenSpicy.Prinkle)
            {
                randomRangeValue.Add(BASE_MENU_WEIGHT);
                randomChickenValue.Add(ChickenSpicy.Prinkle);
            }
        }

        //-------------------------------------------------------------------------------------
        //�ٺ�ť��
        if (gameMgr.playData.upgradeState[(int)Upgrade.Recipe_4])
        {
            if (ignoreSpicy != ChickenSpicy.BBQ)
            {
                randomRangeValue.Add(BASE_MENU_WEIGHT);
                randomChickenValue.Add(ChickenSpicy.BBQ);
            }
        }

        //-------------------------------------------------------------------------------------
        //� �� ġŲ���� �����ϰ� ����
        int randomRange = 0;
        randomRangeValue.ForEach((x) => randomRange += x);

        int randomValue = Random.Range(0, randomRange);
        for (int i = 0; i < randomChickenValue.Count; i++)
        {
            int checkValue = randomRangeValue[i];
            if (randomValue < checkValue)
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
        ChickenSpicy spicy0 = (ChickenSpicy)Mathf.Min((int)chickenSpicy[0], (int)chickenSpicy[1]);
        ChickenSpicy spicy1 = (ChickenSpicy)Mathf.Max((int)chickenSpicy[0], (int)chickenSpicy[1]);

        //�ֹ��� ġŲ �̸� ��ȯ
        switch (spicy0)
        {
            case ChickenSpicy.None:
                {
                    switch (spicy1)
                    {
                        case ChickenSpicy.None:
                            return LanguageMgr.GetText("FRIED_CHICKEN");
                        case ChickenSpicy.Hot:
                            return LanguageMgr.GetText("HOT_AND_FRIED_CHICKEN");
                        case ChickenSpicy.Soy:
                            return LanguageMgr.GetText("SOY_AND_FRIED_CHICKEN");
                        case ChickenSpicy.Hell:
                            return LanguageMgr.GetText("HELL_AND_FRIED_CHICKEN");
                        case ChickenSpicy.Prinkle:
                            return LanguageMgr.GetText("PRINKLE_AND_FRIED_CHICKEN");
                        case ChickenSpicy.BBQ:
                            return LanguageMgr.GetText("BBQ_AND_FRIED_CHICKEN");
                    }
                }
                break;
            case ChickenSpicy.Hot:
                {
                    switch (spicy1)
                    {
                        case ChickenSpicy.Hot:
                            return LanguageMgr.GetText("HOT_SPICY_CHICKEN");
                        case ChickenSpicy.Soy:
                            return LanguageMgr.GetText("HOT_AND_SOY_CHICKEN");
                        case ChickenSpicy.Hell:
                            return LanguageMgr.GetText("HELL_AND_HOT_CHICKEN");
                        case ChickenSpicy.Prinkle:
                            return LanguageMgr.GetText("HOT_AND_PRINKLE_CHICKEN");
                        case ChickenSpicy.BBQ:
                            return LanguageMgr.GetText("HOT_AND_BBQ_CHICKEN");
                    }
                }
                break;
            case ChickenSpicy.Soy:
                {
                    switch (spicy1)
                    {
                        case ChickenSpicy.Soy:
                            return LanguageMgr.GetText("SOY_CHICKEN");
                        case ChickenSpicy.Hell:
                            return LanguageMgr.GetText("HELL_AND_SOY_CHICKEN");
                        case ChickenSpicy.Prinkle:
                            return LanguageMgr.GetText("SOY_AND_PRINKLE_CHICKEN");
                        case ChickenSpicy.BBQ:
                            return LanguageMgr.GetText("SOY_AND_BBQ_CHICKEN");
                    }
                }
                break;
            case ChickenSpicy.Hell:
                {
                    switch (spicy1)
                    {
                        case ChickenSpicy.Hell:
                            return LanguageMgr.GetText("HELL_CHICKEN");
                        case ChickenSpicy.Prinkle:
                            return LanguageMgr.GetText("PRINKLE_AND_HELL_CHICKEN");
                        case ChickenSpicy.BBQ:
                            return LanguageMgr.GetText("BBQ_AND_HELL_CHICKEN");
                    }
                }
                break;
            case ChickenSpicy.Prinkle:
                {
                    switch (spicy1)
                    {
                        case ChickenSpicy.Prinkle:
                            return LanguageMgr.GetText("PRINKLE_CHICKEN");
                        case ChickenSpicy.BBQ:
                            return LanguageMgr.GetText("PRINKLE_AND_BBQ_CHICKEN");
                    }
                }
                break;
            case ChickenSpicy.BBQ:
                {
                    switch (spicy1)
                    {
                        case ChickenSpicy.BBQ:
                            return LanguageMgr.GetText("BBQ_CHICKEN");
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
        if (cola)
        {
            result.Add(LanguageMgr.GetText("COLA"));
        }
        if (pickle)
        {
            result.Add(LanguageMgr.GetText("PICKLE"));
        }
        return result;
    }

    public bool ChickenCntCheck(int pChickenCnt)
    {
        //ġŲ ���� �˻�
        if (chickenCnt == 0)
            return true;
        return chickenCnt == pChickenCnt;
    }

    public bool ChickenSpicyCheck(ChickenSpicy pSpicy0, ChickenSpicy pSpicy1)
    {
        //ġŲ ��� �˻�
        if ((pSpicy0 == chickenSpicy[0] && pSpicy1 == chickenSpicy[1]) || (pSpicy0 == chickenSpicy[1] && pSpicy1 == chickenSpicy[0]))
            return true;
        return false;
    }

    public GuestReviews MenuPoint(GuestData pGuestData, 
        ChickenSpicy pSpicy0, ChickenSpicy pSpicy1, ChickenState pChickenState, bool hasDrink, bool hasPickle)
    {
        GameMgr gameMgr = GameMgr.Instance;

        //�մ��� ������ ġŲ ����
        //�մ� ������ ���� ���ϴ� ���� �����Ѵ�.
        HashSet<GuestType> guestTypes = new HashSet<GuestType>();
        foreach (GuestType type in pGuestData.guestTypes)
        {
            //�������� �ľ��� ���ϰ��ϱ� ���ؼ� set���� �־���
            guestTypes.Add(type);
        }

        int point = 0;

        //--------------------------------------------------------------------------------
        //��� �˻�
        if (ChickenSpicyCheck(pSpicy0, pSpicy1))
        {
            point += 2;
        }

        //--------------------------------------------------------------------------------
        //��Ŭ�̳� �ݶ� �˻�
        if (cola == hasDrink)
        {
            point += 2;
        }
        if (pickle == hasPickle)
        {
            point += 2;
        }

        if (point < 2)
            return GuestReviews.Bad;
        else if (point < 4)
            return GuestReviews.Normal;
        else if (point < 6)
            return GuestReviews.Good;
        else 
            return GuestReviews.Happy;
    }
}
