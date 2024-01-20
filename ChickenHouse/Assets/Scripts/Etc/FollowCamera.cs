using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : Mgr
{
    //카메라 추적

    [SerializeField] private bool followX;
    [SerializeField] private bool followY;

    private void Update()
    {
        float baseX = transform.position.x;
        float baseY = transform.position.y;
        float baseZ = transform.position.z;
        if (followX)
            baseX = Camera.main.transform.position.x;
        if (followY)
            baseY = Camera.main.transform.position.y;
        transform.position = new Vector3(baseX, baseY, baseZ);
    }
}
