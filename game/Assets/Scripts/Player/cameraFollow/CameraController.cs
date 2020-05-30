
using UnityEngine;
using System;
public class CameraController : MonoBehaviour
{
    protected Func<Vector3> cameraFollowPositionFunc;

    protected virtual void SetUp(Func<Vector3> cameraFollowPositionFunc)  {
        this.cameraFollowPositionFunc = cameraFollowPositionFunc;
    }

    protected virtual void UIInteract() { } 
    void LateUpdate()
    {
        Vector3 cameraFollowPosition = cameraFollowPositionFunc();
        cameraFollowPosition.z = transform.position.z;

        Vector3 cameraMoveDir = (cameraFollowPosition - transform.position).normalized;
        float distance = Vector3.Distance(cameraFollowPosition, transform.position);
        float cameraMoveSpeed = 3f;

        if(distance > 0)
        {
            Vector3 newCameraPositiontransform = transform.position + cameraMoveDir * distance * cameraMoveSpeed * Time.deltaTime;

            float distanceAfterMoving = Vector3.Distance(newCameraPositiontransform, cameraFollowPosition);

            if (distanceAfterMoving > distance) {
                newCameraPositiontransform = cameraFollowPosition;
            }

            transform.position = newCameraPositiontransform;
        }
        UIInteract();          
    }
}
