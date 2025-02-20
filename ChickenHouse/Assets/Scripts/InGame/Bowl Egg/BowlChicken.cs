using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlChicken : Mgr
{
    [SerializeField] private ChickenLerp_Shader lerpShader;
    [SerializeField] private BowlEgg bowlEgg;
    [SerializeField] private Rigidbody2D rigidbody2D;

    private Vector3 basePos;
    private bool init;
    private float eggTime = 0;
    private const float EGG_DELAY = 0.5f;
    public bool isUse { private set; get; }

    //Tuto_3
    [SerializeField] private TutoObj tutoObj;

    public void Init()
    {
        if (init == false)
        {
            init = true;
            basePos = transform.localPosition;
        }

        isUse = false;
        transform.localPosition = basePos;
        eggTime = 0;
        lerpShader.SetValue(0);
    }

    public void MoveChicken(Vector2 pValue)
    {
        rigidbody2D.velocity = pValue;
    }

    public void RigidFreeze(RigidbodyConstraints2D rigidbodyConstraints2D)
    {
        rigidbody2D.constraints = rigidbodyConstraints2D;
    }


    public void UseChicken()
    {
        isUse = true;
    }

    public bool CompleteEgg()
    {
        //계란물 묻히기 작업이 완료됬는지 여부를 반환
        return eggTime >= EGG_DELAY && isUse;
    }

    public void WorkerEggChickenPutAway()
    {
        //직원이 계란물을 다묻힌걸 돌려놓는거임
        Init();
        eggTime = EGG_DELAY;
        lerpShader.SetValue(1);
    }

    public void AddEggToChicken(float v)
    {
        //계란물을 묻히기처리
        eggTime += v;
        lerpShader.SetValue(eggTime/ EGG_DELAY);
    }
}
