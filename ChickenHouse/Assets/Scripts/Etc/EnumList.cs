
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
    /** �ٺ�ť ��� **/
    BBQ_Spicy       = 14,
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

public enum GuestType
{ 
    /** ���� **/
    None                = 0,

    /** �Ǹ�                  <�����̿��� �޴��� Ʋ���� 0�� ������ ��> **/
    Devil               = 1,

    /** õ��                  <���͸��� �޴��� ������ �ּ� 3�� ������ ��> **/
    Angel               = 2,

    /** ������                <�ֹ��� �ƹ��� �ʰ� ���͵� ������ ���ڰ� ���� ����> **/
    Leisurely           = 3,

    /** ����                  <�ֹ��� ���� ���� �ȳ����� ������ ���ڰ���> **/
    Haste               = 4,

    /** ����ġ��Ŀ            <�մ��� �̾߱⸦ �� �����ʰ� �ֹ����ΰ��� ������ ���ڰ���> **/
    Too_Much_Talker     = 5,

    /** ö����                <�մ��� ��û�̶� �ٸ� �ֹ��޴��� ������ ������ �ſ� ���ڰ���> **/
    Thoroughness        = 6,

    /** �̽İ�                <ġŲ�� �¿�ų� ġŲ�� �������� ������ ������ �ſ� ���ڰ���> **/
    Epicurean           = 7,

    /** ��İ�                <ġŲ�� ������ 6���� ��Ŵ> **/
    Big_Eater           = 8,

    /** �ҽİ�                <ġŲ�� ������ 4���� ��Ŵ> **/
    Light_Eater         = 9,

    /** �ſ�� �ŴϾ�         <���� Ȯ���� �ſ���� ��Ŵ> **/
    Hot_Mania           = 10,

    /** �ܸ� �ŴϾ�           <���� Ȯ���� �ܰ��� ��Ŵ> **/
    Sweet_Mania         = 11,

    /** �ݶ� �ŴϾ�           <������ �ݶ� ��Ŵ> **/
    Cola_Mania          = 12,

    /** ��Ŭ �ŴϾ�           <������ ��Ŭ�� ��Ŵ> **/
    Pickle_Mania        = 13,

}

public enum Sound
{
    None = 0,

    //-------------------------------------------------------------------------------
    /** ����� **/
    InGame_BG   = 100,
    Title_BG    = 101,


    //-------------------------------------------------------------------------------
    /** �⸧ Ƣ��� �Ҹ� **/
    Oil_SE = 200,

    /** ������ �δ� �Ҹ� **/
    Put_SE = 201,

    /** ��Ҹ� ��� **/
    Voice0_SE = 250,
    Voice1_SE = 251,
    Voice2_SE = 252,
    Voice3_SE = 253,
    Voice4_SE = 254,
    Voice5_SE = 255,
    Voice6_SE = 256,
}

public enum Upgrade
{
    None                = 0,

    /** �⸧�� ���׷��̵� **/
    OIL_Zone_1          = 101,      //ġŲ�� �� ���� Ƣ�����ϴ�.(�ӵ� +40%)
    OIL_Zone_2          = 102,      //ġŲ�� �� ���� Ƣ�����ϴ�.(�ӵ� +80%)
    OIL_Zone_3          = 103,      //ġŲ�� �� ���� Ƣ�����ϴ�.(�ӵ� +120%)
    OIL_Zone_4          = 104,      //ġŲ�� �� ���� Ƣ�����ϴ�.(�ӵ� +160%)
    OIL_Zone_5          = 105,      //ġŲ�� �� ���� Ƣ�����ϴ�.(�ӵ� +200%)
    OIL_Zone_6          = 106,      //ġŲ�� Ÿ�� �ʽ��ϴ�.
    OIL_Zone_MAX        = 150,      //MAX


    /** ������ ���׷��̵�(�� ������ �������� ��ġ �ջ�) **/
    Recipe_1            = 201,      //����ġŲ(���� +20%)
    Recipe_2            = 202,      //�Ҵ�ġŲ(���� +20%)
    Recipe_3            = 203,      //����ŬġŲ(���� +20%)
    Recipe_4            = 204,      //�ٺ�ťġŲ(���� +20%)
    Recipe_MAX          = 250,      //MAX

    /** ���� ���׷��̵� **/
    Advertisement_1     = 301,      //���� ���׷��̵�(�մ� ������ -10%);
    Advertisement_2     = 302,      //���� ���׷��̵�(�մ� ������ -20%);
    Advertisement_3     = 303,      //���� ���׷��̵�(�մ� ������ -30%);
    Advertisement_4     = 304,      //���� ���׷��̵�(�մ� ������ -40%);
    Advertisement_5     = 305,      //���� ���׷��̵�(�մ� ������ -50%);
    Advertisement_MAX   = 350,      //MAX

    MAX                 = 10000,
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
    /** �� ���� **/
    Total_Profit        = 5,
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

    /** ī���ͷ� �������� **/
    Tuto_7,

    /** Ʃ�丮�� �Ϸ� **/
    Tuto_Complete
}

public enum Scene
{
    /** �ΰ� **/
    LOGO    = 0,

    /** Ÿ��Ʋ **/
    TITLE   = 1,

    /** �ΰ��� **/
    INGAME  = 2,

    /** ���� ���׷��̵� **/
    UPGRADE = 3,
}

public enum SceneChangeAni
{
    NOT     = 0,
    FADE    = 1,
    CIRCLE  = 2,
}


public enum Language
{
    Korea   = 100,
    English = 200,
}