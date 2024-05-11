
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
    /** 치킨 슬롯 **/
    Chicken_Slot        = 9,
    /** 치킨 무 슬롯 **/
    Pickle_Slot         = 10,
    /** 음료 슬롯 **/
    Drink_Slot          = 11,
}

public enum GuestReviews
{
    /** 기분나쁨 **/
    Bad                 = 1,
    /** 보통 **/
    Normal              = 2,
    /** 기쁨 **/
    Good                = 3,
    /** 행복 **/
    Happy               = 4,
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
    Chicken_Pickle   = 8,
    /** 치킨 박스(포장) **/
    Chicken_Pack    = 9,
    /** 콜라 **/
    Cola            = 10,
    /** 간장 양념 **/
    Soy_Spicy       = 11,
    /** 불닭 양념 **/
    Hell_Spicy      = 12,
    /** 뿌링클 양념 **/
    Prinkle_Spicy   = 13,
    /** 까르보나라 양념 **/
    Carbonara_Spicy = 14,
    /** 바베큐 양념 **/
    BBQ_Spicy       = 15,
    /** 치킨 박스(구멍) **/
    Chicken_Pack_Holl = 16,
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
    /** 없음 **/
    Not         = -1,
    /** 소스 없음 **/
    None        = 0,
    /** 양념 치킨 소스 **/
    Hot         = 1,
    /** 간장 소스 **/
    Soy         = 2,
    /** 불닭 소스 **/
    Hell        = 3,
    /** 뿌링클 소스 **/
    Prinkle     = 4,
    /** 바베큐 소스 **/
    BBQ         = 5,
    /** 까르보나라 소스 **/
    Carbonara   = 6,
}


public enum Guest
{
    /** 없음 **/
    None     = 0,
    /** 여우 **/
    Fox      = 1,
    /** 강아지 **/
    Dog      = 2,
    /** 고양이 **/
    Cat      = 3,
    /** 호랑이 **/
    Tiger    = 4,
    /** 알락 꼬리 여우 원숭이 **/
    Lemur    = 5,
    /** 태즈마니아 데빌 **/
    TasmanianDevil = 6,
    /** 버지니아 주머니쥐 **/
    VirginiaOpossum = 7,
    /** 플라밍고 **/
    Flamingo = 8,
    /** 비둘기 **/
    Dove = 9,

    MAX
}

public enum Sound
{
    None = 0,

    //-------------------------------------------------------------------------------
    /** 배경음 **/
    InGame_BG   = 100,
    Title_BG    = 101,
    Shop_BG     = 102,
    Prologue_BG = 103,

    //-------------------------------------------------------------------------------
    /** 기름 튀기는 소리 **/
    Oil_SE = 200,

    /** 아이템 두는 소리 **/
    Put_SE = 201,

    /** 돈 획득 소리 **/
    GetMoney_SE = 202,

    /** 튀김조리 완료 소리 **/
    Oil_Zone_End_SE = 203,

    /** 소스획득 **/
    GetSpicy_SE = 204,

    /** 주문추가 **/
    NewOrder_SE = 205,

    /** 목소리 목록 **/
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

    /** 기름통 업그레이드 **/
    OIL_Zone_1          = 101,      //일반 튀김기 입니다.
    OIL_Zone_2          = 102,      //치킨이 빨리 튀겨집니다. (속도 +40%)
    OIL_Zone_3          = 103,      //치킨이 더 빨리 튀겨지고 더 맛있어집니다. (속도 +80%, 수입+20%)
    OIL_Zone_4          = 104,      //치킨이 완벽하게 튀겨집니다. (속도 +160%, 수입+40%, 치킨이 타지 않습니다.)

    /** 기름통 추가 **/
    NEW_OIL_ZONE_1      = 151,      //기름통 추가
    NEW_OIL_ZONE_2      = 152,      //기름통 추가
    NEW_OIL_ZONE_3      = 153,      //기름통 추가

