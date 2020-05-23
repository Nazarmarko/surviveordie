using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform target;
    public GameObject questWindow;
    private bool IsOpened = false;

    void Update()
    {
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);

        if (Input.GetKeyDown(KeyCode.U))
        {
            questWindow.SetActive(true);
            IsOpened = true;
        }
    }
}
