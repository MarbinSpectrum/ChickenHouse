using UnityEngine;
using UnityEngine.UI;

public class DiaryUI_BookGuestSlot : Mgr
{
    [SerializeField] private GuestFace      guestFace;
    [SerializeField] private Material       notHasMat;

    public void SetData(Guest pGuest)
    {
        guestFace.SetUI(pGuest);

        bool isAct = bookMgr.IsActGuest(pGuest);

        //������ �մ��� �̹��� Ȱ��ȭ
        Image img = guestFace.GetNowImg();
        img.material = isAct ? null : notHasMat;
    }
}
