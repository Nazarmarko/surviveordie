
using UnityEngine;

public class CameraFollowOverride : CameraController
{

    public Transform target;
    public GameObject questWindow;

    private bool IsOpened = false;

     void OnEnable()
    {
        SetUp(() => target.position);
    }

    protected override void UIInteract()
    {
        if (questWindow == null)
            return;

        if (Input.GetKeyDown(KeyCode.U) && !IsOpened)
        {     
            questWindow.SetActive(true);
            IsOpened = true;
        }
    }
}
