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
        public Transform counterPos;    //카운터 위치
        public Transform KitchenPos;    //주방 위치
    }

    [SerializeField] private POS        pos;

    private IEnumerator lookCor;

    public void ChangeCamera(LookArea pLookArea, NoParaDel fun = null) => ChangeCamera(pLookArea, duration, fun);
    public void ChangeCamera(LookArea pLookArea,float pDuration, NoParaDel fun = null)
    {
        //특정 방향으로 카메라 이동

        //실행중인 애니메이션 코루틴 제거
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
        //특정 방향으로 카메라 이동하는 애니메이션 처리

        Vector3 cameraPos = Camera.main.transform.position;
        Vector3 endPos = cameraPos;
        switch(pLookArea)
        {
            case LookArea.Counter:
                {
                    //카운터 카메라위치 지정
                    endPos = new Vector3(cameraPos.x, pos.counterPos.position.y, cameraPos.z);

                }
                break;
            case LookArea.Kitchen:
                {
                    //주방 카메라위치 지정
                    endPos = new Vector3(cameraPos.x, pos.KitchenPos.position.y, cameraPos.z);
                }
                break;
        }

        if (pDuration == 0)
        {
            //이동시간이 0초이므로 바로이동
            Camera.main.transform.position = endPos;
            yield break;
        }

        float lerpValue = 0;
        while(lerpValue < 1.0f)
        {
            //러프값을 이용해서 애니메이션처리
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
