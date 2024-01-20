
public enum LookArea
{
    /** None **/
    None            =   0,
    /** 카운터 **/
    Counter         =   1,
    /** 주방 **/
    Kitchen         =   2, 

}

public enum DragArea
{
    /** None **/
    None            = 0,
    /** 치킨 박스 **/
    Chicken_Box     = 1,
    /** 계란물 **/
    Tray_Egg        = 2,
    /** 밀가루 **/
    Tray_Flour      = 3,
    /** 치킨 건지 **/
    Chicken_Strainter = 4,
    /** 기름 **/
    Oil_Zone = 5,
}

public enum DragState
{
    /** None **/
    None            = 0,
    /** 드래그 불가 **/
    DontDrag        = 1,
    /** 기본 **/
    Normal          = 2,
    /** 계란물 묻힘 **/
    Egg             = 3,
    /** 밀가루 묻힘 **/
    Flour           = 4,
    /** 치킨 건지 **/
    ChickenStrainter = 5,
    /** 튀긴 치킨 **/
    FryChicken       = 6,
}

public enum ChickenState
{
    /** 조리시작전 **/
    NotCook     = -1,
    /** 덜익은 치킨 **/
    BadChicken_0 = 0,
    /** 조금탄 치킨 **/
    BadChicken_1 = 2,
    /** 쓰레기 치킨 **/
    BadChicken_2 = 3,
    /** 맛있는치킨 **/
    GoodChicken = 1,
}