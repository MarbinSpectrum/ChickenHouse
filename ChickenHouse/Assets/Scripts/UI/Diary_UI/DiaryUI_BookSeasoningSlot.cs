using UnityEngine;
using UnityEngine.UI;

public class DiaryUI_BookSeasoningSlot : Mgr
{
    [SerializeField] private Material       notHasMat;
    [SerializeField] private SeasoningFace  seasoningFace;

    public void SetData(ChickenSpicy pChickenSpicy)
    {
        seasoningFace.SetUI(pChickenSpicy);

        bool isAct = bookMgr.IsActSpicy(pChickenSpicy);

        //���� ����� �̹��� Ȱ��ȭ
        Image img = seasoningFace.GetNowImg();
        img.material = isAct ? null : notHasMat;
    }
}
