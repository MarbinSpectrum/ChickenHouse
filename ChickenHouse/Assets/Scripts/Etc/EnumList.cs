
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

    /** ġŲ �ڽ�(����) **/
    Chicken_Pack    = 9,
    /** ġŲ �ڽ�(����) **/
    Chicken_Pack_Holl = 16,

    /** ġŲ �ҽ� **/
    Hot_Spicy       = 7,
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

    /** ġŲ �� **/
    Chicken_Radish = 8,
    /** ��Ŭ **/
    Pickle = 18,
    /** �ڿｽ�� **/
    Coleslaw = 19,
    /** �ܻ����� **/
    CornSalad = 20,
    /** ���� Ƣ�� **/
    FrenchFries = 21,
    /** �ʰ� **/
    ChickenNugget = 22,

    /** �ݶ� **/
    Cola            = 10,
    /** ���� **/
    Beer            = 17,
    /** ����� ź�� **/
    SuperPower      = 23,
    /** �޷и� ź�� **/
    LoveMelon       = 24,
    /** �Ҵٸ� ź�� **/
    SodaSoda        = 25,




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
    /** �Ƹ������� **/
    Armadillo = 15,
    /** �ϱذ� **/
    PolarBear = 16,
    /** �ں�� **/
    Cobra = 17,
    /** �Ǿ� **/
    Crocodile = 18,
    /** �ٶ��� **/
    Squirrel = 19,
    /** ����Ĺ **/
    Serval = 20,
    /** ��� **/
    Crow = 21,
    /** �񵵸� ������ **/
    FrilledLizard = 22,
    /** �⸰ **/
    Graffe = 23,
    /** �ź��� **/
    Turtle = 24,
    /** ������ **/
    Lizard = 25,
    /** �����̸� **/
    MalayanTapir = 26,
    /** �����۶��̴� **/
    SugarGlider = 27,
    /** �հ��޹� **/
    Cockatiel = 28,


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
    Morning_BG  = 105,
    Ready_BG    = 106,
    Event_0_BG  = 107,

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
    /** �а��� �Ҹ� **/
    Flour_0_SE = 211,
    Flour_1_SE = 212,
    Flour_2_SE = 213,
    /** ��� �Ҹ� **/
    Egg_Stir_SE = 214,
    Chicken_Strainter_SE = 215,

    /** ��Ҹ� ��� **/
    //�� & �ϱذ�
    Voice0_SE   = 250,

    //���� & ����� & �ں�� & �ϴôٶ���
    Voice1_SE   = 251,
    Voice2_SE   = 252,

    //���ָӴ���
    Voice3_SE = 253,
    Voice4_SE   = 254,

    //�������Ͼ� ����
    Voice5_SE   = 255,
    Voice6_SE   = 256,

    //������
    Voice7_SE   = 257,
    Voice8_SE   = 258,

    //�ö�ְ� & �ٶ���
    Voice9_SE   = 259,
    Voice10_SE  = 260,

    //��ѱ�
    Voice11_SE  = 261,
    Voice12_SE  = 262,

    //ȣ���� & �Ǵ� & �����̸�
    Voice13_SE = 263,
    Voice14_SE  = 264,

    //���
    Voice15_SE  = 265,
    Voice16_SE  = 266,

    //�����Ǵ� & ����
    Voice17_SE = 267,
    Voice18_SE = 268,

    //�䳢 & �񵵸�������
    Voice19_SE = 269,
    Voice20_SE = 270,

    //�罿 & �հ��޹�
    Voice21_SE = 271,
    Voice22_SE = 272,

    //�ش� & �ٹ��� ������
    Voice23_SE = 273,
    Voice24_SE = 274,

    //�Ǿ�
    Voice25_SE = 275,

    //���̵� �����
    Voice26_SE = 276,

    //�ڳ��� & �⸰
    Voice27_SE = 277,

    //����
    Voice28_SE = 278,

    //�Ƹ�������
    Voice29_SE = 279,
    Voice30_SE = 280,

    //�ź���
    Voice31_SE = 281,

    //������
    Voice32_SE = 282,

    //�ҵ�
    Voice33_SE = 282,

    /** ��Ҹ� ��� **/
    //�ʱ��� ���¼Ҹ�
    Laugh0_SE = 400,

    MAX
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
    SuperPower          = 234,      //����� ź�� ���� 
    LoveMelon           = 235,      //�޷и� ź�� ����
    SodaSoda            = 236,      //�Ҵٸ� ź�� ����

    /** ���̵� �޴� **/
    ChickenRadish       = 261,      //ġŲ��
    Pickle              = 262,      //��Ŭ
    Coleslaw            = 263,      //�ڿ콽��
    CornSalad           = 264,      //�ܻ�����
    FrenchFries         = 265,      //����ġ ������
    ChickenNugget       = 266,      //ġŲ�ʰ�

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

