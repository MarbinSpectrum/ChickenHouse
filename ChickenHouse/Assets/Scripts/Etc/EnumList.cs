
public enum LookArea
{
    /** None **/
    None            =   0,
    /** ī���� **/
    Counter         =   1,
    /** �ֹ� **/
    Kitchen         =   2, 

}

public enum DragArea
{
    /** None **/
    None                = 0,
    /** ġŲ �ڽ� **/
    Chicken_Box         = 1,
    /** ����� **/
    Tray_Egg            = 2,
    /** �а��� **/
    Tray_Flour          = 3,
    /** ġŲ ���� **/
    Chicken_Strainter   = 4,
    /** �⸧ **/
    Oil_Zone            = 5,
    /** ������ ��ư **/
    Trash_Btn           = 6,
    /** ġŲ �� **/
    Chicken_Pack        = 7,
    /** ġŲ �ҽ� **/
    Hot_Spicy           = 8,
    /** ġŲ ���� **/
    Chicken_Slot        = 9,
    /** ���̵� �޴� ���� **/
    SideMenu_Slot       = 10,
    /** �����(Bowl) **/
    Bowl_Egg            = 11,
    /** �а���2 **/
    Tray_Flour2         = 12,
}

public enum GuestReviews
{
    /** ��г��� **/
    Bad                 = 1,
    /** ���� **/
    Normal              = 2,
    /** ��� **/
    Good                = 3,
    /** �ູ **/
    Happy               = 4,
}

public enum DragZone
{
    None,
    KitchenTable,
    OilZone,
    SpicyTable,
}

public enum DragState
{
    /** None **/
    None            = 0,
    /** �巡�� �Ұ� **/
    DontDrag        = 1,
    /** �⺻ **/
    Normal          = 2,
    /** ����� ���� **/
    Egg             = 3,
    /** �а��� ���� **/
    Flour           = 4,
    /** ġŲ ���� **/
    Chicken_Strainter = 5,
    /** Ƣ�� ġŲ **/
    Fry_Chicken       = 6,
    /** ġŲ �ҽ� **/
    Hot_Spicy       = 7,
    /** ġŲ �� **/
    Chicken_Pickle   = 8,
    /** ġŲ �ڽ�(����) **/
    Chicken_Pack    = 9,
    /** �ݶ� **/
    Cola            = 10,
    /** ���� ��� **/
    Soy_Spicy       = 11,
    /** �Ҵ� ��� **/
    Hell_Spicy      = 12,
    /** �Ѹ�Ŭ ��� **/
    Prinkle_Spicy   = 13,
    /** ������� ��� **/
    Carbonara_Spicy = 14,
    /** �ٺ�ť ��� **/
    BBQ_Spicy       = 15,
    /** ġŲ �ڽ�(����) **/
    Chicken_Pack_Holl = 16,
    /** ���� **/
    Beer            = 17,
}

public enum ChickenState
{
    /** ���������� **/
    NotCook     = -1,
    /** ������ ġŲ **/
    BadChicken_0 = 0,
    /** ����ź ġŲ **/
    BadChicken_1 = 2,
    /** ������ ġŲ **/
    BadChicken_2 = 3,
    /** ���ִ�ġŲ **/
    GoodChicken = 1,
}

public enum ChickenSpicy
{
    /** ���� **/
    Not         = -1,
    /** �ҽ� ���� **/
    None        = 0,
    /** ��� ġŲ �ҽ� **/
    Hot         = 1,
    /** ���� �ҽ� **/
    Soy         = 2,
    /** �Ҵ� �ҽ� **/
    Hell        = 3,
    /** �Ѹ�Ŭ �ҽ� **/
    Prinkle     = 4,
    /** �ٺ�ť �ҽ� **/
    BBQ         = 5,
    /** ������� �ҽ� **/
    Carbonara   = 6,

    MAX
}


public enum Guest
{
    /** ���� **/
    None     = 0,
    /** ���� **/
    Fox      = 1,
    /** ������ **/
    Dog      = 2,
    /** ����� **/
    Cat      = 3,
    /** ȣ���� **/
    Tiger    = 4,
    /** �˶� ���� ���� ������ **/
    Lemur    = 5,
    /** ����Ͼ� ���� **/
    TasmanianDevil = 6,
    /** �����Ͼ� �ָӴ��� **/
    VirginiaOpossum = 7,
    /** �ö�ְ� **/
    Flamingo = 8,
    /** ��ѱ� **/
    Dove = 9,
    /** �Ҵ� **/
    Panda = 10,
    /** �����Ҵ� **/
    RedPanda = 11,
    /** �䳢 **/
    Rabbit = 12,
    /** �ش� **/
    SeaOtter = 13,
    /** �罿 **/
    Deer = 14,

