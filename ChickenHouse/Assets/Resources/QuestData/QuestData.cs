using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "QuestData", menuName = "ScriptableObjects/Quest", order = 4)]

public class QuestData : ScriptableObject
{
    [Header("퀘스트 종류")]
    public Quest quest;

    [Header("퀘스트 이름")]
    public string questNameKey;
    [Header("퀘스트 내용")]
    public string questInfoKey;
    [Header("퀘스트 요약")]
    public string questSummaryKey;
    [Header("다음 퀘스트")]
    public Quest nextQuest;
    [Header("보상")]
    public List<QuestRewardData> rewards = new List<QuestRewardData>();

    public enum ERewardType
    {
        Spicy,
        Drink,
        SideMenu,
        InteriorItem,
        ShopItem,
        AD_Item,
    }


    [Serializable]
    public class QuestRewardData
    {
        [SerializeField]
        private ERewardType rewardType;
        public ERewardType getRewardType => rewardType;

        [SerializeField, ShowIf("@rewardType==ERewardType.Spicy"), EnumPaging]
        private ChickenSpicy spicy;
        [SerializeField, ShowIf("@rewardType==ERewardType.Drink"), EnumPaging]
        private Drink drink;
        [SerializeField, ShowIf("@rewardType==ERewardType.SideMenu"), EnumPaging]
        private SideMenu sideMenu;
        [SerializeField, ShowIf("@rewardType==ERewardType.InteriorItem"), EnumPaging]
        private InteriorItem interiorItem;
        [SerializeField, ShowIf("@rewardType==ERewardType.ShopItem"), EnumPaging]
        private ShopItem shopItem;
        [SerializeField, ShowIf("@rewardType==ERewardType.AD_Item"), EnumPaging]
        private ADItem adItem;

        public object GetQuestReward()
        {
            switch (getRewardType)
            {
                case ERewardType.Spicy:
                    return spicy;
                case ERewardType.Drink:
                    return drink;
                case ERewardType.SideMenu:
                    return sideMenu;
                case ERewardType.InteriorItem:
                    return interiorItem;
                case ERewardType.ShopItem:
                    return shopItem;
                case ERewardType.AD_Item:
                    return adItem;
            }
            return 0;
        }

        public string GetRewardNameKey()
        {
            switch (getRewardType)
            {
                case ERewardType.Spicy:
                    {
                        SpicyData spicyData = SpicyMgr.Instance.GetSpicyData(spicy);
                        return spicyData.nameKey;
                    }
                case ERewardType.Drink:
                    {
                        DrinkData drinkData = SubMenuMgr.Instance.GetDrinkData(drink);
                        return drinkData.nameKey;
                    }
                case ERewardType.SideMenu:
                    {
                        SideMenuData sideMenuData = SubMenuMgr.Instance.GetSideMenuData(sideMenu);
                        return sideMenuData.nameKey;
                    }
                case ERewardType.InteriorItem:
                    {
                        InteriorData interiorData = InteriorMgr.Instance.GetInteriorData(interiorItem);
                        return interiorData.nameKey;
                    }
                case ERewardType.ShopItem:
                    {
                        ShopData shopData = ShopMgr.Instance.GetShopData(shopItem);
                        return shopData.nameKey;
                    }
                case ERewardType.AD_Item:
                    {
                        ADData adData = ADItemMgr.Instance.GetADData(adItem);
                        return adData.nameKey;
                    }
            }

            return string.Empty;
        }

        public Sprite GetRewardIcon()
        {
            switch (getRewardType)
            {
                case ERewardType.Spicy:
                    {
                        SpicyData spicyData = SpicyMgr.Instance.GetSpicyData(spicy);
                        return spicyData.img;
                    }
                case ERewardType.Drink:
                    {
                        DrinkData drinkData = SubMenuMgr.Instance.GetDrinkData(drink);
                        return drinkData.img;
                    }
                case ERewardType.SideMenu:
                    {
                        SideMenuData sideMenuData = SubMenuMgr.Instance.GetSideMenuData(sideMenu);
                        return sideMenuData.img;
                    }
                case ERewardType.InteriorItem:
                    {
                        InteriorData interiorData = InteriorMgr.Instance.GetInteriorData(interiorItem);
                        return interiorData.img;
                    }
                case ERewardType.ShopItem:
                    {
                        ShopData shopData = ShopMgr.Instance.GetShopData(shopItem);
                        return shopData.icon;
                    }
                case ERewardType.AD_Item:
                    {
                        ADData adData = ADItemMgr.Instance.GetADData(adItem);
                        return adData.img;
                    }
            }

            return null;
        }
    }
}