public enum KitchenSetWorkerPos
{
    CounterWorker   = 0,    //ī���� ����
    PrepWorker      = 1,    //ġŲ �����/�а��� ������ ����
    FryingWorker    = 2,    //ġŲ Ƣ��� ����

    MAX,
    None
}

public enum MenuSetPos
{
    Spicy0 = 0,
    Spicy1 = 1,
    Spicy2 = 2,
    Spicy3 = 3,
    Spicy4 = 4,
    SpicyMAX,

    Drink0 = 0,
    Drink1 = 1,
    Drink2 = 2,
    Drink3 = 3,
    DrinkMAX,

    SideMenu0 = 0,
    SideMenu1 = 1,
    SideMenu2 = 2,
    SideMenu3 = 3,
    SideMenuMAX,

    None = -1,
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
    SuperPower      = 3,
    LoveMelon       = 4,
    SodaSoda        = 5,

    MAX,

    Anything        = 1000000,
}

public enum SideMenu
{
    None            = 0,
    /** ġŲ �� **/
    ChickenRadish  = 1,
    /** ��Ŭ **/
    Pickle = 2,
    /** �ڿｽ�� **/
    Coleslaw = 3,
    /** �ܻ����� **/
    CornSalad = 4,
    /** ���� Ƣ�� **/
    FrenchFries     = 5,
    /** �ʰ� **/
    ChickenNugget = 6,

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
    None = -1,

    /** �ֹ����� �̵���ư ���� **/
    Tuto_0  = 0,

    /** ���� ġŲ���� �ֱ� **/
    Tuto_1  = 1,

    /** �ް��� ������ **/
    Tuto_2  = 2,

    /** ���� �а��������� �ű�� **/
    Tuto_3  = 3,

    /** �а��� ������ **/
    Tuto_4  = 4,

    /** �ش� �۾� �ݺ� **/
    Tuto_5  = 5,

    /** ġŲ ������ �� �ű�� **/
    Tuto_6  = 6,

    /** Ƣ���� ġŲ �ű�� **/
    Tuto_7  = 7,

    /** ġŲ Ƣ��� ���� **/
    Tuto_8_0 = 8,
    Tuto_8_1 = 9,

    /** ���� ��� �ȿ� �ֱ� **/
    Tuto_8_2 = 10,

    /** �ֹ� Ȯ�� �� ��ǥ ��� **/
    Tuto_9_0 = 11,
    Tuto_9_1 = 12,

    /** �ݶ� �� ��Ŭ �ֱ� **/
    Tuto_10 = 13,

    /** ġŲ �ֱ� **/
    Tuto_11 = 14,

    /** ī���ͷ� �������� **/
    Tuto_12 = 15,


    ////////////////////////////////////////////////////////////////////////
    /** ���� ��ġ **/
    Worker_Tuto_1_0 = 100,
    Worker_Tuto_1_1 = 101,
    Worker_Tuto_2_0 = 102,
    Worker_Tuto_2_1 = 103,
    Worker_Tuto_2_2 = 104,

    ////////////////////////////////////////////////////////////////////////
    /** �޴� ��ġ **/
    Menu_Tuto_1 = 200,
    Menu_Tuto_2_0 = 201,
    Menu_Tuto_2_1 = 202,
    Menu_Tuto_2_2 = 203,

