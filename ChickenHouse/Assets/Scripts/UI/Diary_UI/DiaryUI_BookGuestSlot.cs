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

        //만나본 손님은 이미지 활성화
        Image img = guestFace.GetNowImg();
        img.material = isAct ? null : notHasMat;
    }
}
