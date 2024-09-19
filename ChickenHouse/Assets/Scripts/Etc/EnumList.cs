
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
    /** 사이드 메뉴 슬롯 **/
    SideMenu_Slot       = 10,
    /** 계란물(Bowl) **/
    Bowl_Egg            = 11,
    /** 밀가루2 **/
    Tray_Flour2         = 12,
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
    /** 맥주 **/
    Beer            = 17,
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

    MAX
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
    /** 팬더 **/
    Panda = 10,
    /** 래서팬더 **/
    RedPanda = 11,
    /** 토끼 **/
    Rabbit = 12,
    /** 해달 **/
    SeaOtter = 13,
    /** 사슴 **/
    Deer = 14,

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
    Town_BG     = 104,

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
    /** 버튼 소리 **/
    Btn_SE = 206,
    /** 걷는 소리 **/
    Walk_SE = 207,
    /** 종이 소리 **/
    Paper_SE = 208, 
    /** 페이지 소리 **/
    Page_SE = 209,
    /** 도장 소리 **/
    Stamp_SE = 210,

    /** 목소리 목록 **/
    //개
    Voice0_SE   = 250,

    //여우 & 고양이
    Voice1_SE   = 251,
    Voice2_SE   = 252,

    //줄무늬 원숭이
    Voice3_SE   = 253,
    Voice4_SE   = 254,

    //태지베니아 데빌
    Voice5_SE   = 255,
    Voice6_SE   = 256,

    //흰주머니쥐 & 빚쟁이
    Voice7_SE   = 257,
    Voice8_SE   = 258,

    //플라밍고
    Voice9_SE   = 259,
    Voice10_SE  = 260,

    //비둘기
    Voice11_SE  = 261,
    Voice12_SE  = 262,

    //호랑이
    Voice13_SE  = 263,
    Voice14_SE  = 264,

    //판다
    Voice15_SE  = 265,
    Voice16_SE  = 266,

    //래서판다
    Voice17_SE = 267,
    Voice18_SE = 268,

    //토끼
    Voice19_SE = 269,
    Voice20_SE = 270,

    //사슴
    Voice21_SE = 271,
    Voice22_SE = 272,

    //해달
    Voice23_SE = 273,
    Voice24_SE = 274,

    //불독
    Voice25_SE = 275,

    //나이든 고양이
    Voice26_SE = 276,

    //코끼리
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
    Fryer_Buy       = 0,    //튀김기 구매
    Fryer_Add       = 1,    //튀김기 추가
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

    /** 레시피 **/
    Recipe_0            = 200,      //양념치킨
    Recipe_1            = 201,      //간장치킨
    Recipe_2            = 202,      //불닭치킨
    Recipe_3            = 203,      //프링클치킨
    Recipe_4            = 204,      //까르보나라
    Recipe_5            = 205,      //바베큐치킨

    /** 음료 **/
    Cola                = 231,      //콜라
    Beer                = 232,      //맥주

    /** 사이드 메뉴 **/
    Pickle              = 261,      //치킨무

    /** 광고 업그레이드 **/
    Advertisement_1     = 301,      //광고 업그레이드(손님 딜레이 -5%);
    Advertisement_2     = 302,      //광고 업그레이드(손님 딜레이 -7%,수익 +10%);
    Advertisement_3     = 303,      //광고 업그레이드(손님 딜레이 -10%,수익 +10%);
    Advertisement_4     = 304,      //광고 업그레이드(손님 딜레이 -15%,수익 +10%);
    Advertisement_5     = 305,      //광고 업그레이드(손님 딜레이 -20%,수익 +10%);

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
    WorkerSkill_1    = 100,      //주방보조 경력자(주방에서의 움직임 +50%)
    WorkerSkill_2    = 200,      //치킨가게 경력자(주방에서의 움직임 +100%)
    WorkerSkill_3    = 300,      //튀김 전문가(튀기는 속도+100%, 치킨이 타지 않음)
    WorkerSkill_4    = 400,      //잘생긴외모(팁 증가 +100%)
    WorkerSkill_5    = 500,      //카운터 업무 경력자(카운터에 배치시 손님이 방문률 +50%)
    WorkerSkill_6    = 600,      //먹보(종종 치킨 한 조각을 빼 먹음)
    WorkerSkill_7    = 700,      //독심술(손님이 원하는 치킨 양념을 알려줌)
    WorkerSkill_8    = 800,      //건망증(카운터에 배치시 2% 확률로 손님의 주문이 제대로 전달되지 않음)
}