    MAX
}

public enum Sound
{
    None = 0,

    //-------------------------------------------------------------------------------
    /** ����� **/
    InGame_BG   = 100,
    Title_BG    = 101,
    Shop_BG     = 102,
    Prologue_BG = 103,
    Town_BG     = 104,

    //-------------------------------------------------------------------------------
    /** �⸧ Ƣ��� �Ҹ� **/
    Oil_SE = 200,
    /** ������ �δ� �Ҹ� **/
    Put_SE = 201,
    /** �� ȹ�� �Ҹ� **/
    GetMoney_SE = 202,
    /** Ƣ������ �Ϸ� �Ҹ� **/
    Oil_Zone_End_SE = 203,
    /** �ҽ�ȹ�� **/
    GetSpicy_SE = 204,
    /** �ֹ��߰� **/
    NewOrder_SE = 205,
    /** ��ư �Ҹ� **/
    Btn_SE = 206,
    /** �ȴ� �Ҹ� **/
    Walk_SE = 207,
    /** ���� �Ҹ� **/
    Paper_SE = 208, 
    /** ������ �Ҹ� **/
    Page_SE = 209,
    /** ���� �Ҹ� **/
    Stamp_SE = 210,

    /** ��Ҹ� ��� **/
    //��
    Voice0_SE   = 250,

    //���� & �����
    Voice1_SE   = 251,
    Voice2_SE   = 252,

    //�ٹ��� ������
    Voice3_SE   = 253,
    Voice4_SE   = 254,

    //�������Ͼ� ����
    Voice5_SE   = 255,
    Voice6_SE   = 256,

    //���ָӴ��� & ������
    Voice7_SE   = 257,
    Voice8_SE   = 258,

    //�ö�ְ�
    Voice9_SE   = 259,
    Voice10_SE  = 260,

    //��ѱ�
    Voice11_SE  = 261,
    Voice12_SE  = 262,

    //ȣ����
    Voice13_SE  = 263,
    Voice14_SE  = 264,

    //�Ǵ�
    Voice15_SE  = 265,
    Voice16_SE  = 266,

    //�����Ǵ�
    Voice17_SE = 267,
    Voice18_SE = 268,

    //�䳢
    Voice19_SE = 269,
    Voice20_SE = 270,

    //�罿
    Voice21_SE = 271,
    Voice22_SE = 272,

    //�ش�
    Voice23_SE = 273,
    Voice24_SE = 274,

    //�ҵ�
    Voice25_SE = 275,

    //���̵� �����
    Voice26_SE = 276,

    //�ڳ���
    Voice27_SE = 277,
}

public enum TalkBoxType
{
    Normal  = 0,
    Angry   = 1,
    Thank   = 2,
    Happy   = 3,

}

public enum UtensilShopMenu
{
    Fryer_Buy       = 0,    //Ƣ��� ����
    Fryer_Add       = 1,    //Ƣ��� �߰�
}

public enum ShopItem
{
    None                = 0,

    /** �⸧�� ���׷��̵� **/
    OIL_Zone_1          = 101,      //�Ϲ� Ƣ��� �Դϴ�.
    OIL_Zone_2          = 102,      //ġŲ�� ���� Ƣ�����ϴ�. (�ӵ� +40%)
    OIL_Zone_3          = 103,      //ġŲ�� �� ���� Ƣ������ �� ���־����ϴ�. (�ӵ� +80%, ����+20%)
    OIL_Zone_4          = 104,      //ġŲ�� �Ϻ��ϰ� Ƣ�����ϴ�. (�ӵ� +160%, ����+40%, ġŲ�� Ÿ�� �ʽ��ϴ�.)

    /** �⸧�� �߰� **/
    NEW_OIL_ZONE_1      = 151,      //�⸧�� �߰�
    NEW_OIL_ZONE_2      = 152,      //�⸧�� �߰�
    NEW_OIL_ZONE_3      = 153,      //�⸧�� �߰�

    /** ������ **/
    Recipe_0            = 200,      //���ġŲ
    Recipe_1            = 201,      //����ġŲ
    Recipe_2            = 202,      //�Ҵ�ġŲ
    Recipe_3            = 203,      //����ŬġŲ
    Recipe_4            = 204,      //�������
    Recipe_5            = 205,      //�ٺ�ťġŲ

