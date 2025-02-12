using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutoObj : Mgr
{
    [SerializeField] private Tutorial   tutoType;
    [SerializeField] private Transform  tutoPos;
    [SerializeField] private GameObject tutoObj;
    [SerializeField] private float      delay;
    [SerializeField] private Image      nextBtn;
    
    //튜토 연타해서 스킵하는 사람을 위한 딜레이
    [SerializeField] private float      skipDelay = 1f;

    private bool tutoFlag = false;

    public void PlayTuto()
    {
        if (tutoFlag)
            return;
        tutoFlag = true;
        nextBtn.raycastTarget = true;
        StartCoroutine(RunToto());
    }

    private IEnumerator RunToto()
    {
        KitchenMgr      kitchenMgr  = KitchenMgr.Instance;
        tutoMgr.SetTutoFlag(true);
        
        if (kitchenMgr != null)
        {
            //주방튜토리얼용
            kitchenMgr.ActKitchenRect(false);
        }

        if (tutoPos != null && kitchenMgr != null)
        {
            RectTransform kitchenRect = kitchenMgr.KitchenContent();
            float distance = Mathf.Abs(kitchenRect.transform.position.x - tutoPos.position.x);

            yield return new WaitForSeconds(delay);

            while (distance > 0.1f)
            {
                float newX = Mathf.Lerp(kitchenRect.transform.position.x, tutoPos.position.x, 0.1f);
                kitchenRect.transform.position = new Vector3(newX, kitchenRect.transform.position.y, kitchenRect.transform.position.z);
                distance = Mathf.Abs(kitchenRect.transform.position.x - tutoPos.position.x);
                yield return null;
            }


            kitchenRect.transform.position = new Vector3(tutoPos.position.x, kitchenRect.transform.position.y, kitchenRect.transform.position.z);
        }
        else
        {
            if(delay > 0)
                yield return new WaitForSeconds(delay);
        }

        yield return new WaitUntil(() => tutoMgr.CanTuto());

        tutoObj.SetActive(true);
        tutoMgr.ShowText(tutoType);

        yield return new WaitForSeconds(skipDelay);

        nextBtn.raycastTarget = true;
    }

    public void CloseTuto()
    {
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if (kitchenMgr != null)
        {
            kitchenMgr.ActKitchenRect(true);
        }

        tutoMgr.CloseText();
        tutoObj.SetActive(false);
        tutoMgr.SetTuto(tutoType);
        tutoMgr.SetTutoFlag(false);
    }
}
