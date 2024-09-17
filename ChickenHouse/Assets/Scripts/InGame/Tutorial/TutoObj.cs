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

    private bool tutoFlag = false;

    public void PlayTuto()
    {
        if (tutoFlag)
            return;
        tutoFlag = true;
        StartCoroutine(RunToto());
    }

    private IEnumerator RunToto()
    {
        KitchenMgr      kitchenMgr  = KitchenMgr.Instance;
        if (kitchenMgr != null)
            kitchenMgr.kitchenRect.enabled = false;

        if (tutoPos != null && kitchenMgr != null)
        {
            RectTransform   kitchenRect = kitchenMgr.kitchenRect.content;
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
            yield return new WaitForSeconds(delay);
        }


        tutoMgr.ShowText(tutoType);
        tutoObj.SetActive(true);

        yield return new WaitForSeconds(1f);

        nextBtn.raycastTarget = true;
    }

    public void CloseTuto()
    {
        KitchenMgr kitchenMgr = KitchenMgr.Instance;
        if (kitchenMgr != null)
            kitchenMgr.kitchenRect.enabled = true;

        tutoMgr.CloseText();
        tutoObj.SetActive(false);
        tutoMgr.SetTuto(tutoType);
    }
}
