
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
    None                = 0,
    /** 치킨 박스 **/
    Chicken_Box         = 1,
    /** 계란물 **/
    Tray_Egg            = 2,
    /** 밀가루 **/
    Tray_Flour          = 3,
    /** 치킨 건지 **/
    Chicken_Strainter   = 4,
    /** 기름 **/
    Oil_Zone            = 5,
    /** 버리기 버튼 **/
    Trash_Btn           = 6,
    /** 치킨 팩 **/
    Chicken_Pack        = 7,
    /** 치킨 소스 **/
    Hot_Spicy           = 8,
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
    Chicken_Strainter = 5,
    /** 튀긴 치킨 **/
    Fry_Chicken       = 6,
    /** 치킨 소스 **/
    Hot_Spicy       = 7,
    /** 치킨 무 **/
    Chicken_Radish    = 8,
    /** 치킨 박스(포장) **/
    Chicken_Pack    = 9,
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

public enum ChickenSpicy
{
    /** 소스 없음 **/
    None        = 0,
    /** 양념 치킨 소스 **/
    Hot         = 1,

}

public enum Language
{
    Korea   = 100,
    English = 200,
}