    ////////////////////////////////////////////////////////////////////////
    /** ���� �� �ϱ��� **/
    Town_Tuto_1 = 300,
    Town_Tuto_2 = 301,
    Town_Tuto_3 = 302,
    Town_Tuto_3_1 = 314,
    Town_Tuto_3_2 = 315,
    Town_Tuto_3_3 = 316,
    Town_Tuto_3_4 = 317,
    Town_Tuto_3_5 = 318,
    Town_Tuto_4 = 303,
    Town_Tuto_5 = 304,
    Town_Tuto_6 = 305,
    Town_Tuto_7 = 306,
    Town_Tuto_8 = 307,
    Town_Tuto_9 = 308,
    Town_Tuto_10 = 309,
    Town_Tuto_11 = 310,
    Town_Tuto_12 = 311,
    Town_Tuto_13 = 312,
    Town_Tuto_14 = 313,

    ////////////////////////////////////////////////////////////////////////
    /** ġŲ ������ ��� **/
    Event_0_Tuto_1 = 400,
    Event_0_Tuto_2 = 401,
    Event_0_Tuto_3 = 402,
    Event_0_Tuto_4 = 403,
    Event_0_Tuto_5 = 404,
    Event_0_Tuto_6 = 405,
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
    TOWN    = 4,

    /** �̺�Ʈ 0 **/
    EVENT_0 = 5,

    /** ��� ���� **/
    BAD_END = 6,

    /** ���� **/
    DEMO,
}

public enum CookLvStat
{
    None                    = 0,
    /** ġŲ �⺻ ���� �λ�(��) **/
    IncreaseChickenPrice = 1,
    /** ġŲ ���� ����(��) **/
    DecreaseChickenRes = 2,
    /** ���� ���� ����(��) **/
    DecreaseDrinkRes = 3,
    /** ��Ŭ ���� ����(��) **/
    DecreasePickleRes = 4,
    /** ���� �ӵ� ����(%)   **/
    WorkerSpeedUp           = 5,
    /** ���� ����(%)        **/
    IncomeUp                = 6,
    /** ���� ����(%)        **/
    ShopSale                = 7,
    /** �մ� �湮��(%)      **/
    GuestSpawnSpeed          = 8,
    /** �մ� �γ��� ����(%) **/
    GuestPatience           = 9,
    /** �մ� ��(%)          **/
    Tip                     = 10,
    /** �Ӵ��(C)           **/
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

public enum Event_0_Battle_Result
{
    None,
    Draw,
    Win,
    Lose,
}

public enum Quest
{
    /** ����Ʈ ���� **/
    None              = -1,
    MainQuest_1       = 0,        //������ ���� ����Ʈ

    Event_0_Quest      = 100,      //ġŲ�� ��Ʋ

    SpicyQuest_1      = 1000,     //ġŲ 5���� �ȱ�                (����ġŲ ����)
    SpicyQuest_2      = 1001,     //����ġŲ 5���� �ȱ�            (�Ҵ�ġŲ ����)

    SeaOtterQuest     = 1100,     //���� ���� 20,000�óѱ��        (����Ŭ ġŲ ����)
    //SpicyQuest_4      = 1003,     //����Ŭ ġŲ 10���� �ȱ�        (������� ġŲ ����)
    //SpicyQuest_5      = 1004,     //�������ġŲ 10���� �ȱ�     (BBQ ġŲ ����) 
    //SpicyQuest_6      = 1005,     //BBQġŲ 10���� �ȱ�    

    DrinkQuest_1      = 2000,     //�ݶ� 20�� �ȱ�                 (���� ����)
    DrinkQuest_2      = 2001,     //���� 20�� �ȱ�          

    MAX,
}

public enum QuestState
{
    Not         = 0,    //������� 
    Run         = 1,    //������
    Complete    = 2,    //�Ϸ���
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

public enum KeyBoardValue
{
    NONE    = 0,
    RIGHT   = 1,
    LEFT    = 2,


}

public enum Language
{
    NONE    = 0,
    Korea   = 100,
    English = 200,
}