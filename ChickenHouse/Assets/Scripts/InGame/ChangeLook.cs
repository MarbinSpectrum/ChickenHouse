using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLook : Mgr
{
    [SerializeField] private float          duration;
    [SerializeField] private AnimationCurve animationCurve;

    [System.Serializable]
    public struct POS
    {
        public Transform counterPos;    //ī���� ��ġ
        public Transform KitchenPos;    //�ֹ� ��ġ
    }

    [SerializeField] private POS        pos;

    private IEnumerator lookCor;

    public void ChangeCamera(LookArea pLookArea, NoParaDel fun = null) => ChangeCamera(pLookArea, duration, fun);
    public void ChangeCamera(LookArea pLookArea,float pDuration, NoParaDel fun = null)
    {
        //Ư�� �������� ī�޶� �̵�

        //�������� �ִϸ��̼� �ڷ�ƾ ����
        if (lookCor != null)
        {
            StopCoroutine(lookCor);
            lookCor = null;
        }
        lookCor = Run_LookAni(pLookArea, pDuration, fun);

        StartCoroutine(lookCor);
    }

    private IEnumerator Run_LookAni(LookArea pLookArea, float pDuration, NoParaDel fun = null)
    {
        //Ư�� �������� ī�޶� �̵��ϴ� �ִϸ��̼� ó��

        Vector3 cameraPos = Camera.main.transform.position;
        Vector3 endPos = cameraPos;
        switch(pLookArea)
        {
            case LookArea.Counter:
                {
                    //ī���� ī�޶���ġ ����
                    endPos = new Vector3(cameraPos.x, pos.counterPos.position.y, cameraPos.z);

                }
                break;
            case LookArea.Kitchen:
                {
                    //�ֹ� ī�޶���ġ ����
                    endPos = new Vector3(cameraPos.x, pos.KitchenPos.position.y, cameraPos.z);
                }
                break;
        }

        if (pDuration == 0)
        {
            //�̵��ð��� 0���̹Ƿ� �ٷ��̵�
            Camera.main.transform.position = endPos;
            yield break;
        }

        float lerpValue = 0;
        while(lerpValue < 1.0f)
        {
            //�������� �̿��ؼ� �ִϸ��̼�ó��
            lerpValue += Time.deltaTime / pDuration;
            float curveValue = animationCurve.Evaluate(lerpValue);
            Vector3 lerpPos = Vector3.Lerp(cameraPos, endPos, curveValue);
            Camera.main.transform.position = lerpPos;
            yield return null;
        }
        Camera.main.transform.position = endPos;

        fun?.Invoke();
    }
}
