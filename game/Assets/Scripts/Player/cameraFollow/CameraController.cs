using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class CameraController : MonoBehaviour
{
    protected Func<Vector3> cameraFollowPositionFunc;

    protected virtual void SetUp(Func<Vector3> cameraFollowPositionFunc)  {
        this.cameraFollowPositionFunc = cameraFollowPositionFunc;
    }
    protected virtual void UIInteract() { } 
    void Update()
    {
        Application.targetFrameRate = 5;

        Vector3 cameraFollowPosition = cameraFollowPositionFunc();
        cameraFollowPosition.z = transform.position.z;

        Vector3 cameraMoveDir = cameraFollowPosition - transform.position.normalized;
        float distance = Vector3.Distance(cameraFollowPosition, transform.position);
        float cameraMoveSpeed = 1f;

        transform.position = transform.position + cameraMoveDir * distance * cameraMoveSpeed * Time.deltaTime;

            UIInteract();          
    }
}
