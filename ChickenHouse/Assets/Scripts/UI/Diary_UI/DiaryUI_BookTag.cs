using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiaryUI_BookTag : Mgr
{
    [SerializeField] private Sprite selectImg;
    [SerializeField] private Sprite notSelectImg;
    [SerializeField] private Dictionary<BookMenu, Image> tagImg = new Dictionary<BookMenu, Image>();

    public void OnTag(BookMenu pMenu)
    {
        foreach(var pair in tagImg)
        {
            //idx�� �ش��ϴ� ����Ʈ�ո� �Ҽ�ȭ
            bool act = (pMenu == pair.Key);
            Image img = pair.Value;
            img.sprite = act ? selectImg : notSelectImg;
        }
    }
}
