
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
    None            = 0,
    /** ġŲ �ڽ� **/
    Chicken_Box     = 1,
    /** ����� **/
    Tray_Egg        = 2,
    /** �а��� **/
    Tray_Flour      = 3,
    /** ġŲ ���� **/
    Chicken_Strainter = 4,
    /** �⸧ **/
    Oil_Zone = 5,
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
    ChickenStrainter = 5,
    /** Ƣ�� ġŲ **/
    FryChicken       = 6,
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