public enum WorkerCounterTalkBox
{
    Bad,
    Normal,
    Good
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
    /** 없음(쉬러이동) **/
    None                = 0,
    /** 일반 치킨 집으러가는중 **/
    NormalChicken       = 1,
    /** 계란물 치킨 집으러가는중 **/
    EggChicken          = 2,
    /** 밀가루 묻힌 치킨 집으러가는중 **/
    FlourChicken        = 3,
    /** 계란물 묻히기 위해서 손으로 젓는거 **/
    HandShake           = 4,
    /** 밀가루 묻힌 치킨이 들어있는 치킨건지 들기 **/
    StrainterFlour      = 5,
    /** 튀기기가 끝난 치킨건지 들기 **/
    StrainterFry        = 6,
}

public enum Tutorial
{
    /** 없음 **/
    None,

    /** 닭을 치킨물로 넣기 **/
    Tuto_1,

    /** 달걀물 묻히기 **/
    Tuto_2,

    /** 닭을 밀가루쪽으로 옮기기 **/
    Tuto_3,

    /** 밀가루 묻히기 **/
    Tuto_4,

    /** 해당 작업 반복 **/
    Tuto_5,

    /** 치킨 건지로 닭 옮기기 **/
    Tuto_6,

    /** 튀김기로 치킨 옮기기 **/
    Tuto_7,

    /** 치킨 튀기기 설명 **/
    Tuto_8_0,
    Tuto_8_1,

    /** 닭을 용기 안에 넣기 **/
    Tuto_8_2,

    /** 주문 확인 및 목표 상기 **/
    Tuto_9_0,
    Tuto_9_1,

    /** 콜라 및 피클 넣기 **/
    Tuto_10,

    /** 치킨 넣기 **/
    Tuto_11,

    /** 카운터로 가져가기 **/
    Tuto_12,


    ////////////////////////////////////////////////////////////////////////
    /** 직원 배치 **/
    Worker_Tuto_1_0,
    Worker_Tuto_1_1,
    Worker_Tuto_2_0,
    Worker_Tuto_2_1,
    Worker_Tuto_2_2,

    ////////////////////////////////////////////////////////////////////////
    /** 메뉴 배치 **/
    Menu_Tuto_1,
    Menu_Tuto_2_0,
    Menu_Tuto_2_1,
    Menu_Tuto_2_2,

    ////////////////////////////////////////////////////////////////////////
    /** 마을 및 일기장 **/
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

    /** 마을 **/
    TOWN    = 5,

    /** 데모 **/
    DEMO,
}

public enum CookLvStat
{
    None                    = 0,
    /** 치킨 기본 가격 인상($) **/
    IncreaseChickenPrice    = 1,
    /** 치킨 재료비 감소($) **/
    DecreaseChickenRes      = 2,
    /** 음료 재료비 감소($) **/
    DecreaseDrinkRes        = 3,
    /** 피클 재료비 감소($) **/
    DecreasePickleRes       = 4,
    /** 직원 속도 증가(%)   **/
    WorkerSpeedUp           = 5,
    /** 수익 증가(%)        **/
    IncomeUp                = 6,
    /** 상점 할인(%)        **/
    ShopSale                = 7,
    /** 손님 방문률(%)      **/
    GuestSpawnRate          = 8,
    /** 손님 인내심 상한(%) **/
    GuestPatience           = 9,
    /** 손님 팁(%)          **/
    Tip                     = 10,
    /** 임대료($)           **/
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
    /** 퀘스트 메인 **/
    None              = -1,
    MainQuest_1       = 0,        //빚갚기 메인 퀘스트


    SpicyQuest_1      = 1000,     //치킨 5마리 팔기                (간장치킨 오픈)
    SpicyQuest_2      = 1001,     //간장치킨 5마리 팔기            (닭치킨 오픈)
    SpicyQuest_3      = 1002,     //불닭치킨 5마리 팔기            (프링클 치킨 오픈)
    SpicyQuest_4      = 1003,     //프링클 치킨 10마리 팔기        (까르보나라 치킨 오픈)
    SpicyQuest_5      = 1004,     //까르보나라치킨 10마리 팔기     (BBQ 치킨 오픈) 
    SpicyQuest_6      = 1005,     //BBQ치킨 10마리 팔기    

    DrinkQuest_1      = 2000,     //콜라 20개 팔기                 (맥주 오픈)
    DrinkQuest_2      = 2001,     //맥주 20개 팔기          

    MAX,
}

public enum TownMap
{
    None = -1,

    /** 털털마을 **/
    TulTulTown = 0,
    /** 네코 직업 소개소 **/
    NekoJobBank = 1,
    /** 파우 쉐프의 조리 도구 **/
    ChefPauxsCookingUtensils = 2,
    /** 긴 코 광고 회사 **/
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