    /** ���� **/
    Cola                = 231,      //�ݶ�
    Beer                = 232,      //����

    /** ���̵� �޴� **/
    Pickle              = 261,      //ġŲ��

    /** ���� ���׷��̵� **/
    Advertisement_1     = 301,      //���� ���׷��̵�(�մ� ������ -5%);
    Advertisement_2     = 302,      //���� ���׷��̵�(�մ� ������ -7%,���� +10%);
    Advertisement_3     = 303,      //���� ���׷��̵�(�մ� ������ -10%,���� +10%);
    Advertisement_4     = 304,      //���� ���׷��̵�(�մ� ������ -15%,���� +10%);
    Advertisement_5     = 305,      //���� ���׷��̵�(�մ� ������ -20%,���� +10%);

    MAX                 = 10000,
}

public enum EWorker
{
    None        = 0,

    Worker_1    = 1,
    Worker_2    = 2,
    Worker_3    = 3,
    Worker_4    = 4,

    MAX
}

public enum WorkerSkill
{
    WorkerSkill_1    = 100,      //�ֹ溸�� �����(�ֹ濡���� ������ +50%)
    WorkerSkill_2    = 200,      //ġŲ���� �����(�ֹ濡���� ������ +100%)
    WorkerSkill_3    = 300,      //Ƣ�� ������(Ƣ��� �ӵ�+100%, ġŲ�� Ÿ�� ����)
    WorkerSkill_4    = 400,      //�߻���ܸ�(�� ���� +100%)
    WorkerSkill_5    = 500,      //ī���� ���� �����(ī���Ϳ� ��ġ�� �մ��� �湮�� +50%)
    WorkerSkill_6    = 600,      //�Ժ�(���� ġŲ �� ������ �� ����)
    WorkerSkill_7    = 700,      //���ɼ�(�մ��� ���ϴ� ġŲ ����� �˷���)
    WorkerSkill_8    = 800,      //�Ǹ���(ī���Ϳ� ��ġ�� 2% Ȯ���� �մ��� �ֹ��� ����� ���޵��� ����)
}

public enum WorkerCounterTalkBox
{
    Bad,
    Normal,
    Good
}

public enum DayEndList
{
    /** ���� **/
    None                = 0,
    /** ���� ���� **/
    Store_Revenue       = 1,
    /** ü���� ���� **/
    Chain_Store_Revenue = 2,
    /** ���� �Ӵ�� **/
    Rent                = 3,
    /** ��� �� **/
    Supplies_Uesd       = 4,
    /** �޿� **/
    Salary              = 5,

    /** �� ���� **/
    Total_Profit        = 100,
}

public enum Drink
{
    None            = 0,
    Cola            = 1,
    Beer            = 2,

    MAX,
}

public enum SideMenu
{
    None            = 0,
    Pickle          = 1,

    MAX,
}

public enum WorkerHandState
{
    /** ����(�����̵�) **/
    None                = 0,
    /** �Ϲ� ġŲ ������������ **/
    NormalChicken       = 1,
    /** ����� ġŲ ������������ **/
    EggChicken          = 2,
    /** �а��� ���� ġŲ ������������ **/
    FlourChicken        = 3,
    /** ����� ������ ���ؼ� ������ ���°� **/
    HandShake           = 4,
    /** �а��� ���� ġŲ�� ����ִ� ġŲ���� ��� **/
    StrainterFlour      = 5,
    /** Ƣ��Ⱑ ���� ġŲ���� ��� **/
    StrainterFry        = 6,
}

public enum Tutorial
{
    /** ���� **/
    None,

    /** ���� ġŲ���� �ֱ� **/
    Tuto_1,

    /** �ް��� ������ **/
    Tuto_2,

    /** ���� �а��������� �ű�� **/
    Tuto_3,

    /** �а��� ������ **/
    Tuto_4,

    /** �ش� �۾� �ݺ� **/
    Tuto_5,

    /** ġŲ ������ �� �ű�� **/
    Tuto_6,

    /** Ƣ���� ġŲ �ű�� **/
    Tuto_7,

    /** ġŲ Ƣ��� ���� **/
    Tuto_8_0,
    Tuto_8_1,

    /** ���� ��� �ȿ� �ֱ� **/
    Tuto_8_2,

    /** �ֹ� Ȯ�� �� ��ǥ ��� **/
    Tuto_9_0,
    Tuto_9_1,

