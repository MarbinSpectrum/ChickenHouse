using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Counter_BG : Mgr
{
    [SerializeField,Range(0,1)] private float rateValue;

    [SerializeField, Range(0, 1)] private float lerpValue;

    [System.Serializable] public struct BG_Lerp
    {
        public SpriteRenderer bgSprite;
        public SpriteRenderer deepSprite;
        public SpriteRenderer lightSprite;
        public Gradient bgGradient;
        public Gradient deepGradient;
        public Gradient lightGradient;
    }
    [SerializeField] private BG_Lerp bgLerp;

    private void Awake()
    {
        SetScale();
    }

#if UNITY_EDITOR
    private void Update()
    {
        SetScale();
        SetLerpValue(lerpValue);
    }
#endif

    private void SetScale()
    {
        float baseRate = 16.0f / 9.0f;
        float addRate = Camera.main.orthographicSize / 15f;
        if (baseRate > (float)Screen.width / (float)Screen.height)
        {
            float rate = ((float)Screen.width / (float)Screen.height) * (9.0f / 16.0f);
            transform.localScale = Vector3.one * rate * addRate * rateValue;
        }
        else
        {
            transform.localScale = Vector3.one * addRate * rateValue;
        }
    }

    public void SetLerpValue(float t)
    {
        lerpValue = Mathf.Min(1, t);

        bgLerp.lightSprite.color = bgLerp.lightGradient.Evaluate(lerpValue);
        bgLerp.bgSprite.color = bgLerp.bgGradient.Evaluate(lerpValue);
        bgLerp.deepSprite.color = bgLerp.deepGradient.Evaluate(lerpValue);
    }
}
