
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
    Chicken_Radish    = 8,
    /** ġŲ �ڽ�(����) **/
    Chicken_Pack    = 9,
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
    /** �ҽ� ���� **/
    None        = 0,
    /** ��� ġŲ �ҽ� **/
    Hot         = 1,

}

public enum Language
{
    Korea   = 100,
    English = 200,
}