
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
    /** ġŲ �� ���� **/
    Pickle_Slot         = 10,
    /** ���� ���� **/
    Drink_Slot          = 11,
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

    /** ��Ҹ� ��� **/
    Voice0_SE   = 250,
    Voice1_SE   = 251,
    Voice2_SE   = 252,
    Voice3_SE   = 253,
    Voice4_SE   = 254,
    Voice5_SE   = 255,
    Voice6_SE   = 256,
    Voice7_SE   = 257,
    Voice8_SE   = 258,
    Voice9_SE   = 259,
    Voice10_SE  = 260,
    Voice11_SE  = 261,
    Voice12_SE  = 262,
    Voice13_SE  = 263,
    Voice14_SE  = 264,
}

public enum TalkBoxType
{
    Normal  = 0,
    Angry   = 1,
    Thank   = 2,
    Happy   = 3,

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

    /** ������ ���׷��̵�(�� ������ �������� ��ġ �ջ�) **/
    Recipe_1            = 201,      //����ġŲ(���� +20%)
    Recipe_2            = 202,      //�Ҵ�ġŲ(���� +20%)
    Recipe_3            = 203,      //����ŬġŲ(���� +20%)
    Recipe_4            = 204,      //�������(���� +20%)
    Recipe_5            = 205,      //�ٺ�ťġŲ(���� +20%)

    /** ���� ���׷��̵� **/
    Advertisement_1     = 301,      //���� ���׷��̵�(�մ� ������ -10%);
    Advertisement_2     = 302,      //���� ���׷��̵�(�մ� ������ -20%);
    Advertisement_3     = 303,      //���� ���׷��̵�(�մ� ������ -30%);
    Advertisement_4     = 304,      //���� ���׷��̵�(�մ� ������ -40%);
    Advertisement_5     = 305,      //���� ���׷��̵�(�մ� ������ -50%);

    /** �Ƹ�����Ʈ�� ���׷��̵� **/
    Worker_1            = 401,      //�˹ٻ� ���(ġŲ�� ������� �־���)
    Worker_2            = 402,      //�˹ٻ� ����(�˹ٻ� �������� ������)
    Worker_3            = 403,      //�˹ٻ� ����(ġŲ�� Ƣ�谡�縦 ������)
    Worker_4            = 404,      //�˹ٻ� ����(�˹ٻ� �������� ������)
    Worker_5            = 405,      //�˹ٻ� ����(ġŲ�� ġŲ������ �־���)
    Worker_6            = 406,      //�˹ٻ� ����(�˹ٻ� �������� ������)

    MAX                 = 10000,
}

public enum WorkerSkill
{
    WorkerSkill_1    = 100,      //����� �����پ�
    WorkerSkill_2    = 200,      //Ƣ��� �����پ�
    WorkerSkill_3    = 300,      //������ ġŲ �־���
    WorkerSkill_4    = 400,      //���� �ȳ
    WorkerSkill_5    = 500,      //���� �ճ
    WorkerSkill_6    = 600,      //���� ���
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

}

public enum SideMenu
{
    None            = 0,
    Pickle          = 1,

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
}

public enum Tutorial
{
    /** ���� **/
    None,

    /** ���� ġŲ���� �ֱ� **/
    Tuto_1,

    /** ���� �а���� �ֱ� **/
    Tuto_2,

    /** ���� ġŲ ������ �ֱ� **/
    Tuto_3,

    /** ���� �⸧ �뿡 �ֱ� **/
    Tuto_4,

    /** ġŲ Ƣ��� ���� **/
    Tuto_5_0,
    Tuto_5_1,
    /** ���� ��� �ȿ� �ֱ� **/
    Tuto_5_2,

    /** �ݶ� �� ��Ŭ �ֱ� **/
    Tuto_6,

    /** ġŲ �ֱ� **/
    Tuto_7,

    /** ī���ͷ� �������� **/
    Tuto_8,

    /** Ʃ�丮�� �Ϸ� **/
    Tuto_Complete
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