    /** 레시피 업그레이드(총 수입은 레시피의 수치 합산) **/
    Recipe_1            = 201,      //간장치킨(수입 +20%)
    Recipe_2            = 202,      //불닭치킨(수입 +20%)
    Recipe_3            = 203,      //프링클치킨(수입 +20%)
    Recipe_4            = 204,      //까르보나라(수입 +20%)
    Recipe_5            = 205,      //바베큐치킨(수입 +20%)

    /** 광고 업그레이드 **/
    Advertisement_1     = 301,      //광고 업그레이드(손님 딜레이 -10%);
    Advertisement_2     = 302,      //광고 업그레이드(손님 딜레이 -20%);
    Advertisement_3     = 303,      //광고 업그레이드(손님 딜레이 -30%);
    Advertisement_4     = 304,      //광고 업그레이드(손님 딜레이 -40%);
    Advertisement_5     = 305,      //광고 업그레이드(손님 딜레이 -50%);

    /** 아르바이트생 업그레이드 **/
    Worker_1            = 401,      //알바생 고용(치킨을 계란물에 넣어줌)
    Worker_2            = 402,      //알바생 교육(알바생 움직임이 빨라짐)
    Worker_3            = 403,      //알바생 교육(치킨에 튀김가루를 묻혀줌)
    Worker_4            = 404,      //알바생 교육(알바생 움직임이 빨라짐)
    Worker_5            = 405,      //알바생 교육(치킨을 치킨건지에 넣어줌)
    Worker_6            = 406,      //알바생 교육(알바생 움직임이 빨라짐)

    MAX                 = 10000,
}

public enum WorkerSkill
{
    WorkerSkill_1    = 100,      //계란물 묻힐줄암
    WorkerSkill_2    = 200,      //튀김옷 입힐줄암
    WorkerSkill_3    = 300,      //건지에 치킨 넣어줌
    WorkerSkill_4    = 400,      //빠른 팔놀림
    WorkerSkill_5    = 500,      //빠른 손놀림
    WorkerSkill_6    = 600,      //빠른 몸놀림
}

public enum DayEndList
{
    /** 없음 **/
    None                = 0,
    /** 가게 매출 **/
    Store_Revenue       = 1,
    /** 체인점 매출 **/
    Chain_Store_Revenue = 2,
    /** 가게 임대료 **/
    Rent                = 3,
    /** 재료 값 **/
    Supplies_Uesd       = 4,
    /** 급여 **/
    Salary              = 5,

    /** 순 수익 **/
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
    /** 없음(쉬러이동) **/
    None                = 0,
    /** 일반 치킨 집으러가는중 **/
    NormalChicken       = 1,
    /** 계란물 치킨 집으러가는중 **/
    EggChicken          = 2,
    /** 밀가루 묻힌 치킨 집으러가는중 **/
    FlourChicken        = 3,
}

public enum Tutorial
{
    /** 없음 **/
    None,

    /** 닭을 치킨물로 넣기 **/
    Tuto_1,

    /** 닭을 밀가루로 넣기 **/
    Tuto_2,

    /** 닭을 치킨 건지로 넣기 **/
    Tuto_3,

    /** 닭을 기름 통에 넣기 **/
    Tuto_4,

    /** 치킨 튀기기 설명 **/
    Tuto_5_0,
    Tuto_5_1,
    /** 닭을 용기 안에 넣기 **/
    Tuto_5_2,

    /** 콜라 및 피클 넣기 **/
    Tuto_6,

    /** 치킨 넣기 **/
    Tuto_7,

    /** 카운터로 가져가기 **/
    Tuto_8,

    /** 튜토리얼 완료 **/
    Tuto_Complete
}

public enum Scene
{
    /** 로고 **/
    LOGO    = 0,

    /** 타이틀 **/
    TITLE   = 1,

    /** 프롤로그 **/
    PROLOGUE = 2,

    /** 인게임 **/
    INGAME  = 3,

    /** 상점 **/
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