    /** �ݶ� �� ��Ŭ �ֱ� **/
    Tuto_10,

    /** ġŲ �ֱ� **/
    Tuto_11,

    /** ī���ͷ� �������� **/
    Tuto_12,


    ////////////////////////////////////////////////////////////////////////
    /** ���� ��ġ **/
    Worker_Tuto_1_0,
    Worker_Tuto_1_1,
    Worker_Tuto_2_0,
    Worker_Tuto_2_1,
    Worker_Tuto_2_2,

    ////////////////////////////////////////////////////////////////////////
    /** �޴� ��ġ **/
    Menu_Tuto_1,
    Menu_Tuto_2_0,
    Menu_Tuto_2_1,
    Menu_Tuto_2_2,

    ////////////////////////////////////////////////////////////////////////
    /** ���� �� �ϱ��� **/
    Town_Tuto_1,
    Town_Tuto_2,
    Town_Tuto_3,
    Town_Tuto_4,
    Town_Tuto_5,
    Town_Tuto_6,
    Town_Tuto_7,
    Town_Tuto_8,
    Town_Tuto_9,
    Town_Tuto_10,
    Town_Tuto_11,
    Town_Tuto_12,
    Town_Tuto_13,
}

public enum Scene
{
    /** �ΰ� **/
    LOGO    = 0,

    /** Ÿ��Ʋ **/
    TITLE   = 1,

    /** ���ѷα� **/
    PROLOGUE = 2,

    /** �ΰ��� **/
    INGAME  = 3,

    /** ���� **/
    SHOP    = 4,

    /** ���� **/
    TOWN    = 5,

    /** ���� **/
    DEMO,
}

public enum CookLvStat
{
    None                    = 0,
    /** ġŲ �⺻ ���� �λ�($) **/
    IncreaseChickenPrice    = 1,
    /** ġŲ ���� ����($) **/
    DecreaseChickenRes      = 2,
    /** ���� ���� ����($) **/
    DecreaseDrinkRes        = 3,
    /** ��Ŭ ���� ����($) **/
    DecreasePickleRes       = 4,
    /** ���� �ӵ� ����(%)   **/
    WorkerSpeedUp           = 5,
    /** ���� ����(%)        **/
    IncomeUp                = 6,
    /** ���� ����(%)        **/
    ShopSale                = 7,
    /** �մ� �湮��(%)      **/
    GuestSpawnRate          = 8,
    /** �մ� �γ��� ����(%) **/
    GuestPatience           = 9,
    /** �մ� ��(%)          **/
    Tip                     = 10,
    /** �Ӵ��($)           **/
    Rent                    = 11,
}

public enum DiaryMenu
{
    Quest,
    Book,
    File,
}

public enum BookMenu
{
    Guest,
    Seasoning,
    Etc,
}

public enum Quest
{
    /** ����Ʈ ���� **/
    None              = -1,
    MainQuest_1       = 0,        //������ ���� ����Ʈ


    SpicyQuest_1      = 1000,     //ġŲ 5���� �ȱ�                (����ġŲ ����)
    SpicyQuest_2      = 1001,     //����ġŲ 5���� �ȱ�            (��ġŲ ����)
    SpicyQuest_3      = 1002,     //�Ҵ�ġŲ 5���� �ȱ�            (����Ŭ ġŲ ����)
    SpicyQuest_4      = 1003,     //����Ŭ ġŲ 10���� �ȱ�        (������� ġŲ ����)
    SpicyQuest_5      = 1004,     //�������ġŲ 10���� �ȱ�     (BBQ ġŲ ����) 
    SpicyQuest_6      = 1005,     //BBQġŲ 10���� �ȱ�    

    DrinkQuest_1      = 2000,     //�ݶ� 20�� �ȱ�                 (���� ����)
    DrinkQuest_2      = 2001,     //���� 20�� �ȱ�          

    MAX,
}

public enum TownMap
{
    None = -1,

    /** ���и��� **/
    TulTulTown = 0,
    /** ���� ���� �Ұ��� **/
    NekoJobBank = 1,
    /** �Ŀ� ������ ���� ���� **/
    ChefPauxsCookingUtensils = 2,
    /** �� �� ���� ȸ�� **/
    LongNoseCompany = 3,
}

public enum SceneChangeAni
{
    NOT     = 0,
    FADE    = 1,
    CIRCLE  = 2,
}


public enum Language
{
    NONE    = 0,
    Korea   = 100,
    English = 200,
}