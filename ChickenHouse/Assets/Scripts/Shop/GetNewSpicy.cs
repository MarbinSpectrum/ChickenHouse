using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GetNewSpicy : Mgr
{
    private Queue<ShopItem> spicyQueue = new Queue<ShopItem>();
    [SerializeField] private Animation          ani;
    [SerializeField] private Image              spicyImg;
    [SerializeField] private TextMeshProUGUI    spicyText;
    [SerializeField] private Button             closeBtn;

    private IEnumerator spicySeCor;

    public void SetSpicy()
    {
        if(gameMgr.playData.day + 1 == 2 && gameMgr.playData.hasItem[(int)ShopItem.Recipe_1] == false)
        {
            //2일차에 간장 추가
            gameMgr.playData.hasItem[(int)ShopItem.Recipe_1] = true;
            spicyQueue.Enqueue(ShopItem.Recipe_1);
        }

        if (gameMgr.playData.day + 1 == 3 && gameMgr.playData.hasItem[(int)ShopItem.Recipe_2] == false)
        {
            //3일차에 불닭 추가
            gameMgr.playData.hasItem[(int)ShopItem.Recipe_2] = true;
            spicyQueue.Enqueue(ShopItem.Recipe_2);
        }

        if (gameMgr.playData.day + 1 == 4 && gameMgr.playData.hasItem[(int)ShopItem.Recipe_3] == false)
        {
            //4일차에 뿌링클 추가
            gameMgr.playData.hasItem[(int)ShopItem.Recipe_3] = true;
            spicyQueue.Enqueue(ShopItem.Recipe_3);
        }

        if (gameMgr.playData.day + 1 == 5 && gameMgr.playData.hasItem[(int)ShopItem.Recipe_4] == false)
        {
            //5일차에 까르보나라 추가
            gameMgr.playData.hasItem[(int)ShopItem.Recipe_4] = true;
            spicyQueue.Enqueue(ShopItem.Recipe_4);
        }

        if (gameMgr.playData.day + 1 == 6 && gameMgr.playData.hasItem[(int)ShopItem.Recipe_5] == false)
        {
            //6일차에 바베큐 추가
            gameMgr.playData.hasItem[(int)ShopItem.Recipe_5] = true;
            spicyQueue.Enqueue(ShopItem.Recipe_5);
        }

        ani.gameObject.SetActive(false);
        if (spicyQueue.Count > 0)
        {
            ShopItem spicy = spicyQueue.Dequeue();
            ShowSpicyAni(spicy);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void ShowSpicyAni(ShopItem pShopItem)
    {
        if(spicySeCor != null)
        {
            StopCoroutine(spicySeCor);
            spicySeCor = null;
        }

        ani.gameObject.SetActive(true);
        ani.Play();

        ShopData shopData = shopMgr.GetShopData(pShopItem);
        spicyImg.sprite = shopData.icon;
        string spicyStr = LanguageMgr.GetText(shopData.nameKey);
        string subStr = LanguageMgr.GetText("GET_ITEM");
        LanguageMgr.SetText(spicyText, string.Format(subStr, spicyStr));

        closeBtn.onClick.RemoveAllListeners();
        closeBtn.onClick.AddListener(()=>
        {
            ani.gameObject.SetActive(false);
            if (spicyQueue.Count > 0)
            {
                ShopItem spicy = spicyQueue.Dequeue();
                ShowSpicyAni(spicy);
            }
            else
            {
                gameObject.SetActive(false);
            }
        });

        spicySeCor = RunSpicySE();
        StartCoroutine(RunSpicySE());
    }
    private IEnumerator RunSpicySE()
    {
        yield return new WaitForSeconds(0.5f);

        soundMgr.PlaySE(Sound.GetSpicy_SE);
    }

}
