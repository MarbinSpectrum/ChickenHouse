
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

    MAX
}

public enum GuestType
{ 
    /** 없음 **/
    None                = 0,

    /** 악마                  <조금이여도 메뉴가 틀리면 0점 별점을 줌> **/
    Devil               = 1,

    /** 천사                  <엉터리로 메뉴를 꺼내도 최소 3점 별점은 줌> **/
    Angel               = 2,

    /** 느긋함                <주문이 아무리 늦게 나와도 평점을 나쁘게 주지 않음> **/
    Leisurely           = 3,

    /** 급함                  <주문을 내고 빨리 안나오면 평점을 나쁘게줌> **/
    Haste               = 4,

    /** 투머치토커            <손님의 이야기를 다 듣지않고 주방으로가면 별점을 나쁘게줌> **/
    Too_Much_Talker     = 5,

    /** 철저함                <손님의 요청이랑 다른 주문메뉴가 나오면 별점을 매우 나쁘게줌> **/
    Thoroughness        = 6,

    /** 미식가                <치킨을 태우거나 치킨을 덜익혀서 내오면 별점을 매우 나쁘게줌> **/
    Epicurean           = 7,

    /** 대식가                <치킨을 무조건 6조각 시킴> **/
    Big_Eater           = 8,

    /** 소식가                <치킨을 무조건 4조각 시킴> **/
    Light_Eater         = 9,

    /** 매운맛 매니아         <높은 확률로 매운것을 시킴> **/
    Hot_Mania           = 10,

    /** 단맛 매니아           <높은 확률로 단것을 시킴> **/
    Sweet_Mania         = 11,

    /** 콜라 매니아           <무조건 콜라를 시킴> **/
    Cola_Mania          = 12,

    /** 피클 매니아           <무조건 피클을 시킴> **/
    Pickle_Mania        = 13,

}

public enum Sound
{
    None = 0,

    //-------------------------------------------------------------------------------
    /** 배경음 **/
    InGame_BG   = 100,
    Title_BG    = 101,


    //-------------------------------------------------------------------------------
    /** 기름 튀기는 소리 **/
    Oil_SE = 200,

    /** 아이템 두는 소리 **/
    Put_SE = 201,

    /** 목소리 목록 **/
    Voice0_SE = 250,
    Voice1_SE = 251,
    Voice2_SE = 252,

}

public enum Upgrade
{
    None                = 0,

    /** 기름통 업그레이드 **/
    OIL_Zone_1  = 100,
    OIL_Zone_2  = 101,
    OIL_Zone_3  = 102,

    /** 레시피 업그레이드 **/
    Recipe_1    = 200,



    MAX                 = 10000,
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
    /** 순 수익 **/
    Total_Profit        = 5,
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

    /** 카운터로 가져가기 **/
    Tuto_7,

    /** 튜토리얼 완료 **/
    Tuto_Complete
}

public enum Scene
{
    /** 로고 **/
    LOGO    = 0,

    /** 타이틀 **/
    TITLE   = 1,

    /** 인게임 **/
    INGAME  = 2,

    /** 가게 업그레이드 **/
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